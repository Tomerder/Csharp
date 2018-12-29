﻿using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;

namespace SoapFormatterExample
{
    /// <summary>
    /// The SoapFormatter type is deprecated, but it shows an alternative to 
    /// the .NET BinaryFormatter while still implementing the same interface.
    /// SoapFormatter has significant limitations, e.g. it does not support
    /// generics, and therefore its use is strongly discouraged.
    /// 
    /// It also resides in a separate assembly: System.Runtime.Serialization.Formatters.Soap
    /// </summary>
    class Program
    {
        private const string FILE_NAME_XML = "info.xml";

        #region WriteTitle

        private static void WriteTitle(string title)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("############### {0} ##################", title);
            Console.WriteLine();
        }

        #endregion // WriteTitle

        #region WriteFileContent

        private static void WriteFileContent(string fileName)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("------------- Serialized Data -------------");
            Console.WriteLine(File.ReadAllText(fileName));
            Console.WriteLine("-------------------------------------------");
            Console.ForegroundColor = ConsoleColor.White;
        }

        #endregion // WriteFileContent

        #region SerializeRuntime

        /// <summary>
        /// Runtime Serialization.
        /// Serialize Fields
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance">The instance.</param>
        private static void SerializeRuntime<T>(T instance, bool useSaop = true)
        {
            File.Delete(FILE_NAME_XML);

            IFormatter ser = new BinaryFormatter();
            if (useSaop)
                ser = new SoapFormatter(); // System.Runtime.Serialization.Formatters.Soap
            else
                ser = new BinaryFormatter();

            using (var srm = File.OpenWrite(FILE_NAME_XML))
            {
                ser.Serialize(srm, instance);
            }

            WriteFileContent(FILE_NAME_XML);

            using (var srm = File.OpenRead(FILE_NAME_XML))
            {
                var result = (T)ser.Deserialize(srm);
                Console.WriteLine(result);
            }
        }

        #endregion // SerializeRuntime
        
        static void Main(string[] args)
        {
            WriteTitle("Simple Run-time Serialization (SOAP)");
            var rt1 = new FooRTDefault { Id = 1, Name = "John", Pass = "1234" };
            SerializeRuntime(rt1);

            WriteTitle("Simple Run-time Serialization (Binary)");
            SerializeRuntime(rt1, false);
        }
    }
}