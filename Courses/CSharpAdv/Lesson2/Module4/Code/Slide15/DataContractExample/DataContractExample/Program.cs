using System;
using System.IO;
using System.Runtime.Serialization;

namespace DataContractExample
{
    /// <summary>
    /// Data-contract serialization is a mechanism used by Windows Communication
    /// Foundation (WCF) for data serialization between the distributed parties.
    /// It serializes to an XML-like format by default, but there are also flavors
    /// like the NetDataContractSerializer which perform efficient binary serialization.
    /// Unlike the XmlSerializer, DCS can serialize private fields and cyclic
    /// object graphs.  Its behavior can be customized using attributes placed on
    /// the serialized types, including the [DataContract] and [DataMember] attributes.
    /// 
    /// The DataContractSerializer resides in the System.Runtime.Serialization
    /// assembly which is new in .NET 3.0 (introduced together with WCF).
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

        #region SerializeDataContract

        /// <summary>
        /// Data Contract Serialization.
        /// Serialize Properties (with has DataMember attribute)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance">The instance.</param>
        private static void SerializeDataContract<T>(T instance)
        {
            File.Delete(FILE_NAME_XML);

            var ser = new DataContractSerializer(typeof(T));
            using (var srm = File.OpenWrite(FILE_NAME_XML))
            {
                ser.WriteObject(srm, instance);
            }

            WriteFileContent(FILE_NAME_XML);

            using (var srm = File.OpenRead(FILE_NAME_XML))
            {
                var result = (T)ser.ReadObject(srm);
                Console.WriteLine(result);
            }
        }

        #endregion // SerializeDataContract

        static void Main(string[] args)
        {
            WriteTitle("Simple Data Contract Serialization");
            var dc1 = new FooDataContract { Id = 1, Name = "John", Pass = "1234" };
            SerializeDataContract(dc1);
        }
    }
}
