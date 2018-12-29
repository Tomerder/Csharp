using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace CalculatorImplementaion
{
    public partial class JSHost : Form
    {
        public JSHost()
        {
            InitializeComponent();

            webBrowser1.Url = new Uri(Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory, "HTMLPage1.htm"));
        }
    }
}
