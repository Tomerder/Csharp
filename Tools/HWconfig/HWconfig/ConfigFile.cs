using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Windows.Forms;

namespace HWconfig
{
    class ConfigFile
    {
        private const string ERR_MSG_TITLE = "Configuration File Error";
        private int dummy;

        public void ConfigGetRegs(string _fileName)
        {
            if (!File.Exists(_fileName))
            {
                _fileName = "config.xml";
            }

            if (File.Exists(_fileName))
            {
                // Load document
                XmlDocument xml = new XmlDocument();
                xml.Load(_fileName);

                XmlNode rootNode = xml.GetElementsByTagName("configurationRegisters").Item(0);  // SelectSingleNode("configurationRegisters");

                // Iterate all tabs
                int tabIndex = 0;
                foreach (XmlNode tabNode in rootNode.ChildNodes)
                {
                    //add tab to Set
                    string curTabName = tabNode.Attributes["name"].Value; ;
                    
                    try
                    {
                        Program.TabsSet.Add(curTabName);
                    }
                    catch
                    {
                        MessageBox.Show("Duplicate tab name : " + curTabName, ERR_MSG_TITLE);
                    }

                    //tab base address
                    string curTabBaseAddress = tabNode.Attributes["baseAddress"].Value; ;
                    if (!Common.HexaStringToInt32(curTabBaseAddress, out dummy))
                    {
                        MessageBox.Show("Wrong Tab baseAddress : " + curTabName, ERR_MSG_TITLE);
                    }

                    // Iterate all regs in tab
                    foreach (XmlNode regNode in tabNode.ChildNodes)
                    {
                        if (regNode.NodeType == XmlNodeType.Element)
                        {
                            Reg reg2Add2Map = new Reg();

                            reg2Add2Map.TabName = curTabName;
                            reg2Add2Map.RegName = regNode.Name;

                            //default offset from tab base address
                            string regOffsetAddress = "0x0";

                            // Iterate all reg attributes
                            foreach (XmlNode attrNode in regNode.ChildNodes)
                            {
                                string key = attrNode.Attributes["key"].Value;
                                string value = attrNode.Attributes["value"].Value;

                                switch (key)
                                {
                                    case "NAME":
                                        reg2Add2Map.Name = value;
                                        break;

                                    case "OFFSET_ADDRESS":
                                        regOffsetAddress = value;
                                        if (!Common.HexaStringToInt32(regOffsetAddress, out dummy))
                                        {
                                            MessageBox.Show("Wrong OFFSET_ADDRESS : " + reg2Add2Map.Name, ERR_MSG_TITLE);
                                        }
                                        break;

                                    case "IS_READ_ONLY":
                                        try
                                        {
                                            reg2Add2Map.IsReadOnly = Convert.ToBoolean(value);
                                        }
                                        catch
                                        {
                                            MessageBox.Show("Wrong IS_READ_ONLY boolean value : " + reg2Add2Map.Name, ERR_MSG_TITLE);
                                        }
                                        break;

                                    case "VALIDATION":
                                        try
                                        {
                                            reg2Add2Map.EValidation = (ValidationEnum)Enum.Parse(typeof(ValidationEnum), value);
                                        }
                                        catch
                                        {
                                            MessageBox.Show("Wrong VALIDATION Enum value for : " + reg2Add2Map.Name, ERR_MSG_TITLE);
                                        }
                                        break;

                                    case "LIST_OF_VALUES":
                                        reg2Add2Map.PossiableValues = new HashSet<string>();
                                        string[] values2Add2Set = value.Split(',');
                                        foreach (String val2Add2Set in values2Add2Set)
                                        {
                                            try
                                            {
                                                reg2Add2Map.PossiableValues.Add(val2Add2Set);
                                            }
                                            catch
                                            {
                                                MessageBox.Show("Duplicate LIST_OF_VALUES : " + reg2Add2Map.Name, ERR_MSG_TITLE);
                                            }
                                        }
                                        break;

                                    case "MIN_VAL":
                                        try
                                        {
                                            reg2Add2Map.MinVal = Convert.ToDouble(value);
                                        }
                                        catch
                                        {
                                            MessageBox.Show("Wrong MIN_VAL : " + reg2Add2Map.Name, ERR_MSG_TITLE);
                                        }
                                        break;

                                    case "MAX_VAL":
                                        try
                                        {
                                            reg2Add2Map.MaxVal = Convert.ToDouble(value);
                                        }
                                        catch
                                        {
                                            MessageBox.Show("Wrong MAX_VAL : " + reg2Add2Map.Name, ERR_MSG_TITLE);
                                        }
                                        break;

                                    case "STARTS_FROM_BIT_NUM":
                                        try
                                        {
                                            reg2Add2Map.StartsFromBitNum = Convert.ToInt32(value);
                                            if (reg2Add2Map.StartsFromBitNum < 0 || reg2Add2Map.StartsFromBitNum > 31)
                                            {
                                                MessageBox.Show("Out of range STARTS_FROM_BIT_NUM : " + reg2Add2Map.Name, ERR_MSG_TITLE);
                                            }
                                        }
                                        catch
                                        {
                                            MessageBox.Show("Wrong STARTS_FROM_BIT_NUM : " + reg2Add2Map.Name, ERR_MSG_TITLE);
                                        }
                                        break;

                                    case "LENGTH_BITS":
                                        try
                                        {
                                            reg2Add2Map.LengthBits = Convert.ToInt32(value);
                                            if (reg2Add2Map.LengthBits < 1 || reg2Add2Map.LengthBits > 32)
                                            {
                                                MessageBox.Show("Out of range LENGTH_BITS : " + reg2Add2Map.Name, ERR_MSG_TITLE);
                                            }
                                        }
                                        catch
                                        {
                                            MessageBox.Show("Wrong LENGTH_BITS : " + reg2Add2Map.Name, ERR_MSG_TITLE);
                                        }
                                        break;

                                    case "RESET_VALUE":
                                        reg2Add2Map.ResetValue = value;
                                        break;
                                }
                            }//  foreach (XmlNode attrNode in regNode.ChildNodes)

                            //set reg address : base address + offset address
                            try
                            {
                                UInt32 initialBaseAddress = Convert.ToUInt32(Program.INITIAL_BASE_ADDRESS.Substring(2), 16);
                                UInt32 baseAddress = Convert.ToUInt32(curTabBaseAddress.Substring(2), 16);
                                UInt32 offsetAddress = Convert.ToUInt32(regOffsetAddress.Substring(2), 16);

                                UInt32 regAddress = initialBaseAddress + baseAddress + offsetAddress;
                                reg2Add2Map.Address = "0x" + String.Format("{0:X}", regAddress);
                            }
                            catch
                            {
                                MessageBox.Show("Address error for : " + reg2Add2Map.Name, ERR_MSG_TITLE);
                            }

                            if (reg2Add2Map.IsLegal())
                            {
                                try
                                {
                                    Program.RegsMap.Add(reg2Add2Map.Name, reg2Add2Map);
                                }
                                catch
                                {
                                    MessageBox.Show("Duplicate register name : " + reg2Add2Map.Name, ERR_MSG_TITLE);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Missing mandatory parameters for : " + reg2Add2Map.Name, ERR_MSG_TITLE);
                            }

                        }//  if (node.NodeType == XmlNodeType.Element)

                    }//  foreach (XmlNode regNode in rootNode.ChildNodes)

                    tabIndex++;
                }  //foreach (XmlNode tabNode in rootNode.ChildNodes)

            }// if (File.Exists(_fileName))
        }

        /*--------------------------------------------------------------------------------------------------*/

        public string ConfigGetLogMode(string _fileName)
        {
            if (!File.Exists(_fileName))
            {
                _fileName = "config.xml";
            }

            if (File.Exists(_fileName))
            {
                // Load document
                XmlDocument xml = new XmlDocument();
                xml.Load(_fileName);

                XmlNode rootNode = xml.GetElementsByTagName("log").Item(0);  // SelectSingleNode("configurationRegisters");

                string logMode = rootNode.Attributes["loggerMode"].Value;

                return logMode;        
            }

            return Logger.LOG_MODE_ENUM.NONE.ToString();
        }

        /*--------------------------------------------------------------------------------------------------*/

    }  // class ConfigFile

} //namespace HWconfig