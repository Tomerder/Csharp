using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PerformanceHarnessGui
{
    public partial class InputBoxForm : Form
    {
        public InputBoxForm()
        {
            InitializeComponent();
        }

        public static string GetUserInput(string question, string defaultValue)
        {
            InputBoxForm inputBox = new InputBoxForm();
            inputBox.Text = question;
            inputBox.inputTextBox.Text = defaultValue;
            inputBox.ShowDialog();
            return inputBox.inputTextBox.Text;
        }

        private void inputTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                DialogResult = DialogResult.OK;
        }
    }
}