using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoggerLib;

namespace AutoVersionRelease
{
    class GeneralConfigs
    {
        string m_logMode;
        string m_mappingScriptPath;
        string m_envPath;
        string m_inputsPath;     
        string m_outputsPath;

        //notification mail details
        string m_mailFrom;     
        string m_mailTo;      
        string m_mailuserDp;     
        string m_mailUserPassward;
          
        /*---------------------------singleton----------------------------*/
        private static GeneralConfigs m_instance;

        private GeneralConfigs() 
        {
        }

        public static GeneralConfigs Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new GeneralConfigs();
                }

                return m_instance;
            }
        }
        /*------------------------------------------------*/

        public string MailFrom
        {
            get { return m_mailFrom; }
            set { m_mailFrom = value; }
        }

        public string MailTo
        {
            get { return m_mailTo; }
            set { m_mailTo = value; }
        }

        public string MailuserDp
        {
            get { return m_mailuserDp; }
            set { m_mailuserDp = value; }
        }

        public string MailUserPassward
        {
            get { return m_mailUserPassward; }
            set { m_mailUserPassward = value; }
        }

        public string InputsPath
        {
            get { return m_inputsPath; }
            set { m_inputsPath = value; }
        }

        public string OutputsPath
        {
            get { return m_outputsPath; }
            set { m_outputsPath = value; }
        }

        public string EnvPath
        {
            get { return m_envPath; }
            set { m_envPath = value; }
        }

        public string LogMode
        {
            get { return m_logMode; }
            set { m_logMode = value; }
        }

        public string MappingScriptPath
        {
            get { return m_mappingScriptPath; }
            set { m_mappingScriptPath = value; }
        }

        /*------------------------------------------------*/

        internal void UpdateOutputPath(string versionName)
        {
            UtilsLib.UtilsLib.CheckAndFixPath(ref m_outputsPath);
            m_outputsPath = m_outputsPath + versionName;
            UtilsLib.UtilsLib.CheckAndFixPath(ref m_outputsPath);
        }

        /*------------------------------------------------*/
    }
}
