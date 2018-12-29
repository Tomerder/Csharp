#define IS_TEST0

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FaliuresMonitor;
using FailureMonitor;
using System.IO;


namespace FailuresMonitor
{
    static class CommTargetAbstractLayer
    {
        //-----------------------------------------------------------------------------------------------------------

        public const int D_CMD_RESULTS_IN_LINE = 16;
        public const int SLEEP_AFTER_WRITE = 1;

        /*-----------------------------------------------------------------------------------*/

        static public bool GetResultsFromTargetUsingDCmd(string _baseAddress, int _resultLenBits, int _numOfResultsToCheck, out List<bool> _resultsList)
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

            string resultToParse = "";

            //send command for getting results to target 
#if(!IS_TEST)
            int bytesLenToReceive = _resultLenBits * _numOfResultsToCheck / Common.BITS_IN_BYTE;
            resultToParse = GetValuesFromTargetBaseAddressDcommand(_baseAddress, bytesLenToReceive);
#else
                 resultToParse = File.ReadAllText("d.txt");
#endif

            string allResults;
            allResults = ParseDcmdResult(resultToParse);

            for (int i = 0; i < _numOfResultsToCheck; i++)
            {
                //result len in hexa
                int resultLenHexa = _resultLenBits / Common.BITS_PER_HEXA_DIGIT;

                string curResultStr = "";

                try 
                {
                    //get failure result
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

        private static string ParseDcmdResult(string _resultToParse)
        {
 	         //get only the of results => from ":" to "*"
            string allResults = "";
            try
            {
                /* 0x2611a020:  0000 0000 0000 0000 0000 0000 0000 0000   *................* */
                //parse all lines
                while(true)
                {
                    int startIndex = _resultToParse.IndexOf(":");
                    int endIndex = _resultToParse.IndexOf("*");
                    //concat line results
                    allResults += _resultToParse.Substring(startIndex + 1, endIndex - startIndex - 1);
                    //cut line from string to parse
                    _resultToParse = _resultToParse.Substring(endIndex + 1);
                    endIndex = _resultToParse.IndexOf("*");
                    _resultToParse = _resultToParse.Substring(endIndex + 1);
                }                
            }
            catch
            {
                //all lines have been parsed
            }

            //remove all spaces 
            allResults = allResults.Replace(" ", String.Empty);

            return allResults;
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
                resultStr = ((i % 2) == 0) ? ("0x10000001") : ("0x01000010");                                   
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
                if (!Common.GetRegBitsFromToValue(resultStr, Failure.FAILURE_STARTS_FROM_REG_BIT, _resultLenBits, out resultStrBitWise))
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

        static public string GetValuesFromTargetBaseAddressDcommand(string _baseAddress, int _bytesLenToReceive)
        {
            string readFromBaseCmd = BuildReadFromBaseAddressCmd(_baseAddress, _bytesLenToReceive);

            //--------------------------------------------------
            Program.Connection.SendCmd(readFromBaseCmd, SLEEP_AFTER_WRITE);
            string receivedResult = Program.Connection.RecieveResult();
            //--------------------------------------------------

            Logger.Instance.Log(Logger.LOG_MODE_ENUM.DEBUG, "Base address values received : " + receivedResult);

            return receivedResult;
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
            return "d " + _baseAdr;  // +" , " + _bytesLenToReceive;
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
