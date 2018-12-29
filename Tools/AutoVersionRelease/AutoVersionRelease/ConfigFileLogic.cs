using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using ConfigFileLib;

namespace AutoVersionRelease
{
    static class ConfigFileLogic
    {
        private const string CONFIG_FILE_PATH = "C:/AutoRelease/AutoReleaseToolConfigFile.xml";
        private const string NODES_NAME_TO_ITERATE_CSCIS = "CSCI";
        private const string NODES_NAME_TO_ITERATE_GENERAL_CONFIGS = "GENERAL_CONFIGURATIONS";
       
        /*----------------------------------------------------------------------------*/

        static public bool GetConfigData(out Dictionary<string, Csci> _cscisMap)
        {
            _cscisMap = null;

            //read xml config file
            bool success;
            ConfigFile configFile = new ConfigFile(CONFIG_FILE_PATH, out success);
            if (!success)
            {
                MessageBox.Show("Error opening Config file : " + CONFIG_FILE_PATH);
                return false;
            }

            //-------------------------------------------------------
            //get <CSCI_LIST> data from xml config file to maps
            List<Dictionary<string, string>> cscisAttributesList;
            success = configFile.GetNodesAttributesToListOfMaps(NODES_NAME_TO_ITERATE_CSCIS, out cscisAttributesList);
            if (!success)
            {
                MessageBox.Show("Error parsing <" + NODES_NAME_TO_ITERATE_CSCIS + "> data on Config file");
                return false;
            }

            //fill CSCI map from maps (parsed xml file)          
            success = FillCsciMap(cscisAttributesList, out _cscisMap);
            if (!success)
            {
                MessageBox.Show("Error filling CSCIs list from parsed xml");
                return false;
            }

            //-------------------------------------------------------
            //get general attributes from xml config file
            List<Dictionary<string, string>> generalConfigsList;
            success = configFile.GetNodesAttributesToListOfMaps(NODES_NAME_TO_ITERATE_GENERAL_CONFIGS, out generalConfigsList);
            if (!success)
            {
                MessageBox.Show("Error parsing <" + NODES_NAME_TO_ITERATE_GENERAL_CONFIGS + "> data on Config file");
                return false;
            }

            //fill General configs from map (parsed xml file)          
            success = FillGeneralConfigs(generalConfigsList);
            if (!success)
            {
                MessageBox.Show("Error filling General configs from parsed xml");
                return false;
            }

            return success;
        }

        /*----------------------------------------------------------------------------*/

        private static bool FillGeneralConfigs(List<Dictionary<string, string>> generalConfigsList)
        {
            try
            {
                GeneralConfigs.Instance.MappingScriptPath = generalConfigsList[0]["MAPPING_SCRIPT_PATH"];
                GeneralConfigs.Instance.LogMode = generalConfigsList[0]["LOGGER_MODE"];
                GeneralConfigs.Instance.EnvPath = generalConfigsList[0]["ENV_PATH"];
                GeneralConfigs.Instance.InputsPath = generalConfigsList[0]["INPUTS_PATH"];
                GeneralConfigs.Instance.OutputsPath = generalConfigsList[0]["OUTPUTS_PATH"];

                GeneralConfigs.Instance.MailFrom = generalConfigsList[0]["NOTIFICATION_MAIL_FROM"];
                GeneralConfigs.Instance.MailTo = generalConfigsList[0]["NOTIFICATION_MAIL_TO"];               
                GeneralConfigs.Instance.MailuserDp = generalConfigsList[0]["NOTIFICATION_MAIL_USER"];
                GeneralConfigs.Instance.MailUserPassward = generalConfigsList[0]["NOTIFICATION_MAIL_PASS"];
            }
            catch
            {
                return false;
            }

            return true;
        }

        /*----------------------------------------------------------------------------*/

        static private bool FillCsciMap(List<Dictionary<string, string>> cscisAttributesList, out Dictionary<string, Csci> cscisMap)
        {
            cscisMap = new Dictionary<string, Csci>();

            try
            {
                foreach (Dictionary<string, string> csciAttributes in cscisAttributesList)
                {
                    Csci csci = new Csci();

                    csci.CsciName = csciAttributes["Name"];
                    csci.SvnUrlPath = csciAttributes["Svn_Url_Path"];
                    csci.LocalPathToCheckout = csciAttributes["Local_Path_To_Checkout"];
                    
                    csci.PreBuildScriptPath = csciAttributes["Pre_Build_Script_Path"];
                    csci.BuildScriptPath = csciAttributes["Build_Script_Path"];
                  
                    string isSysint = csciAttributes["Is_Sysint"];
                    csci.IsSysint = (isSysint == "1");

                    string dependentCscis = csciAttributes["Dependents_CSCIs"];
                    if(dependentCscis.Contains(','))
                    {
                        string[] dependentCscisArr = dependentCscis.Split(',');
                        foreach (string dependentCsci in dependentCscisArr)
                        {
                            csci.DependentCsciList.Add(dependentCsci);
                        }
                    }

                    string isCheckoutEvenIfAlreadyExists = csciAttributes["Is_Checkout_Even_If_Already_Exists"];
                    csci.IsCheckoutIfAlreadyExists = (isCheckoutEvenIfAlreadyExists == "1");

                    cscisMap[csci.CsciName] = csci;
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        /*----------------------------------------------------------------------------*/
    }
}
