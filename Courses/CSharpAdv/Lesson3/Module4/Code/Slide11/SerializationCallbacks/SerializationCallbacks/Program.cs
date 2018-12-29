using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace SerializationCallbacks
{
    class Program
    {
        static void Main(string[] args)
        {
            User3 joe = new User3("Joe", "123456");
            Console.WriteLine("Before serialization: " + joe);

            //Serialize the user to a binary file.
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Create("joe3.serialized");
            formatter.Serialize(file, joe);
            file.Close();

            ReputationDB.ChangeReputation("Joe", 2);

            Thread.Sleep(2000);

            //Deserialize the user from the binary file.
            file = File.Open("joe3.serialized", FileMode.Open);
            joe = (User3)formatter.Deserialize(file);
            file.Close();

            //Note: the properties of the user have changed thanks
            //to the deserialization callback!
            Console.WriteLine("After deserialization: " + joe);
        }
    }
}
