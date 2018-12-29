using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Sockets;
using System.Net;
using System.Globalization;

namespace SendEthernetMsg
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /*----------------------------------------------------------------------------*/

        static string DEFAULT_IP_TO_SEND = "127.0.0.1";
        static int DEFAULT_PORT_TO_SEND = 9003;

        /*----------------------------------------------------------------------------*/

        delegate string GetIpToSendTextBoxDelegate();
        delegate string GetPortToSendTextBoxDelegate();
        delegate string GetMsgToSendTextBoxDelegate();

        delegate string GetPortToSendFromTextBoxDelegate();

        /*----------------------------------------------------------------------------*/

        public MainWindow()
        {
            InitializeComponent();

            SetDefaults();
        }

        /*----------------------------------------------------------------------------*/

        private void SetDefaults()
        {
            textBoxToIP.Text = DEFAULT_IP_TO_SEND;
            textBoxToPort.Text = DEFAULT_PORT_TO_SEND.ToString();

            textBoxMyIP.Text = UtilsLib.UtilsLib.GetLocalIPAddress();
        }

        /*----------------------------------------------------------------------------*/

        private void buttonSend_Click(object sender, RoutedEventArgs e)
        {
            int portToSendFrom = 0;
            int portToSendTo = DEFAULT_PORT_TO_SEND;

            try
            {
                portToSendFrom = Convert.ToInt32(GetPortToSendFromTextBox());
            }
            catch
            {
                portToSendFrom = 0;
            }

            try
            {
                portToSendTo = Convert.ToInt32(GetPortToSendTextBox());
            }
            catch
            {
                portToSendTo = DEFAULT_PORT_TO_SEND;
            }

            string ipToSendTo = GetIpToSendTextBox();
            string msgToSend = GetMsgToSendTextBox();

            bool sucess = UtilsLib.UtilsLib.SendUdpMessage(portToSendFrom, ipToSendTo, portToSendTo, msgToSend);

            if (!sucess)
            {
                MessageBox.Show("Failed sending message");
            }
        }

        /*----------------------------------------------------------------------------*/

        public string GetIpToSendTextBox()
        {
            //enable calling from another thread which is not the main thread(GUI)
            if (!Dispatcher.CheckAccess())
            {
                return (string)Dispatcher.Invoke(new GetIpToSendTextBoxDelegate(GetIpToSendTextBox));
            }
            else
            {
                return textBoxToIP.Text;
            }
        }

        /*----------------------------------------------------------------------------*/

        public string GetPortToSendTextBox()
        {
            //enable calling from another thread which is not the main thread(GUI)
            if (!Dispatcher.CheckAccess())
            {
                return (string)Dispatcher.Invoke(new GetPortToSendTextBoxDelegate(GetPortToSendTextBox));
            }
            else
            {
                return textBoxToPort.Text;
            }
        }

        /*----------------------------------------------------------------------------*/

        public string GetPortToSendFromTextBox()
        {
            //enable calling from another thread which is not the main thread(GUI)
            if (!Dispatcher.CheckAccess())
            {
                return (string)Dispatcher.Invoke(new GetPortToSendFromTextBoxDelegate(GetPortToSendFromTextBox));
            }
            else
            {
                return textBoxFromPort.Text;
            }
        }

        /*----------------------------------------------------------------------------*/

        public string GetMsgToSendTextBox()
        {
            //enable calling from another thread which is not the main thread(GUI)
            if (!Dispatcher.CheckAccess())
            {
                return (string)Dispatcher.Invoke(new GetMsgToSendTextBoxDelegate(GetMsgToSendTextBox));
            }
            else
            {
                return textBoxMsgToSend.Text;
            }
        }

        /*----------------------------------------------------------------------------*/

    }
}
