using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PerformanceHarness;

namespace PerformanceHarnessGui
{
    public partial class RunSetupForm : Form
    {
        public RunSetupForm()
        {
            InitializeComponent();

            compileMode.Items.AddRange(Enum.GetNames(typeof(CompileMode)));
            compileMode.SelectedIndex = 0;
        }

        public int NumberOfInnerIterations
        {
            get { return (int)numIterations.Value; }
        }

        public int NumberOfOuterIterations
        {
            get { return (int)numSamples.Value; }
        }

        public CompileMode CompileMode
        {
            get { return (CompileMode)Enum.Parse(typeof(CompileMode), compileMode.SelectedItem.ToString()); }
        }
    }
}