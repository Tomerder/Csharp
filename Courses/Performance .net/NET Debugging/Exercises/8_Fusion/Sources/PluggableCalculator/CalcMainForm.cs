using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PluggableCalculator
{
    public partial class CalcMainForm : Form
    {
        public CalcMainForm()
        {
            InitializeComponent();
        }

        private void CalcMainForm_Load(object sender, EventArgs e)
        {
            foreach (string op in PluginLoader.LoadPlugins())
                cmbOperation.Items.Add(op);
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            string op = cmbOperation.SelectedItem as string;
            if (op == null)
                return;

            float result = PluginLoader.Calculate(
                op, float.Parse(txtOp1.Text), float.Parse(txtOp2.Text));
            txtResult.Text = result.ToString();
        }
    }
}
