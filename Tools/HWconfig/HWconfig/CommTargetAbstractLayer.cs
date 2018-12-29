using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HWconfig
{
    static class CommTargetAbstractLayer
    {

        //-----------------------------------------------------------------------------------------------------------

        public const int SLEEP_AFTER_WRITE = 1;

        /*-----------------------------------------------------------------------------------*/

        static public bool GetRegValueFromTarget(string _regAddress, out string _value)
        {

            string readCmd = BuildReadCmd(_regAddress);
            //--------------------------------------------------
            Program.Connection.SendCmd(readCmd, SLEEP_AFTER_WRITE);
            string receivedResult = Program.Connection.RecieveResult();
            //--------------------------------------------------

            _value = "";

            try
            {
                //get hexa value of reg
                int indexFrom = receivedResult.IndexOf("=");
                receivedResult = receivedResult.Substring(indexFrom + 2);

                indexFrom = receivedResult.IndexOf("=");
                receivedResult = receivedResult.Substring(indexFrom + 2);

                int indexTo = receivedResult.IndexOf("[");
                _value = receivedResult.Substring(0, indexTo - 2);
            }
            catch
            {
                Logger.Instance.Log(Logger.LOG_MODE_ENUM.ERROR, "Error taken out reg value from receive answer");
                return false;
            }

            Logger.Instance.Log(Logger.LOG_MODE_ENUM.DEBUG, "Reg value received : " + _value);

            return true;
        }

        /*-----------------------------------------------------------------------------------*/

        static public void SetRegValueOnTarget(string _regAddress, string _value, string _delay)
        {
            //update reg through connection 
            String cmdUpdateReg = BuildWriteCmd(_regAddress, _value);
            Program.Connection.SendCmd(cmdUpdateReg, GetSleepDelay(_delay));

            //clear buffer from answer
            Program.Connection.RecieveResult();
        }

        /*-----------------------------------------------------------------------------------*/

        static public string BuildReadCmd(string _regAdr)
        {
            return "sysIn32 " + _regAdr;
        }

        static public string BuildWriteCmd(string _regAdr, string _val2Write)
        {
            return "sysOut32 " + _regAdr + " , " + _val2Write;
        }

        /*-----------------------------------------------------------------------------------*/

        static public int GetSleepDelay(string _sleepDelay)
        {
            int sleep = SLEEP_AFTER_WRITE;

            try
            {
                sleep = Convert.ToInt32(_sleepDelay);
            }
            catch
            {
                sleep = SLEEP_AFTER_WRITE;
            }

            return sleep;
        }

        //-----------------------------------------------------------------------------------------------------------

    }
}
