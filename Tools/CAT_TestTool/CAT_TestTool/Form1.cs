#define PC_SIM

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace CAT_TestTool
{
    public partial class Form1 : Form
    {
        /****************************************************/
        //delegations for updating GUI from seperate thread (listener)

        delegate void UpdateIpPortGuiDelegate(string _ip, string _port);
        delegate void UpdateMessageGuiDelegate(string _message);
        delegate string GetMessageFromGuiDelegate();
        delegate void UpdateAlignParamsGuiDelegate(string _message);
        delegate void UpdateApmParamsGuiDelegate(string _message);

        /****************************************************/

        Thread m_threadListener;
        UdpClient m_receivingUdpClient;
        IPEndPoint m_remoteIpEndPoint;

        bool m_isListenningToPortEnabled;
        bool m_isListenningToPortInitialized;


        /****************************************************/

        public Form1()
        {
            InitializeComponent();

            //default values
            m_isListenningToPortEnabled = true;
            m_isListenningToPortInitialized = false;

            //Display listenning port
            PortListenTextBox.Text = Program.PORT_RECEIVE.ToString();

            //start main flow
            m_threadListener = new Thread(new ThreadStart(MainFlow));
            m_threadListener.IsBackground = true;
            m_threadListener.Start();
        }

        /****************************************************/

        //Run on thread
        private void MainFlow()
        {                     
            while (true)
            {             
                try
                {
                    Byte[] receiveBytes = new Byte[1];

                    //listen to port for reading message if listen option is selected
                    if (m_isListenningToPortEnabled)
                    {
                        receiveBytes = ListenToPort(); 
                    }
      
                    //cast the string message from textBox to Byte[]
                    string toParse = GetMessageFromGui();
                    receiveBytes = Utils.StringToByteArray(toParse); 
                                       
                    //Parse message and Update GUI for parsed message
                    UpdateGuiParesedMessage(receiveBytes);
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }


                Thread.Sleep(1000);
            }                                     
        }

        /****************************************************/

        private void UpdateGuiParesedMessage(byte[] _receiveBytes)
        {
            //Parse message recieved to struct
            CAT_TestTool.Types.ExtPcDataOutStruct_EPCManager catMessage;
            catMessage = CAT_TestTool.Types.ExtPcDataOutStruct_EPCManager.Deserialize(_receiveBytes);

            //execute from thread : update GUI field - parameters
            int latAlign = catMessage.iLateralAlignValue;
            int verAlign = catMessage.iVerticalAlignValue;
            int rollAlign = catMessage.iRollAlignValue;
            int crcAlign = catMessage.iCalculatedCrc;
#if PC_SIM
            latAlign = (int)Utils.UTL_SwapBigLittleEndian32((UInt32)catMessage.iLateralAlignValue);
            verAlign = (int)Utils.UTL_SwapBigLittleEndian32((UInt32)catMessage.iVerticalAlignValue);
            rollAlign = (int)Utils.UTL_SwapBigLittleEndian32((UInt32)catMessage.iRollAlignValue);
            crcAlign = (int)Utils.UTL_SwapBigLittleEndian32((UInt32)catMessage.iCalculatedCrc);
#endif
            string alignParams = latAlign.ToString() + " / " +
                                 verAlign.ToString() + " / " +
                                 rollAlign.ToString() + " / " +
                                 crcAlign.ToString();
            UpdateAlignParams(alignParams);

            //execute from thread : update GUI field - parameters
            int latApm = catMessage.iApmLateralAlign;
            int verApm = catMessage.iApmVerticallAlign;
            int rollApm = catMessage.iApmRollAlign;
            int crcApm = catMessage.iApmAlignCrc;
#if PC_SIM
            latApm = (int)Utils.UTL_SwapBigLittleEndian32((UInt32)catMessage.iApmLateralAlign);
            verApm = (int)Utils.UTL_SwapBigLittleEndian32((UInt32)catMessage.iApmVerticallAlign);
            rollApm = (int)Utils.UTL_SwapBigLittleEndian32((UInt32)catMessage.iApmRollAlign);
            crcApm = (int)Utils.UTL_SwapBigLittleEndian32((UInt32)catMessage.iApmAlignCrc);
#endif

            string apmParams = latApm.ToString() + " / " +
                                 verApm.ToString() + " / " +
                                 rollApm.ToString() + " / " +
                                 crcApm.ToString();

            UpdateApmParams(apmParams);
        }

        /****************************************************/

        private void InitPortListen()
        {
            //Creates a UdpClient for reading incoming data.
            m_receivingUdpClient = new UdpClient(Program.PORT_RECEIVE);

            //Creates an IPEndPoint to record the IP Address and port number of the sender. 
            // The IPEndPoint will allow you to read datagrams sent from any source.
            m_remoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
        }

        /****************************************************/

        private Byte[] ListenToPort()
        {
            //initialize the listenning socket if not already initialized 
            if (!m_isListenningToPortInitialized)
            {
                InitPortListen();
                m_isListenningToPortInitialized = true;
            }

            // Blocks until a message returns on this socket from a remote host.
            Byte[] receiveBytes = m_receivingUdpClient.Receive(ref m_remoteIpEndPoint);
        
            string ipFrom = m_remoteIpEndPoint.Address.ToString();
            string portFrom = m_remoteIpEndPoint.Port.ToString();
            //execute from thread : update GUI field - IP/PORT
            UpdateIpPortGui(ipFrom, portFrom);

            string returnData = Encoding.ASCII.GetString(receiveBytes);

            String str = returnData.ToString();       

            //execute from thread : update GUI field - MESSAGE
            string message = Utils.ByteArrayToString(receiveBytes);
            UpdateMessageGui(message);

            return receiveBytes;
        }

        /****************************************************/
        //Delegations

        public void UpdateIpPortGui(string _ip, string _port)
        {
            if (this.receivedFromIpPortTextBox.InvokeRequired)
            {
                UpdateIpPortGuiDelegate d = new UpdateIpPortGuiDelegate(UpdateIpPortGui);

                try
                {
                    this.Invoke(d, new object[] { _ip, _port });
                }
                catch
                {
                    return;
                }
            }
            else
            {
                receivedFromIpPortTextBox.Text = _ip + "  /  " + _port;

            }
        }

        /****************************************************/

        public void UpdateMessageGui(string _message)
        {
            if (this.messageTextBox.InvokeRequired)
            {
                UpdateMessageGuiDelegate d = new UpdateMessageGuiDelegate(UpdateMessageGui);

                try
                {
                    this.Invoke(d, new object[] { _message });
                }
                catch
                {
                    return;
                }
            }
            else
            {
                messageTextBox.Text = _message;

            }
        }

        /****************************************************/

        public string GetMessageFromGui()
        {
            if (this.messageTextBox.InvokeRequired)
            {
                GetMessageFromGuiDelegate d = new GetMessageFromGuiDelegate(GetMessageFromGui);
                string toReturn = "";

                try
                {
                    toReturn = this.Invoke(d, new object[] { }).ToString();
                }
                catch
                {
                    //TODO
                }

                return toReturn;
            }
            else
            {
                return messageTextBox.Text;
            }
        }

        /****************************************************/

        public void UpdateAlignParams(string _message)
        {
            if (this.alignParamsTextBox.InvokeRequired)
            {
                UpdateAlignParamsGuiDelegate d = new UpdateAlignParamsGuiDelegate(UpdateAlignParams);

                try
                {
                    this.Invoke(d, new object[] { _message });
                }
                catch
                {
                    return;
                }
            }
            else
            {
                alignParamsTextBox.Text = _message;

            }
        }

        /****************************************************/

        public void UpdateApmParams(string _message)
        {
            if (this.apmParamsTextBox.InvokeRequired)
            {
                UpdateApmParamsGuiDelegate d = new UpdateApmParamsGuiDelegate(UpdateApmParams);

                try
                {
                    this.Invoke(d, new object[] { _message });
                }
                catch
                {
                    return;
                }
            }
            else
            {
                apmParamsTextBox.Text = _message;

            }
        }

        /****************************************************/       
        //Handleres/Events

        private void enableListenCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (enableListenCheckBox.Checked)
            {             
                m_isListenningToPortEnabled = true;
            }
            else
            {
                m_isListenningToPortEnabled = false;
            }
        }

        /****************************************************/

    }
}
