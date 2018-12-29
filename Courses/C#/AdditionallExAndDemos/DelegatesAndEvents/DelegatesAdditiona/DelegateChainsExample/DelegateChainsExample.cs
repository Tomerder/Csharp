using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DelegateChainsExample
{
    delegate int MathAction(int a, int b);

    class Program
    {

        static void Main(string[] args)
        {
            MathAction m1 = new MathAction(Add);
            m1 += Sub;

            m1 += (x,y)=> x*y;

           
            Console.WriteLine(m1.Invoke(2, 4));

            // Demonstrate invocation lists
            Delegate[] invocationList = m1.GetInvocationList();
            foreach (var del in invocationList)
            {

                int result = 0;
                MathAction ma = (MathAction)del;
               // ma(2, 4);

                if (ma.Method.Name.Equals("Add")) ;
                else
                    result = ma(2, 4);
                Console.WriteLine(result);




                // if we do not know the type of the delegate
                // in compilation, we might want to use
                // DynamicInvoke which is more flexible than
                // specifically invoking a certain delegate
                //del.DynamicInvoke(new object[] { 2, 4 });
            }
        }

        static int Add(int a, int b)
        {
            Console.WriteLine("In Add");
            return a + b;
        }

        static int Sub(int a, int b)
        {
            Console.WriteLine("In Sub");
            return a - b;
        }
    }
}
