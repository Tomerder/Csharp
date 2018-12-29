namespace CalculateAls1Crc
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
            this.buttonCalc = new System.Windows.Forms.Button();
            this.DecResultTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.HexResultTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBoxReflect = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.HexToCalcTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.HexFromDecTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.DecToHexTextBox = new System.Windows.Forms.TextBox();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape2 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.SuspendLayout();
            // 
            // buttonCalc
            // 
            this.buttonCalc.Location = new System.Drawing.Point(161, 217);
            this.buttonCalc.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCalc.Name = "buttonCalc";
            this.buttonCalc.Size = new System.Drawing.Size(100, 28);
            this.buttonCalc.TabIndex = 1;
            this.buttonCalc.Text = "Calculate";
            this.buttonCalc.UseVisualStyleBackColor = true;
            this.buttonCalc.Click += new System.EventHandler(this.buttonCalc_Click);
            // 
            // DecResultTextBox
            // 
            this.DecResultTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.DecResultTextBox.ForeColor = System.Drawing.Color.Blue;
            this.DecResultTextBox.Location = new System.Drawing.Point(83, 279);
            this.DecResultTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.DecResultTextBox.Name = "DecResultTextBox";
            this.DecResultTextBox.ReadOnly = true;
            this.DecResultTextBox.Size = new System.Drawing.Size(121, 26);
            this.DecResultTextBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 285);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Result : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 129);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "0x";
            // 
            // HexResultTextBox
            // 
            this.HexResultTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.HexResultTextBox.ForeColor = System.Drawing.Color.Blue;
            this.HexResultTextBox.Location = new System.Drawing.Point(228, 279);
            this.HexResultTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.HexResultTextBox.Name = "HexResultTextBox";
            this.HexResultTextBox.ReadOnly = true;
            this.HexResultTextBox.Size = new System.Drawing.Size(126, 26);
            this.HexResultTextBox.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(115, 260);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Decimal";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(275, 260);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = "Hexa";
            // 
            // checkBoxReflect
            // 
            this.checkBoxReflect.AutoSize = true;
            this.checkBoxReflect.Location = new System.Drawing.Point(152, 177);
            this.checkBoxReflect.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxReflect.Name = "checkBoxReflect";
            this.checkBoxReflect.Size = new System.Drawing.Size(119, 21);
            this.checkBoxReflect.TabIndex = 10;
            this.checkBoxReflect.Text = "Swap Endians";
            this.checkBoxReflect.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(115, 101);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(224, 17);
            this.label5.TabIndex = 11;
            this.label5.Text = "Hexa Value For calculating CRC32";
            // 
            // HexToCalcTextBox
            // 
            this.HexToCalcTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.HexToCalcTextBox.ForeColor = System.Drawing.Color.Blue;
            this.HexToCalcTextBox.Location = new System.Drawing.Point(32, 122);
            this.HexToCalcTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.HexToCalcTextBox.Name = "HexToCalcTextBox";
            this.HexToCalcTextBox.Size = new System.Drawing.Size(376, 26);
            this.HexToCalcTextBox.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(293, 15);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 17);
            this.label6.TabIndex = 19;
            this.label6.Text = "Hexa";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(133, 15);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 17);
            this.label7.TabIndex = 18;
            this.label7.Text = "Decimal";
            // 
            // HexFromDecTextBox
            // 
            this.HexFromDecTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.HexFromDecTextBox.ForeColor = System.Drawing.Color.Blue;
            this.HexFromDecTextBox.Location = new System.Drawing.Point(246, 34);
            this.HexFromDecTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.HexFromDecTextBox.Name = "HexFromDecTextBox";
            this.HexFromDecTextBox.ReadOnly = true;
            this.HexFromDecTextBox.Size = new System.Drawing.Size(126, 26);
            this.HexFromDecTextBox.TabIndex = 17;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 40);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 17);
            this.label8.TabIndex = 16;
            this.label8.Text = "Dec to Hex : ";
            // 
            // DecToHexTextBox
            // 
            this.DecToHexTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.DecToHexTextBox.ForeColor = System.Drawing.Color.Blue;
            this.DecToHexTextBox.Location = new System.Drawing.Point(101, 34);
            this.DecToHexTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.DecToHexTextBox.Name = "DecToHexTextBox";
            this.DecToHexTextBox.Size = new System.Drawing.Size(121, 26);
            this.DecToHexTextBox.TabIndex = 15;
            this.DecToHexTextBox.TextChanged += new System.EventHandler(this.DecToHexTextBox_TextChanged);
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape2,
            this.lineShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(421, 332);
            this.shapeContainer1.TabIndex = 20;
            this.shapeContainer1.TabStop = false;
            // 
            // lineShape2
            // 
            this.lineShape2.BorderWidth = 2;
            this.lineShape2.Name = "lineShape2";
            this.lineShape2.X1 = 4;
            this.lineShape2.X2 = 417;
            this.lineShape2.Y1 = 163;
            this.lineShape2.Y2 = 163;
            // 
            // lineShape1
            // 
            this.lineShape1.BorderWidth = 2;
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.X1 = 4;
            this.lineShape1.X2 = 417;
            this.lineShape1.Y1 = 79;
            this.lineShape1.Y2 = 79;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 332);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.HexFromDecTextBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.DecToHexTextBox);
            this.Controls.Add(this.HexToCalcTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.checkBoxReflect);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.HexResultTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DecResultTextBox);
            this.Controls.Add(this.buttonCalc);
            this.Controls.Add(this.shapeContainer1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Generic CRC calculator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCalc;
        private System.Windows.Forms.TextBox DecResultTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox HexResultTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBoxReflect;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox HexToCalcTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox HexFromDecTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox DecToHexTextBox;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape2;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
    }
}

