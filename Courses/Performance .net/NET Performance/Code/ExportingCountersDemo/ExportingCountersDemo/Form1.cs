using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace ExportingCountersDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _counter.Increment();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _counter.Decrement();
        }

        private PerformanceCounter _counter;

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!PerformanceCounterCategory.Exists("EmployeeEnrollment"))
            {
                CounterCreationDataCollection ccdc = new CounterCreationDataCollection();
                CounterCreationData ccd = new CounterCreationData(
                    "# employees checked in", "The number of employees who have come through the door and didn't leave yet.",
                    PerformanceCounterType.NumberOfItems32);
                ccdc.Add(ccd);
                PerformanceCounterCategory.Create("EmployeeEnrollment",
                    "Information about the employee enrollment in the Gazit classroom.",
                    PerformanceCounterCategoryType.SingleInstance, ccdc);
            }
            _counter = new PerformanceCounter(
                "EmployeeEnrollment", "# employees checked in", readOnly: false);
            _counter.RawValue = 0;
        }
    }
}
