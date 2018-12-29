using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace CustomSerialization
{
    class Program
    {
        static void Main(string[] args)
        {
            User4 joe = new User4("Joe", "123456");
            Console.WriteLine("Before serialization: " + joe);

            //Serialize the user to a binary file.  Inspect the
            //binary file to make sure that the password was not
            //serialized in plain text.
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Create("joe4.serialized");
            formatter.Serialize(file, joe);
            file.Close();

            ReputationDB.ChangeReputation("Joe", 2);

            Thread.Sleep(2000);

            //Deserialize the user from the binary file.
            file = File.Open("joe4.serialized", FileMode.Open);
            joe = (User4)formatter.Deserialize(file);
            file.Close();

            //Note: the properties of the user have changed thanks
            //to the deserialization constructor!
            Console.WriteLine("After deserialization: " + joe);
        }
    }
}
