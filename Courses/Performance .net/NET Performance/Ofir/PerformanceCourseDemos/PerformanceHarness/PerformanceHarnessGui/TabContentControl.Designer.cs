namespace PerformanceHarnessGui
{
    partial class TabContentControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
            System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TabContentControl));
            this.mainPanel = new System.Windows.Forms.TableLayoutPanel();
            this.tabToolStrip = new System.Windows.Forms.ToolStrip();
            this.addTabToRunsButton = new System.Windows.Forms.ToolStripButton();
            this.runThisTabButton = new System.Windows.Forms.ToolStripButton();
            this.renameButton = new System.Windows.Forms.ToolStripButton();
            this.duplicateTabButton = new System.Windows.Forms.ToolStripButton();
            this.deleteButton = new System.Windows.Forms.ToolStripButton();
            this.loadCodeButton = new System.Windows.Forms.ToolStripButton();
            this.saveCodeButton = new System.Windows.Forms.ToolStripButton();
            this.contentTextBox = new System.Windows.Forms.RichTextBox();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mainPanel.SuspendLayout();
            this.tabToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // mainPanel
            // 
            this.mainPanel.ColumnCount = 1;
            this.mainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.mainPanel.Controls.Add(this.tabToolStrip, 0, 0);
            this.mainPanel.Controls.Add(this.contentTextBox, 0, 1);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.RowCount = 2;
            this.mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainPanel.Size = new System.Drawing.Size(615, 421);
            this.mainPanel.TabIndex = 0;
            // 
            // tabToolStrip
            // 
            this.tabToolStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addTabToRunsButton,
            this.runThisTabButton,
            toolStripSeparator1,
            this.renameButton,
            this.duplicateTabButton,
            this.deleteButton,
            toolStripSeparator2,
            this.loadCodeButton,
            this.saveCodeButton});
            this.tabToolStrip.Location = new System.Drawing.Point(0, 0);
            this.tabToolStrip.Name = "tabToolStrip";
            this.tabToolStrip.Size = new System.Drawing.Size(615, 25);
            this.tabToolStrip.TabIndex = 0;
            this.tabToolStrip.Text = "toolStrip1";
            // 
            // addTabToRunsButton
            // 
            this.addTabToRunsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.addTabToRunsButton.Image = ((System.Drawing.Image)(resources.GetObject("addTabToRunsButton.Image")));
            this.addTabToRunsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addTabToRunsButton.Name = "addTabToRunsButton";
            this.addTabToRunsButton.Size = new System.Drawing.Size(71, 22);
            this.addTabToRunsButton.Text = "Add to Tests";
            this.addTabToRunsButton.ToolTipText = "Add this code to the tests list.";
            this.addTabToRunsButton.Click += new System.EventHandler(this.addTabToRunsButton_Click);
            // 
            // runThisTabButton
            // 
            this.runThisTabButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.runThisTabButton.Image = ((System.Drawing.Image)(resources.GetObject("runThisTabButton.Image")));
            this.runThisTabButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.runThisTabButton.Name = "runThisTabButton";
            this.runThisTabButton.Size = new System.Drawing.Size(31, 22);
            this.runThisTabButton.Text = "Run";
            this.runThisTabButton.ToolTipText = "Run just this tab.";
            this.runThisTabButton.Click += new System.EventHandler(this.runThisTabButton_Click);
            // 
            // renameButton
            // 
            this.renameButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.renameButton.Image = ((System.Drawing.Image)(resources.GetObject("renameButton.Image")));
            this.renameButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.renameButton.Name = "renameButton";
            this.renameButton.Size = new System.Drawing.Size(51, 22);
            this.renameButton.Text = "Rename";
            this.renameButton.ToolTipText = "Rename this test tab.";
            this.renameButton.Click += new System.EventHandler(this.renameButton_Click);
            // 
            // duplicateTabButton
            // 
            this.duplicateTabButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.duplicateTabButton.Image = ((System.Drawing.Image)(resources.GetObject("duplicateTabButton.Image")));
            this.duplicateTabButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.duplicateTabButton.Name = "duplicateTabButton";
            this.duplicateTabButton.Size = new System.Drawing.Size(56, 22);
            this.duplicateTabButton.Text = "Duplicate";
            this.duplicateTabButton.ToolTipText = "Duplicate this tab and its contents.";
            this.duplicateTabButton.Click += new System.EventHandler(this.duplicateTabButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.deleteButton.Image = ((System.Drawing.Image)(resources.GetObject("deleteButton.Image")));
            this.deleteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(42, 22);
            this.deleteButton.Text = "Delete";
            this.deleteButton.ToolTipText = "Delete this tab.";
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // loadCodeButton
            // 
            this.loadCodeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.loadCodeButton.Image = ((System.Drawing.Image)(resources.GetObject("loadCodeButton.Image")));
            this.loadCodeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.loadCodeButton.Name = "loadCodeButton";
            this.loadCodeButton.Size = new System.Drawing.Size(63, 22);
            this.loadCodeButton.Text = "Load Code";
            this.loadCodeButton.ToolTipText = "Load the code to execute from a file.";
            this.loadCodeButton.Click += new System.EventHandler(this.loadCodeButton_Click);
            // 
            // saveCodeButton
            // 
            this.saveCodeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.saveCodeButton.Image = ((System.Drawing.Image)(resources.GetObject("saveCodeButton.Image")));
            this.saveCodeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveCodeButton.Name = "saveCodeButton";
            this.saveCodeButton.Size = new System.Drawing.Size(64, 22);
            this.saveCodeButton.Text = "Save Code";
            this.saveCodeButton.ToolTipText = "Save your code to a file.";
            this.saveCodeButton.Click += new System.EventHandler(this.saveCodeButton_Click);
            // 
            // contentTextBox
            // 
            this.contentTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentTextBox.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.contentTextBox.Location = new System.Drawing.Point(10, 35);
            this.contentTextBox.Margin = new System.Windows.Forms.Padding(10);
            this.contentTextBox.Name = "contentTextBox";
            this.contentTextBox.Size = new System.Drawing.Size(595, 376);
            this.contentTextBox.TabIndex = 1;
            this.contentTextBox.Text = "";
            // 
            // TabContentControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainPanel);
            this.Name = "TabContentControl";
            this.Size = new System.Drawing.Size(615, 421);
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.tabToolStrip.ResumeLayout(false);
            this.tabToolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainPanel;
        private System.Windows.Forms.ToolStrip tabToolStrip;
        private System.Windows.Forms.RichTextBox contentTextBox;
        private System.Windows.Forms.ToolStripButton addTabToRunsButton;
        private System.Windows.Forms.ToolStripButton runThisTabButton;
        private System.Windows.Forms.ToolStripButton duplicateTabButton;
        private System.Windows.Forms.ToolStripButton loadCodeButton;
        private System.Windows.Forms.ToolStripButton saveCodeButton;
        private System.Windows.Forms.ToolStripButton renameButton;
        private System.Windows.Forms.ToolStripButton deleteButton;
    }
}
