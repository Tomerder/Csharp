using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication2
{
    class Program
    {
        static struct test
        {
            int a1;
            int a2;
            int a3;
        }

        static void Main(string[] args)
        {
            int arr[3];  



            //string reg = "0xffffffff";

            //reg = GetRegBitsFromToValue(reg , 0 , 6 , true);

            //int hex;
            //bool b = HexaStringToInt32("0x5b56435d", out hex); 

            //string valueBitwise = "";

            //GetRegValueForUpdatingBitwise("0xaa" , out valueBitwise);

            //string str = "0X1";
            //bool isGood = IsValueToUpdateFitInReg(8, str);

            /*
            float f = 1.7f;

            int i1 = (int)f;

            int i2 = *(int*)(&f);

             */ 
            //Console.WriteLine(isGood);
        

        
        
        }


        private static bool IsValueToUpdateFitInReg(int _LengthBits, string _valueToUpdate)
        {
            String binValueToUpdate = "";

            try
            {
                int base10ValueToUpdate = Convert.ToInt32(_valueToUpdate, 16);
                binValueToUpdate = Convert.ToString(base10ValueToUpdate, 2);
            }
            catch
            {
                return false;
            }

            if (binValueToUpdate.Length > _LengthBits)
            {
                return false;
            }

            return true;
        }


        private static bool GetRegValueForUpdatingBitwise(string _regValueForUpdate, out string _regValueForUpdateBitwise)
        {
            _regValueForUpdateBitwise = "";
            int regValueForUpdatingBitwise = 0;

            //read current value from reg address
            string regValueStr = "0xffffffff";

            int regValue = 0;
            if (!HexaStringToInt32(regValueStr, out regValue))
            {
                return false;
            }

            /***unite regValueForUpdate within regValue**/
            //1.preper mask : 1111111000000111111
            int mask = 1;

            for (int i = 0; i < 8 - 1; i++) //8==len
            {
                mask = mask << 1;
                mask = mask | 1;
            }

            mask = mask << 16;  //16==starts

            mask = ~mask;

            //2.and with mask => xxxxxxx000000xxxxxx
            regValueForUpdatingBitwise = regValue & mask;

            //3.or with shifted regValueForUpdate : 0000000xxxxxx000000
            int regValueForUpdate = 0;
            if (!HexaStringToInt32(_regValueForUpdate, out regValueForUpdate))
            {
                return false;
            }

            regValueForUpdate = regValueForUpdate << 16;  //16==starts

            regValueForUpdatingBitwise = regValueForUpdatingBitwise | regValueForUpdate;

            //setting the result : total reg value for updating
            _regValueForUpdateBitwise = Int32ToHexaString(regValueForUpdatingBitwise);

            return true;
        }

        private static bool HexaStringToInt32(string _regValueStr, out int _regValueToRet)
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

        private static string GetRegBitsFromToValue(string _regValue, int _fromBit, int _len, bool _isHexaDisplay)
        {
            //case that value stays the same 
            if (_fromBit == 0 && _len == 32)
            {
                return _regValue;
            }

            //convert reg value to int32
            int regValueToRet = 0;

            try
            {
                regValueToRet = Convert.ToInt32(_regValue, 16);
            }
            catch
            {
                Console.WriteLine("Error trying to convert address");
                return "";
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

            return Int32ToHexaString(regValueToRet);
        }


        private static string Int32ToHexaString(int _regValue)
        {
            return "0x" + String.Format("{0:X}", _regValue);
        }

    }





}
