using System;
using System.Runtime.Serialization;

namespace CreateInstanceFail
{
    /// <summary>
    /// This method demonstrates that an object which is not marked as serializable
    /// (marshal-by-value) or marshal-by-reference cannot be passed across application
    /// domain boundaries.  This demo does not demonstrate the proper way to pass
    /// objects across this boundary - it only shows that it is not as simple as it
    /// may at first appear.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain domain = AppDomain.CreateDomain("InstanceDomain");
            try
            {
                Rectangle rect = (Rectangle)domain.CreateInstanceAndUnwrap(typeof(Rectangle).Assembly.FullName, typeof(Rectangle).FullName);
                Console.WriteLine("Rectangle area: " + rect.Area);
            }
            catch (SerializationException ex)
            {   //A serialization exception occurs because the Rectangle class
                //is not marked as serializable.  Therefore, there is no safe
                //way to transmit it across application domain boundaries.
                //Note that the Rectangle constructor executes, because there is 
                //no problem with loading the Rectangle type on the other side
                //and creating an instance of it.  The problem begins when the
                //instance is serialized to the caller.
                Console.WriteLine(ex);
            }
            finally
            {
                AppDomain.Unload(domain);
            }
        }
    }
}
