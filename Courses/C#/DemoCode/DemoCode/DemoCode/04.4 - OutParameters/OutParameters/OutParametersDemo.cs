using System;
using System.Collections.Generic;
using System.Text;

namespace OutParameters
{
    class OutParametersDemo
    {
        public static void GetValues1(ref int val1, ref int val2)
        {
            val1 = 1;
            val2 = 2;
        }

        public static void GetValues2(out int val1, out int val2)
        {
            val1 = 1;
            val2 = 2;

            // Note that if you forget to initialize one of the
            //  'out' parameters, the compiler will complain:
            //      error CS0177: The out parameter 'val2' must be assigned to before control leaves the current method
        }

        static void Main(string[] args)
        {
            // The following lines will not compile.  The compiler
            //  complains that we are attempting to use an unassigned
            //  variable:
            //      error CS0165: Use of unassigned local variable 'val1'
            //
            //int val1, val2;
            //GetValues1(ref val1, ref val2);

            // This will compile, but it's quite ugly, as the user
            //  shouldn't really initialize parameters that he expects
            //  to receive from the method.
            //
            int val1 = 0, val2 = 0;
            GetValues1(ref val1, ref val2);

            // This will compile, and is the appropriate way to do it.
            //
            int val3, val4;
            GetValues2(out val3, out val4);
        }
    }
}
