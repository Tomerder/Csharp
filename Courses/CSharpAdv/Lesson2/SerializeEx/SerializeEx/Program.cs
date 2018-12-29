using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Threading.Tasks;

namespace SerializeEx
{
    class Program
    {
        const string FILE_NAME = "output.serialized";

        static void Main(string[] args)
        {
            //Serialize();

            DeSerialize();

            Console.ReadLine();
        }

        static void Serialize()
        {
            Person person = new Person() { PrivateName = "aaa" , LastName = "bbb" } ;

            IFormatter serialize = new SoapFormatter();

            using (var stream = File.OpenWrite(FILE_NAME))
            {
                serialize.Serialize(stream, person);
            }
        }

        static void DeSerialize()
        {
            Person person = new Person();

            IFormatter serialize = new SoapFormatter();

            using (var stream = File.OpenRead(FILE_NAME))
            {
                Person result = (Person)serialize.Deserialize(stream);
                Console.WriteLine(result);
            }
        }

    }

    [Serializable]
    class Person 
    {
        public string PrivateName { get; set; }
        public string LastName { get; set; }

        public override string ToString()
        {
            return PrivateName + ", " + LastName;
        }


    }
}
