using System;
using System.Threading;

namespace InvokingEvents
{
    
    class Program
    {
        static void Main(string[] args)
        {
            Publisher publisher = new Publisher();
            publisher._del += F;
            publisher._del += G;
            publisher.RaiseEvent();
            Console.ReadLine();
        }

        static void F(int x)
        {
            for (int i = 0; i < x; i++)
            {
                Console.Write("F");
                Thread.Sleep(1000);
            }
        }

        static void G(int x)
        {
            for (int i = 0; i < x; i++)
            {
                Console.Write("G");
                Thread.Sleep(1000);
            }
        }
    }
}
