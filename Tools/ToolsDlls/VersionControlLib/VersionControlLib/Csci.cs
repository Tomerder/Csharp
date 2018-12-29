using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VersionControlLib
{
    class Csci
    {
        private string sCsciName;
        private string sLuhName;
        private string sVerFilePath;
        private string sVersion;
        private bool bInVdd;
        private VersionFile stVerFileType;

        //====================================================================================
        // Name: SetVersionFileType
        // Description:  recieve type and create versionFile
        // Input:  _sType - TypeA/ TypeB to create versionFile
        // Output: bool - success/failed
        //====================================================================================
        private void SetVersionFileType(string _sType)
        {
            switch (_sType)
            {
                case "TypeA":
                    stVerFileType = new VersionFileA();
                    break;
                case "TypeB":
                    stVerFileType = new VersionFileB();
                    break;
                default:
                    break;
            }

        }

        //====================================================================================
        // Name: sgVerFileType
        // Description:  set/get for ersion file type
        // Input:  --
        // Output: --
        //====================================================================================
        internal VersionFile sgVerFileType
        {
            get { return stVerFileType; }
            set { stVerFileType = value; }
        }

        //====================================================================================
        // Name: Csci
        // Description:  constarctor for csci class
        // Input:  --
        // Output: --
        //====================================================================================
        public Csci(string _sLuhCsci, string _sPath, string _sVddName, string _sType)
        {
            if (_sVddName.Length != 0)
            {
                sCsciName = _sVddName;
                bInVdd = true;
            }
            else
            {
                sCsciName = _sLuhCsci;
                bInVdd = false;
            }
            sVerFilePath = _sPath;
            sLuhName = _sLuhCsci;
            SetVersionFileType(_sType);
        }

        //====================================================================================
        // Name: sgCsciName
        // Description:  set/get csci name
        // Input:  --
        // Output: --
        //====================================================================================
        public string sgCsciName
        {
            get { return sCsciName; }
            set { sCsciName = value; }
        }

        //====================================================================================
        // Name: sgVerFilePath
        // Description:  set/get version file path
        // Input:  --
        // Output: --
        //====================================================================================
        public string sgVerFilePath
        {
            get { return sVerFilePath; }
            set { sVerFilePath = value; }
        }

        //====================================================================================
        // Name: sgVersion
        // Description:  set/get for version of csci
        // Input:  --
        // Output: --
        //====================================================================================
        public string sgVersion
        {
            get { return sVersion; }
            set { sVersion = value; }
        }

        //====================================================================================
        // Name: sgLuhName
        // Description:  set/get csci name as defined in LUG
        // Input:  --
        // Output: --
        //====================================================================================
        public string sgLuhName
        {
            get { return sLuhName; }
            set { sLuhName = value; }
        }

        //====================================================================================
        // Name: sgInVdd
        // Description:  set/get if csci should be in vdd
        // Input:  --
        // Output: --
        //====================================================================================
        public bool sgInVdd
        {
            get { return bInVdd; }
            set { bInVdd = value; }
        }

        //====================================================================================
        // Name: GetVersion
        // Description:  provide version of csci according to version file type
        // Input:  --
        // Output: bool - success/failed
        //====================================================================================
        public bool GetVersion()
        {
            bool bSucess = false;
            try
            {
                if (sVerFilePath.Length != 0)
                {
                    bSucess = stVerFileType.GetVersion(sVerFilePath, out sVersion);
                }
                else
                {
                    Console.WriteLine("The CSCI " + sCsciName + " doesn't have defined path in config.xml");
                }
                bSucess = true;

            }
            catch (Exception exp)
            {
                Console.WriteLine("Exception: " + exp.Message);
            }
            return bSucess;       
        }

        //====================================================================================
        // Name: UpdateVersion
        // Description:  increase version of csci according to version file type
        // Input:  --
        // Output: bool - success/failed
        //====================================================================================
        public bool UpdateVersion(string _sNewVersion)
        {
            bool bSucess = false;
            try
            {
                if (sVerFilePath.Length != 0)
                {
                    bSucess = stVerFileType.UpdateVersion(sVerFilePath, out sVersion, _sNewVersion);
                }
                else
                {
                    Console.WriteLine("The CSCI " + sCsciName + " doesn't have defined path in config.xml");
                }
                bSucess = true;

            }
            catch (Exception exp)
            {
                Console.WriteLine("Exception: " + exp.Message);
            }
            return bSucess; 
        }
    }
}
