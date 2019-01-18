using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ConfigFileLib
{
    /******************* see example of class using this manager, below ) *************************/

    public class ConfigManager
    {
        //----------------------------------------------------------
        // Types
        //----------------------------------------------------------
        public struct ConfigsRetrieverStruct
        {
            //public ConfigsType s_configType;
            public Type s_configTypeClass;
            public string s_labelToRetrieve;
        }

        //----------------------------------------------------------
        // APIs
        //----------------------------------------------------------
                    
        public bool RetrieveConfigs(string[] _pathesToSearchIn, ConfigsRetrieverStruct[] _configsToRetrieveTable, /*output*/ ConfigsAbs[] _configsToRetreiveArr, out string _errorMsg)
        {
            _errorMsg = "";

            //read xml config file
            ConfigFile configFile;
            string expStr;
            bool success = ReadConfigFile(_pathesToSearchIn, out configFile, out expStr);
            if (!success)
            {
                _errorMsg = "Error opening Config file : " + Environment.NewLine + expStr;
                return false;
            }

            //init each config according to table
            for (int i = 0; i < _configsToRetrieveTable.Length; i++)
            {
                //create concrete config file object - according to table (using reflection)
                Type typeTocreate = _configsToRetrieveTable[i].s_configTypeClass;
                ConstructorInfo[] ctors = typeTocreate.GetConstructors();
                object[] parameters = { configFile };
                _configsToRetreiveArr[i] = (ConfigsAbs)(ctors[0].Invoke(parameters));

                //get configs fot the concrete config type
                string labelToRetreive = _configsToRetrieveTable[i].s_labelToRetrieve;
                success = _configsToRetreiveArr[i].GetConfigs(labelToRetreive);
                if (!success)
                {
                    _errorMsg = "Error parsing - " + labelToRetreive;
                    return false;
                }
            }

            return true;
        }

        //----------------------------------------------------------
        // Inner imp.
        //----------------------------------------------------------
        private bool ReadConfigFile(string[] _pathesToSearchIn, out ConfigFile _configFileToRead, out string _expMsg)
        {
            _expMsg = "";
            _configFileToRead = null;
            bool success;
            string expStr;

            //try to get config from path array
            foreach (string path in _pathesToSearchIn)
            {
                _configFileToRead = new ConfigFile(path, out success, out _expMsg);
                if (success)
                {
                    return true;
                }
            }

            return false;
        }

        //----------------------------------------------------------
    }
}





/**********************************example of class using this manager************************************************
        //--------------------------------------------------
        //Types
        //--------------------------------------------------
        public enum ConfigType {GENERAL, GENETIC_ALGO, WCET, TSP};

        //--------------------------------------------------
        //DM
        //--------------------------------------------------

        static public ConfigsRetrieverStruct[] m_configRetrieverTable = new ConfigsRetrieverStruct[]
        {
            new ConfigsRetrieverStruct{s_configTypeClass = typeof(GeneralConfigs), s_labelToRetrieve = GENERAL_CONFIGS_LABEL },
            new ConfigsRetrieverStruct{s_configTypeClass = typeof(GeneticAlgoConfigs), s_labelToRetrieve = GENETIC_ALGO_CONFIGS_LABEL },
            new ConfigsRetrieverStruct{s_configTypeClass = typeof(WcetConfigs), s_labelToRetrieve = WCET_CONFIGS_LABEL },
            new ConfigsRetrieverStruct{s_configTypeClass = typeof(TspConfigs), s_labelToRetrieve = TSP_CONFIGS_LABEL },
        };

        static public ConfigsAbs[] ConfigsArr { get; }

        //--------------------------------------------------
        //CTOR
        //--------------------------------------------------    

        static ConfigsContainer()
        {
            int numOfConfigs = m_configRetrieverTable.Length;
            ConfigsArr = new ConfigsAbs[numOfConfigs];
        }

        //--------------------------------------------------
        //APIs
        //--------------------------------------------------     

        internal static ConfigsAbs GetSpecificConfig(ConfigType _configTypeToGet)
        {
            ConfigsAbs confifToRet = ConfigsArr[_configTypeToGet.GetHashCode()];

            return confifToRet;
        }


        static public bool RetrieveConfigData()
        {
            try
            {
                //Retrieve  configs
                ConfigManager confManager = new ConfigManager();
                string errorMsg;
                bool success = confManager.RetrieveConfigs(CONFIG_FILE_PATHES, m_configRetrieverTable, ConfigsArr, out errorMsg);
                if (!success)
                {
                    MessageBox.Show(errorMsg);
                    return false;
                }
            }
            catch(Exception e)
            {
                return false;
            }

            return true;
        }   

        //--------------------------------------------------
*********************************example of class using this manager************************************************/
