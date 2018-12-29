using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Microsoft.CSharp;
using System.IO;
using System.CodeDom.Compiler;
using System.Reflection;

namespace ReadingUnmanagedStructures
{
    public struct MyMessage
    {
        public int SenderId;
        public int ReceiverId;
        public short DataSize;
        public short Checksum;
    }

    class Program
    {
        public static unsafe MyMessage ReadMyMessageUnsafe(byte[] data, int offset)
        {
            //pin data, return address of the offset-th element in the array
            fixed (byte* temp = &data[offset])
            {
                MyMessage* p = (MyMessage*)temp; //Cast
                return *p; //Dereference -- memory copy done by the JIT!
            }
        }

        static class MyDelegateHolder<T>
        {
            public static Func<byte[], int, T> TheDelegate;
        }

        public static T ReadUnsafe<T>(byte[] data, int offset)
        {
            Func<byte[], int, T> theActualWorkerMethod = GetMethodFromSomewhere<T>();
            return theActualWorkerMethod(data, offset);
        }

        private static Func<byte[], int, T> GetMethodFromSomewhere<T>()
        {
            if (MyDelegateHolder<T>.TheDelegate != null)
                return MyDelegateHolder<T>.TheDelegate;

            CSharpCodeProvider compiler = new CSharpCodeProvider();
            string source = File.ReadAllText("MyCode.txt");
            source = source.Replace("$$$", typeof(T).FullName);
            CompilerParameters parameters = new CompilerParameters();
            parameters.CompilerOptions = "/unsafe /optimize+";
            parameters.ReferencedAssemblies.Add(typeof(T).Assembly.Location);
            CompilerResults result =
                compiler.CompileAssemblyFromSource(parameters, source);
            if (result.Errors.HasErrors)
            {
                throw new ApplicationException();
            }
            Assembly asm = result.CompiledAssembly;
            Type type = asm.GetType("HelpfulUtilities");
            MethodInfo method = type.GetMethod("ReadUnsafe");
            MyDelegateHolder<T>.TheDelegate = (Func<byte[],int,T>)
                Delegate.CreateDelegate(typeof(Func<byte[], int, T>), method);
            
            return MyDelegateHolder<T>.TheDelegate;
        }

        //public static unsafe T ReadUnsafe<T>(byte[] data, int offset)
        //    where T : struct
        //{
        //    fixed (byte* temp = &data[offset])
        //    {
        //        T* p = (T*)temp;
        //        return *p;
        //    }
        //}

        public static T Read<T>(byte[] data, int offset)
        {
            GCHandle gch = GCHandle.Alloc(data, GCHandleType.Pinned);
            try
            {
                IntPtr pointer = gch.AddrOfPinnedObject();
                //pointer += offset; //Impossible, there's no operator+
                ulong t = (ulong)pointer;
                t += (ulong)offset;
                pointer = (IntPtr)t;
                return (T) Marshal.PtrToStructure(pointer, typeof(T));
            }
            finally
            {
                gch.Free(); //No one frees the GCHandle automagically
            }
        }

        static void Main(string[] args)
        {
            byte[] data = { 42, 0, 0, 0, 39, 0, 0, 0, 17, 0, 1, 0 };
            MyMessage msg;
            for (int j = 0; j < 5; ++j)
            {
                Stopwatch stopper = Stopwatch.StartNew();
                for (int i = 0; i < 1000000; ++i)
                {
                    msg = Read<MyMessage>(data, 0);
                }
                Console.WriteLine("Read with Marshal.PtrToStructure: " + stopper.ElapsedMilliseconds);
                stopper = Stopwatch.StartNew();
                for (int i = 0; i < 1000000; ++i)
                {
                    msg = ReadMyMessageUnsafe(data, 0);
                }
                Console.WriteLine("Read with pointers: " + stopper.ElapsedMilliseconds);
                stopper = Stopwatch.StartNew();
                for (int i = 0; i < 1000000; ++i)
                {
                    msg = ReadUnsafe<MyMessage>(data, 0);
                }
                Console.WriteLine("Read with pointers -- generic: " + stopper.ElapsedMilliseconds);
            }
        }
    }
}
