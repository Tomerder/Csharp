using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace DeadlockDetector.Client
{
    public partial class DeadlockDetectorForm : Form
    {
        public DeadlockDetectorForm()
        {
            InitializeComponent();
        }

        private void DeadlockDetectorForm_Load(object sender, EventArgs e)
        {
            foreach (Process p in Process.GetProcesses())
            {
                listBox1.Items.Add(p.ProcessName + " " + p.Id);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string proc = listBox1.SelectedItem as string;
            if (proc == null)
                return;

            int procId = int.Parse(proc.Split(' ')[1]);

            textBox1.Text = WctHelper.NativeWctHelper.GetWaitChainRepresentationForProcess(procId);
        }
    }
}
