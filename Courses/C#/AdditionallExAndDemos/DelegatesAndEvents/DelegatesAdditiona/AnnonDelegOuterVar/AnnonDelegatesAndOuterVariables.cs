using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnnonDelegatesAndOuterVariables
{
    public delegate int BinaryOp(int a);

    class Program
    {
        // one way to pass data to a delegate is using a global such as
        // a static class member, note globalInput1

        static string globalInput1;
        


        static void Main(string[] args)
        {
            
  	        Console.WriteLine("Please enter a number to add");
            globalInput1 = Console.ReadLine(); // assume data is OK for simplicity
            

            int res = addOperation(5);
            Console.WriteLine("result of adding 5 to the input is: {0}",res);



	        Console.WriteLine("Please enter another number to add");
            string Input2 = Console.ReadLine();
            
          
            Action action = delegate()
            {
                Console.WriteLine("in annon delegate, see how we can work with the input");
                Console.WriteLine(Input2);
            };
            // this was not active code. Nothing will happen till next command.

            action.Invoke();

            BinaryOp delOperation = delegate(int a)
            {
                Console.WriteLine("in annon delegate delOperation");
                int numToAdd = int.Parse(Input2);
                return a - numToAdd;
            };
            // this was not active code. Nothing will happen till next command.

	        delOperation(5);
        }

        // this method must use global data such as globalInput to operate
	    public static int addOperation(int a) {
		        int numToAdd = int.Parse(globalInput1);
                return a+ numToAdd;
        }

        
    }
}
