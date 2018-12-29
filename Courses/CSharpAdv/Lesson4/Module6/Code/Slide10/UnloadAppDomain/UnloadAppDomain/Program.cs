using System;

namespace UnloadAppDomain
{
    /// <summary>
    /// Demonstrates how an application domain is created and how
    /// its properties can be queried and displayed.  Also demonstrates
    /// how to retrieve the assemblies loaded into an application domain
    /// and how to unload the application domain.  Note that after
    /// unloading an application domain any attempt to access it will
    /// result in an AppDomainUnloadedException exception.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain domain =
                AppDomain.CreateDomain("MyFirstDomain");
            Console.WriteLine("Base directory: " +
                domain.BaseDirectory);
            Console.WriteLine("Id: " + domain.Id);
            Console.WriteLine("Configuration file: " + domain.SetupInformation.ConfigurationFile);

            Array.ForEach(
                domain.GetAssemblies(), Console.WriteLine);
            //Output:
            //mscorlib, Version=2.0.0.0, Culture=neutral,
            //PublicKeyToken=b77a5c561934e089

            AppDomain.Unload(domain);
            domain.GetAssemblies();
            //Output:
            //Unhandled Exception:
            //System.AppDomainUnloadedException:
            //Attempted to access an unloaded AppDomain.
            //  at System.AppDomain.GetAssemblies()
        }
    }
}
