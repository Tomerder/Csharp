using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace BinaryFormatterExample
{
    class Program
    {
        static void Main(string[] args)
        {
            User joe = new User("Joe", "123456");
            Console.WriteLine("Before serialization: " + joe);

            //Serialize the user to a binary file.  In the process,
            //we are also serializing the password, which is easy to
            //inspect by loading the binary data file.  (There is no
            //encryption or compression in the process.)
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Create("joe.serialized");
            formatter.Serialize(file, joe);
            file.Close();

            ReputationDB.ChangeReputation("Joe", 2);

            Thread.Sleep(2000);

            //Deserialize the user from the binary file.
            file = File.Open("joe.serialized", FileMode.Open);
            joe = (User)formatter.Deserialize(file);
            file.Close();

            //Note: the properties of the user have not changed
            //even though the reputation database has been updated.
            Console.WriteLine("After deserialization: " + joe);
        }
    }
}
