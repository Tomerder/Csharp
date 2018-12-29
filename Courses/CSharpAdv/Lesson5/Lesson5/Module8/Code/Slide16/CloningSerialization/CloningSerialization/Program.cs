using System;

namespace CloningSerialization
{
    /// <summary>
    /// Demonstrates serialization through cloning.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Box box = new Box { Name = "Chips", Volume = 0.2f };
            Box copy = (Box)box.Clone();    //Or could call box.Clone<Box>() directly here

            Console.WriteLine("Old box: " + box + ", new box: " + copy);
        }
    }
}
