using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using ConfigFileLib;

namespace VersionControlLib
{
    public class VersionControl
    {
        /*----------------------------------------------------------------------------*/

        private const string NODES_NAME_TO_ITERATE_CSCIS = "CSCI";

        Dictionary<string, Csci> m_cscisMap;

        /*----------------------------------------------------------------------------*/

        public VersionControl(string _configFilePath, out bool _success)
        {
            _success = true;

            //read xml config file
            bool success;
            string expStr;
            ConfigFile configFile = new ConfigFile(_configFilePath, out success, out expStr);
            if (!success)
            {
                _success = false;
                return;
            }

            //get <CSCI_LIST> data from xml config file to maps
            List<Dictionary<string, string>> cscisAttributesList;
            success = configFile.GetNodesAttributesToListOfMaps(NODES_NAME_TO_ITERATE_CSCIS, out cscisAttributesList);
            if (!success)
            {
                _success = false;
                return;
            }

            //fill CSCI map from maps (parsed xml file)  
            success = FillCsciMap(cscisAttributesList);
            if (!success)
            {
                _success = false;
                return;
            }
        }

        /*----------------------------------------------------------------------------*/

        private bool FillCsciMap(List<Dictionary<string, string>> cscisAttributesList)
        {
            
            m_cscisMap = new Dictionary<string, Csci>();

            try
            {
                foreach (Dictionary<string, string> csciAttributes in cscisAttributesList)
                {
                    Csci csci = new Csci();

                    csci.CsciName = csciAttributes["Name"];

                    //version file path must be updated on CSCI before updating type -> so VersionHandler will be initialized with version file path
                    string versionFilePathLabel = "Version_File_Path";
                    if (csciAttributes.ContainsKey(versionFilePathLabel))
                    {
                        csci.VersionFilePath = csciAttributes[versionFilePathLabel];
                    }
                    else
                    {
                        //if version file path is not defined on config file for CSCI -> skip CSCI 
                        continue;
                    }
                    
                    //Version_File_Type
                    string fileTypeLabel = "Version_File_Type";
                    string fileType;
                    if (csciAttributes.ContainsKey(fileTypeLabel))
                    {
                        fileType = csciAttributes[fileTypeLabel];
                    }
                    else
                    {
                        //default
                        fileType = "0";
                    }
                    csci.VersionFileType = (Csci.VersionFileTypeEnum)(Convert.ToInt32(fileType));

                    //LUH_File_Path
                    string luhFilePathLabel = "LUH_File_Path";
                    string luhFilePath;
                    if (csciAttributes.ContainsKey(luhFilePathLabel))
                    {
                        luhFilePath = csciAttributes[luhFilePathLabel];
                    }
                    else
                    {
                        //default
                        luhFilePath = "";
                    }
                    csci.LuhFilePath = luhFilePath;
                    
                    //Names_On_LUH
                    string namesOnLuhLabel = "Names_On_LUH";
                    if (csciAttributes.ContainsKey(namesOnLuhLabel))
                    {
                        string namesOnLuh = csciAttributes[namesOnLuhLabel];
                        if (namesOnLuh != String.Empty)
                        {
                            string[] namesOnLuhArr = namesOnLuh.Split(',');
                            foreach (string luhName in namesOnLuhArr)
                            {
                                csci.NamesOnLuh.Add(luhName);
                            }
                        }
                    }

                    //Luh_To_Update_Version_On
                    string LuhToUpdateVersionOnLabel = "Luh_To_Update_Version_On";
                    string LuhToUpdateVersionOn = ""; //default
                    if (csciAttributes.ContainsKey(LuhToUpdateVersionOnLabel))                  
                    {
                        LuhToUpdateVersionOn = csciAttributes[LuhToUpdateVersionOnLabel];
                    }
                    csci.LuhToUpdateVersionOn = LuhToUpdateVersionOn;

                    //Is_Sysint
                    string isSysintLabel = "Is_Sysint";
                    string isSysint = "0"; //default
                    if (csciAttributes.ContainsKey(isSysintLabel))
                    {
                        isSysint = csciAttributes[isSysintLabel];
                    }
                    csci.IsSysint = (isSysint == "1"); 

                    //update CSCIs Map
                    m_cscisMap[csci.CsciName] = csci;
                }
            }
            catch(Exception e)
            {
                return false;
            }

            return true;
        }

        /*----------------------------------------------------------------------------*/
        /*APIs*/
        /*----------------------------------------------------------------------------*/

        public bool GetVersion(string _csciName, out string _version)
        {
            bool bSuccess = false;
            _version = "";
            try
            {
                Csci csciToGetVersion = m_cscisMap[_csciName];
                bSuccess = csciToGetVersion.VersionFileHandler.GetVersion(out _version);
            }
            catch (Exception exp)
            {
                Console.WriteLine("Exception: " + exp.Message);
                bSuccess = false;
            }
            return bSuccess;
        }

        /*----------------------------------------------------------------------------*/

        public bool UpdateVersionFile(string _csciName, string _newVersionToSet = "")
        {
            bool bSuccess = false;
            try
            {
                Csci csciToUpdateVersion = m_cscisMap[_csciName];
                string versionUpdated;
                bSuccess = csciToUpdateVersion.VersionFileHandler.UpdateVersion(out versionUpdated, _newVersionToSet);                                  
            }
            catch (Exception exp)
            {
                Console.WriteLine("Exception: " + exp.Message);
                bSuccess = false;
            }

            return bSuccess;
        }

        /*----------------------------------------------------------------------------*/

        public bool LuhUpdate(string _sysintName, out string _oldLuhFoloderName, out string _newLuhFoloderName)
        {
            bool bSuccess = false;
            _oldLuhFoloderName = "";
            _newLuhFoloderName = "";

            try
            {
                //get LUH path from sysint
                Csci sysintToAlign = m_cscisMap[_sysintName];
                string sysintName = sysintToAlign.CsciName;
                string luhFileToUpdate = sysintToAlign.LuhFilePath;

                File.SetAttributes(luhFileToUpdate, File.GetAttributes(luhFileToUpdate) & ~FileAttributes.ReadOnly);
                XmlDocument stCurrentXml = new XmlDocument();
                stCurrentXml.Load(luhFileToUpdate);

                //get old LUH folder name 
                _oldLuhFoloderName = stCurrentXml.GetElementsByTagName("LoadTypeDesc")[0].InnerXml + "_" + stCurrentXml.GetElementsByTagName("LoadPartNumber")[0].InnerXml;

                //for iterate on vesions
                XmlNodeList elemList1 = stCurrentXml.GetElementsByTagName("FileName");
                XmlNodeList elemList2 = stCurrentXml.GetElementsByTagName("FilePartNumber");
              
                //iterate over luh names in luh file and update if needed
                for (int i = 0; i < elemList1.Count; i++)
                {
                    //get luhName from LUH file
                    string luhName = Path.GetFileName(elemList1[i].InnerXml);
                    string csciVersionToUpdateOnLuh = "";

                    //find relevant csci for luh name
                    foreach (Csci csci in m_cscisMap.Values)
                    {
                        if(csci.NamesOnLuh.Contains(luhName))
                        {
                            //skip csci if defined not for this LUH
                            if(!String.IsNullOrEmpty(csci.LuhToUpdateVersionOn) && csci.LuhToUpdateVersionOn != sysintName)
                            {
                                continue;
                            }

                            //skip csci if sysint and LUH for other sysint
                            if (csci.IsSysint && !csci.CsciName.Equals(sysintName))
                            {
                                continue;
                            }

                            csci.VersionFileHandler.GetVersion(out csciVersionToUpdateOnLuh, true);
                            //update csci version on LUH
                            string oldLuhVersion = elemList2[i].InnerXml;
                            if (oldLuhVersion.Length > csciVersionToUpdateOnLuh.Length)
                            {
                                int prefixCharsNum = oldLuhVersion.Length - csciVersionToUpdateOnLuh.Length;
                                elemList2[i].InnerXml = oldLuhVersion.Substring(0, prefixCharsNum) + csciVersionToUpdateOnLuh;
                            }
                            else
                            {
                                elemList2[i].InnerXml = csciVersionToUpdateOnLuh;
                            }
                            break;
                        }
                    }              
                }

                //update sysint version on LUH header
                XmlNodeList PartitionNum = stCurrentXml.GetElementsByTagName("LoadPartNumber");
                string prefix = PartitionNum[0].InnerXml.Substring(0, PartitionNum[0].InnerXml.Length - 4);
                string sysintVersion;
                bSuccess = sysintToAlign.VersionFileHandler.GetVersion(out sysintVersion);
                if (bSuccess)
                {
                    PartitionNum[0].InnerXml = prefix + sysintVersion.Substring(sysintVersion.Length - 5, 2) + sysintVersion.Substring(sysintVersion.Length - 2, 2);
                }

                //get new LUH folder name 
                _newLuhFoloderName = stCurrentXml.GetElementsByTagName("LoadTypeDesc")[0].InnerXml + "_" + stCurrentXml.GetElementsByTagName("LoadPartNumber")[0].InnerXml;
   
                //SaveDoc (luhfile);
                File.SetAttributes(luhFileToUpdate, File.GetAttributes(luhFileToUpdate) & ~FileAttributes.ReadOnly);
                stCurrentXml.Save(luhFileToUpdate);

                bSuccess = true;
                Console.WriteLine("luh file " + luhFileToUpdate + " was updated\n");
                
            }         
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                bSuccess = false;
            }

            return bSuccess;
        }

        /*----------------------------------------------------------------------------*/

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
                
                // create headers row
                oSheet.Cells[1, 1] = "#";
                oSheet.Cells[1, 2] = "S\\W Name";
                oSheet.Cells[1, 3] = "Version From File";
                /*
                for(int i = 1 ; i < lstLuhFiles.Count+1 ; ++i)
                {
                    oSheet.Cells[1, 3+i] = "LUH" + i + " Version";
                }
                */
 
                oSheet.get_Range("A1", "Z1").Font.Bold = true;
                
                int index = 2;

                // place data from versions file
                foreach (Csci csci in m_cscisMap.Values)
                {
                    oSheet.Cells[index, 1] = index - 1;
                    oSheet.Cells[index, 2] = csci.CsciName;
                    oSheet.Cells[index, 3].NumberFormat = "@";

                    string csciVersion = "";
                    csci.VersionFileHandler.GetVersion(out csciVersion, true);
                    oSheet.Cells[index, 3] = csciVersion;

                    index++;
             
                }
                
                // copy data from luh files
                /*
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
                    foreach (Csci csci in dicCsciMap.Values)
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
                */
 
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

        /*----------------------------------------------------------------------------*/
        
        /*
        private bool SysIntUpdate()
        {
            bool bSuccess = true;
            // extracting all elements from luh file
            XmlNodeList elemList1 = stCurrentXml.GetElementsByTagName("FileName");
            XmlNodeList elemList2 = stCurrentXml.GetElementsByTagName("FilePartNumber");
            XmlNodeList PartitionNum = stCurrentXml.GetElementsByTagName("LoadPartNumber");
            string currentVersion = "";
            // in case of updating sysint sr1
            if (sLuhpath.Contains("SR1"))
            {
                currentVersion = dicCsciMap["SYSINT_SR1"].sgVersion;
            }
            // in case of updating sysint sr2
            else if (sLuhpath.Contains("SR2"))
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
            }
            return bSuccess;
        }
        */ 

        /*
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
        */ 

        /*----------------------------------------------------------------------------*/
    }
}
