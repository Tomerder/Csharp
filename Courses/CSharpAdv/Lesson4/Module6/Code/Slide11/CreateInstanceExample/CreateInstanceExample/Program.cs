using System;

namespace CreateInstanceExample
{
    /// <summary>
    /// This method demonstrates how the CreateInstanceAndUnwrap method can be used
    /// to create an instance of a type within another application domain.  The type
    /// created in this example is System.Int32 (a simple 'int').  Note that even after
    /// the application domain is unloaded, the integer value is still usable, indicating
    /// that it was copied to the calling domain.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain domain = AppDomain.CreateDomain("InstanceDomain");
            int intFromOtherDomain = (int)domain.CreateInstanceAndUnwrap(typeof(Int32).Assembly.FullName, typeof(Int32).FullName);
            AppDomain.Unload(domain);

            //Note: the value is still usable after the application domain was unloaded.
            //This means that the integer was COPIED from the target application domain
            //to this application domain.  This is possible because the Int32 type is
            //loaded in both application domains and is marked as serializable.
            Console.WriteLine(intFromOtherDomain);
        }
    }
}
