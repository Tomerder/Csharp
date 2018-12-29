using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VersionControlLib;

namespace AutoVersionRelease
{
    static class VersionsLibInterfaceLayer
    {
        /*----------------------------------------------------------------------------*/

        static LUHConfig m_luhConfig;

        /*----------------------------------------------------------------------------*/

        static public bool CreateVersionsLibInterfaceLayer(string _configFilePath)
        {
            bool success;

            m_luhConfig = new LUHConfig(_configFilePath, out success);

            return success;
        }

        /*----------------------------------------------------------------------------*/

        static public bool GetCsciVersion(string _csciName, out string _version)
        {
            bool success = m_luhConfig.GetVersion(_csciName, out _version);

            return success;
        }

        /*----------------------------------------------------------------------------*/

        static public bool IncreaseCsciVersion(string _csciName, out string _versionIncreased)
        {
            _versionIncreased = "XXX";

            //increase version
            bool success = m_luhConfig.UpdateVersionFile(_csciName);
            if (!success)
            {
                return false;
            }

            //set increased version as output parameter
            success = GetCsciVersion(_csciName, out _versionIncreased);
            if (!success)
            {
                return false;
            }

            return success;
        }

        /*----------------------------------------------------------------------------*/

        static public bool CreateVddExcel(string _outputPath, string _outputFileName)
        {
            bool success = true;

            try
            {
                success = m_luhConfig.CreateVddExcel(GeneralConfigs.Instance.OutputsPath, _outputFileName);
            }
            catch
            {
                return false;
            }

            return success;
        }

        /*----------------------------------------------------------------------------*/

        internal static bool AlignLuhVersionsFile(string _sysintName)
        {
            bool success = true;

            success = m_luhConfig.LuhUpdate();  

            return success;
        }

        /*----------------------------------------------------------------------------*/
    }
}
