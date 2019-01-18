using ConfigFileLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigFileLib
{
    public abstract class ConfigsAbs
    {
        ConfigFile m_configFile;

        //--------------------------------------------------
        public ConfigsAbs(ConfigFile _configFile)
        {
            m_configFile = _configFile;
        }

        //--------------------------------------------------

        public bool GetConfigs(string _configLabelToGet)
        {
            try
            {
                //get configs from xml config file as a List of Dictionaries
                List<Dictionary<string, string>> configsList;
                bool success = ConfigFile.GetNodesAttributesToListOfMaps(_configLabelToGet, out configsList);
                if (!success)
                {
                    return false;
                }

                success = SetDataMembers(configsList);
                if (!success)
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

            return true;
        }

        //--------------------------------------------------

        abstract protected bool SetDataMembers(List<Dictionary<string, string>> _configsList);  

        //--------------------------------------------------

        public ConfigFile ConfigFile
        {
            get
            {
                return m_configFile;
            }
        }

        //--------------------------------------------------
        // Useful methods for parsing configs
        //--------------------------------------------------
        public string GetPathFromConfigDic(string _path)
        {
            string pathToRet = UtilsLib.UtilsLib.CheckAndFixPathToBackslesh(_path, true);
            return pathToRet;
        }

        //--------------------------------------------------

        public string GetStringByLabelOrDefault(Dictionary<string, string> _attributesMap, string _label, string _default)
        {
            string toReturn = _default;

            if (_attributesMap.ContainsKey(_label))
            {
                toReturn = _attributesMap[_label];
            }

            return toReturn;
        }

        /*----------------------------------------------------------------------------*/

        public void SetStringListByLabelFromAttributesMap(Dictionary<string, string> _attributesMap, string _label, /*output*/ List<string> _outputList)
        {
            if (_attributesMap.ContainsKey(_label))
            {
                //get and "clean" string 
                string valuesStr = _attributesMap[_label];
                valuesStr = valuesStr.Replace("\r", String.Empty);
                valuesStr = valuesStr.Replace("\n", String.Empty);
                valuesStr = valuesStr.Replace("\t", String.Empty);

                //set from  string[] => List<string>
                if (valuesStr != String.Empty)
                {
                    string[] strArr = valuesStr.Split(',');
                    foreach (string strToAdd in strArr)
                    {
                        _outputList.Add(strToAdd);
                    }
                }
            }
        }

        //--------------------------------------------------
    }
}
