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
    public partial class Main : Form
    {
        internal static Switcher _switcher = new Switcher();

        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            frm.MdiParent = this;
            frm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            frm.MdiParent = this;
            frm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // old way
            for (int i = 0; i < this.MdiChildren.Length; i++)
            {
                Form1 frm = MdiChildren[i] as Form1;
                if (frm != null)
                {
                    //frm.PublicMethod();
                }
                else
                {
                    Form2 frm2 = MdiChildren[i] as Form2;
                    if (frm2 != null)
                    {
                        //frm2.PublicMethod();
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _switcher.DoSwitch();
        }
    }
}
