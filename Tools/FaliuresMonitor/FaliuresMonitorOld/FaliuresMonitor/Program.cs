using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using FaliuresMonitor;
//using Microsoft.Build.Utilities.Logger;

namespace FailuresMonitor
{
    static class Program
    {
        //map which contains all failures enums
        static private Dictionary<int, Failure> m_failuresMap = new Dictionary<int, Failure>();  
   
        //serial port for sending serial commands to target shell
        static private Connection m_connection;

        /*-----------------------------------------------------------------------------------*/

        public const string INITIAL_BASE_ADDRESS = "0x2611a018";

        /*-----------------------------------------------------------------------------------*/

        public static bool isInitCommFormClosed; 

        /*-----------------------------------------------------------------------------------*/

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //init 
            Init();

            isInitCommFormClosed = false;

            //GUI 
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //init comm - serial port/telnet
            Application.Run(new FormInitComm());
            
            //main screen
            if (isInitCommFormClosed)
            {
                //fill map from config file         
                if (!ConfigFile.ConfigGetFailuresFromPath())
                {
                    MessageBox.Show("Error Loading failures list from file", "Error");
                    return;
                }
                Application.Run(new FailuresMonitorForm());
            }
        }

        /*-----------------------------------------------------------------------------------*/

        private static void Init()
        {
            string logMode = ConfigFile.ConfigGetLogMode(Environment.CurrentDirectory + "\\..\\..\\config.xml");

            switch (logMode)
            {
                case "DEBUG":
                    Logger.Instance.Mode = Logger.LOG_MODE_ENUM.DEBUG;
                    break;

                case "ERROR":
                    Logger.Instance.Mode = Logger.LOG_MODE_ENUM.ERROR;
                    break;

                case "WARNING":
                    Logger.Instance.Mode = Logger.LOG_MODE_ENUM.WARNING;
                    break;

                case "IMPORTANT":
                    Logger.Instance.Mode = Logger.LOG_MODE_ENUM.IMPORTANT;
                    break;

                default:
                    Logger.Instance.Mode = Logger.LOG_MODE_ENUM.NONE;
                    break;
            }         
        }

        /*-----------------------------------------------------------------------------------*/

        public static void UpdateFailuresWithResults(List<bool> _resultsList, out List<int> _changedList)
        {
            int resultNum = 0;

            _changedList = new List<int>();

            foreach (bool result in _resultsList)
            {
                //check changes
                if (FailuresMap[resultNum].FailureResult != result)
                {
                    _changedList.Add(resultNum);
                }

                //set failure result
                FailuresMap[resultNum].FailureResult = result;

                resultNum++;
            }
        }

        /*-----------------------------------------------------------------------------------*/

        internal static Dictionary<int, Failure> FailuresMap
        {
            get { return Program.m_failuresMap; }
            set { Program.m_failuresMap = value; }
        }

        internal static Connection Connection
        {
            get { return Program.m_connection; }
            set { Program.m_connection = value; }
        }

        /*-----------------------------------------------------------------------------------*/

        /*-----------------------------------------------------------------------------------*/
    }//static class Program

}//namespace FailuresMonitor
