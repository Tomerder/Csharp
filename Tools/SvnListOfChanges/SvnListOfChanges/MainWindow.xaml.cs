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
using System.Threading;
using SvnLib;
using CsvLib;
using System.Data;
using SvnListOfChanges;

namespace SvnFilesHistory
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /*--------------------------------------------------------------------------*/

        delegate string GetTextBoxValueDelegate(TextBox _textBox);
        delegate void SetButtonEnableDelegate(bool _isEnable);

        public enum OutputTypeEnum { HISTORY, LIST_OF_CHANGES };

        Thread m_threadExecute;

        /*--------------------------------------------------------------------------*/

        static string LABEL1_LOC = "From Svn Path :";
        static string LABEL1_HISTORY = "Trunk Svn Path :";
        static string LABEL2_LOC = "To Svn Path :";
        static string LABEL2_HISTORY = "Tags Svn Path :";
        static string BUTTON_HISTORY = "Get History";
        static string BUTTON_LOC = "Get List Of Changes";

        static string DEFAULT_SVN_PATH_FROM = "http://svn-subs:18080/svn/efvs/Software/Projects/Dassault/Implementation/Source/SR1/OFP_SR1/tags/3681A02-09";
        static string DEFAULT_SVN_PATH_TO = "http://svn-subs:18080/svn/efvs/Software/Projects/Dassault/Implementation/Source/SR1/OFP_SR1/tags/3681A02-10";
        static string DEFAULT_OUTPUT_FILE = "I:/Tools/SvnListOfChanges/Output/output.csv";
        static OutputTypeEnum DEFAULT_OUTPUT_TYPE = OutputTypeEnum.LIST_OF_CHANGES;

        static string DEFAULT_SVN_PATH_TRUNK = "http://svn-subs:18080/svn/efvs/Software/Projects/Dassault/Implementation/Source/SR1/OFP_SR1/trunk/";
        static string DEFAULT_SVN_PATH_TAGS = "http://svn-subs:18080/svn/efvs/Software/Projects/Dassault/Implementation/Source/SR1/OFP_SR1/tags/";
        static string DEFAULT_SVN_TAG_START = "3681A02-06";    

        /*--------------------------------------------------------------------------*/

        public MainWindow()
        {
            InitializeComponent();

            //set combo box outputType by enum values
            comboBoxOutputType.ItemsSource = Enum.GetValues(typeof(OutputTypeEnum)); 

            //get args - execute from GUI/COMMAND LINE
            string[] args = Environment.GetCommandLineArgs();
            if(args.Length > 1)
            {
                if(args[1] == "LOC")
                {
                    //set inputs 
                    textBoxFrom.Text = args[2];
                    textBoxTo.Text = args[3];
                    textBoxOutputFile.Text = args[4];

                    //build loc file and exit program
                    ExecuteLoc();
                }
                else if (args[1] == "HST")
                {
                    //set inputs 
                    textBoxFrom.Text = args[2];
                    textBoxTo.Text = args[3];
                    textBoxTagStart.Text = args[4];
                    textBoxOutputFile.Text = args[5];

                    //build history file and exit program
                    ExecuteHistory();
                }
               
                //close tool
                Application.Current.Shutdown();
            }
            else
            {
                //defaults
                textBoxFrom.Text = DEFAULT_SVN_PATH_FROM;
                textBoxTo.Text = DEFAULT_SVN_PATH_TO;
                textBoxTagStart.Text = DEFAULT_SVN_TAG_START;
                textBoxOutputFile.Text = DEFAULT_OUTPUT_FILE;
                comboBoxOutputType.SelectedIndex = DEFAULT_OUTPUT_TYPE.GetHashCode();
            }
        }

        /*--------------------------------------------------------------------------*/

        private void buttonGetReport_Click(object sender, RoutedEventArgs e)
        {
            //start thread for doing the work on background - not on GUI thread
            if (comboBoxOutputType.SelectedIndex == OutputTypeEnum.LIST_OF_CHANGES.GetHashCode())
            {
                m_threadExecute = new Thread(new ThreadStart(ExecuteLoc));
            }
            else
            {
                m_threadExecute = new Thread(new ThreadStart(ExecuteHistory));
            }

            m_threadExecute.IsBackground = true;
            m_threadExecute.Start();           
        }

        /*--------------------------------------------------------------------------*/

        private void ExecuteHistory()
        {
            //disable button - one build process at a time
            UtilsGui.SetButtonEnable(this, buttonGetReport, false); 
            UtilsGui.AppendMessageToPanel(this, textBoxMessages, "History Generation Started, Please Wait...", UtilsGui.MessageTypeEnum.NONE);

            //DO LOGIC
            try
            {
                bool sucess = HistoryLogic.DoHistoryLogic(this);
            }
            catch (Exception e)
            {
                UtilsGui.AppendMessageToPanel(this, textBoxMessages, "Unhandled failure", UtilsGui.MessageTypeEnum.ERROR);
            }

            //enable button - one build process at a time
            UtilsGui.SetButtonEnable(this, buttonGetReport, true); 
        }
    
        /*--------------------------------------------------------------------------*/

        private void ExecuteLoc()
        {
            bool success = true;

            //disable button - one build process at a time
            UtilsGui.SetButtonEnable(this, buttonGetReport, false); 
            UtilsGui.AppendMessageToPanel(this, textBoxMessages, "LOC Generation Started, Please Wait...", UtilsGui.MessageTypeEnum.NONE);

            //DO LOGIC
            try
            {
                success = LocLogic.DoLocLogic(this);
            }
            catch (Exception e)
            {
                UtilsGui.AppendMessageToPanel(this, textBoxMessages, "Unhandled failure", UtilsGui.MessageTypeEnum.ERROR);
                success = false;
            }

            //write result
            if (!success)
            {
                UtilsGui.AppendMessageToPanel(this, textBoxMessages, "Writting LOC to file has failed",  UtilsGui.MessageTypeEnum.ERROR);
            }
            else
            {
                UtilsGui.AppendMessageToPanel(this, textBoxMessages, "Finished successfuly", UtilsGui.MessageTypeEnum.SUCCESS);
            }

            //enable button - one build process at a time
            UtilsGui.SetButtonEnable(this, buttonGetReport, true);        
        }

        /*--------------------------------------------------------------------------*/

        private void comboBoxOutputType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxOutputType.SelectedIndex == OutputTypeEnum.LIST_OF_CHANGES.GetHashCode() )
            {
                label1.Content = LABEL1_LOC;
                label2.Content = LABEL2_LOC; 
                textBoxFrom.Text = DEFAULT_SVN_PATH_FROM;
                textBoxTo.Text = DEFAULT_SVN_PATH_TO;
                label3.Visibility = Visibility.Hidden;
                textBoxTagStart.Visibility = Visibility.Hidden;
                buttonGetReport.Content = BUTTON_LOC;
            }
            else
            {
                label1.Content = LABEL1_HISTORY;
                label2.Content = LABEL2_HISTORY;
                textBoxFrom.Text = DEFAULT_SVN_PATH_TRUNK;
                textBoxTo.Text = DEFAULT_SVN_PATH_TAGS;
                label3.Visibility = Visibility.Visible;
                textBoxTagStart.Visibility = Visibility.Visible;
                buttonGetReport.Content = BUTTON_HISTORY;
            }
        }

        /*--------------------------------------------------------------------------*/

        private void buttonGetPath_Click(object sender, RoutedEventArgs e)
        {
            string path;
            UtilsLib.UtilsLib.GetPathFromUser("Select path for output file", out path);

            textBoxOutputFile.Text = path + "\\output.csv";
        }

        /*--------------------------------------------------------------------------*/
        
    }
}
