using System;

namespace GCExperiment
{
    class GCExperimentMain
    {
        static void Main(string[] args)
        {
            Func1();
            Console.WriteLine("Back to main, this is the last line of Main");

            // This time, this will be last line you will see because
            //  resources are disposed of deterministically, using
            //  the TestFile.Dispose method.
        }

        private static void Func1()
        {
            Console.WriteLine("Enter function Func1()");
		    TestFile f1 = new TestFile("file1.txt");
    		f1.ProcessTitle();
 		    f1.ProcessParagraph();
		    Func2();
            f1.Dispose();   // Explicitly dispose of the f1 instance.
		    Console.WriteLine("Leave function Func1()");
        }

	    private static void Func2()
	    {
		    Console.WriteLine("Enter function Func2()");
            
            // The using statement makes sure to call
            //  TestFile.Dispose when it ends.
            //
            using (TestFile f2 = new TestFile("file2.txt"))
            {
                f2.ProcessTitle();
                f2.ProcessParagraph();
            }
		    Console.WriteLine("Leave function Func2()");
	    }
    }
}
