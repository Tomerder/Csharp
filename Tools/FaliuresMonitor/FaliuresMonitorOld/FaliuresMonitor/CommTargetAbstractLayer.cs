#define IS_TEST0

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FaliuresMonitor;
using FailureMonitor;


namespace FailuresMonitor
{
    static class CommTargetAbstractLayer
    {
        //-----------------------------------------------------------------------------------------------------------

        public const int SLEEP_AFTER_WRITE = 1;

        /*-----------------------------------------------------------------------------------*/

        static public bool GetResultsFromTargetOneCmd(string _baseAddress, int _resultLenBits, int _numOfResultsToCheck, out List<bool> _resultsList)
        {
            _resultsList = new List<bool>();

            //check connection
            #if(!IS_TEST)
            if (!Program.Connection.IsHandshake())
            {
                Logger.Instance.Log(Logger.LOG_MODE_ENUM.ERROR, "IsHandshake - failed");
                return false;
            }
            else
            {
                Logger.Instance.Log(Logger.LOG_MODE_ENUM.DEBUG, "IsHandshake - success");
            }
            #endif

            string allResults = "";

            //send command for getting results to target 
            #if(!IS_TEST)
            int bytesLenToReceive = _resultLenBits * _numOfResultsToCheck / Common.BITS_IN_BYTE;
            if (!GetValuesFromTargetBaseAddress(_baseAddress, bytesLenToReceive, out allResults))
            {
                return false;
            }
            #endif

            for (int i = 0; i < _numOfResultsToCheck; i++)
            {
                //result len in hexa
                int resultLenHexa = _resultLenBits / Common.BITS_PER_HEXA_DIGIT;

                string curResultStr = "";

                try 
                {
                    curResultStr = allResults.Substring(i * resultLenHexa, resultLenHexa);
                }
                catch
                {
                    Logger.Instance.Log(Logger.LOG_MODE_ENUM.ERROR, "Result from target base address is to short to get all results");
                    return false;
                }

                int curResult;
                if (!Common.HexaStringToInt32(curResultStr, out curResult))
                {
                    return false;
                }

                //add result to results list
                if (curResult == 0)
                {
                    _resultsList.Add(false);
                }
                else
                {
                    _resultsList.Add(true);
                }
            }

            return true;
        }

        /*-----------------------------------------------------------------------------------*/

        static public bool GetResultsFromTarget(string _baseAddress, int _resultLenBits, int _numOfResultsToCheck, out List<bool> _resultsList)
        {
            string curAddress = _baseAddress;

            _resultsList = new List<bool>();

            //check connection
            #if(!IS_TEST)
            if (!Program.Connection.IsHandshake())
            {
                Logger.Instance.Log(Logger.LOG_MODE_ENUM.ERROR, "IsHandshake - failed");
                return false;
            }
            else
            {
                Logger.Instance.Log(Logger.LOG_MODE_ENUM.DEBUG, "IsHandshake - success");
            }
            #endif

            //for each failure
            for (int i = 0; i < _numOfResultsToCheck; i++)
            {
                //get failure result from target
                string resultStr;
                
                #if(IS_TEST)
                    resultStr = ((i % 2) == 0) ? ("0x01000101") : ("0x01000100");                                   
                #else
                if (!GetRegValueFromTarget(curAddress, out resultStr))
                {
                    Logger.Instance.Log(Logger.LOG_MODE_ENUM.ERROR, "GetRegValueFromTarget failed");
                    return false;
                }
                else
                {
                    Logger.Instance.Log(Logger.LOG_MODE_ENUM.DEBUG, "GetRegValueFromTarget : " + resultStr);
                }
                #endif



                     //get only first <len> bits from reg
                string resultStrBitWise;
                if (!Common.GetRegBitsFromToValue(resultStr, 0, _resultLenBits, out resultStrBitWise))
                {
                    return false;
                }

                int result;
                if (!Common.HexaStringToInt32(resultStrBitWise, out result))
                {
                    return false;
                }

                //add result to results list
                if (result == 0)
                {
                    _resultsList.Add(false);
                }
                else
                {
                    _resultsList.Add(true);
                }

                //advance to next address in NVRAM
                if (!Common.GetNextHexaAddress(curAddress, _resultLenBits / Common.BITS_IN_BYTE, out curAddress))
                {
                    return false;
                }

            }

            return true;
        }

        /*-----------------------------------------------------------------------------------*/

        static public bool GetValuesFromTargetBaseAddress(string _baseAddress, int _bytesLenToReceive, out string _value)
        {
            string readFromBaseCmd = BuildReadFromBaseAddressCmd(_baseAddress, _bytesLenToReceive);

            //--------------------------------------------------
            Program.Connection.SendCmd(readFromBaseCmd, SLEEP_AFTER_WRITE);
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

            Logger.Instance.Log(Logger.LOG_MODE_ENUM.DEBUG, "Base address values received : " + _value);

            return true;
        }

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

        /*-----------------------------------------------------------------------------------*/

        static public string BuildReadFromBaseAddressCmd(string _baseAdr, int _bytesLenToReceive)
        {
            return "d " + _baseAdr + " , " + _bytesLenToReceive + " , " + _bytesLenToReceive;
        }

        /*-----------------------------------------------------------------------------------*/

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
