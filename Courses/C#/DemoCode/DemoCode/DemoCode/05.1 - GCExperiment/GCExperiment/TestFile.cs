using System;

namespace GCExperiment
{
    class TestFile
    {
        public TestFile(string name)
        {
            Console.WriteLine("Opening " + (_name = name));
        }

        // Non-implemented methods.
        //
        public void ProcessTitle() { }
        public void ProcessParagraph() { }

        // Note that we declare the destructor in C++ syntax.
        //  We should expect it to be called whenever the
        //  TestFile instance goes out of scope, or is deleted.
        //
        ~TestFile()
        {
            Console.WriteLine("Closing " + _name);
        }

        private string _name;
    }
}
