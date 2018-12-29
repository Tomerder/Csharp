using System;

namespace RegisterExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Publisher publisher = new Publisher();
            publisher._myEvent += (() => { Console.WriteLine("Hi from lambda"); });
            Console.ReadLine();
        }
    }
}
