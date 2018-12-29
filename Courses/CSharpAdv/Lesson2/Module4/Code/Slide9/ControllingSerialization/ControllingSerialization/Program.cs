using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace ControllingSerialization
{
    class Program
    {
        static void Main(string[] args)
        {
            User2 joe = new User2("Joe", "123456");
            Console.WriteLine("Before serialization: " + joe);

            //Serialize the user to a binary file.  This time, the
            //password is not serialized to the file, which is easy
            //to see by inspecting it.
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Create("joe2.serialized");
            formatter.Serialize(file, joe);
            file.Close();

            ReputationDB.ChangeReputation("Joe", 2);

            Thread.Sleep(2000);

            //Deserialize the user from the binary file.
            file = File.Open("joe2.serialized", FileMode.Open);
            joe = (User2)formatter.Deserialize(file);
            file.Close();

            //Note: the properties of the user have not changed
            //even though the reputation database has been updated.
            Console.WriteLine("After deserialization: " + joe);
        }
    }
}
