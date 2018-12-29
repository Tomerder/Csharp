using System;

namespace CreatingAppDomain
{
    /// <summary>
    /// Demonstrates how an application domain is created and how
    /// its properties can be queried and displayed.   
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
        }
    }
}
