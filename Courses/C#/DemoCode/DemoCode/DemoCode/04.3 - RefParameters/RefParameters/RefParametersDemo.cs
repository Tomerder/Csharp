using System;
using System.Collections.Generic;
using System.Text;

namespace RefParameters
{
    class Util
    {
        // Doesn't actually swap because the parameters
        //  are value types and they are passed by value.
        //
        static public void Swap(int i1, int i2)
        {
            int tmp = i1;
            i1 = i2;
            i2 = tmp;
        }

        // The code inside the method isn't affected,
        //  but a reference to the variables is passed
        //  to the method.
        //
        static public void Swap(ref int i1, ref int i2)
        {
            int tmp = i1;
            i1 = i2;
            i2 = tmp;
        }
    }

    class RefParametersDemo
    {
        static void Main(string[] args)
        {
            int i1 = 5, i2 = 10;
            Console.WriteLine("i1 = {0} i2 = {1}", i1, i2);
            
            // The first method doesn't actually swap...
            //
            Util.Swap(i1, i2);
            Console.WriteLine("i1 = {0} i2 = {1}", i1, i2);
            
            // Note the use of the 'ref' keyword when
            //  calling the method, as well as when
            //  declaring it.
            //
            Util.Swap(ref i1, ref i2);
            Console.WriteLine("i1 = {0} i2 = {1}", i1, i2);
        }
    }
}
