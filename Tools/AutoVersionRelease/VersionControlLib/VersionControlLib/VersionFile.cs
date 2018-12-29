using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace VersionControlLib
{
    class Functions
    {
        //====================================================================================
        // Name: ExtractVersionString
        // Description: extract the version string from list of lines  
        // Input:  _sLines - list of lines from version file
        //         _sVer - version string (can be 2 types)
        // Output: bool - success/failed
        //====================================================================================
        public static bool ExtractVersionString(ref string[] _sLines, out string _sVer)
        {
            bool bSuccess = false;
            _sVer = "";
            foreach (var sLine in _sLines)
            {
                if (sLine.Contains("VERSION ") || sLine.Contains("VERSION\t"))
                {
                    int iStartIndex = sLine.IndexOf("\"");
                    int iEndIndex = sLine.LastIndexOf("\"");
                    _sVer = sLine.Substring(iStartIndex, iEndIndex - iStartIndex + 1);
                    bSuccess = true;
                    break;
                }
            }
            return bSuccess;
        }

        //====================================================================================
        // Name: UpdateToProvidedVersion
        // Description: function for updating version according to provided version 
        // Input:  _sGivenVersion - provided version (if empty, doesnt do anything)
        //         _sNewVersion - new version
        // Output: bool - success/failed
        //====================================================================================
        public static bool UpdateToProvidedVersion(string _sGivenVersion, ref string _sNewVersion)
        {
            bool bSuccess = false;
            if (_sGivenVersion.Length != 0)
            {
                _sNewVersion = _sGivenVersion;
                bSuccess = true;
            }
            return bSuccess;
        }
    }

    interface VersionFile
    {
        //====================================================================================
        // Name: GetVersion
        // Description:  return the current version of the csci
        // Input:  _sPath - path to version file
        //         _sVersion - version of the csci
        // Output: bool - success/failed
        //====================================================================================
        bool GetVersion(string _sPath, out string _sVersion);
        //====================================================================================
        // Name: UpdateVersion
        // Description: increase the version number of the csci by 1 
        // Input:  _sPath - path to version file
        //         _sNewVersion - new revision instead of increasing existing version
        //         _sReturnedNewVersion - new version after increase by 1, to return to update csci
        // Output: bool - success/failed
        //====================================================================================
        bool UpdateVersion(string _sPath, out string _sReturnedNewVersion, string _sNewVersion);
        //====================================================================================
        // Name: SetVersionForLuh
        // Description: modify the version string to fit the luh file
        // Input:  _sVersion - version as defined according to type
        //         _sVersionFromXml - version as defined in LUH
        // Output: --
        //====================================================================================
        void SetVersionForLuh(string _sVersion, ref string _sVersionFromLuh);


    }

    /*----------------------------------------------------------------------------------*/

    class VersionFileA : VersionFile
    {
        /*example: version = 3681A02-07 */

        public bool GetVersion(string _sPath, out string _sVersion)
        {
            _sVersion = "";
            try
            {
                string[] sLines = System.IO.File.ReadAllLines(_sPath);
                Functions.ExtractVersionString(ref sLines, out _sVersion);
                _sVersion = _sVersion.Substring(1, _sVersion.Length - 2);

            }
            catch
            {
                _sVersion = "";
                return false;
            }
            return true;
        }

        public bool UpdateVersion(string _sPath, out string _sReturnedNewVersion, string _sNewVersion)
        {
            bool bSuccess = false;
            _sReturnedNewVersion = "";
            try
            {
                string sOldVersion = ""; 
                if (GetVersion(_sPath, out sOldVersion))
                {
                    string sNewVersion = "";
                    // update version with provided version (if provided)
                    if (!Functions.UpdateToProvidedVersion(_sNewVersion, ref sNewVersion))
                    {
                        // calculate new version (increasse by 1)
                        int iNewRevisionA =
                                    Convert.ToInt32(sOldVersion.Substring(sOldVersion.Length - 2, 2)) + 1;
                        int iNewRevisionB =
                            Convert.ToInt32(sOldVersion.Substring(sOldVersion.Length - 5, 2));
                        if (iNewRevisionA == 100)
                        {
                            iNewRevisionB++;
                            iNewRevisionA = 0;
                        }
                        sNewVersion = sOldVersion.Substring(0, sOldVersion.Length - 5) +
                            iNewRevisionB.ToString("00") + "-" + iNewRevisionA.ToString("00");
                    }
                    // update the version file with new version
                    File.SetAttributes(_sPath, File.GetAttributes(_sPath) & ~FileAttributes.ReadOnly);
                    File.WriteAllText(_sPath, File.ReadAllText(_sPath).Replace(sOldVersion, sNewVersion));
                    _sReturnedNewVersion = sNewVersion;
                    bSuccess = true;
                }
            }
            catch
            {
                Console.WriteLine("Updating version Failed\n");
                _sReturnedNewVersion = "";
                bSuccess = false;
            }
            return bSuccess;
        }

        public void SetVersionForLuh(string _sVersion, ref string _sVersionFromLuh)
        {
            _sVersionFromLuh = _sVersion;
        }
    }

    /*----------------------------------------------------------------------------------*/

    class VersionFileB : VersionFile
    {
        /*example: version = 02-07 */

        public bool GetVersion(string _sPath, out string _sVersion)
        {
            _sVersion = "";
            try
            {
                string[] sLines = System.IO.File.ReadAllLines(_sPath);
                Functions.ExtractVersionString(ref sLines, out _sVersion);
                _sVersion = _sVersion.Substring(1, _sVersion.Length - 2);
                _sVersion = _sVersion.Substring(_sVersion.Length - 4, 2) + "-" + _sVersion.Substring(_sVersion.Length-2, 2);
            }
            catch
            {
                _sVersion = "";
                return false;
            }
            return true;
        }

        public bool UpdateVersion(string _sPath, out string _sReturnedNewVersion, string _sNewVersion)
        {
            bool bSuccess = false;
            _sReturnedNewVersion = ""; 
            string sNewVersion = "";
            try
            {
                string sOldVersion = "";
                if (GetVersion(_sPath, out sOldVersion))
                {
                    // update version with provided version (if provided)
                    if (!Functions.UpdateToProvidedVersion(_sNewVersion, ref sNewVersion))
                    {
                        // calculate new version (increasse by 1)
                        int iNewRevision =
                            Convert.ToInt32(sOldVersion.Substring(0, 2)) * 100 +
                            Convert.ToInt32(sOldVersion.Substring(3, 2)) + 1;
                        sNewVersion = iNewRevision.ToString("0000");
                        _sReturnedNewVersion = sNewVersion.Substring(0, 2) + "-" + sNewVersion.Substring(2, 2);
                    }
                    else
                    {
                        _sReturnedNewVersion = sNewVersion;
                    }
                    // update the version file with new version
                    File.SetAttributes(_sPath, File.GetAttributes(_sPath) & ~FileAttributes.ReadOnly);
                    File.WriteAllText(_sPath, File.ReadAllText(_sPath).Replace(sOldVersion, sNewVersion));
                    bSuccess = true;
                }
            }
            catch
            {
                Console.WriteLine("Updating version Failed\n");
                _sNewVersion = "";
                bSuccess = false;
            }
           return bSuccess;
        }
      
        public void SetVersionForLuh(string _sVersion, ref string _sVersionFromLuh)
        {
            _sVersionFromLuh = _sVersionFromLuh.Substring(0, _sVersionFromLuh.Length - 5) + _sVersion;
        }

    }
    /*----------------------------------------------------------------------------------*/

    class VersionFileC : VersionFile
    {
        /*example: version = Master_1_039 */

        public bool GetVersion(string _sPath, out string _sVersion)
        {
            _sVersion = "";
            try
            {
                string[] sLines = System.IO.File.ReadAllLines(_sPath);
                Functions.ExtractVersionString(ref sLines, out _sVersion);
                _sVersion = _sVersion.Substring(1, _sVersion.Length - 2);
            }
            catch
            {
                _sVersion = "";
                return false;
            }
            return true;
        }

        public bool UpdateVersion(string _sPath, out string _sReturnedNewVersion, string _sNewVersion)
        {
            bool bSuccess = false;
            _sReturnedNewVersion = "";

            try
            {
                string sOldVersion = "";
                if (GetVersion(_sPath, out sOldVersion))
                {
                    string sNewVersion = "";
                    // update version with provided version (if provided)
                    if (!Functions.UpdateToProvidedVersion(_sNewVersion, ref sNewVersion))
                    {
                        // calculate new version (increasse by 1)
                        int iNewRevisionA = Convert.ToInt32(sOldVersion.Substring(sOldVersion.Length - 3, 3)) + 1;
                        int iNewRevisionB = Convert.ToInt32(sOldVersion.Substring(sOldVersion.Length - 5, 1));
                        if (iNewRevisionA == 1000)
                        {
                            iNewRevisionB++;
                            iNewRevisionA = 0;
                        }
                        sNewVersion = sOldVersion.Substring(0, sOldVersion.Length - 5) + iNewRevisionB.ToString("0") + "_" + iNewRevisionA.ToString("000");
                    }
                    // update the version file with new version
                    File.SetAttributes(_sPath, File.GetAttributes(_sPath) & ~FileAttributes.ReadOnly);
                    File.WriteAllText(_sPath, File.ReadAllText(_sPath).Replace(sOldVersion, sNewVersion));
                    _sReturnedNewVersion = sNewVersion;
                    bSuccess = true;
                }
            }
            catch
            {
                Console.WriteLine("Updating version Failed\n");
                _sReturnedNewVersion = "";
                bSuccess = false;
            }
            return bSuccess;
        }

        public void SetVersionForLuh(string _sVersion, ref string _sVersionFromLuh)
        {
            _sVersionFromLuh = _sVersion;
        }
    }

    /*----------------------------------------------------------------------------------*/
}

