using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DelegatesBeginInvokeExample
{
    public delegate void LoopPrinter(int count);

    class Program
    {
        static void Main(string[] args)
        {

	    // first, show running methods without the usage of delegates, no 
            // parallel execution

            //PrintInt(10);
            //PrintChar(10);
            //PrintHi(10);

            LoopPrinter printer1 = new LoopPrinter(PrintInt);
            LoopPrinter printer2 = new LoopPrinter(PrintChar);
            LoopPrinter printer4 = new LoopPrinter(PrintHi);
            
            // delegates, but no parallel
            //printer1.Invoke(10);
            //printer2.Invoke(10);
            //printer4.Invoke(10);

            printer1.BeginInvoke(100, null, null);
            printer2.BeginInvoke(50, null, null);
            printer4.BeginInvoke(10, null, null);


	    // delegate may work with instance method, not only static
            //MyClaSSS mcc = new MyClaSSS();
            //mcc.Name = "Michal";
            //LoopPrinter printer3 = new LoopPrinter(mcc.Do);
            //printer3(50);
            
            Console.ReadLine(); // so that our console will not close at end program
        }

        static void PrintInt(int count)
        {
            
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(i);
            }
        }

        static void PrintChar(int count)
        {
	    // for simplicity we will not do too many checks here
            
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine((char)i);
                
            }
        }
	static void PrintHi(int count)
        {
	    // for simplicity we will not do too many checks here
            
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine("Hi");
            }
        }
    }


    public class MyClaSSS
    {
        public string Name { set; get; }
        public void Do(int i)
        {
            Console.WriteLine("My name is: {0}", Name);
            Console.WriteLine(i * 10000);
        }
    }
}
