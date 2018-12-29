using FailuresMonitor;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace FaliuresMonitor
{
    partial class FailuresMonitorForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonGetFailuresResults = new System.Windows.Forms.Button();
            this.labelBaseAddress = new System.Windows.Forms.Label();
            this.textBoxBaseAddress = new System.Windows.Forms.TextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonConnection = new System.Windows.Forms.ToolStripButton();
            this.textBoxMessages = new System.Windows.Forms.RichTextBox();
            this.buttonGetFailuresResultsD = new System.Windows.Forms.Button();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel1.Location = new System.Drawing.Point(6, 65);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(771, 570);
            this.panel1.TabIndex = 0;
            // 
            // buttonGetFailuresResults
            // 
            this.buttonGetFailuresResults.Location = new System.Drawing.Point(290, 701);
            this.buttonGetFailuresResults.Name = "buttonGetFailuresResults";
            this.buttonGetFailuresResults.Size = new System.Drawing.Size(151, 23);
            this.buttonGetFailuresResults.TabIndex = 1;
            this.buttonGetFailuresResults.Text = "Get Failures Results";
            this.buttonGetFailuresResults.UseVisualStyleBackColor = true;
            this.buttonGetFailuresResults.Click += new System.EventHandler(this.buttonGetFailuresResults_Click);
            // 
            // labelBaseAddress
            // 
            this.labelBaseAddress.AutoSize = true;
            this.labelBaseAddress.Font = new System.Drawing.Font("Miriam", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelBaseAddress.Location = new System.Drawing.Point(3, 35);
            this.labelBaseAddress.Name = "labelBaseAddress";
            this.labelBaseAddress.Size = new System.Drawing.Size(106, 16);
            this.labelBaseAddress.TabIndex = 2;
            this.labelBaseAddress.Text = "Base Address :";
            // 
            // textBoxBaseAddress
            // 
            this.textBoxBaseAddress.Location = new System.Drawing.Point(128, 33);
            this.textBoxBaseAddress.Name = "textBoxBaseAddress";
            this.textBoxBaseAddress.Size = new System.Drawing.Size(100, 20);
            this.textBoxBaseAddress.TabIndex = 3;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonConnection});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(799, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonConnection
            // 
            this.toolStripButtonConnection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonConnection.Image = global::FaliuresMonitor.Properties.Resources.disconnect;
            this.toolStripButtonConnection.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonConnection.Name = "toolStripButtonConnection";
            this.toolStripButtonConnection.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonConnection.Text = "Connection";
            this.toolStripButtonConnection.ToolTipText = "Disconnect";
            this.toolStripButtonConnection.Click += new System.EventHandler(this.toolStripButtonConnection_Click);
            // 
            // textBoxMessages
            // 
            this.textBoxMessages.Location = new System.Drawing.Point(6, 641);
            this.textBoxMessages.Name = "textBoxMessages";
            this.textBoxMessages.ReadOnly = true;
            this.textBoxMessages.Size = new System.Drawing.Size(771, 56);
            this.textBoxMessages.TabIndex = 5;
            this.textBoxMessages.Text = "";
            // 
            // buttonGetFailuresResultsD
            // 
            this.buttonGetFailuresResultsD.Location = new System.Drawing.Point(516, 703);
            this.buttonGetFailuresResultsD.Name = "buttonGetFailuresResultsD";
            this.buttonGetFailuresResultsD.Size = new System.Drawing.Size(151, 23);
            this.buttonGetFailuresResultsD.TabIndex = 6;
            this.buttonGetFailuresResultsD.Text = "Get Failures Results D";
            this.buttonGetFailuresResultsD.UseVisualStyleBackColor = true;
            this.buttonGetFailuresResultsD.Click += new System.EventHandler(this.buttonGetFailuresResultsD_Click);
            // 
            // FailuresMonitorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(799, 736);
            this.Controls.Add(this.buttonGetFailuresResultsD);
            this.Controls.Add(this.textBoxMessages);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.textBoxBaseAddress);
            this.Controls.Add(this.labelBaseAddress);
            this.Controls.Add(this.buttonGetFailuresResults);
            this.Controls.Add(this.panel1);
            this.Name = "FailuresMonitorForm";
            this.Text = "Failures Monitor";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    
        //------------------------------------------------------------------------------------------------

        private const int LABELS_X = 30;
        private const int LABELS_Y_START = 10;
        private const int LABELS_Y_INC = 20;

        private const int CHECKBOX_X = LABELS_X + 450;
        private const int CHECKBOX_Y_START = LABELS_Y_START - 5;

        //dynamically create form according to failures
        private void CreateFormFieldsFromRegsMap()
        {
            Dictionary<int, Failure> failuresMap = Program.FailuresMap;

            int failureNum = 0;

            //iterate through regs map
            foreach (KeyValuePair<int, Failure> regEntry in failuresMap)
            {
                //create label
                Label label = new Label();
                label.AutoSize = true;
                label.Location = new System.Drawing.Point(LABELS_X, LABELS_Y_START + LABELS_Y_INC * failureNum);
                label.Name = "labelFailure" + failureNum;
                label.Text = failureNum + " : " + failuresMap[failureNum].FailureName;
                label.Font = new Font("Arial", 9);
                label.SuspendLayout();
                panel1.Controls.Add(label);

                //create checkbox
                /*CheckBox checkBox = new CheckBox();
                checkBox.Location = new System.Drawing.Point(CHECKBOX_X, CHECKBOX_Y_START + LABELS_Y_INC * failureNum);
                checkBox.Name = "checkBoxFailure" + failureNum;
                checkBox.SuspendLayout();
                panel1.Controls.Add(checkBox);*/

                failureNum++;
            }
        }

        //------------------------------------------------------------------------------------------------

        private Panel panel1;
        private Button buttonGetFailuresResults;
        private Label labelBaseAddress;
        private TextBox textBoxBaseAddress;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripButtonConnection;
        private RichTextBox textBoxMessages;
        private Button buttonGetFailuresResultsD;

        //------------------------------------------------------------------------------------------------

    }//partial class FailuresMonitorForm


}//namespace FaliuresMonitor

