using System;

namespace AppDomainInitializer
{
    /// <summary>
    /// Demonstrates how the AppDomainInitializer delegate can be used
    /// to execute bootstrapping code within an application domain.
    /// The delegate is passed along with the AppDomainSetup object when
    /// the application domain is created, and is invoked as part of
    /// the application domain's initialization process.  Parameters can
    /// be passed to the initialization delegate as an array of strings.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            AppDomainSetup setup = new AppDomainSetup();
            setup.AppDomainInitializer = OnAppDomainInitialization;
            setup.AppDomainInitializerArguments = new string[] { "Hello from the primary domain!" };
            AppDomain domain = AppDomain.CreateDomain("AnotherDomain", null, setup);
            AppDomain.Unload(domain);
        }

        /// <summary>
        /// This method is invoked within the second application domain.
        /// </summary>
        private static void OnAppDomainInitialization(string[] args)
        {
            Console.WriteLine("Initialization arguments: " + String.Join(" ", args));
            Console.WriteLine("Domain name: " + AppDomain.CurrentDomain.FriendlyName);
        }
    }
}
