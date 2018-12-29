using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CalculatorImplementaion;
using System.Reflection;

namespace CalculatorDemo
{
    public partial class CalculatorGUI : Form
    {
        public CalculatorGUI()
        {
            InitializeComponent();

            this.Text = Factory.Instance.CalcType;
        }

         private void btnAdd_Click(object sender, EventArgs e)
        {
            int num1 = int.Parse(txtNum1.Text);
            int num2 = int.Parse(txtNum2.Text);

            txtResult.Text = Factory.Instance.CalculatorDynamic.Add(num1, num2).ToString();
        }

        private void btnSubtract_Click(object sender, EventArgs e)
        {
            int num1 = int.Parse(txtNum1.Text);
            int num2 = int.Parse(txtNum2.Text);

            txtResult.Text = Factory.Instance.CalculatorDynamic.Subtract(num1, num2).ToString();
        }

        private void btnMultiply_Click(object sender, EventArgs e)
        {
            int num1 = int.Parse(txtNum1.Text);
            int num2 = int.Parse(txtNum2.Text);

            txtResult.Text = Factory.Instance.CalculatorDynamic.Multiply(num1, num2).ToString();
        }

        private void btnDivide_Click(object sender, EventArgs e)
        {
            int num1 = int.Parse(txtNum1.Text);
            int num2 = int.Parse(txtNum2.Text);

            txtResult.Text = Factory.Instance.CalculatorDynamic.Divide(num1, num2).ToString();
        }

    }
}
