using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HWconfig
{
    //a package of functions that are being used  
    static class Common
    {
        /*-----------------------------------------------------------------------------------*/

        static public bool GetRegBitsFromToValue(string _regValue, int _fromBit, int _len, bool _isHexaDisplay, out string _regValueBitwise)
        {
            _regValueBitwise = "";

            //case that value stays the same 
            if (_fromBit == 0 && _len == 32)
            {
                _regValueBitwise = _regValue;
                return true;
            }

            //convert reg value to int32
            int regValueToRet = 0;

            if (!HexaStringToInt32(_regValue, out regValueToRet))
            {
                Logger.Instance.Log(Logger.LOG_MODE_ENUM.ERROR, _regValue + " : Error converting address");
                return false;
            }

            //create mask
            int mask = 1;

            for (int i = 0; i < _len - 1; i++)
            {
                mask = mask << 1;
                mask = mask | 1;
            }

            //shift right reg to get starting bit 
            regValueToRet = regValueToRet >> _fromBit;

            //get wanted part bit[from..to]
            regValueToRet = regValueToRet & mask;

            _regValueBitwise = Int32ToHexaString(regValueToRet);

            return true;
        }

        /*-----------------------------------------------------------------------------------*/

        static public bool BitwiseUpdatePartOf32BitValue(string _cur32BitValue, int _updateFromBit, int _lenToUpdate, string _updateValue, out string _32BitValueForUpdateBitwise)
        {
            int regValueForUpdatingBitwise = 0;

            _32BitValueForUpdateBitwise = "";

            int regCur32Value = 0;
            if (!HexaStringToInt32(_cur32BitValue, out regCur32Value))
            {
                return false;
            }

            /***unite regValueForUpdate within regValue**/
            //1.preper mask : 1111111000000111111
            int mask = 1;

            for (int i = 0; i < _lenToUpdate - 1; i++)
            {
                mask = mask << 1;
                mask = mask | 1;
            }

            mask = mask << _updateFromBit;

            mask = ~mask;

            //2.and with mask => xxxxxxx000000xxxxxx
            regValueForUpdatingBitwise = regCur32Value & mask;

            //3.or with shifted regValueForUpdate : 0000000xxxxxx000000
            int valueForUpdate = 0;
            if (!HexaStringToInt32(_updateValue, out valueForUpdate))
            {
                return false;
            }

            valueForUpdate = valueForUpdate << _updateFromBit;

            regValueForUpdatingBitwise = regValueForUpdatingBitwise | valueForUpdate;

            //setting the result : final reg value for updating
            _32BitValueForUpdateBitwise = Int32ToHexaString(regValueForUpdatingBitwise);

            return true;
        }

        /*-----------------------------------------------------------------------------------*/

        static public bool HexaStringToInt32(string _regValueStr, out int _regValueToRet)
        {
            _regValueToRet = 0;

            try
            {
                _regValueToRet = Convert.ToInt32(_regValueStr, 16);
            }
            catch
            {
                return false;
            }

            return true;
        }

        /*-----------------------------------------------------------------------------------*/

        static public string Int32ToHexaString(int _regValue)
        {
            return "0x" + String.Format("{0:X}", _regValue);
        }

        /*-----------------------------------------------------------------------------------*/

        static public bool IsValueToUpdateFitInReg(int _LengthBits, string _valueToUpdate)
        {
            String binValueToUpdate = "";

            try
            {
                int base10ValueToUpdate = Convert.ToInt32(_valueToUpdate, 16);
                binValueToUpdate = Convert.ToString(base10ValueToUpdate, 2);
            }
            catch
            {
                Logger.Instance.Log(Logger.LOG_MODE_ENUM.ERROR, "Conversion error");
                return false;
            }

            if (binValueToUpdate.Length > _LengthBits)
            {
                return false;
            }

            return true;
        }

        /*-----------------------------------------------------------------------------------*/
    }
}
