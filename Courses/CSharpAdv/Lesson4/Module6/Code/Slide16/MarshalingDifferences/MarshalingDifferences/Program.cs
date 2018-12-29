using System;

namespace MarshalingDifferences
{
    /// <summary>
    /// This demo shows the difference between marshaling objects by value as opposed
    /// to marshaling objects by reference.  Two versions of the Rectangle class, one
    /// called RectangleMBV and another called RectangleMBR demonstrate that an MBV
    /// object is copied across application domain boundaries, while an MBR object is
    /// referenced from other application domains and can be changed from other application
    /// domains as if it belonged there.
    /// </summary>
    class Marshaling
    {
        static void Main(string[] args)
        {
            MarshalByValue();
            MarshalByReference();
        }

        /// <summary>
        /// Demonstrates that a marshal-by-value instance created and copied from another
        /// application domain is a local copy which does not affect the original object
        /// belonging in its own domain.  The DomainRepresentative class is used to create
        /// an instance within the other application domain and to display its contents.
        /// </summary>
        private static void MarshalByValue()
        {
            AppDomain domain = AppDomain.CreateDomain("MBV");
            DomainRepresentative rep = (DomainRepresentative)domain.CreateInstanceAndUnwrap(
                typeof(DomainRepresentative).Assembly.FullName, typeof(DomainRepresentative).FullName);

            RectangleMBV copy = rep.GetRectangleMBV();
            Console.WriteLine("Local instance: " + copy);
            rep.PrintRectangleMBV();

            //The remote instance is not affected by these lines because
            //we're working with a local copy that was passed to us by value.
            //This is the essence of marshal-by-value.
            copy.Height = 5;
            copy.Width = 6;
            Console.WriteLine("Local instance: " + copy);
            rep.PrintRectangleMBV();
            AppDomain.Unload(domain);
        }

        /// <summary>
        /// Demonstrates that a marshal-by-reference instance is not copied from its application
        /// domain, but instead a reference to it is passed to the caller.  Changes made to
        /// the reference (also called a proxy) affect the original object which still resides
        /// in its domain of origin.
        /// </summary>
        private static void MarshalByReference()
        {
            AppDomain domain = AppDomain.CreateDomain("MBR");
            DomainRepresentative rep = (DomainRepresentative)domain.CreateInstanceAndUnwrap(
                typeof(DomainRepresentative).Assembly.FullName, typeof(DomainRepresentative).FullName);

            RectangleMBR proxy = rep.GetRectangleMBR();
            Console.WriteLine("Local instance: " + proxy);
            rep.PrintRectangleMBR();

            //The remote instance is affected by our work on the Height
            //and Width properties because we have a local proxy to the object
            //which actually lives in another application domain.  There is 
            //only one instance of the RectangleMBR class here - it's in
            //the second domain!  We're working with a cross-domain reference
            //to it, and that's the essence of marshal-by-reference.
            proxy.Height = 5;
            proxy.Width = 6;
            Console.WriteLine("Local instance: " + proxy);
            rep.PrintRectangleMBR();
            AppDomain.Unload(domain);
        }
    }
}
