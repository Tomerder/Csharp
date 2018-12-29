using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Windows.Forms;
using System.Threading;

namespace FailuresMonitor
{
    class SerialComm : Connection
    {
        private SerialPort m_serialPort;

        private bool m_isRecieveHandler;

        private string m_lastCmd;

        private Mutex mutexProtectPort = new Mutex();
        private Mutex mutexProtectLastCmd = new Mutex();

        /*--------------------------------------------------------------------------------------------*/
      
        private const int READ_TIMEOUT = 500;
        private const int WRITE_TIMEOUT = 500;

        private const int READ_BUF_SIZE = 1024;
        
        //in read handler
        private const int SLEEP_BEFORE_READ = 1;

        private const int RECEIVE_RESULT_TIMEOUT = 500;

        private const string DUMMY_STR = "AAAAAAAAAAAAaaaaaaaaaaaaaaaaaAAAAAAAAAAA";

        /*--------------------------------------------------------------------------------------------*/

        //throws exeptions when serial port in use  
        public SerialComm(string _com, out bool _isGood)
        {
            m_serialPort = new SerialPort(_com, 9600, Parity.None, 8, StopBits.One);          

            //m_serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            m_isRecieveHandler = false;

            m_serialPort.ReadTimeout = READ_TIMEOUT;
            m_serialPort.WriteTimeout = WRITE_TIMEOUT;

            m_serialPort.Open();

            if (!m_serialPort.IsOpen) 
            {
                _isGood = false;
            }

            m_lastCmd = DUMMY_STR;

            _isGood = true;
        }

        /*--------------------------------------------------------------------------------------------*/

        public override void Disconnect()
        {
            m_serialPort.Close();
        }

        public override bool Connect()
        {
            m_serialPort.Open();

            if (!m_serialPort.IsOpen)
            {
                return false;
            }

            return true;
        }

        public override bool IsConnected()
        {
            if (!m_serialPort.IsOpen)
            {
                return false;
            }

            return true;
        }

        /*--------------------------------------------------------------------------------------------*/

        public override void SetDataReceivedHandlerEnabled(bool _isEnabled)
        {
            if (_isEnabled)
            {
                m_serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler); 
            }
            else
            {
                m_serialPort.DataReceived -= new SerialDataReceivedEventHandler(DataReceivedHandler); 
            }
        }

        /*--------------------------------------------------------------------------------------------*/
       
        public static string[] GetSerialPorts()
        {
            string[] portsToRet = SerialPort.GetPortNames();

            Array.Sort(portsToRet, StringComparer.InvariantCulture);

            return portsToRet;          
        }

        /*--------------------------------------------------------------------------------------------*/

        /*override abstract class method : Connection*/
        public override void SendCmd(string _cmd, int _sleepDelay=0)
        {
            Logger.Instance.Log(Logger.LOG_MODE_ENUM.DEBUG, "Enter");

            //clear last read before sending new cmd 
            mutexProtectLastCmd.WaitOne();
            if (_cmd.Length > 1)
            {
                m_lastCmd = _cmd;
            }
            else
            {
                m_lastCmd = DUMMY_STR;
            }
            mutexProtectLastCmd.ReleaseMutex();

            //write to port
            /*----------------------------------------------------*/
            mutexProtectPort.WaitOne();
            /*----------------------------------------------------*/
            //clear in and out buffers before sending new cmd
            m_serialPort.DiscardInBuffer();
            m_serialPort.DiscardOutBuffer();
            
            //m_serialPort.Write(_cmd + Environment.NewLine); // '\r'
            m_serialPort.WriteLine(_cmd);          
            /*----------------------------------------------------*/
            mutexProtectPort.ReleaseMutex();
            /*----------------------------------------------------*/

            Console.WriteLine("Writing to port :" + _cmd);

            Logger.Instance.Log(Logger.LOG_MODE_ENUM.DEBUG, "After Write : " + _cmd);

            //sleep after write
            if (_sleepDelay != 0)
            {
                Thread.Sleep(_sleepDelay);
            }

            Logger.Instance.Log(Logger.LOG_MODE_ENUM.DEBUG, "After Sleep : " + _sleepDelay);

            //clean output buffer
            //mutexProtectPort.WaitOne();
            //m_serialPort.DiscardOutBuffer();
            //mutexProtectPort.ReleaseMutex();
        }

        /*--------------------------------------------------------------------------------------------*/

        //-override abstract class method : Connection-/
        public override string RecieveResult(int _sleepDelay = 1)
        {
            Logger.Instance.Log(Logger.LOG_MODE_ENUM.DEBUG, "Enter");

            int timeout = 0;
            string dataReadFromPort = m_serialPort.ReadExisting();

            while (!dataReadFromPort.Contains("[coreOS] ->") && timeout <= RECEIVE_RESULT_TIMEOUT)
            {
                Thread.Sleep(_sleepDelay);

                timeout += _sleepDelay;

                dataReadFromPort += m_serialPort.ReadExisting();
            }

            Logger.Instance.Log(Logger.LOG_MODE_ENUM.DEBUG, "Data received : " + dataReadFromPort);

            return dataReadFromPort;
        }

        /*--------------------------------------------------------------------------------------------*/
        
        /*called from reading thread whenever data is writen to serial input port - RUNS ON READING THREAD*/ 
        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            Logger.Instance.Log(Logger.LOG_MODE_ENUM.DEBUG, "Enter");

            string readFromPort = "";

            //sleep a while so the cmd will be completely written to port 
            Thread.Sleep(SLEEP_BEFORE_READ);
            Logger.Instance.Log(Logger.LOG_MODE_ENUM.DEBUG, "After sleep before read : " + SLEEP_BEFORE_READ);

            //SerialPort serialPort = (SerialPort)sender;

            // read from port
            //-------------------------------------------------------
            mutexProtectPort.WaitOne();
            //-------------------------------------------------------
            //clean output buffer before reading 
            m_serialPort.DiscardOutBuffer();

            try
            {
                //readFromPort = m_serialPort.ReadExisting();
                Byte[] buf = new Byte[READ_BUF_SIZE];
                m_serialPort.Read(buf, 0, READ_BUF_SIZE);
                readFromPort = Encoding.UTF8.GetString(buf, 0, buf.Length);          
            }
            catch
            {
                mutexProtectPort.ReleaseMutex();
                MessageBox.Show("Read Timeout", "Error");
                return;
            }

            //-------------------------------------------------------
            mutexProtectPort.ReleaseMutex();
            //-------------------------------------------------------

            Logger.Instance.Log(Logger.LOG_MODE_ENUM.DEBUG, "After read : " + readFromPort + " Last CMD : " + m_lastCmd);

            //-------------------------------------------------------
            //dont display the command
            mutexProtectLastCmd.WaitOne();
            if (readFromPort.Contains(m_lastCmd))
            {
                int indFrom = readFromPort.IndexOf(m_lastCmd);
                readFromPort = readFromPort.Substring(indFrom + m_lastCmd.Length);
                m_lastCmd = DUMMY_STR;
            }
            mutexProtectLastCmd.ReleaseMutex();
            //-------------------------------------------------------

            //call THREAD SAFE method - that was registered to handle data that was received from port 
            //show read result on terminal (in this case)
            RecievedDataHandlerFuncPtr(readFromPort);

            Logger.Instance.Log(Logger.LOG_MODE_ENUM.DEBUG, "After AppendTerminal : " + readFromPort);

            //clean input serial port buffer 
            //mutexProtectPort.WaitOne();
            //m_serialPort.DiscardInBuffer();
            //mutexProtectPort.ReleaseMutex();
        }
        
        /*--------------------------------------------------------------------------------------------*/

        public override bool IsHandshake()
        {
            bool isHandshakeOk = true;

            Logger.Instance.Log(Logger.LOG_MODE_ENUM.DEBUG, "Enter");

            SetDataReceivedHandlerEnabled(false);
            mutexProtectPort.WaitOne();

            m_serialPort.WriteLine("");

            try
            {
                m_serialPort.ReadLine(); 
            }
            catch
            {
                isHandshakeOk = false;
            }

            mutexProtectPort.ReleaseMutex();

            //set the receive handler as it was before
            if (m_isRecieveHandler)
            {
                SetDataReceivedHandlerEnabled(true);
            }

            Logger.Instance.Log(Logger.LOG_MODE_ENUM.DEBUG, "IsHandshake = " + isHandshakeOk);

            return isHandshakeOk;
        }

        /*--------------------------------------------------------------------------------------------*/
    }
}
