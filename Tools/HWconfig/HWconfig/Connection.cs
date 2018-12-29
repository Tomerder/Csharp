using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HWconfig
{
    abstract class Connection
    {
        //----------------------------------------------------------------------------------------------

        public delegate void RegisteredRecievedDataHandlerFuncCallBack(String _dataRecievedFromPort);

        //CALL BACK
        private RegisteredRecievedDataHandlerFuncCallBack m_recievedDataHandlerFuncCallBack;

        //----------------------------------------------------------------------------------------------

        public Connection()
        {
            //default initialization of DM  
            m_recievedDataHandlerFuncCallBack = DummyFunc;
        }

        //----------------------------------------------------------------------------------------------
        /**********************   derived class will have to implement!!!!  **************************/

        abstract public void SendCmd(string _cmd, int _sleepDelay = 0);
        abstract public string RecieveResult(int _sleepDelay = 1);

        //----------------------------------------------------------------------------------------------
        /*************   defined virtual so derived class wont have to implement it   ****************/

        virtual public void SetDataReceivedHandlerEnabled(bool _isEnabled)
        {
            //Do nothing - default implementation
        }


        virtual public bool IsHandshake()
        {
            return false;
        }

        virtual public void RegisterRecievedDataHandler(RegisteredRecievedDataHandlerFuncCallBack _handlerPtr)
        {
            m_recievedDataHandlerFuncCallBack = _handlerPtr;
        }

        //----------------------------------------------------------------------------------------------

        private void DummyFunc(string _dummy)
        {
            //Do nothing - default implementation
        }

        //----------------------------------------------------------------------------------------------

        public RegisteredRecievedDataHandlerFuncCallBack RecievedDataHandlerFuncPtr
        {
            get { return m_recievedDataHandlerFuncCallBack; }
            set { m_recievedDataHandlerFuncCallBack = value; }
        }

        //----------------------------------------------------------------------------------------------
    }
}
