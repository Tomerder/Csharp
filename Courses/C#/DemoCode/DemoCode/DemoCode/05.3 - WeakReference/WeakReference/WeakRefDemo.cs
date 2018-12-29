using System;
using System.Collections.Generic;
using System.Text;

namespace WeakReferences
{
    class HugeObject
    {
        // Our object holds a buffer one megabyte large.
        //
        private byte[] _memory = new byte[1048576];
    }

    class WeakRefDemo
    {
        static void Main(string[] args)
        {
            // We allocate our huge object...
            HugeObject hugeObj = new HugeObject();
            
            // We create a weak reference to it, meaning
            //  that the GC mechanism may collect it if
            //  it has to.
            //
            WeakReference weakRef = new WeakReference(hugeObj);
            
            // We remove the last 'strong' root to the object,
            //  leaving only the weak reference pointing to it.
            //
            hugeObj = null;

            // The following line asks the Garbage Collector to
            //  collect memory from all generations, and therefore
            //  we can be sure that the WeakReference will be pointing
            //  to an invalid object.  If we want, however, to be
            //  less deterministic, we can comment the following line.
            //
            //GC.Collect();

            // Now we must check to see whether the GC mechanism
            //  has collected our object, or whether we can access
            //  it from the cached copy inside the WeakReference.
            //
            HugeObject oldObj = weakRef.Target as HugeObject;
            if (oldObj == null)
            {
                Console.WriteLine("The object was freed, I have to create it");
                oldObj = new HugeObject();
            }

            // Work as usual with the object.
        }
    }
}
