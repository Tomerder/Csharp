using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Threading;
using VersionControlLib;
using LoggerLib;
using System.ComponentModel;
using SvnLib;

namespace AutoVersionRelease
{

    /*----------------------------------------------------------------------------*/
    //Enums

    public enum CheckBoxType { SVN_CHECKOUT, MAP_ENV, BUILD_VERSION, WRAP_UP, AUTO_COMMIT };

    /*----------------------------------------------------------------------------*/

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static string VERSION = "1.0";

        /*----------------------------------------------------------------------------*/
        //delegations

        delegate bool IsCheckedOutDelegate(CheckBoxType _checkBoxType);

        /*----------------------------------------------------------------------------*/
        //DM

        Thread m_threadExecute;

        Dictionary<string, Csci> g_cscisMap;

        /*----------------------------------------------------------------------------*/

        public MainWindow()
        {
            InitializeComponent();

            SetDefaults();

            //get data from XML config file into CSCIs map          
            bool success = ConfigFileLogic.GetConfigData(out g_cscisMap);
            if (!success)
            {
                //exit 
                Close();               
            }

            //set looger mode
            //Logger.Instance.Mode = Logger.LOG_MODE_ENUM.ALL;
        }

        /*----------------------------------------------------------------------------*/

        private void SetDefaults()
        {
            this.Title = "Automatic Version Release : " + VERSION;
            CheckBoxSvnCheckout.IsChecked = true;
            checkBoxBuildVersion.IsChecked = true;
            checkBoxWrapUp.IsChecked = true;
        }
    
        /*----------------------------------------------------------------------------*/

        private void buttonReleaseVersion_Click(object sender, RoutedEventArgs e)
        {
            //clear log (message textBox)
            UtilsGui.ClearMessagesPanel(this, textBoxMessages);

            //disable button - one build process at a time
            UtilsGui.SetButtonEnable(this, buttonReleaseVersion, false);

            //update OUTPUT PATH by version name
            string versionName = UtilsGui.GetTextBox(this, textBoxVersionToRelease);
            GeneralConfigs.Instance.UpdateOutputPath(versionName);

            //start thread for doing the work on background - not on GUI thread
            m_threadExecute = new Thread(new ThreadStart(StartReleaseVersion));
            m_threadExecute.IsBackground = true;
            m_threadExecute.Start();           
        }

        /*----------------------------------------------------------------------------*/
        /*----------------------------------------------------------------------------*/
        /*----------------------------------------------------------------------------*/

        private void StartReleaseVersion()
        {
            bool success = true;

            try
            {
                //checkout environment from SVN       
                if (IsSelectBoxChecked(CheckBoxType.SVN_CHECKOUT))
                {
                    success = SvnLogic.CheckoutAllCscis(g_cscisMap, this);
                    if (!success)
                    {
                        DoReturnValue(false);
                        return;
                    }
                }

                //map envirement 
                if (IsSelectBoxChecked(CheckBoxType.MAP_ENV))
                {
                    success = BuildLogic.MapEnviriement(this);
                    if (!success)
                    {
                        DoReturnValue(false);
                        return;
                    }
                }

                //for each csci that is not TAG - check if (Checkout revison != Last TAG revision)
                //if so, build dependencies and then build csci
                if (IsSelectBoxChecked(CheckBoxType.BUILD_VERSION))
                {
                    success = BuildLogic.DoBuildLogic(g_cscisMap, this);
                    if (!success)
                    {
                        DoReturnValue(false);
                        return;
                    }
                }

                //WRAP-UP
                if (IsSelectBoxChecked(CheckBoxType.WRAP_UP))
                {
                    success = WrapUpLogic.DoWrapUpLogic(g_cscisMap, this);
                    if (!success)
                    {
                        DoReturnValue(false);
                        return;
                    }
                }
            }
            catch
            {
                UtilsGui.AppendMessageToPanel(this, textBoxMessages, "Unhandled error", UtilsGui.MessageType.ERROR);
                DoReturnValue(false);
                return;
            }
       
            DoReturnValue(success);
        }

        /*----------------------------------------------------------------------------*/

        public bool IsSelectBoxChecked(CheckBoxType _checkBoxType)
        {
            //enable calling from another thread which is not the main thread(GUI)
            if (!Dispatcher.CheckAccess())
            {
                return (bool)Dispatcher.Invoke(new IsCheckedOutDelegate(IsSelectBoxChecked), _checkBoxType);
            }
            else
            {
                switch (_checkBoxType)
                {
                    case(CheckBoxType.SVN_CHECKOUT):
                        return (CheckBoxSvnCheckout.IsChecked.GetValueOrDefault() == true);
                        break;
                    case (CheckBoxType.MAP_ENV):
                        return (checkBoxMapEnv.IsChecked.GetValueOrDefault() == true);
                        break;
                    case (CheckBoxType.BUILD_VERSION):
                        return (checkBoxBuildVersion.IsChecked.GetValueOrDefault() == true);
                        break;
                    case (CheckBoxType.WRAP_UP):
                        return (checkBoxWrapUp.IsChecked.GetValueOrDefault() == true);
                        break;
                    case (CheckBoxType.AUTO_COMMIT):
                        return (checkBoxAutoCommitAndTag.IsChecked.GetValueOrDefault() == true);
                        break;
                    default:
                        return true;
                }
            }
        }

        /*----------------------------------------------------------------------------*/

        private void DoReturnValue(bool _success)
        {
            string message = "";

            if (_success)
            {
                message = "Process finished succeccfully";
                UtilsGui.AppendMessageToPanel(this, textBoxMessages, message, UtilsGui.MessageType.SUCCESS);
            }
            else
            {
                message = "Process finished with errors";
                UtilsGui.AppendMessageToPanel(this, textBoxMessages, message, UtilsGui.MessageType.ERROR);
            }

            UtilsGui.SetButtonEnable(this, buttonReleaseVersion, true);
        }

        /*----------------------------------------------------------------------------*/
        /*----------------------------------------------------------------------------*/
        /*----------------------------------------------------------------------------*/
        
    }
}
