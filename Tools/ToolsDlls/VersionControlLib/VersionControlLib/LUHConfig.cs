using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace VersionControlLib
{
    public class LUHConfig
    {
        private XmlDocument stCurrentXml = new XmlDocument();
        private XmlDocument stXmlFile = new XmlDocument();
        // dicationary that contain all cscis from config.xml
        private Dictionary<string, Csci> dicCsciMap = new Dictionary<string, Csci>();
        // list of luh files from config.xml
        private LinkedList<string> lstLuhFiles = new LinkedList<string>();
        private string sLuhpath = "";

        //====================================================================================
        // Name: SetCsciMap
        // Description: transfer all data from config file to dictionary
        // Input: _lst - list of dictionary, each dicationary is CSCI
        // Output: --
        //====================================================================================                              
        private void SetCsciMap(ref List<Dictionary<string, string>> _lst)
        {
            foreach (var vdic in _lst)
            {
                dicCsciMap.Add(vdic["luhName"], new Csci(vdic["luhName"], vdic["Path"], vdic["CSCIName"], vdic["Type"]));
            }
        }

        //====================================================================================
        // Name: GetNodesAttributesToListOfMaps
        // Description: return dictionary for each partition, which conatin data that defined in the 
        //              configuration file
        // Input: _sNodeName 
        //        _lstNodesAttrList
        // Output: bool - success/failed
        //====================================================================================
        private bool GetNodesAttributesToListOfMaps(string _sNodeName,
                ref List<Dictionary<string, string>> _lstNodesAttrList)
        {
            bool bSuccess = false;
            try
            {
                XmlNodeList nodeList = stXmlFile.GetElementsByTagName(_sNodeName);

                foreach (XmlNode csciNode in nodeList)
                {
                    // create dictionary for each csci
                    Dictionary<string, string> csciAtrributes = new Dictionary<string, string>();

                    foreach (XmlNode attribute in csciNode.ChildNodes)
                    {
                        string sKey = attribute.Name;
                        string sValue = attribute.InnerXml;
                        csciAtrributes[sKey] = sValue;
                    }
                    _lstNodesAttrList.Add(csciAtrributes);
                }
                bSuccess = true;
            }
            catch (Exception exp)
            {
                Console.WriteLine("Exception: " + exp.Message);
                bSuccess = false;
            }
            return bSuccess;
        }

        //====================================================================================
        // Name: SysIntUpdate
        // Description: update sysint version in the LUH file
        // Input: --- 
        // Output: ---
        //====================================================================================
        private bool SysIntUpdate()
        {
            bool bSuccess = true;
            // extracting all elements from luh file
            XmlNodeList elemList1 = stCurrentXml.GetElementsByTagName("FileName");
            XmlNodeList elemList2 = stCurrentXml.GetElementsByTagName("FilePartNumber");
            XmlNodeList PartitionNum = stCurrentXml.GetElementsByTagName("LoadPartNumber");
            string currentVersion = "";
            // in case of updating sysint sr1
            if (sLuhpath.Contains("SYSINT_SR1"))
            {
                currentVersion = dicCsciMap["SYSINT_SR1"].sgVersion;
            }
            // in case of updating sysint sr2
            else if (sLuhpath.Contains("SYSINT_SR2"))
            {
                currentVersion = dicCsciMap["SYSINT_SR2"].sgVersion;
            }
            else
            {
                Console.WriteLine("No such SYS_INT exist\n");
                bSuccess = false;
            }
            if (bSuccess)
            {
                PartitionNum[0].InnerXml =
                PartitionNum[0].InnerXml.Substring(0, PartitionNum[0].InnerXml.Length - 4) +
                currentVersion.Substring(currentVersion.Length - 5, 2) + currentVersion.Substring(currentVersion.Length - 2, 2);
                for (int i = 0; i < elemList1.Count; i++)
                {
                    if (elemList1[i].InnerXml == "configRecord.bin" || elemList1[i].InnerXml == "sms_romPayload.bin")
                    {
                        elemList2[i].InnerXml = currentVersion;
                    }
                }
            }
            return bSuccess;
        }

        //====================================================================================
        // Name: LoadDoc
        // Description: load LUH doc according to path
        // Input: _doc - path to document 
        // Output: bool - success/failed
        //====================================================================================
        private bool LoadDoc(string _sDoc)
        {
            try
            {
                File.SetAttributes(_sDoc, File.GetAttributes(_sDoc) & ~FileAttributes.ReadOnly);
                stCurrentXml.Load(_sDoc);
                sLuhpath = _sDoc.Substring(0, _sDoc.LastIndexOf("\\"));
            }
            catch
            {
                Console.WriteLine("The file doesn't exist in defined path");
                return false;
            }
            return true;
        }

        //====================================================================================
        // Name: SaveDoc
        // Description: Save LUH doc 
        // Input: _doc - path to document  
        // Output: bool - success/failed
        //====================================================================================
        private bool SaveDoc(string _sDoc)
        {
            try
            {
                File.SetAttributes(_sDoc, File.GetAttributes(_sDoc) & ~FileAttributes.ReadOnly);
                stCurrentXml.Save(_sDoc);
            }
            catch
            {
                Console.WriteLine("The file doesn't exist in defined path");
                return false;
            }
            return true;
        }

        //====================================================================================
        // Name: LUHConfig
        // Description: constructor
        // Input:  _configFile - path to configuration file
        //         _success - filed/success creating the object
        // Output: --
        //====================================================================================
        public LUHConfig(string _sConfigFile, out bool _bSuccess)
        {
            _bSuccess = false;
            try
            {
                List<Dictionary<string, string>> lstNodesAttrList = new List<Dictionary<string, string>>();
                stXmlFile = new XmlDocument();
                stXmlFile.Load(_sConfigFile);
                if (GetNodesAttributesToListOfMaps("Partition",ref lstNodesAttrList))
                {
                    SetCsciMap(ref lstNodesAttrList);
                    foreach (Csci vCsci in dicCsciMap.Values)
                    {
                        vCsci.GetVersion();
                    }
                    XmlNodeList files = stXmlFile.SelectNodes("/root/LUHFiles/SR");
                    foreach (XmlNode file in files)
                    {
                        lstLuhFiles.AddLast(file["Path"].InnerText);
                    }
                    
                    _bSuccess = true;
                }
            }
            catch
            {
                _bSuccess = false;
            }
        }

        //====================================================================================
        // Name: UpdateVersionFile
        // Description: 
        // Input: _sCsciUpdate - name of CSCI to update, as defined key in the Configuration file 
        //        _sNewVersion - for insert new version (not increased by 1)
        // Output: bool - success/failed
        //====================================================================================
        public bool UpdateVersionFile(string _sCsciName, string _sNewVersion = "")
        {
            bool bSuccess = false;
            try
            {
                foreach (Csci csci in dicCsciMap.Values)
                {
                    if (csci.sgCsciName == _sCsciName)
                    {
                        csci.UpdateVersion(_sNewVersion);
                        if (_sNewVersion.Length != 0)
                        {
                            Console.WriteLine(csci.sgCsciName + " version was changes to " + _sNewVersion);
                        }
                        else
                        {
                            Console.WriteLine(csci.sgCsciName + " version was increased by 1\n");
                        }
                        bSuccess = true;
                        break;
                    }
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine("Exception: " + exp.Message);
                bSuccess = false;
            }
            return bSuccess;
        }

        //====================================================================================
        // Name: LuhUpdate
        // Description: update the LUH files from config files with versions from CSCI's ver files 
        // Input: --- 
        // Output: ---
        //====================================================================================
        public bool LuhUpdate()
        {
            bool bSuccess = false;
            foreach (string luhfile in lstLuhFiles)
            {
                try
                {
                    File.SetAttributes(luhfile, File.GetAttributes(luhfile) & ~FileAttributes.ReadOnly);
                    LoadDoc(luhfile);
                    XmlNodeList elemList1 = stCurrentXml.GetElementsByTagName("FileName");
                    XmlNodeList elemList2 = stCurrentXml.GetElementsByTagName("FilePartNumber");
                    for (int i = 0; i < elemList1.Count; i++)
                    {
                        string csci = Path.GetFileName(elemList1[i].InnerXml);
                        if (dicCsciMap.Keys.Contains(csci))
                        {
                            string sNewVersion = elemList2[i].InnerXml.ToString();
                            dicCsciMap[csci].sgVerFileType.SetVersionForLuh(dicCsciMap[csci].sgVersion, ref sNewVersion);
                            elemList2[i].InnerXml = sNewVersion;
                        }
                        else
                        {
                            Console.Write(csci + " has no path defined in configFile\n");
                        }
                    }
                    SysIntUpdate();
                    SaveDoc(luhfile);
                    bSuccess = true;
                    Console.WriteLine("luh file " + luhfile + "was updated\n");
                }
                catch
                {
                    Console.WriteLine("The LUH file doesn't exist in defined path");
                    bSuccess = false;
                }
            }
            return bSuccess;
        }

        //====================================================================================
        // Name: CreateVddExcel
        // Description: create table in excel for all versions asdefined in configuration file
        // Input: _sOutputPath - define the path to locate the excel
        //        _sFileName - define excel name
        // Output: bool - success/failed
        //====================================================================================
        public bool CreateVddExcel(string _sOutputPath, string _sFileName)
        {
            try
            {
                Dictionary<string, string> data = new Dictionary<string, string>();
                Microsoft.Office.Interop.Excel.Application oXL;
                Microsoft.Office.Interop.Excel._Workbook oWB;
                Microsoft.Office.Interop.Excel._Worksheet oSheet;
                object misvalue = System.Reflection.Missing.Value;

                //Start Excel and get Application object.
                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = false;

                //Get a new workbook.
                oWB = (Microsoft.Office.Interop.Excel._Workbook)(oXL.Workbooks.Add(""));
                oSheet = (Microsoft.Office.Interop.Excel._Worksheet)oWB.ActiveSheet;
                // create headers for columns
                oSheet.Cells[1, 1] = "#";
                oSheet.Cells[1, 2] = "S\\W Name";
                oSheet.Cells[1, 3] = "Version From File";
                for(int i = 1 ; i < lstLuhFiles.Count+1 ; ++i)
                {
                    oSheet.Cells[1, 3+i] = "LUH" + i + " Version";
                }
                oSheet.get_Range("A1", "Z1").Font.Bold = true;
                int index = 2;

                // place data from versions file
                foreach (Csci csci in dicCsciMap.Values)
                {
                    if (csci.sgInVdd)
                    {
                        oSheet.Cells[index, 1] = index - 1;
                        oSheet.Cells[index, 2] = csci.sgCsciName;
                        oSheet.Cells[index, 3].NumberFormat = "@";
                        oSheet.Cells[index, 3] = csci.sgVersion;
                        index++;
                    }
                }
                
                // copy data fromn luh files
                int luhNum = 1;
                foreach (var luh in lstLuhFiles)
                {
                    stCurrentXml.Load(luh);
                    XmlNodeList elemList1 = stCurrentXml.GetElementsByTagName("FileName");
                    XmlNodeList elemList2 = stCurrentXml.GetElementsByTagName("FilePartNumber");
                    Dictionary<string, string> csciList = new Dictionary<string, string>();
                    for (int i = 0; i < elemList1.Count; ++i)
                    {
                        csciList.Add(elemList1[i].InnerText, elemList2[i].InnerText);
                    }
                    index = 2;
                    foreach (Csci csci in  dicCsciMap.Values)
                    {
                        if (csci.sgInVdd)
                        {
                            if (csciList.Keys.Contains(csci.sgLuhName))
                            {
                                oSheet.Cells[index, luhNum + 3] = csciList[csci.sgLuhName];
                            }
                            index++;
                        }
                    }
                    luhNum++;
                    csciList.Clear();
                }
                
                oXL.UserControl = false;
                // save excel file
                _sOutputPath = _sOutputPath.Replace('/', '\\');
                if (!_sOutputPath.EndsWith("\\"))
                {
                    _sOutputPath += "\\";
                }

                string fullFilePath = _sOutputPath + _sFileName + ".xls";

                oWB.SaveAs(fullFilePath, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,
                    false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                oWB.Close();
                Console.WriteLine("Excel VDD was created\n");
            }
            catch(Exception ex)
            {
                Console.WriteLine("Execption: " + ex.Message);
                return false;
            }

            return true;
        }

        //====================================================================================
        // Name: GetVersion
        // Description: extract version of CSCI from its ver file
        // Input: _sCsci - The CSCI for version extraction
        //        _sVersion - contain result
        // Output: bool - success/failed
        //====================================================================================
        public bool GetVersion(string _sCsci, out string _sVersion)
        {
            bool bSuccess = false;
            _sVersion = "";
            try
            {
                foreach (Csci csci in dicCsciMap.Values)
                {
                    if (csci.sgCsciName == _sCsci)
                    {
                        _sVersion = csci.sgVersion;
                        bSuccess = true;

                    }
                }
                if (!bSuccess)
                {
                    Console.WriteLine(_sCsci + " is not defined in configuration file\n");
                }
            }
            catch(Exception exp)
            {
                Console.WriteLine("Exception: " + exp.Message);
                bSuccess = false;
            }
            return bSuccess;
        }

        //--------------------------------------------------------------------------------
    }
}
