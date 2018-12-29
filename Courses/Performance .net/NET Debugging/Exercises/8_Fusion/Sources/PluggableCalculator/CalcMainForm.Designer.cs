namespace PluggableCalculator
{
    partial class CalcMainForm
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
            this.cmbOperation = new System.Windows.Forms.ComboBox();
            this.txtOp1 = new System.Windows.Forms.MaskedTextBox();
            this.txtOp2 = new System.Windows.Forms.MaskedTextBox();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.btnGo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmbOperation
            // 
            this.cmbOperation.FormattingEnabled = true;
            this.cmbOperation.Location = new System.Drawing.Point(113, 11);
            this.cmbOperation.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbOperation.Name = "cmbOperation";
            this.cmbOperation.Size = new System.Drawing.Size(180, 33);
            this.cmbOperation.TabIndex = 0;
            // 
            // txtOp1
            // 
            this.txtOp1.Location = new System.Drawing.Point(22, 14);
            this.txtOp1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtOp1.Mask = "00000";
            this.txtOp1.Name = "txtOp1";
            this.txtOp1.Size = new System.Drawing.Size(69, 30);
            this.txtOp1.TabIndex = 3;
            this.txtOp1.ValidatingType = typeof(int);
            // 
            // txtOp2
            // 
            this.txtOp2.Location = new System.Drawing.Point(21, 58);
            this.txtOp2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtOp2.Mask = "00000";
            this.txtOp2.Name = "txtOp2";
            this.txtOp2.Size = new System.Drawing.Size(70, 30);
            this.txtOp2.TabIndex = 4;
            this.txtOp2.ValidatingType = typeof(int);
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(312, 11);
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.Size = new System.Drawing.Size(120, 30);
            this.txtResult.TabIndex = 5;
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(113, 53);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(180, 34);
            this.btnGo.TabIndex = 6;
            this.btnGo.Text = "Calculate";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // CalcMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 99);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.txtOp2);
            this.Controls.Add(this.txtOp1);
            this.Controls.Add(this.cmbOperation);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "CalcMainForm";
            this.Text = "Calculator";
            this.Load += new System.EventHandler(this.CalcMainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbOperation;
        private System.Windows.Forms.MaskedTextBox txtOp1;
        private System.Windows.Forms.MaskedTextBox txtOp2;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Button btnGo;
    }
}

