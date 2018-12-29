﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace CalculateAls1Crc
{
    //a package of functions that are being used  
    static class Common
    {
        /*-----------------------------------------------------------------------------------*/

        public const int BITS_IN_BYTE = 8;
        public const int BITS_PER_HEXA_DIGIT = 4;
        public const int BYTES_IN_INT = 4;
        public const int LAST_BIT_IN_BYTE_ON = 0x80;
        public const int FIRST_BIT_IN_BYTE_ON = 0x01;
        public const UInt32 BYTE_MASK = 0xFF;
        public const int BIT_OFF = 0;
        public const UInt32 HIGH_BYTES_MASK = 0xFFFF0000;
        public const UInt32 LOW_BYTES_MASK = 0x0000FFFF;
        public const int HEXA_DIGITS_IN_BYTE = 2;

        /*-----------------------------------------------------------------------------------*/
/*
        static public bool GetRegBitsFromToValue(string _regValue, int _fromBit, int _len, out string _regValueBitwise)
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
*/
        /*-----------------------------------------------------------------------------------*/

        //static public bool BitwiseUpdatePartOf32BitValue(string _cur32BitValue, int _updateFromBit, int _lenToUpdate, string _updateValue, out string _32BitValueForUpdateBitwise)
        //{
        //    int regValueForUpdatingBitwise = 0;

        //    _32BitValueForUpdateBitwise = "";

        //    int regCur32Value = 0;
        //    if (!HexaStringToInt32(_cur32BitValue, out regCur32Value))
        //    {
        //        return false;
        //    }

        //    /***unite regValueForUpdate within regValue**/
        //    //1.preper mask : 1111111000000111111
        //    int mask = 1;

        //    for (int i = 0; i < _lenToUpdate - 1; i++)
        //    {
        //        mask = mask << 1;
        //        mask = mask | 1;
        //    }

        //    mask = mask << _updateFromBit;

        //    mask = ~mask;

        //    //2.and with mask => xxxxxxx000000xxxxxx
        //    regValueForUpdatingBitwise = regCur32Value & mask;

        //    //3.or with shifted regValueForUpdate : 0000000xxxxxx000000
        //    int valueForUpdate = 0;
        //    if (!HexaStringToInt32(_updateValue, out valueForUpdate))
        //    {
        //        return false;
        //    }

        //    valueForUpdate = valueForUpdate << _updateFromBit;

        //    regValueForUpdatingBitwise = regValueForUpdatingBitwise | valueForUpdate;

        //    //setting the result : final reg value for updating
        //    _32BitValueForUpdateBitwise = Int32ToHexaString(regValueForUpdatingBitwise);

        //    return true;
        //}

        /*-----------------------------------------------------------------------------------*/

        static public bool IntStringToHexString(string _decStr, out string _HexStr)
        {
            int decTohex = 0;

            try
            {
                decTohex = Convert.ToInt32(_decStr, 10);
                _HexStr = "0x" + String.Format("{0:X}", decTohex);
            }
            catch
            {
                _HexStr = "";
                return false;
            }

            return true;
        }

        /*-----------------------------------------------------------------------------------*/

        static public bool HexaStringToUInt32(string _regValueStr, out UInt32 _regValueToRet)
        {
            _regValueToRet = 0;

            try
            {
                _regValueToRet = Convert.ToUInt32(_regValueStr, 16);
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

        static public bool IsValueToUpdateFitInReg(int _engthBits, string _valueToUpdate)
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

            if (binValueToUpdate.Length > _engthBits)
            {
                return false;
            }

            return true;
        }

        /*-----------------------------------------------------------------------------------*/

        //static public bool GetNextHexaAddress(string _curAddress, int _bytesToNext, out string _nextAddress)
        //{
        //    int curAddress;

        //    _nextAddress = "";

        //    if (!HexaStringToInt32(_curAddress, out curAddress))
        //    {
        //        return false;
        //    }

        //    int nextAddress = curAddress + _bytesToNext;

        //    _nextAddress = Int32ToHexaString(nextAddress);

        //    return true;
        //}

        /*-----------------------------------------------------------------------------------*/

        private static UInt32[] m_CrcArr = new UInt32[] {
	        0x00000000, 0x77073096, 0xEE0E612C, 0x990951BA,
	        0x076DC419, 0x706AF48F, 0xE963A535, 0x9E6495A3,
	        0x0EDB8832, 0x79DCB8A4, 0xE0D5E91E, 0x97D2D988,
	        0x09B64C2B, 0x7EB17CBD, 0xE7B82D07, 0x90BF1D91,
	        0x1DB71064, 0x6AB020F2, 0xF3B97148, 0x84BE41DE,
	        0x1ADAD47D, 0x6DDDE4EB, 0xF4D4B551, 0x83D385C7,
	        0x136C9856, 0x646BA8C0, 0xFD62F97A, 0x8A65C9EC,
	        0x14015C4F, 0x63066CD9, 0xFA0F3D63, 0x8D080DF5,
	        0x3B6E20C8, 0x4C69105E, 0xD56041E4, 0xA2677172,
	        0x3C03E4D1, 0x4B04D447, 0xD20D85FD, 0xA50AB56B,
	        0x35B5A8FA, 0x42B2986C, 0xDBBBC9D6, 0xACBCF940,
	        0x32D86CE3, 0x45DF5C75, 0xDCD60DCF, 0xABD13D59,
	        0x26D930AC, 0x51DE003A, 0xC8D75180, 0xBFD06116,
	        0x21B4F4B5, 0x56B3C423, 0xCFBA9599, 0xB8BDA50F,
	        0x2802B89E, 0x5F058808, 0xC60CD9B2, 0xB10BE924,
	        0x2F6F7C87, 0x58684C11, 0xC1611DAB, 0xB6662D3D,
	        0x76DC4190, 0x01DB7106, 0x98D220BC, 0xEFD5102A,
	        0x71B18589, 0x06B6B51F, 0x9FBFE4A5, 0xE8B8D433,
	        0x7807C9A2, 0x0F00F934, 0x9609A88E, 0xE10E9818,
	        0x7F6A0DBB, 0x086D3D2D, 0x91646C97, 0xE6635C01,
	        0x6B6B51F4, 0x1C6C6162, 0x856530D8, 0xF262004E,
	        0x6C0695ED, 0x1B01A57B, 0x8208F4C1, 0xF50FC457,
	        0x65B0D9C6, 0x12B7E950, 0x8BBEB8EA, 0xFCB9887C,
	        0x62DD1DDF, 0x15DA2D49, 0x8CD37CF3, 0xFBD44C65,
	        0x4DB26158, 0x3AB551CE, 0xA3BC0074, 0xD4BB30E2,
	        0x4ADFA541, 0x3DD895D7, 0xA4D1C46D, 0xD3D6F4FB,
	        0x4369E96A, 0x346ED9FC, 0xAD678846, 0xDA60B8D0,
	        0x44042D73, 0x33031DE5, 0xAA0A4C5F, 0xDD0D7CC9,
	        0x5005713C, 0x270241AA, 0xBE0B1010, 0xC90C2086,
	        0x5768B525, 0x206F85B3, 0xB966D409, 0xCE61E49F,
	        0x5EDEF90E, 0x29D9C998, 0xB0D09822, 0xC7D7A8B4,
	        0x59B33D17, 0x2EB40D81, 0xB7BD5C3B, 0xC0BA6CAD,
	        0xEDB88320, 0x9ABFB3B6, 0x03B6E20C, 0x74B1D29A,
	        0xEAD54739, 0x9DD277AF, 0x04DB2615, 0x73DC1683,
	        0xE3630B12, 0x94643B84, 0x0D6D6A3E, 0x7A6A5AA8,
	        0xE40ECF0B, 0x9309FF9D, 0x0A00AE27, 0x7D079EB1,
	        0xF00F9344, 0x8708A3D2, 0x1E01F268, 0x6906C2FE,
	        0xF762575D, 0x806567CB, 0x196C3671, 0x6E6B06E7,
	        0xFED41B76, 0x89D32BE0, 0x10DA7A5A, 0x67DD4ACC,
	        0xF9B9DF6F, 0x8EBEEFF9, 0x17B7BE43, 0x60B08ED5,
	        0xD6D6A3E8, 0xA1D1937E, 0x38D8C2C4, 0x4FDFF252,
	        0xD1BB67F1, 0xA6BC5767, 0x3FB506DD, 0x48B2364B,
	        0xD80D2BDA, 0xAF0A1B4C, 0x36034AF6, 0x41047A60,
	        0xDF60EFC3, 0xA867DF55, 0x316E8EEF, 0x4669BE79,
	        0xCB61B38C, 0xBC66831A, 0x256FD2A0, 0x5268E236,
	        0xCC0C7795, 0xBB0B4703, 0x220216B9, 0x5505262F,
	        0xC5BA3BBE, 0xB2BD0B28, 0x2BB45A92, 0x5CB36A04,
	        0xC2D7FFA7, 0xB5D0CF31, 0x2CD99E8B, 0x5BDEAE1D,
	        0x9B64C2B0, 0xEC63F226, 0x756AA39C, 0x026D930A,
	        0x9C0906A9, 0xEB0E363F, 0x72076785, 0x05005713,
	        0x95BF4A82, 0xE2B87A14, 0x7BB12BAE, 0x0CB61B38,
	        0x92D28E9B, 0xE5D5BE0D, 0x7CDCEFB7, 0x0BDBDF21,
	        0x86D3D2D4, 0xF1D4E242, 0x68DDB3F8, 0x1FDA836E,
	        0x81BE16CD, 0xF6B9265B, 0x6FB077E1, 0x18B74777,
	        0x88085AE6, 0xFF0F6A70, 0x66063BCA, 0x11010B5C,
	        0x8F659EFF, 0xF862AE69, 0x616BFFD3, 0x166CCF45,
	        0xA00AE278, 0xD70DD2EE, 0x4E048354, 0x3903B3C2,
	        0xA7672661, 0xD06016F7, 0x4969474D, 0x3E6E77DB,
	        0xAED16A4A, 0xD9D65ADC, 0x40DF0B66, 0x37D83BF0,
	        0xA9BCAE53, 0xDEBB9EC5, 0x47B2CF7F, 0x30B5FFE9,
	        0xBDBDF21C, 0xCABAC28A, 0x53B39330, 0x24B4A3A6,
	        0xBAD03605, 0xCDD70693, 0x54DE5729, 0x23D967BF,
	        0xB3667A2E, 0xC4614AB8, 0x5D681B02, 0x2A6F2B94,
	        0xB40BBE37, 0xC30C8EA1, 0x5A05DF1B, 0x2D02EF8D
        };

        static public UInt32 CalculateCRC(Byte[] _pBuffer, int _len)
        {
            UInt32 retValue = 0xFFFFFFFFU;

            for (int i = 0; i < _len; i++)
            {
                if (_pBuffer != null)
                {
                    retValue = m_CrcArr[(retValue ^ _pBuffer[i]) & 0xFFU] ^ (retValue >> 8);
                }
            }

            return (retValue ^ 0xFFFFFFFF);
        }

        /*-----------------------------------------------------------------------------------*/

        static public Byte[] ConvertUInt16ToBytesArr(UInt16 _toCovert)
        {
            Byte[] bytesArr = new Byte[2];

            bytesArr[0] = Convert.ToByte(_toCovert & 0xFF);
            bytesArr[1] = Convert.ToByte((_toCovert >> 8) & 0xFF);

            return bytesArr;
        }

        static public Byte[] ConvertUInt32ToBytesArr(UInt32 _toCovert)
        {
            Byte[] bytesArr = new Byte[4];

            bytesArr[0] = Convert.ToByte(_toCovert & 0xFF);
            bytesArr[1] = Convert.ToByte((_toCovert >> 8) & 0xFF);
            bytesArr[2] = Convert.ToByte((_toCovert >> 16) & 0xFF);
            bytesArr[3] = Convert.ToByte((_toCovert >> 24) & 0xFF);

            return bytesArr;
        }

        static public Byte[] ConvertUInt32ToBytesArr(UInt32 _toCovert, int _numOfBytes)
        {
            Byte[] bytesArr = new Byte[_numOfBytes];

            for (int i = 0; i < _numOfBytes; i++)
            {
                bytesArr[i] = Convert.ToByte((_toCovert >> (i * BITS_IN_BYTE)) & 0xFF);
            }

            return bytesArr;
        }

        /*-----------------------------------------------------------------------------------*/

        static public UInt32 ReflectBitsPerByte(UInt32 _toReflect)
        {
            int toRet = 0;

            //loop on bytes
            for (int i = 0; i < BYTES_IN_INT; i++)
            {
                int curByte = Convert.ToByte(  (_toReflect & (BYTE_MASK << (i * BITS_IN_BYTE))) >> (i * BITS_IN_BYTE)  );
                
                //reflect bits in byte 
                for(int j=0; j < BITS_IN_BYTE/2; j++)
                {
                    //if bits to swap are not equal 
                    bool isbitRightIndOn = (curByte & (FIRST_BIT_IN_BYTE_ON << j)) != BIT_OFF;
                    bool isbitLeftIndOn = (curByte & (LAST_BIT_IN_BYTE_ON >> j)) != BIT_OFF;
                    if (isbitRightIndOn != isbitLeftIndOn)
                    {
                        //flip both bits
                        curByte = curByte ^ (FIRST_BIT_IN_BYTE_ON << j);
                        curByte = curByte ^ (LAST_BIT_IN_BYTE_ON >> j);
                    }                 
                }

                toRet = toRet | (curByte << BITS_IN_BYTE * i);
            }

            return (UInt32)(toRet);
        }

        /*-----------------------------------------------------------------------------------*/

        static public UInt32 ReflectUint32Bytewise(UInt32 _toReflect)
        {           
            UInt32 firstByteShiftedLeft =  (_toReflect & BYTE_MASK) << (BITS_IN_BYTE * 3);
            UInt32 SecondByteShiftedLeft = (_toReflect & (BYTE_MASK << (BITS_IN_BYTE * 1) ) ) << (BITS_IN_BYTE * 1);
            UInt32 ThirdByteShiftedRight = (_toReflect & (BYTE_MASK << (BITS_IN_BYTE * 2) ) ) >> (BITS_IN_BYTE * 1);
            UInt32 ForthByteShiftedRight = (_toReflect & (BYTE_MASK << (BITS_IN_BYTE * 3))) >> (BITS_IN_BYTE * 3);

            UInt32 toRet = firstByteShiftedLeft | SecondByteShiftedLeft | ThirdByteShiftedRight | ForthByteShiftedRight;
       
            return toRet;
        }

        /*-----------------------------------------------------------------------------------*/

        static public UInt32 GetUint32High2Bytes(UInt32 _toGetHighBytes)
        {
            return (_toGetHighBytes & HIGH_BYTES_MASK) >> (BITS_IN_BYTE * 2);
        }

        /*-----------------------------------------------------------------------------------*/

        static public UInt32 GetUint32Low2Bytes(UInt32 _toGetHighBytes)
        {
            return (_toGetHighBytes & LOW_BYTES_MASK);        }

        /*-----------------------------------------------------------------------------------*/

        static public Byte[] ReverseBytes(Byte[] _toReverse)
        {
            _toReverse = _toReverse.Reverse().ToArray();
            return _toReverse;
        }

        /*-----------------------------------------------------------------------------------*/

        public static byte[] StringToByteArray(string hex)
        {
            if (hex.Length % 2 != 0)
            {
                hex = "0" + hex;
            }

            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        /*-----------------------------------------------------------------------------------*/

        static public Byte[] SwapEndians(Byte[] _toSwap)
        {
            int numBytesInArr = _toSwap.Length;
            Byte[] toReturn = new Byte[numBytesInArr];
            UInt32[] intArr = new UInt32[numBytesInArr / BYTES_IN_INT];
            int indexToCopyTo = 0;

            //Byte[] to Int32[]
            Buffer.BlockCopy(_toSwap, 0, intArr, 0, numBytesInArr);

            foreach (UInt32 reg in intArr)
            {
                UInt32 reflected = ReflectUint32Bytewise(reg);

                //copy reflecrte
                intArr[indexToCopyTo] = reflected;                   
                indexToCopyTo++;
            }

            //Int32[] to Byte[]
            Buffer.BlockCopy(intArr, 0, toReturn, 0, numBytesInArr);

            return toReturn;
        }
    }
}
