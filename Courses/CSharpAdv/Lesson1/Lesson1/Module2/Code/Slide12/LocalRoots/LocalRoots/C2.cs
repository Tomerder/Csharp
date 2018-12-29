using System;

namespace LocalRoots
{
    class C2
    {
        public C1 C { get; set; }

        ~C2()
        {
            Console.WriteLine("Finalizer of C2 is called");
        }
    }
}
