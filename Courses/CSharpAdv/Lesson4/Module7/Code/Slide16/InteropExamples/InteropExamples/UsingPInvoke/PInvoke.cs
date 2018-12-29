using System;
using System.IO;
using System.Runtime.InteropServices;

namespace UsingPInvoke
{
    /// <summary>
    /// Demonstrates the use of P/Invoke and reverse P/Invoke for interoperability
    /// between managed code and a C-style DLL with exported functions.
    /// </summary>
    class PInvoke
    {
        const string NATIVE_LIB_PATH = @"..\..\..\..\..\Debug\NativeLibrary.dll";

        [DllImport(NATIVE_LIB_PATH)]
        static extern void Find(ITEM[] items, int count, ref ITEM lookup, out int index);

        static void Main(string[] args)
        {            
            string path = Path.GetFullPath(NATIVE_LIB_PATH);
                        
            //Marshaling an array of items.  Note that the ITEM structure is duplicated
            //in the C and C# code.  The process of duplication can be streamlined by using
            //an automatic tool such as the P/Invoke Signature Assistant.
            ITEM[] items = { new ITEM(0), new ITEM(1), new ITEM(6), new ITEM(18) };
            ITEM lookup = new ITEM(6);
            int index;
            Find(items, items.Length, ref lookup, out index);
            Console.WriteLine(index);
        }
    }
}
