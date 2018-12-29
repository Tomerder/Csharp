using System;

namespace AppDomainExercise
{
    class Program
    {
        static void Main(string[] args)
        {
            Execute("Domain (Instance by value)", "AppDomainExercise", "AppDomainExercise.FooByValue");
            Execute("Domain (Instance by ref)", "AppDomainExercise", "AppDomainExercise.FooByRef");
            Execute("Other Domain (Instance by ref)", "AppDomainExercise", "AppDomainExercise.FooByRef");
        }

        static void Execute(string friendlyName, string assemblyName, string typeName)
        {
            try
            {
                AppDomain domain = AppDomain.CreateDomain(friendlyName);
                IFoo fromOtherDomain = (IFoo)domain.CreateInstanceAndUnwrap(assemblyName, typeName);
                fromOtherDomain.WriteDomainInfo();
                AppDomain.Unload(domain);
                fromOtherDomain.WriteDomainInfo();
            }

            catch (AppDomainUnloadedException)
            {
                Console.WriteLine("AppDomainUnloadedException");
            }        
        }
    }
}
