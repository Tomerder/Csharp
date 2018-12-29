namespace CAT_TestTool
{
    partial class Form1
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
            this.receivedFromIpPortTextBox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.messageTextBox = new System.Windows.Forms.RichTextBox();
            this.alignParamsTextBox = new System.Windows.Forms.RichTextBox();
            this.apmParamsTextBox = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.PortListenTextBox = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.enableListenCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // receivedFromIpPortTextBox
            // 
            this.receivedFromIpPortTextBox.Location = new System.Drawing.Point(190, 12);
            this.receivedFromIpPortTextBox.Name = "receivedFromIpPortTextBox";
            this.receivedFromIpPortTextBox.Size = new System.Drawing.Size(116, 30);
            this.receivedFromIpPortTextBox.TabIndex = 0;
            this.receivedFromIpPortTextBox.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Received from IP/Port : ";
            // 
            // messageTextBox
            // 
            this.messageTextBox.Location = new System.Drawing.Point(30, 92);
            this.messageTextBox.Name = "messageTextBox";
            this.messageTextBox.Size = new System.Drawing.Size(1067, 103);
            this.messageTextBox.TabIndex = 2;
            this.messageTextBox.Text = "";
            // 
            // alignParamsTextBox
            // 
            this.alignParamsTextBox.Location = new System.Drawing.Point(236, 221);
            this.alignParamsTextBox.Name = "alignParamsTextBox";
            this.alignParamsTextBox.Size = new System.Drawing.Size(349, 30);
            this.alignParamsTextBox.TabIndex = 3;
            this.alignParamsTextBox.Text = "";
            // 
            // apmParamsTextBox
            // 
            this.apmParamsTextBox.Location = new System.Drawing.Point(236, 271);
            this.apmParamsTextBox.Name = "apmParamsTextBox";
            this.apmParamsTextBox.Size = new System.Drawing.Size(349, 30);
            this.apmParamsTextBox.TabIndex = 4;
            this.apmParamsTextBox.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(437, 326);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 17);
            this.label2.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 224);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(155, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Lateral/vertical/roll/crc :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 274);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(188, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = "APM Lateral/vertical/roll/crc :";
            // 
            // PortListenTextBox
            // 
            this.PortListenTextBox.Enabled = false;
            this.PortListenTextBox.Location = new System.Drawing.Point(881, 12);
            this.PortListenTextBox.Name = "PortListenTextBox";
            this.PortListenTextBox.ReadOnly = true;
            this.PortListenTextBox.Size = new System.Drawing.Size(68, 28);
            this.PortListenTextBox.TabIndex = 10;
            this.PortListenTextBox.Text = "";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(765, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 17);
            this.label5.TabIndex = 11;
            this.label5.Text = "Listen to Port # :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(29, 72);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(140, 17);
            this.label6.TabIndex = 12;
            this.label6.Text = "Message Received : ";
            // 
            // enableListenCheckBox
            // 
            this.enableListenCheckBox.AutoSize = true;
            this.enableListenCheckBox.Location = new System.Drawing.Point(977, 14);
            this.enableListenCheckBox.Name = "enableListenCheckBox";
            this.enableListenCheckBox.Size = new System.Drawing.Size(135, 21);
            this.enableListenCheckBox.TabIndex = 14;
            this.enableListenCheckBox.Text = "Enable Listening";
            this.enableListenCheckBox.UseVisualStyleBackColor = true;
            this.enableListenCheckBox.CheckedChanged += new System.EventHandler(this.enableListenCheckBox_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1119, 503);
            this.Controls.Add(this.enableListenCheckBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.PortListenTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.apmParamsTextBox);
            this.Controls.Add(this.alignParamsTextBox);
            this.Controls.Add(this.messageTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.receivedFromIpPortTextBox);
            this.Name = "Form1";
            this.Text = "CAT Test Tool";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox receivedFromIpPortTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox messageTextBox;
        private System.Windows.Forms.RichTextBox alignParamsTextBox;
        private System.Windows.Forms.RichTextBox apmParamsTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox PortListenTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox enableListenCheckBox;
    }
}

