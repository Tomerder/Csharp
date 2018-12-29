namespace HWconfig
{
    partial class FormInitComm
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

        /*----------------------------------------------------------------------------------------------*/
        //Init - generated code 

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBoxConnectionType = new System.Windows.Forms.ComboBox();
            this.labelConnectionType = new System.Windows.Forms.Label();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.labelConnectionAttr1 = new System.Windows.Forms.Label();
            this.labelConnectionAttr3 = new System.Windows.Forms.Label();
            this.labelConnectionAttr2 = new System.Windows.Forms.Label();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.comboBoxConnectionAttr1 = new System.Windows.Forms.ComboBox();
            this.textBoxConnectionAttr2 = new System.Windows.Forms.TextBox();
            this.textBoxConnectionAttr3 = new System.Windows.Forms.TextBox();
            this.textBoxConnectionAttr1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // comboBoxConnectionType
            // 
            this.comboBoxConnectionType.FormattingEnabled = true;
            this.comboBoxConnectionType.Items.AddRange(new object[] {
            "SERIAL PORT",
            "TELNET"});
            this.comboBoxConnectionType.Location = new System.Drawing.Point(192, 37);
            this.comboBoxConnectionType.Name = "comboBoxConnectionType";
            this.comboBoxConnectionType.Size = new System.Drawing.Size(162, 21);
            this.comboBoxConnectionType.TabIndex = 0;
            this.comboBoxConnectionType.SelectedIndexChanged += new System.EventHandler(this.comboBoxConnectionType_SelectedIndexChanged);
            // 
            // labelConnectionType
            // 
            this.labelConnectionType.AutoSize = true;
            this.labelConnectionType.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelConnectionType.Location = new System.Drawing.Point(25, 37);
            this.labelConnectionType.Name = "labelConnectionType";
            this.labelConnectionType.Size = new System.Drawing.Size(128, 18);
            this.labelConnectionType.TabIndex = 1;
            this.labelConnectionType.Text = "Connection Type :";
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(524, 376);
            this.shapeContainer1.TabIndex = 2;
            this.shapeContainer1.TabStop = false;
            // 
            // lineShape1
            // 
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.X1 = 15;
            this.lineShape1.X2 = 474;
            this.lineShape1.Y1 = 94;
            this.lineShape1.Y2 = 94;
            // 
            // labelConnectionAttr1
            // 
            this.labelConnectionAttr1.AutoSize = true;
            this.labelConnectionAttr1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelConnectionAttr1.Location = new System.Drawing.Point(25, 130);
            this.labelConnectionAttr1.Name = "labelConnectionAttr1";
            this.labelConnectionAttr1.Size = new System.Drawing.Size(37, 18);
            this.labelConnectionAttr1.TabIndex = 4;
            this.labelConnectionAttr1.Text = "attr1";
            // 
            // labelConnectionAttr3
            // 
            this.labelConnectionAttr3.AutoSize = true;
            this.labelConnectionAttr3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelConnectionAttr3.Location = new System.Drawing.Point(25, 231);
            this.labelConnectionAttr3.Name = "labelConnectionAttr3";
            this.labelConnectionAttr3.Size = new System.Drawing.Size(37, 18);
            this.labelConnectionAttr3.TabIndex = 5;
            this.labelConnectionAttr3.Text = "attr3";
            // 
            // labelConnectionAttr2
            // 
            this.labelConnectionAttr2.AutoSize = true;
            this.labelConnectionAttr2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelConnectionAttr2.Location = new System.Drawing.Point(25, 179);
            this.labelConnectionAttr2.Name = "labelConnectionAttr2";
            this.labelConnectionAttr2.Size = new System.Drawing.Size(37, 18);
            this.labelConnectionAttr2.TabIndex = 6;
            this.labelConnectionAttr2.Text = "attr2";
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(192, 302);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(104, 23);
            this.buttonConnect.TabIndex = 7;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // comboBoxConnectionAttr1
            // 
            this.comboBoxConnectionAttr1.FormattingEnabled = true;
            this.comboBoxConnectionAttr1.Location = new System.Drawing.Point(192, 127);
            this.comboBoxConnectionAttr1.Name = "comboBoxConnectionAttr1";
            this.comboBoxConnectionAttr1.Size = new System.Drawing.Size(162, 21);
            this.comboBoxConnectionAttr1.TabIndex = 8;
            // 
            // textBoxConnectionAttr2
            // 
            this.textBoxConnectionAttr2.Location = new System.Drawing.Point(192, 176);
            this.textBoxConnectionAttr2.Name = "textBoxConnectionAttr2";
            this.textBoxConnectionAttr2.Size = new System.Drawing.Size(162, 20);
            this.textBoxConnectionAttr2.TabIndex = 11;
            // 
            // textBoxConnectionAttr3
            // 
            this.textBoxConnectionAttr3.Location = new System.Drawing.Point(192, 229);
            this.textBoxConnectionAttr3.Name = "textBoxConnectionAttr3";
            this.textBoxConnectionAttr3.Size = new System.Drawing.Size(162, 20);
            this.textBoxConnectionAttr3.TabIndex = 12;
            // 
            // textBoxConnectionAttr1
            // 
            this.textBoxConnectionAttr1.Location = new System.Drawing.Point(192, 127);
            this.textBoxConnectionAttr1.Name = "textBoxConnectionAttr1";
            this.textBoxConnectionAttr1.Size = new System.Drawing.Size(162, 20);
            this.textBoxConnectionAttr1.TabIndex = 13;
            // 
            // FormInitComm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 376);
            this.Controls.Add(this.textBoxConnectionAttr1);
            this.Controls.Add(this.textBoxConnectionAttr3);
            this.Controls.Add(this.textBoxConnectionAttr2);
            this.Controls.Add(this.comboBoxConnectionAttr1);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.labelConnectionAttr2);
            this.Controls.Add(this.labelConnectionAttr3);
            this.Controls.Add(this.labelConnectionAttr1);
            this.Controls.Add(this.labelConnectionType);
            this.Controls.Add(this.comboBoxConnectionType);
            this.Controls.Add(this.shapeContainer1);
            this.Name = "FormInitComm";
            this.Text = "Set Connection ";
            this.Load += new System.EventHandler(this.FormInitComm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        /*----------------------------------------------------------------------------------------------*/

        private void InitForm()
        {
            this.comboBoxConnectionAttr1.Items.AddRange(SerialComm.GetSerialPorts());
        }

        /*----------------------------------------------------------------------------------------------*/

        private System.Windows.Forms.ComboBox comboBoxConnectionType;
        private System.Windows.Forms.Label labelConnectionType;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
        private System.Windows.Forms.Label labelConnectionAttr1;
        private System.Windows.Forms.Label labelConnectionAttr3;
        private System.Windows.Forms.Label labelConnectionAttr2;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.ComboBox comboBoxConnectionAttr1;
        private System.Windows.Forms.TextBox textBoxConnectionAttr2;
        private System.Windows.Forms.TextBox textBoxConnectionAttr3;
        private System.Windows.Forms.TextBox textBoxConnectionAttr1;
    }
}