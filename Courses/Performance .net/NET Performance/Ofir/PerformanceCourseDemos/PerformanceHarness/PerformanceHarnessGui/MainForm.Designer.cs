namespace PerformanceHarnessGui
{
    partial class MainForm
    {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TableLayoutPanel mainPanel;
            System.Windows.Forms.ToolStripLabel outputMethodLabel;
            System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
            System.Windows.Forms.ToolStripLabel outputFileLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
            System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
            System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.outputMethodCombo = new System.Windows.Forms.ToolStripComboBox();
            this.fileNameText = new System.Windows.Forms.ToolStripTextBox();
            this.chooseFileNameButton = new System.Windows.Forms.ToolStripButton();
            this.btnRunTests = new System.Windows.Forms.ToolStripButton();
            this.testsProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.selectedTests = new System.Windows.Forms.ListView();
            this.listContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itemRunThis = new System.Windows.Forms.ToolStripMenuItem();
            this.itemRunAll = new System.Windows.Forms.ToolStripMenuItem();
            this.itemDeleteThis = new System.Windows.Forms.ToolStripMenuItem();
            this.itemDeleteAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.startPage = new System.Windows.Forms.TabPage();
            this.addReferenceButton = new System.Windows.Forms.ToolStripButton();
            this.startPageContent = new PerformanceHarnessGui.TabContentControl();
            this.saveTabGroupButton = new System.Windows.Forms.ToolStripButton();
            this.loadTabGroupButton = new System.Windows.Forms.ToolStripButton();
            mainPanel = new System.Windows.Forms.TableLayoutPanel();
            outputMethodLabel = new System.Windows.Forms.ToolStripLabel();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            outputFileLabel = new System.Windows.Forms.ToolStripLabel();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            mainPanel.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.listContextMenu.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.startPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            mainPanel.ColumnCount = 2;
            mainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            mainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            mainPanel.Controls.Add(this.toolStrip, 0, 0);
            mainPanel.Controls.Add(this.selectedTests, 0, 1);
            mainPanel.Controls.Add(this.tabControl, 1, 1);
            mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            mainPanel.Location = new System.Drawing.Point(0, 0);
            mainPanel.Name = "mainPanel";
            mainPanel.RowCount = 2;
            mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.615385F));
            mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 95.38461F));
            mainPanel.Size = new System.Drawing.Size(916, 546);
            mainPanel.TabIndex = 0;
            // 
            // toolStrip
            // 
            mainPanel.SetColumnSpan(this.toolStrip, 2);
            this.toolStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            outputMethodLabel,
            this.outputMethodCombo,
            toolStripSeparator1,
            outputFileLabel,
            this.fileNameText,
            this.chooseFileNameButton,
            toolStripSeparator2,
            this.btnRunTests,
            toolStripSeparator3,
            this.testsProgressBar,
            this.addReferenceButton,
            toolStripSeparator4,
            this.saveTabGroupButton,
            this.loadTabGroupButton});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(916, 25);
            this.toolStrip.TabIndex = 0;
            this.toolStrip.Text = "toolStrip1";
            // 
            // outputMethodLabel
            // 
            outputMethodLabel.Name = "outputMethodLabel";
            outputMethodLabel.Size = new System.Drawing.Size(80, 22);
            outputMethodLabel.Text = "Output method:";
            // 
            // outputMethodCombo
            // 
            this.outputMethodCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.outputMethodCombo.Name = "outputMethodCombo";
            this.outputMethodCombo.Size = new System.Drawing.Size(75, 25);
            this.outputMethodCombo.ToolTipText = "Choose the output method for the test results.";
            this.outputMethodCombo.SelectedIndexChanged += new System.EventHandler(this.outputMethodCombo_SelectedIndexChanged);
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // outputFileLabel
            // 
            outputFileLabel.Name = "outputFileLabel";
            outputFileLabel.Size = new System.Drawing.Size(87, 22);
            outputFileLabel.Text = "Output file name:";
            // 
            // fileNameText
            // 
            this.fileNameText.Name = "fileNameText";
            this.fileNameText.Size = new System.Drawing.Size(150, 25);
            this.fileNameText.Text = "C:\\Temp\\Results.log";
            this.fileNameText.ToolTipText = "Displays the results file name.\r\nHit [RETURN] to view the results.";
            this.fileNameText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.fileNameText_KeyPress);
            // 
            // chooseFileNameButton
            // 
            this.chooseFileNameButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.chooseFileNameButton.Image = ((System.Drawing.Image)(resources.GetObject("chooseFileNameButton.Image")));
            this.chooseFileNameButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.chooseFileNameButton.Name = "chooseFileNameButton";
            this.chooseFileNameButton.Size = new System.Drawing.Size(23, 22);
            this.chooseFileNameButton.Text = "...";
            this.chooseFileNameButton.ToolTipText = "Choose a file name where the results will be saved.";
            this.chooseFileNameButton.Click += new System.EventHandler(this.chooseFileNameButton_Click);
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnRunTests
            // 
            this.btnRunTests.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnRunTests.Image = ((System.Drawing.Image)(resources.GetObject("btnRunTests.Image")));
            this.btnRunTests.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRunTests.Name = "btnRunTests";
            this.btnRunTests.Size = new System.Drawing.Size(60, 22);
            this.btnRunTests.Text = "Run Tests";
            this.btnRunTests.ToolTipText = "Run the selected tests on the left.";
            this.btnRunTests.Click += new System.EventHandler(this.btnRunTests_Click);
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // testsProgressBar
            // 
            this.testsProgressBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.testsProgressBar.Name = "testsProgressBar";
            this.testsProgressBar.Size = new System.Drawing.Size(150, 22);
            // 
            // selectedTests
            // 
            this.selectedTests.ContextMenuStrip = this.listContextMenu;
            this.selectedTests.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectedTests.Location = new System.Drawing.Point(3, 28);
            this.selectedTests.Name = "selectedTests";
            this.selectedTests.Size = new System.Drawing.Size(121, 515);
            this.selectedTests.TabIndex = 1;
            this.selectedTests.UseCompatibleStateImageBehavior = false;
            this.selectedTests.View = System.Windows.Forms.View.List;
            // 
            // listContextMenu
            // 
            this.listContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemRunThis,
            this.itemRunAll,
            this.itemDeleteThis,
            this.itemDeleteAll});
            this.listContextMenu.Name = "listContextMenu";
            this.listContextMenu.Size = new System.Drawing.Size(129, 92);
            // 
            // itemRunThis
            // 
            this.itemRunThis.Name = "itemRunThis";
            this.itemRunThis.Size = new System.Drawing.Size(128, 22);
            this.itemRunThis.Text = "Run This";
            this.itemRunThis.Click += new System.EventHandler(this.itemRunThis_Click);
            // 
            // itemRunAll
            // 
            this.itemRunAll.Name = "itemRunAll";
            this.itemRunAll.Size = new System.Drawing.Size(128, 22);
            this.itemRunAll.Text = "Run All";
            this.itemRunAll.Click += new System.EventHandler(this.btnRunTests_Click);
            // 
            // itemDeleteThis
            // 
            this.itemDeleteThis.Name = "itemDeleteThis";
            this.itemDeleteThis.Size = new System.Drawing.Size(128, 22);
            this.itemDeleteThis.Text = "Delete This";
            this.itemDeleteThis.Click += new System.EventHandler(this.itemDeleteThis_Click);
            // 
            // itemDeleteAll
            // 
            this.itemDeleteAll.Name = "itemDeleteAll";
            this.itemDeleteAll.Size = new System.Drawing.Size(128, 22);
            this.itemDeleteAll.Text = "Delete All";
            this.itemDeleteAll.Click += new System.EventHandler(this.itemDeleteAll_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.startPage);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(130, 28);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(783, 515);
            this.tabControl.TabIndex = 2;
            // 
            // startPage
            // 
            this.startPage.Controls.Add(this.startPageContent);
            this.startPage.Location = new System.Drawing.Point(4, 22);
            this.startPage.Name = "startPage";
            this.startPage.Size = new System.Drawing.Size(775, 489);
            this.startPage.TabIndex = 0;
            this.startPage.Text = "Test Page 0";
            this.startPage.UseVisualStyleBackColor = true;
            // 
            // addReferenceButton
            // 
            this.addReferenceButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.addReferenceButton.Image = ((System.Drawing.Image)(resources.GetObject("addReferenceButton.Image")));
            this.addReferenceButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addReferenceButton.Name = "addReferenceButton";
            this.addReferenceButton.Size = new System.Drawing.Size(83, 22);
            this.addReferenceButton.Text = "Add Reference";
            this.addReferenceButton.ToolTipText = "Add an additional reference.";
            this.addReferenceButton.Click += new System.EventHandler(this.addReferenceButton_Click);
            // 
            // startPageContent
            // 
            this.startPageContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.startPageContent.Location = new System.Drawing.Point(0, 0);
            this.startPageContent.Name = "startPageContent";
            this.startPageContent.RunEnabled = true;
            this.startPageContent.Size = new System.Drawing.Size(775, 489);
            this.startPageContent.TabIndex = 0;
            this.startPageContent.TestName = "Test Page 0";
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // saveTabGroupButton
            // 
            this.saveTabGroupButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.saveTabGroupButton.Image = ((System.Drawing.Image)(resources.GetObject("saveTabGroupButton.Image")));
            this.saveTabGroupButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveTabGroupButton.Name = "saveTabGroupButton";
            this.saveTabGroupButton.Size = new System.Drawing.Size(63, 22);
            this.saveTabGroupButton.Text = "Save Tabs";
            this.saveTabGroupButton.ToolTipText = "Save the tab group for later use.";
            this.saveTabGroupButton.Click += new System.EventHandler(this.saveTabGroupButton_Click);
            // 
            // loadTabGroupButton
            // 
            this.loadTabGroupButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.loadTabGroupButton.Image = ((System.Drawing.Image)(resources.GetObject("loadTabGroupButton.Image")));
            this.loadTabGroupButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.loadTabGroupButton.Name = "loadTabGroupButton";
            this.loadTabGroupButton.Size = new System.Drawing.Size(62, 22);
            this.loadTabGroupButton.Text = "Load Tabs";
            this.loadTabGroupButton.ToolTipText = "Load a previously saved tab group.";
            this.loadTabGroupButton.Click += new System.EventHandler(this.loadTabGroupButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(916, 546);
            this.Controls.Add(mainPanel);
            this.Name = "MainForm";
            this.Text = "Performance Harness";
            mainPanel.ResumeLayout(false);
            mainPanel.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.listContextMenu.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.startPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ListView selectedTests;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.ToolStripComboBox outputMethodCombo;
        private System.Windows.Forms.ToolStripButton chooseFileNameButton;
        private System.Windows.Forms.ToolStripTextBox fileNameText;
        private System.Windows.Forms.TabPage startPage;
        private TabContentControl startPageContent;
        private System.Windows.Forms.ToolStripButton btnRunTests;
        private System.Windows.Forms.ToolStripProgressBar testsProgressBar;
        private System.Windows.Forms.ContextMenuStrip listContextMenu;
        private System.Windows.Forms.ToolStripMenuItem itemRunThis;
        private System.Windows.Forms.ToolStripMenuItem itemRunAll;
        private System.Windows.Forms.ToolStripMenuItem itemDeleteThis;
        private System.Windows.Forms.ToolStripMenuItem itemDeleteAll;
        private System.Windows.Forms.ToolStripButton addReferenceButton;
        private System.Windows.Forms.ToolStripButton saveTabGroupButton;
        private System.Windows.Forms.ToolStripButton loadTabGroupButton;
    }
}

