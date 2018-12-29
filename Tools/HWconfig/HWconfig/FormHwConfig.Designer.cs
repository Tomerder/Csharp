using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;

namespace HWconfig
{
    partial class FormHwConfig
    {
        public const string TERMINAL_TAB_NAME = "tabPageTerminal";
        /*public const string TERMINAL_TAB_TEXT = "Terminal";
        public const string SEARCH_LABEL_TEXT = "Search Field : ";
        public const string SEND_CMD_DELAY_LABEL_TEXT = "Transmition Delay : ";
        public const string LOAD_REGS_VALUES_BUTTON_TEXT = "Read Registers Values";
        public const string MENU_STRIP_TEXT = "Configuration File";
        public const string MENU_STRIP_ITEM1_TEXT = "Reload";
        public const string MENU_STRIP_ITEM2_TEXT = "Export To Word Document";
        public const string UPDATE_BUTTON_TEXT = "Update";*/

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /*---------------------------------------------------------------------------------------------*/
        //Init - generated code 

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageTerminal = new System.Windows.Forms.TabPage();
            this.labelSendCmdDelay = new System.Windows.Forms.Label();
            this.textBoxTerminal = new System.Windows.Forms.RichTextBox();
            this.textBoxSendCmdDelay = new System.Windows.Forms.TextBox();
            this.searchLabel = new System.Windows.Forms.Label();
            this.searchFieldTextBox = new System.Windows.Forms.TextBox();
            this.buttonReadRegs = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToWordDocumentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.valuesFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemExportToFile = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemImportFromFile = new System.Windows.Forms.ToolStripMenuItem();
            this.textBoxMessagesPanel = new System.Windows.Forms.RichTextBox();
            this.tabControl1.SuspendLayout();
            this.tabPageTerminal.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Location = new System.Drawing.Point(550, 650);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(138, 23);
            this.buttonUpdate.TabIndex = 0;
            this.buttonUpdate.Text = "Update";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageTerminal);
            this.tabControl1.Location = new System.Drawing.Point(40, 81);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 10000;
            this.tabControl1.Size = new System.Drawing.Size(956, 476);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPageTerminal
            // 
            this.tabPageTerminal.BackColor = System.Drawing.Color.LightSteelBlue;
            this.tabPageTerminal.Controls.Add(this.labelSendCmdDelay);
            this.tabPageTerminal.Controls.Add(this.textBoxTerminal);
            this.tabPageTerminal.Controls.Add(this.textBoxSendCmdDelay);
            this.tabPageTerminal.Location = new System.Drawing.Point(4, 22);
            this.tabPageTerminal.Name = "tabPageTerminal";
            this.tabPageTerminal.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTerminal.Size = new System.Drawing.Size(948, 450);
            this.tabPageTerminal.TabIndex = 0;
            this.tabPageTerminal.Text = "Terminal";
            // 
            // labelSendCmdDelay
            // 
            this.labelSendCmdDelay.AutoSize = true;
            this.labelSendCmdDelay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelSendCmdDelay.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelSendCmdDelay.Location = new System.Drawing.Point(16, 414);
            this.labelSendCmdDelay.Name = "labelSendCmdDelay";
            this.labelSendCmdDelay.Size = new System.Drawing.Size(149, 22);
            this.labelSendCmdDelay.TabIndex = 9;
            this.labelSendCmdDelay.Text = "Transmition Delay : ";
            // 
            // textBoxTerminal
            // 
            this.textBoxTerminal.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBoxTerminal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxTerminal.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.textBoxTerminal.Location = new System.Drawing.Point(16, 20);
            this.textBoxTerminal.Name = "textBoxTerminal";
            this.textBoxTerminal.Size = new System.Drawing.Size(914, 382);
            this.textBoxTerminal.TabIndex = 1;
            this.textBoxTerminal.Text = "";
            this.textBoxTerminal.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxTerminal_KeyDown);
            // 
            // textBoxSendCmdDelay
            // 
            this.textBoxSendCmdDelay.Location = new System.Drawing.Point(171, 416);
            this.textBoxSendCmdDelay.Name = "textBoxSendCmdDelay";
            this.textBoxSendCmdDelay.Size = new System.Drawing.Size(76, 20);
            this.textBoxSendCmdDelay.TabIndex = 7;
            // 
            // searchLabel
            // 
            this.searchLabel.AutoSize = true;
            this.searchLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.searchLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.searchLabel.Location = new System.Drawing.Point(40, 41);
            this.searchLabel.Name = "searchLabel";
            this.searchLabel.Size = new System.Drawing.Size(112, 22);
            this.searchLabel.TabIndex = 2;
            this.searchLabel.Text = "Search Field : ";
            // 
            // searchFieldTextBox
            // 
            this.searchFieldTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.searchFieldTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.searchFieldTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.searchFieldTextBox.Location = new System.Drawing.Point(169, 39);
            this.searchFieldTextBox.Name = "searchFieldTextBox";
            this.searchFieldTextBox.Size = new System.Drawing.Size(234, 24);
            this.searchFieldTextBox.TabIndex = 3;
            this.searchFieldTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchFieldTextBox_KeyDown);
            // 
            // buttonReadRegs
            // 
            this.buttonReadRegs.Location = new System.Drawing.Point(382, 650);
            this.buttonReadRegs.Name = "buttonReadRegs";
            this.buttonReadRegs.Size = new System.Drawing.Size(138, 23);
            this.buttonReadRegs.TabIndex = 8;
            this.buttonReadRegs.Text = "Read Registers Values";
            this.buttonReadRegs.UseVisualStyleBackColor = true;
            this.buttonReadRegs.Click += new System.EventHandler(this.buttonReadRegs_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.valuesFileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1032, 24);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reloadToolStripMenuItem,
            this.exportToWordDocumentToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(93, 20);
            this.toolStripMenuItem1.Text = "Configuration";
            // 
            // reloadToolStripMenuItem
            // 
            this.reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
            this.reloadToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.reloadToolStripMenuItem.Text = "Reload From File";
            this.reloadToolStripMenuItem.Click += new System.EventHandler(this.reloadToolStripMenuItem_Click);
            // 
            // exportToWordDocumentToolStripMenuItem
            // 
            this.exportToWordDocumentToolStripMenuItem.Name = "exportToWordDocumentToolStripMenuItem";
            this.exportToWordDocumentToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.exportToWordDocumentToolStripMenuItem.Text = "Export To Word Document";
            this.exportToWordDocumentToolStripMenuItem.Click += new System.EventHandler(this.exportToWordDocumentToolStripMenuItem_Click);
            // 
            // valuesFileToolStripMenuItem
            // 
            this.valuesFileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemExportToFile,
            this.ToolStripMenuItemImportFromFile});
            this.valuesFileToolStripMenuItem.Name = "valuesFileToolStripMenuItem";
            this.valuesFileToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.valuesFileToolStripMenuItem.Text = "Values";
            // 
            // ToolStripMenuItemExportToFile
            // 
            this.ToolStripMenuItemExportToFile.Name = "ToolStripMenuItemExportToFile";
            this.ToolStripMenuItemExportToFile.Size = new System.Drawing.Size(174, 22);
            this.ToolStripMenuItemExportToFile.Text = "Export To File...";
            this.ToolStripMenuItemExportToFile.Click += new System.EventHandler(this.ToolStripMenuItemExportToFile_Click);
            // 
            // ToolStripMenuItemImportFromFile
            // 
            this.ToolStripMenuItemImportFromFile.Name = "ToolStripMenuItemImportFromFile";
            this.ToolStripMenuItemImportFromFile.Size = new System.Drawing.Size(174, 22);
            this.ToolStripMenuItemImportFromFile.Text = "Import To Screen...";
            this.ToolStripMenuItemImportFromFile.Click += new System.EventHandler(this.ToolStripMenuItemImportFromFile_Click);
            // 
            // textBoxMessagesPanel
            // 
            this.textBoxMessagesPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.textBoxMessagesPanel.Location = new System.Drawing.Point(40, 563);
            this.textBoxMessagesPanel.Name = "textBoxMessagesPanel";
            this.textBoxMessagesPanel.ReadOnly = true;
            this.textBoxMessagesPanel.Size = new System.Drawing.Size(956, 76);
            this.textBoxMessagesPanel.TabIndex = 12;
            this.textBoxMessagesPanel.Text = "";
            // 
            // FormHwConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(1032, 699);
            this.Controls.Add(this.textBoxMessagesPanel);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.buttonReadRegs);
            this.Controls.Add(this.searchFieldTextBox);
            this.Controls.Add(this.searchLabel);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.buttonUpdate);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormHwConfig";
            this.Text = "Hardware Configurations";
            this.Load += new System.EventHandler(this.FormHwConfig_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPageTerminal.ResumeLayout(false);
            this.tabPageTerminal.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        /*---------------------------------------------------------------------------------------------*/

        //consts
        private const int NUM_OF_FIELDS = 2;
        
        private const int LABELS_X = 30;
        private const int LABELS_Y_START = 20;
        private const int LABELS_Y_INC = 40;

        private const int TEXTBOXS_X = LABELS_X + 250;
        private const int TEXTBOXS_Y_START = LABELS_Y_START;
        private const int TEXTBOXS_Y_INC = LABELS_Y_INC;
        private const int TEXTBOXS_WIDTH = 100;
        private const int TEXTBOXS_HEIGHT = 50;

        private const int LABEL_RO_X = TEXTBOXS_X + TEXTBOXS_WIDTH + 10;

        private const int LABEL_BITS_X = LABEL_RO_X + 35;

        /*---------------------------------------------------------------------------------------------*/

        //init apart from generated code ( InitializeComponent() ) 
        private void InitForm()
        {
            this.searchFieldTextBox.AutoCompleteCustomSource = Program.GetRegsName();
        }

        /*---------------------------------------------------------------------------------------------*/

        //dinamically create form tabs
        private void CreateFormTabsFromTabsSet()
        {
            //iterate through tabs set
            foreach (string tabName in Program.TabsSet)
            {
                TabPage tabPage = new TabPage();
                tabPage.AutoScroll = true;
                tabPage.BackColor = System.Drawing.Color.LightSteelBlue;
                tabPage.Location = new System.Drawing.Point(4, 22);
                tabPage.Name = tabName;
                tabPage.Padding = new System.Windows.Forms.Padding(3);
                tabPage.Size = new System.Drawing.Size(948, 354);
                tabPage.Text = tabPage.Name;
                this.tabControl1.Controls.Add(tabPage);
            }
        }

        /*---------------------------------------------------------------------------------------------*/

        //dinamically create form fields
        private void CreateFormFieldsFromRegsMap()
        {
            Dictionary<string, Reg> regsMap = Program.RegsMap;
            int regNum = 0;
            string lastTabName = "";

            //iterate through regs map
            foreach(KeyValuePair<string, Reg> regEntry in regsMap)
            {
                Reg reg = regEntry.Value;

                //update regNum to 0 if new tab
                if (!lastTabName.Equals(reg.TabName))
                {
                    regNum = 0;
                    lastTabName = reg.TabName;
                }

                //get reg tab 
                TabPage regTabPage = tabControl1.Controls[reg.TabName] as TabPage;
                if (regTabPage == null)
                {
                    break;
                }

                //create label
                Label label = new Label();
                label.AutoSize = true;
                label.Location = new System.Drawing.Point(LABELS_X, LABELS_Y_START + LABELS_Y_INC * regNum);
                label.Name = "labelReg" + regNum;
                label.Text = reg.Name;
                label.Font = new Font("Arial", 12);
                label.SuspendLayout();
                regTabPage.Controls.Add(label);
                //this.Controls.Add(label);

                //create textBox / comboBox
                if (reg.EValidation == ValidationEnum.LIST_OF_VALUES)
                {
                    ComboBox comboBox = new ComboBox();
                    comboBox.AutoSize = true;
                    comboBox.Size = new System.Drawing.Size(TEXTBOXS_WIDTH, TEXTBOXS_HEIGHT);
                    comboBox.Location = new System.Drawing.Point(TEXTBOXS_X, TEXTBOXS_Y_START + TEXTBOXS_Y_INC * regNum);          
                    comboBox.Name = reg.Name;
                    comboBox.DropDownStyle = ComboBoxStyle.DropDown;
                    comboBox.AutoCompleteMode = AutoCompleteMode.Suggest;
                    comboBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    comboBox.TabIndex = regNum; 

                    //is read only
                    if (reg.IsReadOnly)
                    {
                        comboBox.Enabled = false;
                    }

                    //add possiable values to combo box
                    foreach(string value in reg.PossiableValues)
                    {
                        comboBox.Items.Add(value);
                    }

                    comboBox.AutoCompleteCustomSource = reg.GetPossiableValues();

                    comboBox.SuspendLayout();
                    regTabPage.Controls.Add(comboBox);      
                }
                else
                {
                    TextBox textBox = new TextBox();
                    textBox.AutoSize = true;
                    textBox.Size = new System.Drawing.Size(TEXTBOXS_WIDTH, TEXTBOXS_HEIGHT);
                    textBox.Location = new System.Drawing.Point(TEXTBOXS_X, TEXTBOXS_Y_START + TEXTBOXS_Y_INC * regNum);                   
                    textBox.Name = reg.Name;
                    textBox.TabIndex = regNum;

                    //is read only
                    if (reg.IsReadOnly)
                    {
                        textBox.ReadOnly = true;
                        textBox.BackColor = Color.LightGray;
                    }
                    
                    textBox.SuspendLayout();
                    regTabPage.Controls.Add(textBox);    
                }     
     
                //create RO label in case of read only reg
                if (reg.IsReadOnly)
                {
                    Label labelRO = new Label();
                    labelRO.AutoSize = true;
                    labelRO.Location = new System.Drawing.Point(LABEL_RO_X, LABELS_Y_START + LABELS_Y_INC * regNum);
                    labelRO.Name = "labelRO" + regNum;
                    labelRO.Text = "RO";
                    labelRO.ForeColor = Color.Red;
                    labelRO.Font = new Font("Arial", 12);
                    labelRO.SuspendLayout();
                    regTabPage.Controls.Add(labelRO);
                }

                //create BITS[from..to] label 
                Label labelBits = new Label();
                labelBits.AutoSize = true;
                labelBits.Location = new System.Drawing.Point(LABEL_BITS_X, LABELS_Y_START + LABELS_Y_INC * regNum);
                labelBits.Name = "labelBits" + regNum;
                labelBits.Text = reg.GetBitsRangeStr();                          
                labelBits.ForeColor = Color.Black;
                labelBits.Font = new Font("Arial", 12);
                labelBits.SuspendLayout();
                regTabPage.Controls.Add(labelBits);
               

                regNum++;
            }
        }

        /*---------------------------------------------------------------------------------------------*/
        private TabControl tabControl1;
        private TabPage tabPageTerminal;
        private Label searchLabel;
        private TextBox searchFieldTextBox;
        private RichTextBox textBoxTerminal;
        private Button buttonUpdate;
        private TextBox textBoxSendCmdDelay;
        private Button buttonReadRegs;
        private Label labelSendCmdDelay;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem reloadToolStripMenuItem;
        private ToolStripMenuItem exportToWordDocumentToolStripMenuItem;
        private RichTextBox textBoxMessagesPanel;
        private ToolStripMenuItem valuesFileToolStripMenuItem;
        private ToolStripMenuItem ToolStripMenuItemExportToFile;
        private ToolStripMenuItem ToolStripMenuItemImportFromFile;
        /*---------------------------------------------------------------------------------------------*/
  
    }
}

