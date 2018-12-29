using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace LoggerLib
{
    class Logger
    {
        public enum LOG_MODE_ENUM { NONE, ERROR, WARNING, IMPORTANT, DEBUG, ALL };

        private const string FILE_NAME = "log.txt";

        public LOG_MODE_ENUM m_mode;

        private Mutex m_lock; //thread safe protection

        /*---------------------------singleton----------------------------*/
       
        private static Logger m_instance;

        private Logger() 
        {
            m_mode = LOG_MODE_ENUM.NONE;

            m_lock = new Mutex();
        }

        public static Logger Instance
        {
            get 
            {
                if (m_instance == null)
                {
                    m_instance = new Logger();
                }
                
                return m_instance;
            }
        }

        /*----------------------------------------------------------------*/

        public void Log(LOG_MODE_ENUM _mode, string _toLog = "")
        {
            string delimiter = "  ***  "; 

            if(_mode <= m_mode)
            {
                try
                {
                    m_lock.WaitOne();  //thread safe protection

                    using (StreamWriter w = File.AppendText(FILE_NAME))
                    {
                        StackFrame caller = new StackFrame(1, true);
                        string classMethod = caller.GetMethod().DeclaringType.Name + "::" + caller.GetMethod().Name;

                        w.WriteLine(_mode + delimiter  //mode 
                            + DateTime.Now.GetDateTimeFormats('d')[0] + delimiter   //date
                            + DateTime.Now.GetDateTimeFormats('t')[0] + delimiter         //time
                            + Thread.CurrentThread.ManagedThreadId.ToString().PadRight(4) + delimiter            //Thread ID
                            + classMethod.PadRight(45) + delimiter   //class::method
                            + _toLog);
                    }

                    m_lock.ReleaseMutex();
                }
                catch 
                {
                    m_lock.ReleaseMutex();
                    //error
                }
            }
        }

        /*----------------------------------------------------------------*/

        public LOG_MODE_ENUM Mode
        {
            get { return m_mode; }
            set { m_mode = value; }
        }

        /*----------------------------------------------------------------*/


    }
}
