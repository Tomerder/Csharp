using System;
using System.IO;
using System.Runtime.InteropServices;

namespace UsingPInvoke
{
    class PInvoke
    {
        const string NATIVE_LIB_PATH = @"..\..\..\..\..\Debug\NativeLibrary.dll";
        delegate bool IsMatch(string text);

        [DllImport(NATIVE_LIB_PATH, CharSet = CharSet.Ansi)]
        static extern void StoreMatch(IsMatch match);

        [DllImport(NATIVE_LIB_PATH, CharSet = CharSet.Ansi)]
        static extern void UseMatch();

        bool Matcher(string s)
        {
            return !String.IsNullOrEmpty(s);
        }

        static void Main(string[] args)
        {            
            string path = Path.GetFullPath(NATIVE_LIB_PATH);
            
            //Callback on garbage-collected delegate:
            StoreMatch(new PInvoke().Matcher);
            GC.Collect();
            UseMatch();

            //Marshaling delegates which are stored by native code and invoked later.
            //The lifetime of such delegates must be explicitly managed to ensure that
            //the delegate is not garbage collected while native code still has a
            //reference to it through a native function pointer.
            IsMatch match = (new PInvoke().Matcher);
            StoreMatch(match);
            match = null;
            GC.Collect();
            UseMatch();

            Console.ReadKey();
        }
    }
}
