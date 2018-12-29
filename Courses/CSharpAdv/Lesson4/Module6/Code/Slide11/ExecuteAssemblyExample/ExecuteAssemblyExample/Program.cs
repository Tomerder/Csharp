using System;
using System.Reflection;

namespace ExecuteAssemblyExample
{
    /// <summary>
    /// This method demonstrates how the ExecuteAssembly instance method
    /// can be used to execute an arbitrary assembly within the boundaries of
    /// another application domain.  
    /// The ExecuteAssembly method executes synchronously and returns only
    /// when the assembly's entry point has completed execution.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            //Used when this Main method is invoked from another application
            //domain and not when the application is executed directly.
            if (args.Length > 0 && args[0] == "fromDomain")
            {
                Console.WriteLine("My domain name: " + AppDomain.CurrentDomain.FriendlyName);
                return;
            }

            Assembly thisAssembly = Assembly.GetExecutingAssembly();

            AppDomain domain = AppDomain.CreateDomain("ExecuteDomain");
            domain.ExecuteAssembly(thisAssembly.Location, new string[] { "fromDomain" });
            AppDomain.Unload(domain);
        }
    }
}
