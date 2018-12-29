using System;

namespace Boxing
{
    class BoxingDemo
    {
        static void Main(string[] args)
        {
            int i = 42;

            // No boxing: Console.WriteLine has an overload that
            //  accepts an int (System.Int32) parameter, so there's
            //  no need to wrap the int as an object.
            //
            Console.WriteLine(i);

            // Explicit boxing: assigning a value-type (int) to
            //  a reference-type variable (object).  After this
            //  statement, o references a new instance allocated
            //  on the managed heap.
            //
            object o = i;

            // The following statement only modifies the copy
            //  that resides on the managed heap.  The original
            //  i variable is not influenced.
            //
            o = 5;

            // Boxing: Console.WriteLine(string format, object arg0)
            //  takes an object parameter, which means i has to
            //  be boxed before passing itself to the method.
            //
            Console.WriteLine("{0}", i);

            // The following line would not compile because without
            //  an explicit cast, the compiler cannot tell whether
            //  the object variable o contains an integer.
            //
            //int j = o;

            // The following line has an explicit cast and therefore
            //  involves unboxing.  The value o references is copied
            //  from the managed heap to the variable j that is allocated
            //  on the method's stack.
            //
            int j = (int)o;

            // The following code will throw an InvalidCastException
            //  during runtime, because o2 does not contain a boxed
            //  variable of type int (System.Int32).
            //
            object o2 = new object();
            int j2 = (int)o2;
        }
    }
}
