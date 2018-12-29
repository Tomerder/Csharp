using System;

namespace GCExperiment
{
    // Note that now our class has to implement the IDisposable
    //  interface, so that we can
    //  use it inside the 'using' statement.
    //
    class TestFile : IDisposable
    {
        public TestFile(string name)
        {
            Console.WriteLine("Opening " + (_name = name));
        }

        // Non-implemented methods.
        //
        public void ProcessTitle() { }
        public void ProcessParagraph() { }

        // This is the one method the IDisposable interface
        //  contains, so we implement it implicitly.
        //
        public void Dispose()
        {
            Console.WriteLine("Closing " + _name);

            // Don't forget to ask the GC to remove our object
            //  from the finalization queue, because every resource
            //  we had was already disposed of in this method.
            //
            GC.SuppressFinalize(this);
        }

        ~TestFile()
        {
            // Now, the finalizer ('destructor') merely calls
            //  Dispose.  However, if the user calls Dispose
            //  first, the finalizer will not be invoked at all.
            //
            Dispose();
        }

        private string _name;
    }
}
