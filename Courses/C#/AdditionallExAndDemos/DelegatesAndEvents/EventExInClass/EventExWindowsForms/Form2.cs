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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            
        }

        void _switcher_Switch(object sender, SwitchEventArgs e)
        {
            this.Text = e.NewState.ToString();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Main._switcher.Switch += _switcher_Switch;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Main._switcher.Switch -= _switcher_Switch;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Main._switcher.Switch.nvoke(null, null);
        }

    }
}
