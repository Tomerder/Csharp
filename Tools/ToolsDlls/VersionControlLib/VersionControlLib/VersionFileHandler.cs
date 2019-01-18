using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace VersionControlLib
{
    public abstract class VersionFileHandler
    {
        /*----------------------------------------------------------------------------*/
        /*DM*/
        string m_versionFilePath;

        public string VersionFilePath
        {
            get { return m_versionFilePath; }
            set { m_versionFilePath = value; }
        }

        /*----------------------------------------------------------------------------*/
        /*CTOR*/
        public VersionFileHandler(string _versionFilePath)
        {
            VersionFilePath = _versionFilePath;
        }

        /*----------------------------------------------------------------------------*/
        /*Abstract methods*/

        public abstract bool GetVersion(out string _version, bool _isReturnFullVersion = false);
        public abstract bool UpdateVersion(out string _sReturnedNewVersion, string _sNewVersion);

        /*----------------------------------------------------------------------------*/
        /*Common base methods*/
        /*----------------------------------------------------------------------------*/

        protected bool ExtractVersion(ref string[] _sLines, out string _sVersion, string _sKey, int iSize)
        {
            _sVersion = "";
            bool bSuccess = true;
            try
            {
                foreach (string sLine in _sLines)
                {
                    if (sLine.Contains(_sKey + " ") || sLine.Contains(_sKey + "\t") || sLine.Contains(_sKey + "_NUMBER"))
                    {
                        if (sLine.Contains('\"'))
                        {
                            int iStartIndex = sLine.IndexOf('\"');
                            int iLastIndex = sLine.LastIndexOf('\"');
                            _sVersion = sLine.Substring(iStartIndex + 1, iLastIndex - iStartIndex - 1);
                        }
                        else
                        {
                            _sVersion = sLine.Substring(sLine.Length - iSize, iSize);
                        }
                        break;
                    }
                }
            }
            catch
            {
                bSuccess = false;
            }
            return bSuccess;
        }

        /*----------------------------------------------------------------------------*/

        protected bool UpdateVersionFile(string _sOldVersion, ref string _sNewVersion)
        {
            bool bSuccess = false;
            try
            {
                // calculate new version (increasse by 1)
                if (_sOldVersion.Length == 5)
                {
                    int iNewRevision =
                                Convert.ToInt32(_sOldVersion.Substring(0, 2)) * 100 +
                                Convert.ToInt32(_sOldVersion.Substring(3, 2)) + 1;
                    _sNewVersion = iNewRevision.ToString("00-00");
                    bSuccess = true;
                }
            }
            catch
            {
                bSuccess = false;
            }
            return bSuccess;
        }

        /*----------------------------------------------------------------------------*/

        /*
        public bool SetVersionForLuh(string _sVersion, ref string _sVersionFromLuh) 
        {
            bool bSuccess = false;
            try
            {
                if (_sVersion != null || _sVersion.Length < 6)
                {
                    _sVersionFromLuh = _sVersionFromLuh.Substring(0, _sVersionFromLuh.Length - 6) + _sVersion.Substring(_sVersion.Length - 6, 6);
                    bSuccess = true;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return bSuccess;
        }    
        */ 
    }

    /*----------------------------------------------------------------------------*/

    public class VersionFileA : VersionFileHandler
    {
        /*example: version = "3681A02-07" */
         
        /*CTOR*/
        public VersionFileA(string _versionFilePath) : base(_versionFilePath){}

        public override bool GetVersion(out string _sVersion, bool _isReturnFullVersion = false)
        {
            _sVersion = "";
            bool bSuccess = true;
            try
            {
                string[] sLines = System.IO.File.ReadAllLines(VersionFilePath);
                base.ExtractVersion(ref sLines, out _sVersion,"_VERSION",10);
                _sVersion = _sVersion.Replace('.', '-');
                if (!_sVersion.Contains('-'))
                {
                    _sVersion = _sVersion.Substring(0, _sVersion.Length - 2) + "-" + _sVersion.Substring(_sVersion.Length - 2, 2);
                }

                if (_isReturnFullVersion)
                {
                    if(_sVersion.Contains('_'))
                    {
                        _sVersion = _sVersion.Replace("_", String.Empty);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                bSuccess = false;
            }

            return bSuccess;
        }

        public override bool UpdateVersion(out string _sReturnedNewVersion, string _sNewVersion)
        {
            bool bSuccess = false;
            _sReturnedNewVersion = "";
            try
            {
                string sOldVersion = "";
                if (GetVersion(out sOldVersion))
                {
                    // update version with provided version (if provided)
                    //if (_sNewVersion.Length != sOldVersion.Length)
                    if (String.IsNullOrEmpty(_sNewVersion))
                    {
                        // calculate new version (increasse by 1)
                        bSuccess = base.UpdateVersionFile(sOldVersion.Substring(sOldVersion.Length-5,5), ref _sNewVersion);
                        _sNewVersion = sOldVersion.Substring(0, sOldVersion.Length - 5) + _sNewVersion;
                    }
                    // update the version file with new version
                    File.SetAttributes(VersionFilePath, File.GetAttributes(VersionFilePath) & ~FileAttributes.ReadOnly);
                    File.WriteAllText(VersionFilePath, File.ReadAllText(VersionFilePath).Replace(sOldVersion, _sNewVersion));
                    _sReturnedNewVersion = _sNewVersion;
                    bSuccess = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _sNewVersion = "";
                bSuccess = false;
            }
            return bSuccess;
        }

    }

    /*----------------------------------------------------------------------------*/

    public class VersionFileB : VersionFileHandler
    {
        /*example: version = "0207" */

        /*CTOR*/
        public VersionFileB(string _versionFilePath) : base(_versionFilePath){}

        public override bool GetVersion(out string _sVersion, bool _isReturnFullVersion = false)
        {
            _sVersion = "";
            bool bSuccess = true;
            try
            {
                string[] sLines = System.IO.File.ReadAllLines(VersionFilePath);
                base.ExtractVersion(ref sLines, out _sVersion,"VERSION",4);
                if(_sVersion.Contains('u'))
                {
                    base.ExtractVersion(ref sLines, out _sVersion, "VERSION", 5);
                    _sVersion = _sVersion.Substring(0, 4);
                }
                _sVersion = _sVersion.Substring(_sVersion.Length - 4, 2) + "-" + _sVersion.Substring(_sVersion.Length-2, 2);

                //if (_isReturnFullVersion)
                //{
                //    _sVersion = _sVersion;
                //}

            }
            catch
            {
                _sVersion = "";
                bSuccess = false;
            }
            return bSuccess;
        }

        public override bool UpdateVersion(out string _sReturnedNewVersion, string _sNewVersion)
        {
            bool bSuccess = false;
            _sReturnedNewVersion = ""; 
            try
            {
                string sOldVersion = "";
                if (GetVersion(out sOldVersion))
                {
                    // update version with provided version (if provided)
                    if (_sNewVersion.Length != sOldVersion.Length)
                    {
                        // calculate new version (increasse by 1)
                        bSuccess = base.UpdateVersionFile(sOldVersion, ref _sNewVersion);
                    }
                    // update the version file with new version
                    File.SetAttributes(VersionFilePath, File.GetAttributes(VersionFilePath) & ~FileAttributes.ReadOnly);
                    _sReturnedNewVersion = _sNewVersion;
                    sOldVersion = sOldVersion.Substring(0, 2) + sOldVersion.Substring(3, 2);
                    _sNewVersion = _sNewVersion.Substring(0, 2) + _sNewVersion.Substring(3, 2);
                    File.WriteAllText(VersionFilePath, File.ReadAllText(VersionFilePath).Replace(sOldVersion, _sNewVersion));
                    bSuccess = true;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                bSuccess = false;
            }
           return bSuccess;
        }
        
    }

    /*----------------------------------------------------------------------------*/

    public class VersionFileC : VersionFileHandler
    {
        /* example:  
         #define XXX_VERSION		"00.71" Or "00.00.00"   OR  //XXX_VERSION_NUMBER 0x0757 
         #define XXX_VDD_NUM		"6030A"
        */

        /*CTOR*/
        public VersionFileC(string _versionFilePath) : base(_versionFilePath){}

        public override bool GetVersion(out string _sVersion, bool _isReturnFullVersion = false)
        {
            bool bSuccess = true;
            _sVersion = "";
            try
            {             
                string[] sLines = System.IO.File.ReadAllLines(VersionFilePath);
               
                //XXX_VERSION_NUMBER 0x0757
                base.ExtractVersion(ref sLines, out _sVersion, "VERSION_NUMBER", 4);
                if (!String.IsNullOrEmpty(_sVersion))
                {
                    //add "-" : 0757 => 07-57
                    _sVersion = _sVersion.Substring(0, 2) + "-" + _sVersion.Substring(2, 2);
                }
                //XXX_VERSION		"00.71"
                else 
                {
                    base.ExtractVersion(ref sLines, out _sVersion, "_VERSION", 5);
                }

                if (_isReturnFullVersion)
                {
                    string csciCatalog;
                    base.ExtractVersion(ref sLines, out csciCatalog, "_VDD_NUM", 5);
                    _sVersion = csciCatalog + _sVersion;
                    _sVersion = _sVersion.Replace('.', '-');
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                bSuccess = false;
            }

            return bSuccess;
        }

        public override bool UpdateVersion(out string _sReturnedNewVersion, string _sNewVersion)
        {
            bool bSuccess = false;
            try
            {
                string sOldVersion = string.Empty;
                if (GetVersion(out sOldVersion))
                {
                    // update version with provided version (if provided)
                    if (_sNewVersion.Length != sOldVersion.Length)
                    {
                        string sVersion = sOldVersion.Substring(0, 5);
                        sVersion.Replace('.', '-');
                        // calculate new version (increasse by 1)
                        bSuccess = base.UpdateVersionFile(sVersion, ref _sNewVersion);
                        _sNewVersion = _sNewVersion.Replace('-', '.');
                        _sNewVersion = sOldVersion.Replace(sOldVersion.Substring(0, 5), _sNewVersion);
                    }

                    //07-57 => 0x0757
                    if (sOldVersion.Contains('-') && sOldVersion.Length == 5)
                    {
                        sOldVersion = sOldVersion.Substring(0, 2) + sOldVersion.Substring(3, 2);
                        if (_sNewVersion.Contains('.') && _sNewVersion.Length == 5)
                        {
                            _sNewVersion = _sNewVersion.Substring(0, 2) + _sNewVersion.Substring(3, 2);
                        }
                    }

                    // update the version file with new version
                    File.SetAttributes(VersionFilePath, File.GetAttributes(VersionFilePath) & ~FileAttributes.ReadOnly);
                    File.WriteAllText(VersionFilePath, File.ReadAllText(VersionFilePath).Replace(sOldVersion, _sNewVersion));
                    bSuccess = true;
                }
                _sReturnedNewVersion = _sNewVersion;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _sReturnedNewVersion = string.Empty;
            }
            return bSuccess;
        }

    }

    /*----------------------------------------------------------------------------*/

    public class VersionFileD : VersionFileHandler
    {
        /*example: version = Master_1_039 */

          /*CTOR*/
        public VersionFileD(string _versionFilePath) : base(_versionFilePath){}

        public override bool GetVersion(out string _sVersion, bool _isReturnFullVersion = false)
        {
            bool bSuccess = true;
            _sVersion = "";
            try
            {
                string[] sLines = System.IO.File.ReadAllLines(VersionFilePath);
                base.ExtractVersion(ref sLines, out _sVersion, "VERSION",12);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                bSuccess = false;
            }
            return bSuccess;
        }

        public override bool UpdateVersion(out string _sReturnedNewVersion, string _sNewVersion)
        {
            bool bSuccess = true;
            _sReturnedNewVersion = "";
            try
            {
                string sOldVersion = "";
                if (GetVersion(out sOldVersion))
                {
                    string sFormatOldVersion = sOldVersion.Substring(sOldVersion.Length-5,5);
                    sFormatOldVersion = sFormatOldVersion[0] + sFormatOldVersion.Substring(sFormatOldVersion.Length-3,3);
                    sFormatOldVersion = sFormatOldVersion.Substring(0, 2) + '-' + sFormatOldVersion.Substring(2, 2);
                    // calculate new version (increasse by 1)
                    bSuccess = base.UpdateVersionFile(sFormatOldVersion, ref _sNewVersion);
                    _sNewVersion = sOldVersion.Substring(0, sOldVersion.Length - 5) + _sNewVersion[0] + '_' + _sNewVersion[1] + _sNewVersion.Substring(sFormatOldVersion.Length-2,2) ;
                    // update the version file with new version
                    File.SetAttributes(VersionFilePath, File.GetAttributes(VersionFilePath) & ~FileAttributes.ReadOnly);
                    File.WriteAllText(VersionFilePath, File.ReadAllText(VersionFilePath).Replace(sOldVersion, _sNewVersion));
                    _sReturnedNewVersion = _sNewVersion;
                    bSuccess = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                bSuccess = false;
            }
            return bSuccess;
        }

    }

    /*----------------------------------------------------------------------------*/

    public class VersionFileE : VersionFileHandler
    {
        /* example: .xml file
         <SERIAL_NUMBER size="1 1">5423A</SERIAL_NUMBER>
	     <VERSION_NUMBER size="1 1">0702</VERSION_NUMBER>
        */

          /*CTOR*/
        public VersionFileE(string _versionFilePath) : base(_versionFilePath){}

        public override bool GetVersion(out string _sVersion, bool _isReturnFullVersion = false)
        {
            bool bSuccess = true;
            _sVersion = "";
            try
            {
                XmlDocument doc = new XmlDocument();
                string[] sLines = System.IO.File.ReadAllLines(VersionFilePath);
                sLines[0] = "<?xml version=\"1.0\"?>";
                sLines[1] = "<!-- Config -->";
                string sXmlLines = string.Empty;
                foreach(string sline in sLines)
                {
                    sXmlLines += sline;
                }
                XDocument xDoc = XDocument.Parse(sXmlLines);
                string sCsciVdd = xDoc.Root.Element("SERIAL_NUMBER").Value;
                string sCsciVersion = xDoc.Root.Element("VERSION_NUMBER").Value;
                _sVersion = sCsciVdd + sCsciVersion.Substring(0, 2) + '-' + sCsciVersion.Substring(2, 2);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                bSuccess = false;
            }
            return bSuccess;
        }

        public override bool UpdateVersion(out string _sReturnedNewVersion, string _sNewVersion)
        {
            bool bSuccess = true;
            _sReturnedNewVersion = "";

            /*
            try
            {             
                string sOldVersion = string.Empty;
                if (GetVersion(out sOldVersion))
                {
                    // update version with provided version (if provided)
                    if (_sNewVersion.Length != sOldVersion.Length)
                    {
                        string sFormVersion = sCsciVersion.Substring(0, 2) + '-' + sCsciVersion.Substring(2, 2);
                        // calculate new version (increasse by 1)
                        bSuccess = base.UpdateVersionFile(sFormVersion, ref _sNewVersion);
                    }
                    else
                    {
                        sNewVdd = _sNewVersion.Substring(0, _sNewVersion.Length - 5);
                        _sNewVersion = _sNewVersion.Substring(_sNewVersion.Length - 5, 5);
                        _sNewVersion = _sNewVersion.Substring(0, 2) + _sNewVersion.Substring(2, 2);
                    }
                    // update the version file with new version
                    File.SetAttributes(VersionFilePath, File.GetAttributes(VersionFilePath) & ~FileAttributes.ReadOnly);
                    File.WriteAllText(VersionFilePath, File.ReadAllText(VersionFilePath).Replace(sCsciVersion, _sNewVersion));
                    File.WriteAllText(VersionFilePath, File.ReadAllText(VersionFilePath).Replace(sCsciVdd, sNewVdd));
                    bSuccess = true;
                }
                sCsciVersion = _sNewVersion;
                sCsciVdd = sNewVdd;
                _sReturnedNewVersion = sCsciVdd + sCsciVersion.Substring(0, 2) + '-' + sCsciVersion.Substring(2, 2);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                bSuccess = false;
            }
            */

            return bSuccess;
        }

    }
    /*----------------------------------------------------------------------------*/
}
