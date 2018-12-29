using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using CsvReadWrite;
using System.IO;
using Microsoft.VisualBasic;


namespace HWconfig
{
    public partial class FormHwConfig : Form
    {
        public static FormHwConfig Instance; //singleton

        /*-----------------------------------------------------------------------------------*/
        private const string EXPORTED_WORD_DOC_NAME = "HSID_tables";

        //is being used for saving last position (index) that was written to terminal (in order to retrieve next cmd)
        private int m_currentTerminalIndex;

        //vector for saving history of terminal commands 
        private List<string> m_historyCmdVector;
        private int m_curHistoryCmdVectorIndex;

        /*-----------------------------------------------------------------------------------*/

        // This delegate enables asynchronous calls for setting
        // the text property on a TextBox control.
        // enables thread safe call of AppendTerminalResult from Connection.ReadHandler 
        delegate void AppendTerminalResultCallback(string text);

        /*-----------------------------------------------------------------------------------*/

        public FormHwConfig()
        {
            Instance = this;
            
            //init - generated code 
            InitializeComponent();

            //init - manual code
            InitForm();

            m_currentTerminalIndex = 0;

            m_historyCmdVector = new List<string>();
            m_curHistoryCmdVectorIndex = 0;

            textBoxSendCmdDelay.Text = Convert.ToString(CommTargetAbstractLayer.SLEEP_AFTER_WRITE);

            //register received data handler func to the communication object
            //this function will be called whenever data is received from the communication 
            Program.Connection.RegisterRecievedDataHandler(AppendTerminalResult);
        }

        /*-----------------------------------------------------------------------------------*/

        private void FormHwConfig_Load(object sender, EventArgs e)
        {
            //dynamically add tabs
            CreateFormTabsFromTabsSet();

            //dynamically build form fields in each tab according to regs map
            CreateFormFieldsFromRegsMap();

            //load regs values from serial/telnet connection + check if loaded values are equal to reset values 
            //LoadRegsValues(false);

            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowOnly;
        }

        /*-----------------------------------------------------------------------------------*/

        private bool LoadRegsValues(bool _isReloadAfterUpdate)    /*-*READ*-*/
        {
            Logger.Instance.Log(Logger.LOG_MODE_ENUM.DEBUG, "Enter");

            if (!Program.Connection.IsHandshake())
            {
                MessageBox.Show("Connection to target is not established", "Error");
                Logger.Instance.Log(Logger.LOG_MODE_ENUM.ERROR, "Handshake failed");
                return false;
            }

            //disable reading thread 
            Program.Connection.SetDataReceivedHandlerEnabled(false);

            bool wasLastUpdateSuccessful = true;

            //go over every reg  
            foreach (Reg reg in Program.RegsMap.Values)
            {
                RegFormData regFormData;
                if (!LocateRegInForm(reg.Name, out regFormData))
                {
                    AppendMessagePanel(reg.Name + " Field was not found");
                    wasLastUpdateSuccessful = false;
                    continue;
                }

                //-------------------------------------------------------------------------------------
                //get reg value (from connection)
                string regValue = "";
                if (!CommTargetAbstractLayer.GetRegValueFromTarget(reg.Address, out regValue))
                {
                    MessageBox.Show(reg.Name + " : Error reading register value", "error");
                    Logger.Instance.Log(Logger.LOG_MODE_ENUM.ERROR, reg.Name + " : Error reading register value");
                    continue;
                }
                //-------------------------------------------------------------------------------------

                //check value that was read from target (hexa string) 
                int dummy;
                if (!Common.HexaStringToInt32(regValue, out dummy))
                {
                    MessageBox.Show(reg.Name + " : Error reading register value", "error");
                    Logger.Instance.Log(Logger.LOG_MODE_ENUM.ERROR, reg.Name + " : Error converting register value");
                    continue;
                }

                //Get the bits[from..to] value within 32bit reg value
                bool isHexaDisplay = true;
                string regValueBitwise = "";
                if (!Common.GetRegBitsFromToValue(regValue, reg.StartsFromBitNum, reg.LengthBits, isHexaDisplay, out regValueBitwise))
                {
                    ErrorMsg(reg.Name + "Error getting bitwise value");
                    Logger.Instance.Log(Logger.LOG_MODE_ENUM.ERROR, reg.Name + " : Error getting bitwise value from reg value");
                    continue;
                }

                //set reg value at reg field
                SetRegFieldWithValue(reg.Name, regValueBitwise);

                //check that saved updated value match the value that was read 
                if (_isReloadAfterUpdate)
                {
                    if (regValueBitwise != null && reg.ValueUpdated != null && reg.ValueUpdated != "" && reg.ValueUpdated != regValueBitwise)
                    {
                        wasLastUpdateSuccessful = false;
                        Logger.Instance.Log(Logger.LOG_MODE_ENUM.ERROR, reg.Name + " : loaded value is not the same as updated value");
                        AppendMessagePanel(reg.Name + " : was not updated successfully");
                    }
                }

                //save loaded value 
                reg.ValueLastLoaded = regValueBitwise;

            }

            //make sure buffer is empty before enabling reading thread
            //Program.Connection.RecieveResult();

            //enable reading thread 
            Program.Connection.SetDataReceivedHandlerEnabled(true);

            //check if loaded values are equal to reset values 
            CheckResetValues();

            return wasLastUpdateSuccessful;
        }

        /*-----------------------------------------------------------------------------------*/

        private void buttonReadRegs_Click(object sender, EventArgs e)
        {        
            Logger.Instance.Log(Logger.LOG_MODE_ENUM.DEBUG, "Enter");

            Cursor.Current = Cursors.WaitCursor;
           
            bool isLoadOk = LoadRegsValues(false);

            Cursor.Current = Cursors.Default;

            if (isLoadOk)
            {
                AppendMessagePanel("Registers Values Loaded Successfully");
            }
        }

        /*-----------------------------------------------------------------------------------*/

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            Logger.Instance.Log(Logger.LOG_MODE_ENUM.DEBUG, "Enter");

            if (CheckFieldsValidity())
            {
                Cursor.Current = Cursors.WaitCursor;

                //Update HW through serial connection + save reg.ValueUpdated for update successful check 
                bool isUpdatedGood = UpdateRegs();   /*-*WRITE*-*/

                //reload regs values + check if updated correctly + check reset values 
                bool isLoadedValsEqualUpdatedVals = LoadRegsValues(true);

                if (isUpdatedGood && isLoadedValsEqualUpdatedVals)
                {
                    AppendMessagePanel("Updated successfully");
                }
                else
                {
                    AppendMessagePanel("Update was finished with errors");
                }

                Cursor.Current = Cursors.Default;
            }
        }

        /*-----------------------------------------------------------------------------------*/

        private bool UpdateRegs()
        {
            Logger.Instance.Log(Logger.LOG_MODE_ENUM.DEBUG, "Enter");
           
            bool isUpdatedGood = true;

            if (!Program.Connection.IsHandshake())
            {
                MessageBox.Show("Connection to target is not established", "Error");
                Logger.Instance.Log(Logger.LOG_MODE_ENUM.ERROR, "Handshake failed");
                return false;
            }

            //disable reading thread 
            Program.Connection.SetDataReceivedHandlerEnabled(false);

            //go over every reg  
            foreach (Reg reg in Program.RegsMap.Values)
            {
                //skip READ ONLY regs
                if (reg.IsReadOnly == true)
                {
                    continue;
                }

                RegFormData regFormData;
                if (!LocateRegInForm(reg.Name, out regFormData))
                {
                    AppendMessagePanel(reg.Name + " Field was not found");
                    isUpdatedGood = false;
                    continue;
                }

                //get reg value for update 
                string valueToUpdate = regFormData.regFieldvalue;

                //if valueToUpdate == valueLastLoaded : skip
                if (reg.ValueLastLoaded != null && reg.ValueLastLoaded == valueToUpdate)
                {
                    Logger.Instance.Log(Logger.LOG_MODE_ENUM.DEBUG, reg.Name + " : update skipped - value has not been changed");
                    continue;
                }

                //if valueToUpdate == "" : skip
                if (valueToUpdate.Equals(""))
                {
                    Logger.Instance.Log(Logger.LOG_MODE_ENUM.DEBUG, reg.Name + " : update skipped - value is empty");
                    continue;
                }

                //if valueToUpdate is longer then reg length - skip
                if (!Common.IsValueToUpdateFitInReg(reg.LengthBits, valueToUpdate))
                {
                    AppendMessagePanel(reg.Name + " : value is too big to fit in range (" + reg.LengthBits + " bits)");
                    isUpdatedGood = false;
                    continue;
                }

                //preper bitwise value for updating only necessary bits
                string regValueForUpdateBitwise = "";
                if (!GetRegValueForUpdatingBitwise(reg, valueToUpdate, out regValueForUpdateBitwise))
                {
                    ErrorMsg(reg.Name + " : Error updating value");
                    Logger.Instance.Log(Logger.LOG_MODE_ENUM.ERROR, reg.Name + " : Error GetRegValueForUpdatingBitwise");
                    isUpdatedGood = false;
                    continue;
                }

                //----------------------------------------------------------------------------------
                //update reg through connection 
                CommTargetAbstractLayer.SetRegValueOnTarget(reg.Address, regValueForUpdateBitwise, textBoxSendCmdDelay.Text);

                //----------------------------------------------------------------------------------

                Logger.Instance.Log(Logger.LOG_MODE_ENUM.IMPORTANT, "Update Reg : " + reg.Address + " <= " + regValueForUpdateBitwise);

                AppendMessagePanel(reg.Name + " update command was sent to set the value to : " + valueToUpdate);

                //save updated value for correctness checking 
                reg.ValueUpdated = valueToUpdate;
            }// foreach (Reg reg in Program.RegsMap.Values)

            //enable reading thread 
            Program.Connection.SetDataReceivedHandlerEnabled(true);

            return isUpdatedGood;
        }

        /*-----------------------------------------------------------------------------------*/

        private bool GetRegValueForUpdatingBitwise(Reg _reg, string _regValueForUpdate, out string _regValueForUpdateBitwise)
        {
            Logger.Instance.Log(Logger.LOG_MODE_ENUM.DEBUG, "Enter");

            _regValueForUpdateBitwise = "";

            //in case all 32Bits of reg need to be updated
            if (_reg.StartsFromBitNum == 0 && _reg.LengthBits == 32)
            {
                _regValueForUpdateBitwise = _regValueForUpdate;
                return true; ;
            }

            //read current value from reg address
            string regValueStr = "";
            if (!CommTargetAbstractLayer.GetRegValueFromTarget(_reg.Address, out regValueStr))
            {
                Logger.Instance.Log(Logger.LOG_MODE_ENUM.ERROR, _reg.Name + " : Error reading register value");
                return false;
            }

            Logger.Instance.Log(Logger.LOG_MODE_ENUM.DEBUG, "regValue before update: " + regValueStr + 
                                                            " , Value For Update : " + _regValueForUpdate +
                                                            " , from-to : " + _reg.StartsFromBitNum + "-" + (_reg.StartsFromBitNum + _reg.LengthBits - 1));

            //get 32bit value that is united from current value and value to update (from-to bit)
            if (!Common.BitwiseUpdatePartOf32BitValue(regValueStr, _reg.StartsFromBitNum, _reg.LengthBits, _regValueForUpdate, out _regValueForUpdateBitwise) )
            {
                Logger.Instance.Log(Logger.LOG_MODE_ENUM.ERROR, _reg.Name + " : Error getting bitwise value for updating");
            }

            Logger.Instance.Log(Logger.LOG_MODE_ENUM.DEBUG, "regValueForUpdatingBitwise : " + _regValueForUpdateBitwise);
            

            return true;
        }

        /*-----------------------------------------------------------------------------------*/

        private void searchFieldTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                string field2Search = this.searchFieldTextBox.Text;

                RegFormData regFormData;
                if (!LocateRegInForm(field2Search, out regFormData))
                {
                    MessageBox.Show("Field was not found", "Search result");
                    return;
                }

                //goto tab
                tabControl1.SelectedTab = regFormData.regTab;

                //focus field
                if (regFormData.isComboBox)
                {
                    regFormData.regComboBox.Select();
                }
                else
                {
                    regFormData.regTextBox.Focus();
                }
            }
        }

        /*-----------------------------------------------------------------------------------*/

        private void textBoxTerminal_KeyDown(object sender, KeyEventArgs e)
        {
            // if cursor is before '===> ' : ignore any key pressed expept Right
            if (textBoxTerminal.SelectionStart < m_currentTerminalIndex && e.KeyCode != Keys.Right)
            {
                e.SuppressKeyPress = true;
            }

            // LEFT/BACKSPACE : going before '===>' is not possible
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Back)
            {
                if (textBoxTerminal.SelectionStart <= m_currentTerminalIndex)
                {
                    e.SuppressKeyPress = true;
                }
            }

            //DOWN/UP : get history ++/--
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
            {
                e.SuppressKeyPress = true;

                //delete current line
                if (m_currentTerminalIndex <= textBoxTerminal.Text.Length)
                {
                    textBoxTerminal.Text = textBoxTerminal.Text.Substring(0, m_currentTerminalIndex);
                }

                //get cmd from history and update current index
                if (m_curHistoryCmdVectorIndex >= 0 && (m_curHistoryCmdVectorIndex <= m_historyCmdVector.Count - 1)  )
                {
                    textBoxTerminal.Text += m_historyCmdVector[m_curHistoryCmdVectorIndex];
                 
                    if (e.KeyCode == Keys.Down && m_curHistoryCmdVectorIndex < m_historyCmdVector.Count - 1)
                    {
                        m_curHistoryCmdVectorIndex++;
                    }
                    else if (e.KeyCode == Keys.Up && m_curHistoryCmdVectorIndex > 0)
                    {
                        m_curHistoryCmdVectorIndex--;
                    }
                }

                //set cursor position to end 
                textBoxTerminal.SelectionStart = textBoxTerminal.Text.Length;
            }

            //ENTER
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                e.SuppressKeyPress = true;

                string terminalText = textBoxTerminal.Text;
                string cmdToSend = "";

                //if cmd was entered 
                if (terminalText.Length - m_currentTerminalIndex > 0)
                {
                    cmdToSend = terminalText.Substring(m_currentTerminalIndex, terminalText.Length - m_currentTerminalIndex);

                    //add cmd to history    and     update cmd history vector index <= last cmd 
                    m_historyCmdVector.Add(cmdToSend);
                    m_curHistoryCmdVectorIndex = m_historyCmdVector.Count - 1;
                }        

                //update terminal index  
                m_currentTerminalIndex = terminalText.Length;

                //send cmd
                Program.Connection.SendCmd(cmdToSend, CommTargetAbstractLayer.GetSleepDelay(textBoxSendCmdDelay.Text));
            }
        }

        /*-----------------------------------------------------------------------------------*/

        /*called from serial communication read handler - THREAD SAFE*/
        public void AppendTerminalResult(string _result)
        {
            Logger.Instance.Log(Logger.LOG_MODE_ENUM.DEBUG, "Enter");

            if (this.textBoxTerminal.InvokeRequired)
            {
                AppendTerminalResultCallback d = new AppendTerminalResultCallback(AppendTerminalResult);
                this.Invoke(d, new object[] { _result });
            }
            else
            {
                //_result.Replace("\r\n", "" + System.Environment.NewLine);
                textBoxTerminal.AppendText(_result);
                textBoxTerminal.ScrollToCaret();

                //set cursor position and new cmd position to end 
                m_currentTerminalIndex = textBoxTerminal.Text.Length;
                textBoxTerminal.SelectionStart = m_currentTerminalIndex;

            }
        }

        /*-----------------------------------------------------------------------------------*/

        public void AppendMessagePanel(string _msg)
        {
            textBoxMessagesPanel.AppendText(_msg + Environment.NewLine);
            textBoxMessagesPanel.ScrollToCaret();
        }

        /*-----------------------------------------------------------------------------------*/

        public void ErrorMsg(string _errMsg)
        {
            MessageBox.Show(_errMsg, "Error");
        }

        /*-----------------------------------------------------------------------------------*/

        //check validity of each field 
        private bool CheckFieldsValidity()
        {
            //iterate over every reg on map
            foreach (Reg reg in Program.RegsMap.Values)
            {
                RegFormData regFormData;
                if (!LocateRegInForm(reg.Name, out regFormData))
                {
                    AppendMessagePanel(reg.Name + " was not found for validity check");
                    return false;
                }

                //min max
                if (reg.EValidation == ValidationEnum.MIN_MAX_VALUE)
                {
                    double min = reg.MinVal;
                    double max = reg.MaxVal;

                    try
                    {
                        double value = Convert.ToDouble(regFormData.regFieldvalue);
                        if (value > max || value < min)
                        {
                            ErrorMsg(reg.Name + " : wrong value (min max)");
                            tabControl1.SelectedTab = regFormData.regTab;
                            regFormData.regTextBox.Focus();
                            return false;
                        }
                    }
                    catch
                    {
                        ErrorMsg("wrong value for :" + reg.Name);
                        tabControl1.SelectedTab = regFormData.regTab;
                        regFormData.regTextBox.Focus();
                        return false;
                    }
                }
                //comboBox 
                else if (reg.EValidation == ValidationEnum.LIST_OF_VALUES)
                {
                    if (!regFormData.regFieldvalue.Equals("") && !reg.PossiableValues.Contains(regFormData.regFieldvalue))
                    {
                        ErrorMsg(reg.Name + " : value does not exist in list of possible values");
                        tabControl1.SelectedTab = regFormData.regTab;
                        regFormData.regComboBox.Focus();
                        return false;
                    }
                }
            } //foreach (Reg reg in Program.RegsMap.Values)

            return true;
        }

        /*-----------------------------------------------------------------------------------*/

        //check validity of each field 
        private void CheckResetValues()
        {
            //iterate over every reg on map
            foreach (Reg reg in Program.RegsMap.Values)
            {
                RegFormData regFormData;
                if (!LocateRegInForm(reg.Name, out regFormData))
                {
                    AppendMessagePanel(reg.Name + " was not found for reset value check");
                }

                if (regFormData.regFieldvalue != "") 
                {
                    //reset value wrong
                    if (regFormData.regFieldvalue != reg.ResetValue) 
                    {
                        if (regFormData.isComboBox)
                        {
                            regFormData.regComboBox.ForeColor = Color.Red;
                        }
                        else
                        {
                            regFormData.regTextBox.ForeColor = Color.Red;
                        }
                    }
                    //reset value OK
                    else 
                    {
                        if (regFormData.isComboBox)
                        {
                            regFormData.regComboBox.ForeColor = Color.Black;
                        }
                        else
                        {
                            regFormData.regTextBox.ForeColor = Color.Black;
                        }
                    }
                }
            } // foreach (Reg reg in Program.regsMap.Values)
        }

        /*-----------------------------------------------------------------------------------*/

        private void exportToWordDocumentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ExportToWord())
            {
                MessageBox.Show("Word document :'"+ EXPORTED_WORD_DOC_NAME + "' was created." ,"Export");
            }
        }

        /*-----------------------------------------------------------------------------------*/

        private bool ExportToWord()
        {
            if (!OpenXmlWordWrapper.Instance.CreateWordDoc(EXPORTED_WORD_DOC_NAME))
            {
                MessageBox.Show("Word document is already open", "Error");
                return false;
            }

            //titles
            List<string> titles = new List<string>();

            titles.Add("Register name");
            titles.Add("Name");
            titles.Add("Address");
            titles.Add("Bits");
            titles.Add("Reset Value");
            titles.Add("R/W");

            //iterate over tabs
            foreach (string tabName in Program.TabsSet)
            {
                List<List<string>> tableData = new List<List<string>>();

                foreach (Reg reg in Program.RegsMap.Values)
                {
                    if (reg.TabName == tabName)
                    {
                        List<string> regData = new List<string>();

                        regData.Add(reg.RegName);
                        regData.Add(reg.Name);
                        regData.Add(reg.GetAddressStr());
                        regData.Add(reg.GetBitsRangeStr());
                        regData.Add(reg.ResetValue);
                        regData.Add(reg.GetRWstr());

                        tableData.Add(regData);
                    }
                } // foreach (Reg reg in Program.RegsMap.Values)

                OpenXmlWordWrapper.Instance.CreateTable(tabName, titles, tableData);
            } // foreach (string tabName in Program.TabsSet)

            return true;
        }

        /*-----------------------------------------------------------------------------------*/

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReloadDataFromConfig();
 
            ReloadForm();

            AppendMessagePanel("Configurations have been Reloaded from XML file");
        }

        /*-----------------------------------------------------------------------------------*/
        private void ReloadDataFromConfig()
        {
            Program.RegsMap.Clear();
            Program.TabsSet.Clear();

            ConfigFile config = new ConfigFile();            
            config.ConfigGetRegs(Environment.CurrentDirectory + "\\..\\..\\config.xml");
        }

        /*-----------------------------------------------------------------------------------*/
        private void ReloadForm()
        {
            RemoveTabs();
            FormHwConfig_Load(this, null);
        }

        /*-----------------------------------------------------------------------------------*/

        private void RemoveTabs()
        {
            foreach (TabPage tab in this.tabControl1.TabPages)
            {
                if (tab.Name != TERMINAL_TAB_NAME)
                {
                    tabControl1.Controls.Remove(tab);
                }
            }
        }

        /*-----------------------------------------------------------------------------------*/

        private void ToolStripMenuItemExportToFile_Click(object sender, EventArgs e)
        {
            string fileName = GetFileNameFromUser();

            if (ExportRegsValuesToFile(fileName))
            {
                MessageBox.Show("Exported successfully", "Export To File");
            }
            else
            {
                MessageBox.Show("File already open", "Export To File");
            }
        }

        /*-----------------------------------------------------------------------------------*/

        private string GetFileNameFromUser()
        {
            string fileName = Interaction.InputBox("Please Enter file name", "Export To File", "exportFile");

            if (fileName == "")
            {
                return "exportFile.csv"; 
            }

            if (!fileName.Contains(".csv"))
            {
                fileName += ".csv";
            }

            return fileName;
        }

        /*-----------------------------------------------------------------------------------*/

        private bool ExportRegsValuesToFile(string _fileName)
        {
            try
            {
                using (CsvFileWriter writer = new CsvFileWriter(_fileName))
                {
                    foreach (Reg reg in Program.RegsMap.Values)
                    {
                        CsvRow row = new CsvRow();

                        row.Add(reg.Name);
                        row.Add(reg.ValueLastLoaded);

                        writer.WriteRow(row);
                    }
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        /*-----------------------------------------------------------------------------------*/

        private void ToolStripMenuItemImportFromFile_Click(object sender, EventArgs e)
        {
            //get file name from user   
            string fileName;
            if (!GetFileFromUser(out fileName))
            {
                return;
            }

            if (ImportRegsValuesToScreen(fileName))
            {
                MessageBox.Show("Imported successfully", "Import Register Values");
            }
            else
            {
                MessageBox.Show("Errors occurred during import", "Import Register Values");
            }

        }

        /*-----------------------------------------------------------------------------------*/

        private bool ImportRegsValuesToScreen(string _fileName)
        {
            bool isAllGood = true;

            using (CsvFileReader reader = new CsvFileReader(_fileName))
            {
                CsvRow row = new CsvRow();
                while (reader.ReadRow(row))
                {
                    //get reg name and value from file
                    string regName = row.ElementAt(0);
                    string regValue = row.ElementAt(1);
                   
                    //set reg value in its text/combo box
                    if(!SetRegFieldWithValue(regName, regValue))
                    {                     
                        isAllGood = false;     
                    } 
                }
            }

            return isAllGood;
        }

        /*-----------------------------------------------------------------------------------*/

        private struct RegFormData 
        {
            public TabPage regTab;
            public bool isComboBox;
            public TextBox regTextBox;
            public ComboBox regComboBox;
            public string regFieldvalue;
        }

        private bool LocateRegInForm(string _regName, out RegFormData _regFormData)
        {
            _regFormData = new RegFormData();

            Reg reg = Program.RegsMap[_regName];
            if(reg == null)
            {
                 return false;
            }

            TabPage regTabPage = tabControl1.Controls[reg.TabName] as TabPage;
            if (regTabPage == null)
            {
                return false;
            }

            _regFormData.regTab = regTabPage;

            _regFormData.isComboBox = (reg.EValidation == ValidationEnum.LIST_OF_VALUES);

            if (!_regFormData.isComboBox)
            {
                TextBox textBox = regTabPage.Controls[reg.Name] as TextBox;
                if (textBox == null)
                {
                    return false;
                }

                _regFormData.regTextBox = textBox;
                _regFormData.regFieldvalue = textBox.Text;
            }
            else
            {
                ComboBox comboBox = regTabPage.Controls[reg.Name] as ComboBox;
                if (comboBox == null)
                {
                    return false;
                }

                _regFormData.regComboBox = comboBox;
                _regFormData.regFieldvalue = comboBox.Text;
            }
            
            return true;
        }

        /*-----------------------------------------------------------------------------------*/

        private bool SetRegFieldWithValue(string _regName, string _value)
        {
            RegFormData regFormData;

            if (!LocateRegInForm(_regName, out regFormData))
            {
                return false;
            }

            if (regFormData.isComboBox)
            {
                regFormData.regComboBox.Text = _value;
            }
            else
            {
                regFormData.regTextBox.Text = _value;
            }

            return true;
        }

        /*-----------------------------------------------------------------------------------*/

        private bool GetRegFieldValue(string _regName, out string _value)
        {
            _value = "";

            RegFormData regFormData;

            if (!LocateRegInForm(_regName, out regFormData))
            {
                return false;
            }

            _value = regFormData.regFieldvalue;

            return true;
        }

        /*-----------------------------------------------------------------------------------*/

        private bool GetFileFromUser(out string _fileName)
        {
            _fileName = "";
            Stream stream = null;

            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.csv)|*.csv|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            try
            {              
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if ((stream = openFileDialog1.OpenFile())== null)
                    {
                        return false;
                    }
                }

                FileStream fs = stream as FileStream;

                _fileName = fs.Name;
            }
            catch
            {
                return false;
            }

            return true;
        }

        /*-----------------------------------------------------------------------------------*/

    } //class
} //namespace
