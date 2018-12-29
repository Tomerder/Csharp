using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace HWconfig
{
    public partial class FormInitComm : Form
    {
        public enum CONNECTION_TYPE { SERIAL_PORT , TELNET };
        public enum COM { COM1, COM2, COM3, COM4 };

        /*-----------------------------------------------------------------------------------------------*/

        public FormInitComm()
        {
            //init - generated code
            InitializeComponent();

            //init - manual code
            InitForm();
        }

        /*-----------------------------------------------------------------------------------------------*/

        private void FormInitComm_Load(object sender, EventArgs e)
        {
            comboBoxConnectionType.SelectedIndex = (int)CONNECTION_TYPE.SERIAL_PORT;
            comboBoxConnectionAttr1.SelectedIndex = (int)COM.COM1;
            BuildScreen(CONNECTION_TYPE.SERIAL_PORT);
        }

        /*-----------------------------------------------------------------------------------------------*/

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if (CheckFieldsValidity())
            {
                //init connection
                if (!InitSerialComm())
                {
                    //MessageBox.Show("Connection was not established", "Error");
                    return;
                }

                //proceed to main form
                Program.isInitCommFormClosed = true;
                this.Close();               
            }
        }

        /*-----------------------------------------------------------------------------------------------*/

        private bool InitSerialComm()
        {
            //serial port
            bool isGood = false;

            try
            {
                Program.Connection = new SerialComm(comboBoxConnectionAttr1.SelectedItem.ToString(), out isGood);
            }
            catch
            {
                MessageBox.Show("Serial port is already in use", "Error");
                isGood = false;
                //Application.Exit();
            }

            Thread.Sleep(100);

            return isGood;
        }


        private void comboBoxConnectionType_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            CONNECTION_TYPE connectionType = (CONNECTION_TYPE)comboBoxConnectionType.SelectedIndex;
            BuildScreen(connectionType);
        }

        /*-----------------------------------------------------------------------------------------------*/

        private void BuildScreen(CONNECTION_TYPE _connectionType)
        {
            switch (_connectionType)  
            {
                case CONNECTION_TYPE.SERIAL_PORT:
                    labelConnectionAttr1.Text = "COM :";
                    labelConnectionAttr2.Visible = false;
                    labelConnectionAttr3.Visible = false;
                    textBoxConnectionAttr1.Visible = false;
                    comboBoxConnectionAttr1.Visible = true;
                    textBoxConnectionAttr2.Visible = false;
                    textBoxConnectionAttr3.Visible = false;
                    break;

                case CONNECTION_TYPE.TELNET:
                    labelConnectionAttr1.Text = "IP :";
                    labelConnectionAttr2.Visible = true;
                    labelConnectionAttr3.Visible = true;
                    textBoxConnectionAttr1.Visible = true;
                    comboBoxConnectionAttr1.Visible = false;
                    textBoxConnectionAttr2.Visible = true;
                    textBoxConnectionAttr3.Visible = true;
                    labelConnectionAttr2.Text = "Username :";
                    labelConnectionAttr3.Text = "Password :";
                    break;

                default:
                    break;
                 
            
            }
        }

        /*-----------------------------------------------------------------------------------------------*/

        private bool CheckFieldsValidity()
        {
            return true;
        }


    }
}
