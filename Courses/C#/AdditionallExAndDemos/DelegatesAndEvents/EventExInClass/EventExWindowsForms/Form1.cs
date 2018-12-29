using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public int Counter { get; private set; }
        public Form1()
        {
            InitializeComponent();
            Counter = 0;
            
        }

        private void _switcher_Switch(object sender, SwitchEventArgs e)
        {
            if (e.NewState == SwitchState.Off)
            {
                BackColor = Color.Gray;
            }
            else
            {
                BackColor = Color.FromArgb(255, 255, 255);
            }
        }

        private void _switcher_BeforeSwitch(object sender, SwitchEventArgs e)
        {
            Counter++;

            this.Text = "BeforeSwitchEvent arrived!!!, Counter: " + Counter;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Main._switcher.Switch -= _switcher_Switch;
            Main._switcher.Switch += _switcher_Switch;

            Main._switcher.BeforeSwitch -= _switcher_BeforeSwitch;
            Main._switcher.BeforeSwitch += _switcher_BeforeSwitch;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Main._switcher.Switch -= _switcher_Switch;
            Main._switcher.BeforeSwitch -= _switcher_BeforeSwitch;
        }

        
    }
}
