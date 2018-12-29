using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CAT_TestTool
{
    static class Utils
    {
        public static string ByteArrayToString(byte[] ba)
        {
            string hex = BitConverter.ToString(ba);
            return hex.Replace("-", "");
        }

        public static UInt32 UTL_SwapBigLittleEndian32(UInt32 _toSwap)
        {
            return (( _toSwap & 0x000000ffU) << 24) |
                   (( _toSwap & 0x0000ff00U) << 8) |
                   (( _toSwap & 0x00ff0000U) >> 8) |
                   (( _toSwap & 0xff000000U) >> 24);
        }

        public static byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

    }
}
