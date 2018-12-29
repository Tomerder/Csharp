using Newtonsoft.Json;
using System;
using System.IO;

namespace JsonExample
{
    class Program
    {
        private const string FILE_NAME_TXT = "info.txt";

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
        /// Json Serialization.
        /// Serialize Properties (with has DataMember attribute)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance">The instance.</param>
        private static void SerializeJson<T>(T instance)
        {
            File.Delete(FILE_NAME_TXT);

            var ser = new JsonSerializer();
            using (var writer = new StreamWriter(FILE_NAME_TXT))
            using (var jsonWriter = new JsonTextWriter(writer))
            {
                ser.Serialize(jsonWriter, instance);
            }

            WriteFileContent(FILE_NAME_TXT);

            using (var reader = new StreamReader(FILE_NAME_TXT))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var result = ser.Deserialize<T>(jsonReader); 
                Console.WriteLine(result);
            }
        }

        #endregion // SerializeJson

        static void Main(string[] args)
        {
            WriteTitle("Simple Json Serialization");
            var json1 = new FooJson { Id = 1, Name = "John", Pass = "1234" };
            SerializeJson(json1);
        }
    }
}
