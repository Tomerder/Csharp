using System;
using System.Runtime.InteropServices;

namespace UsingPInvoke
{
    unsafe class PInvoke
    {
        const string NATIVE_LIB_PATH = @"..\..\..\..\..\Debug\NativeLibrary.dll";

        [DllImport(NATIVE_LIB_PATH)]
        static unsafe extern void Find(ITEM* items, int count, ITEM* lookup, int* index);

        static void Main(string[] args)
        {            
            ITEM[] items = { new ITEM(0), new ITEM(1), new ITEM(6), new ITEM(18) };
            ITEM lookup = new ITEM(6);
            int index;

            //Marshaling arrays and structures as pointers, using unsafe code and the
            //fixed statement to obtain a pointer.  In this example, a pointer to the middle
            //of the array (the 3rd element) is passed to the Find method.
            unsafe
            {
                fixed (ITEM* p = &items[2])
                {
                    Find(p, items.Length - 2, &lookup, &index);
                    Console.WriteLine(index);
                }
            }
        }
    }
}
