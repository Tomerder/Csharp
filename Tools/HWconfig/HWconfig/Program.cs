using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
//using Microsoft.Build.Utilities.Logger;

namespace HWconfig
{
    static class Program
    {
        //set which contains all form tabs : tabName  
        static private HashSet<string> m_tabsSet = new HashSet<string>();

        //map which contains all regs data from config file 
        static private Dictionary<string, Reg> m_regsMap = new Dictionary<string, Reg>();  
   
        //serial port for sending serial commands to target shell
        static private Connection m_connection;

        /*-----------------------------------------------------------------------------------*/

        public const string INITIAL_BASE_ADDRESS = "0xb8000000";

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

            //fill map from config file 
            //FillMap(regsMap);
            ConfigFile config = new ConfigFile();
            
            config.ConfigGetRegs(Environment.CurrentDirectory + "\\..\\..\\config.xml");

            isInitCommFormClosed = false;

            //GUI 
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //init comm - serial port/telnet
            Application.Run(new FormInitComm());
            
            //main screen
            if (isInitCommFormClosed)
            {
                Application.Run(new FormHwConfig());
            }
        }

        /*-----------------------------------------------------------------------------------*/

        private static void Init()
        {
            ConfigFile config = new ConfigFile();
            string logMode = config.ConfigGetLogMode(Environment.CurrentDirectory + "\\..\\..\\config.xml");

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

        public static HashSet<string> TabsSet
        {
            get { return Program.m_tabsSet; }
            set { Program.m_tabsSet = value; }
        }  

        internal static Dictionary<string, Reg> RegsMap
        {
            get { return Program.m_regsMap; }
            set { Program.m_regsMap = value; }
        }

        internal static Connection Connection
        {
            get { return Program.m_connection; }
            set { Program.m_connection = value; }
        }

        /*-----------------------------------------------------------------------------------*/

        //return auto complete source for search text field
        public static AutoCompleteStringCollection GetRegsName()
        {
            AutoCompleteStringCollection autoCompleteSource = new AutoCompleteStringCollection();

            foreach (string regName in RegsMap.Keys)
            {
                autoCompleteSource.Add(regName);
            }

            return autoCompleteSource;
        }

        /*-----------------------------------------------------------------------------------*/
    }

}
