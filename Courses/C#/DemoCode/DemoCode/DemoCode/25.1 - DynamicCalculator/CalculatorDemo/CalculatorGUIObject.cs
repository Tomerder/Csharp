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
    public partial class CalculatorGUIObject : Form
    {
        public CalculatorGUIObject()
        {
            InitializeComponent();

            this.Text = Factory.Instance.CalcType;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Type calcType = Factory.Instance.CalculatorObject.GetType();

            int num1 = int.Parse(txtNum1.Text);
            int num2 = int.Parse(txtNum2.Text);

            int result = Convert.ToInt32(calcType.InvokeMember("Add",
                                        BindingFlags.InvokeMethod, null,
                                        Factory.Instance.CalculatorObject,
                                        new object[] { num1, num2 }));

            txtResult.Text = result.ToString();
        }

        private void btnSubtract_Click(object sender, EventArgs e)
        {
            Type calcType = Factory.Instance.CalculatorObject.GetType();

            int num1 = int.Parse(txtNum1.Text);
            int num2 = int.Parse(txtNum2.Text);

            int result = Convert.ToInt32(calcType.InvokeMember("Subtract",
                                        BindingFlags.InvokeMethod, null,
                                        Factory.Instance.CalculatorObject,
                                        new object[] { num1, num2 }));

            txtResult.Text = result.ToString();
        }

        private void btnMultiply_Click(object sender, EventArgs e)
        {
            Type calcType = Factory.Instance.CalculatorObject.GetType();

            int num1 = int.Parse(txtNum1.Text);
            int num2 = int.Parse(txtNum2.Text);

            int result = Convert.ToInt32(calcType.InvokeMember("Multiply",
                                        BindingFlags.InvokeMethod, null,
                                        Factory.Instance.CalculatorObject,
                                        new object[] { num1, num2 }));

            txtResult.Text = result.ToString();
        }

        private void btnDivide_Click(object sender, EventArgs e)
        {
            Type calcType = Factory.Instance.CalculatorObject.GetType();

            int num1 = int.Parse(txtNum1.Text);
            int num2 = int.Parse(txtNum2.Text);

            int result = Convert.ToInt32(calcType.InvokeMember("Divide",
                                        BindingFlags.InvokeMethod, null,
                                        Factory.Instance.CalculatorObject,
                                        new object[] { num1, num2 }));

            txtResult.Text = result.ToString();
        }

    }
}
