using System;

namespace DoCallBackExample
{
    /// <summary>
    /// This example demonstrates how the DoCallback instance method can be used to execute
    /// an arbitrary delegate (with no parameters and a void return value) within another
    /// application domain.  
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain domain = AppDomain.CreateDomain("CallbackDomain");
            domain.DoCallBack(TheCrossDomainCallback);
            AppDomain.Unload(domain);
        }

        /// <summary>
        /// This is the callback method which is executed in the target application domain.
        /// Note that because it has no parameters and a void return value it cannot be used
        /// to communicate information into and outside from the application domain.  If this
        /// is desired, then the callback should be placed inside a class (which is either
        /// serializable or marshal-by-reference) and act as an instance method by modifying
        /// the data on the class for output purposes and reading the data on the class for
        /// input purposes.
        /// </summary>
        private static void TheCrossDomainCallback()
        {
            Console.WriteLine("Callback executes in domain: " + AppDomain.CurrentDomain.FriendlyName);
        }
    }
}
