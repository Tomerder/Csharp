using System;

namespace GCExperiment
{
    class GCExperimentMain
    {
        static void Main(string[] args)
        {
            Func1();
            Console.WriteLine("Back to main, this is the last line of Main");

            // In the output, you will see that the 'destructor'
            //  we defined earlier is only called after this point,
            //  i.e. when the program terminates.  This is due to
            //  the fact that the 'destructor' (finalizer) is called
            //  by the .NET Garbage Collector (GC), so it is invoked
            //  non-deterministically and not on function exit.
            //
        }

        private static void Func1()
        {
            Console.WriteLine("Enter function Func1()");
		    TestFile f1 = new TestFile("file1.txt");
    		f1.ProcessTitle();
 		    f1.ProcessParagraph();
		    Func2();
		    Console.WriteLine("Leave function Func1()");
        }

	    private static void Func2()
	    {
		    Console.WriteLine("Enter function Func2()");
		    TestFile f2 = new TestFile("file2.txt");
 		    f2.ProcessTitle();
 		    f2.ProcessParagraph();
		    Console.WriteLine("Leave function Func2()");
	    }
    }
}
