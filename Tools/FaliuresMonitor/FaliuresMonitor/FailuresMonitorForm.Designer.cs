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
            this.checkBoxReceiveIndication = new System.Windows.Forms.CheckBox();
            this.labelIsHudFail = new System.Windows.Forms.Label();
            this.labelIsSvsFail = new System.Windows.Forms.Label();
            this.labelIsEvsFail = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
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
            this.buttonGetFailuresResults.Enabled = false;
            this.buttonGetFailuresResults.Location = new System.Drawing.Point(540, 2);
            this.buttonGetFailuresResults.Name = "buttonGetFailuresResults";
            this.buttonGetFailuresResults.Size = new System.Drawing.Size(215, 23);
            this.buttonGetFailuresResults.TabIndex = 1;
            this.buttonGetFailuresResults.Text = "Get Failures Results - Multiple Commands";
            this.buttonGetFailuresResults.UseVisualStyleBackColor = true;
            this.buttonGetFailuresResults.Visible = false;
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
            this.textBoxMessages.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxMessages.Location = new System.Drawing.Point(6, 641);
            this.textBoxMessages.Name = "textBoxMessages";
            this.textBoxMessages.ReadOnly = true;
            this.textBoxMessages.Size = new System.Drawing.Size(771, 172);
            this.textBoxMessages.TabIndex = 5;
            this.textBoxMessages.Text = "";
            // 
            // buttonGetFailuresResultsD
            // 
            this.buttonGetFailuresResultsD.Enabled = false;
            this.buttonGetFailuresResultsD.Location = new System.Drawing.Point(490, 2);
            this.buttonGetFailuresResultsD.Name = "buttonGetFailuresResultsD";
            this.buttonGetFailuresResultsD.Size = new System.Drawing.Size(151, 23);
            this.buttonGetFailuresResultsD.TabIndex = 6;
            this.buttonGetFailuresResultsD.Text = "Get Failures Results";
            this.buttonGetFailuresResultsD.UseVisualStyleBackColor = true;
            this.buttonGetFailuresResultsD.Visible = false;
            this.buttonGetFailuresResultsD.Click += new System.EventHandler(this.buttonGetFailuresResultsD_Click);
            // 
            // checkBoxReceiveIndication
            // 
            this.checkBoxReceiveIndication.AutoSize = true;
            this.checkBoxReceiveIndication.Enabled = false;
            this.checkBoxReceiveIndication.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.checkBoxReceiveIndication.Location = new System.Drawing.Point(132, 7);
            this.checkBoxReceiveIndication.Name = "checkBoxReceiveIndication";
            this.checkBoxReceiveIndication.Size = new System.Drawing.Size(15, 14);
            this.checkBoxReceiveIndication.TabIndex = 7;
            this.checkBoxReceiveIndication.UseVisualStyleBackColor = true;
            // 
            // labelIsHudFail
            // 
            this.labelIsHudFail.AutoSize = true;
            this.labelIsHudFail.Location = new System.Drawing.Point(528, 35);
            this.labelIsHudFail.Name = "labelIsHudFail";
            this.labelIsHudFail.Size = new System.Drawing.Size(31, 13);
            this.labelIsHudFail.TabIndex = 8;
            this.labelIsHudFail.Text = "HUD";
            // 
            // labelIsSvsFail
            // 
            this.labelIsSvsFail.AutoSize = true;
            this.labelIsSvsFail.Location = new System.Drawing.Point(597, 35);
            this.labelIsSvsFail.Name = "labelIsSvsFail";
            this.labelIsSvsFail.Size = new System.Drawing.Size(28, 13);
            this.labelIsSvsFail.TabIndex = 9;
            this.labelIsSvsFail.Text = "SVS";
            // 
            // labelIsEvsFail
            // 
            this.labelIsEvsFail.AutoSize = true;
            this.labelIsEvsFail.Location = new System.Drawing.Point(668, 35);
            this.labelIsEvsFail.Name = "labelIsEvsFail";
            this.labelIsEvsFail.Size = new System.Drawing.Size(28, 13);
            this.labelIsEvsFail.TabIndex = 10;
            this.labelIsEvsFail.Text = "EVS";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Connected : ";
            // 
            // FailuresMonitorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(799, 825);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelIsEvsFail);
            this.Controls.Add(this.labelIsSvsFail);
            this.Controls.Add(this.labelIsHudFail);
            this.Controls.Add(this.checkBoxReceiveIndication);
            this.Controls.Add(this.buttonGetFailuresResults);
            this.Controls.Add(this.buttonGetFailuresResultsD);
            this.Controls.Add(this.textBoxMessages);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.textBoxBaseAddress);
            this.Controls.Add(this.labelBaseAddress);
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

        private const int HUD_FAIL_X = LABELS_X + 480;
        private const int SVS_FAIL_X = HUD_FAIL_X + 70;
        private const int EVS_FAIL_X = SVS_FAIL_X + 70;

        private const string HUD_FAIL_TEXT = "HUD_FAIL";
        private const string SVS_FAIL_TEXT = "SVS_FAIL";
        private const string EVS_FAIL_TEXT = "EVS_FAIL";

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

                //create attributes labels - hud fail
                if (failuresMap[failureNum].IsHudFail)
                {
                    Label labelHudFail = new Label();
                    labelHudFail.AutoSize = true;
                    labelHudFail.Location = new System.Drawing.Point(HUD_FAIL_X, LABELS_Y_START + LABELS_Y_INC * failureNum);
                    labelHudFail.Name = "labelFailure" + failureNum + "HudFail";
                    labelHudFail.Text = HUD_FAIL_TEXT;
                    labelHudFail.Font = new Font("Arial", 9);
                    labelHudFail.SuspendLayout();
                    panel1.Controls.Add(labelHudFail);
                }

                //create attributes labels - svs fail
                if (failuresMap[failureNum].IsSvsFail)
                {
                    Label labelSvsFail = new Label();
                    labelSvsFail.AutoSize = true;
                    labelSvsFail.Location = new System.Drawing.Point(SVS_FAIL_X, LABELS_Y_START + LABELS_Y_INC * failureNum);
                    labelSvsFail.Name = "labelFailure" + failureNum + "SvsFail";
                    labelSvsFail.Text = SVS_FAIL_TEXT;
                    labelSvsFail.Font = new Font("Arial", 9);
                    labelSvsFail.SuspendLayout();
                    panel1.Controls.Add(labelSvsFail);
                }

                //create attributes labels - evs fail
                if (failuresMap[failureNum].IsEvsFail)
                {
                    Label labelHudFail = new Label();
                    labelHudFail.AutoSize = true;
                    labelHudFail.Location = new System.Drawing.Point(EVS_FAIL_X, LABELS_Y_START + LABELS_Y_INC * failureNum);
                    labelHudFail.Name = "labelFailure" + failureNum + "EvsFail";
                    labelHudFail.Text = EVS_FAIL_TEXT;
                    labelHudFail.Font = new Font("Arial", 9);
                    labelHudFail.SuspendLayout();
                    panel1.Controls.Add(labelHudFail);
                }
                           
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
        private CheckBox checkBoxReceiveIndication;
        private Label labelIsHudFail;
        private Label labelIsSvsFail;
        private Label labelIsEvsFail;
        private Label label1;

        //------------------------------------------------------------------------------------------------

    }//partial class FailuresMonitorForm


}//namespace FaliuresMonitor

