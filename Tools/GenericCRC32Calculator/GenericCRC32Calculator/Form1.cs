using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CalculateAls1Crc;

namespace CalculateAls1Crc
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        /*----------------------------------------------------------------------------------------------------*/

        private void buttonCalc_Click(object sender, EventArgs e)
        {
            String hextoCalcStr = this.HexToCalcTextBox.Text;
            UInt32 toCalc = 0;
            Byte[] toCalcBytesArr;
            int numOfBytesToCalculate = 0;
           
            try
            {
                //calculate num of bytes
                numOfBytesToCalculate = hextoCalcStr.Length / Common.HEXA_DIGITS_IN_BYTE + hextoCalcStr.Length % 2;
                //convert to bytes array for CRC32 calculation
                toCalcBytesArr = Common.StringToByteArray(hextoCalcStr);

                //reflect bytes before calculation
                if (checkBoxReflect.Checked)
                {
                    toCalcBytesArr = Common.SwapEndians(toCalcBytesArr);
                }

                //Calculate CRC32
                UInt32 crcCalculated = Common.CalculateCRC(toCalcBytesArr, numOfBytesToCalculate);

                //Display result to screen
                this.DecResultTextBox.Text = crcCalculated.ToString();
                this.HexResultTextBox.Text = "0x" + String.Format("{0:X}", crcCalculated);
            }
            catch
            {
                MessageBox.Show("Input Error");
                return;
            }       
                  
        }

        /*----------------------------------------------------------------------------------------------------*/

        private void DecToHexTextBox_TextChanged(object sender, EventArgs e)
        {
            string hexToScreen = "";
            Common.IntStringToHexString(DecToHexTextBox.Text, out hexToScreen);

            HexFromDecTextBox.Text = hexToScreen;
        }

        /*----------------------------------------------------------------------------------------------------*/
  

   
    }
}
