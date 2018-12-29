using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using ConfigFileLib;

namespace ToolsPortal
{
    static class ConfigFileLogic
    {
        private const string CONFIG_FILE_PATH = "../../ConfigFile.xml";
        private const string NODES_NAME_TO_ITERATE_TOOLS = "TOOL";

        /*----------------------------------------------------------------------------*/

        static public bool GetConfigData(out Dictionary<string, Tool> _toolsMap)
        {
            _toolsMap = null;

            //read xml config file
            bool success;
            ConfigFile configFile = new ConfigFile(CONFIG_FILE_PATH, out success);
            if (!success)
            {
                MessageBox.Show("Error opening Config file : " + CONFIG_FILE_PATH);
                return false;
            }

            //get <TOOLS> data from xml config file to maps
            List<Dictionary<string, string>> toolsAttributesList;
            success = configFile.GetNodesAttributesToListOfMaps(NODES_NAME_TO_ITERATE_TOOLS, out toolsAttributesList);
            if (!success)
            {
                MessageBox.Show("Error parsing <" + NODES_NAME_TO_ITERATE_TOOLS + "> data on Config file");
                return false;
            }

            //fill CSCI map from maps (parsed xml file)          
            success = FillToolsMap(toolsAttributesList, out _toolsMap);
            if (!success)
            {
                MessageBox.Show("Error filling CSCIs list from parsed xml");
                return false;
            }

            return success;
        }    

        /*----------------------------------------------------------------------------*/

        static private bool FillToolsMap(List<Dictionary<string, string>> toolsAttributesList, out Dictionary<string, Tool> _toolsMap)
        {
            _toolsMap = new Dictionary<string, Tool>();

            try
            {
                foreach (Dictionary<string, string> csciAttributes in toolsAttributesList)
                {
                    Tool tool = new Tool();

                    tool.Name = csciAttributes["Name"];
                    tool.ExeFilePath = csciAttributes["Exe_File_Path"];
                    tool.IconFilePath = csciAttributes["Icon_File_Path"];
                    tool.ReadmeFilePath = csciAttributes["Readme_File_Path"];
                    tool.Author = csciAttributes["Author"];

                    _toolsMap[tool.Name] = tool;
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
