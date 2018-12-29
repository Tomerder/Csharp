using System;

namespace RegisterExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Publisher publisher = new Publisher();
            publisher._myEvent += delegate() { Console.WriteLine("Hi from anonymous method"); };
            Console.ReadLine();
        }
    }
}
