namespace PerformanceHarnessGui
{
    partial class RunSetupForm
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
            System.Windows.Forms.TableLayoutPanel mainPanel;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label1;
            this.numSamples = new System.Windows.Forms.NumericUpDown();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.numIterations = new System.Windows.Forms.NumericUpDown();
            this.compileMode = new System.Windows.Forms.ComboBox();
            mainPanel = new System.Windows.Forms.TableLayoutPanel();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSamples)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numIterations)).BeginInit();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            mainPanel.ColumnCount = 3;
            mainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.94737F));
            mainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.89474F));
            mainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.89474F));
            mainPanel.Controls.Add(this.numSamples, 1, 1);
            mainPanel.Controls.Add(this.btnCancel, 2, 1);
            mainPanel.Controls.Add(label3, 0, 2);
            mainPanel.Controls.Add(label2, 0, 1);
            mainPanel.Controls.Add(label1, 0, 0);
            mainPanel.Controls.Add(this.btnRun, 2, 0);
            mainPanel.Controls.Add(this.numIterations, 1, 0);
            mainPanel.Controls.Add(this.compileMode, 1, 2);
            mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            mainPanel.Location = new System.Drawing.Point(0, 0);
            mainPanel.Name = "mainPanel";
            mainPanel.RowCount = 3;
            mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            mainPanel.Size = new System.Drawing.Size(380, 81);
            mainPanel.TabIndex = 0;
            // 
            // numSamples
            // 
            this.numSamples.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numSamples.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numSamples.Location = new System.Drawing.Point(189, 32);
            this.numSamples.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numSamples.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSamples.Name = "numSamples";
            this.numSamples.Size = new System.Drawing.Size(100, 20);
            this.numSamples.TabIndex = 6;
            this.numSamples.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.Location = new System.Drawing.Point(295, 32);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(82, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Dock = System.Windows.Forms.DockStyle.Fill;
            label3.Location = new System.Drawing.Point(3, 58);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(180, 27);
            label3.TabIndex = 2;
            label3.Text = "Compile the code as:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = System.Windows.Forms.DockStyle.Fill;
            label2.Location = new System.Drawing.Point(3, 29);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(180, 29);
            label2.TabIndex = 1;
            label2.Text = "Number of samples (number of times the total code mass is run):";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = System.Windows.Forms.DockStyle.Fill;
            label1.Location = new System.Drawing.Point(3, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(180, 29);
            label1.TabIndex = 0;
            label1.Text = "Number of iterations in the measurable code:";
            // 
            // btnRun
            // 
            this.btnRun.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnRun.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRun.Location = new System.Drawing.Point(295, 3);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(82, 23);
            this.btnRun.TabIndex = 3;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            // 
            // numIterations
            // 
            this.numIterations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numIterations.Increment = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numIterations.Location = new System.Drawing.Point(189, 3);
            this.numIterations.Maximum = new decimal(new int[] {
            -727379968,
            232,
            0,
            0});
            this.numIterations.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numIterations.Name = "numIterations";
            this.numIterations.Size = new System.Drawing.Size(100, 20);
            this.numIterations.TabIndex = 5;
            this.numIterations.Value = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            // 
            // compileMode
            // 
            this.compileMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.compileMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.compileMode.FormattingEnabled = true;
            this.compileMode.Location = new System.Drawing.Point(189, 61);
            this.compileMode.Name = "compileMode";
            this.compileMode.Size = new System.Drawing.Size(100, 21);
            this.compileMode.TabIndex = 7;
            // 
            // RunSetupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(380, 81);
            this.Controls.Add(mainPanel);
            this.Name = "RunSetupForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Run Setup";
            mainPanel.ResumeLayout(false);
            mainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSamples)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numIterations)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.NumericUpDown numIterations;
        private System.Windows.Forms.NumericUpDown numSamples;
        private System.Windows.Forms.ComboBox compileMode;

    }
}