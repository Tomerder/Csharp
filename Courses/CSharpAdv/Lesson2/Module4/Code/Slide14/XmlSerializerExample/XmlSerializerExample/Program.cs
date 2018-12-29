using System;
using System.IO;
using System.Xml.Serialization;

namespace XmlSerializerExample
{
    /// <summary>
    /// XML serialization is a mechanism completely different from .NET runtime
    /// serialization.  It works on acyclic object graphs only, and serializes
    /// only public read-write properties.  Unlike the .NET serialization we have
    /// seen which uses reflection to serialize objects, the XmlSerializer uses
    /// code generation techniques to create (once) the code that will be used for
    /// serialization and deserialization of a known type.  This is the reason why
    /// the XmlSerializer takes a type in its constructor - that's the type that
    /// will have a serialization assembly built for its serialization and
    /// deserialization purposes.
    /// 
    /// There are ways to customize XML serialization using attributes placed on
    /// properties of the serialized type.  For example, the [XmlAttribute] attribute
    /// indicates that a property should be serialized as an XML attribute and not
    /// an XML element.
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

        /// <summary>
        /// Xml Serialization (must have parameter-less CTOR).
        /// Serialize Properties
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance">The instance.</param>
        private static void SerializeXml<T>(T instance)
        {
            File.Delete(FILE_NAME_XML);

            var ser = new XmlSerializer(typeof(T));
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
        
        static void Main(string[] args)
        {
            WriteTitle("Simple Xml Serialization");
            var xml1 = new FooXmlDefault { Id = 1, Name = "John", Pass = "1234" };
            SerializeXml(xml1);

            Console.WriteLine();
            Console.WriteLine();

            WriteTitle("Xml Serialization with attribute annotation");
            var xml2 = new FooXmlAnnotated { Id = 1, Name = "John", Pass = "1234" };
            SerializeXml(xml2);
        }
    }
}
