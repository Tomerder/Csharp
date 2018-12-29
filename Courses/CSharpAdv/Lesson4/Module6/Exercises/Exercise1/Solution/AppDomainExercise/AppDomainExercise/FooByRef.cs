using System;

namespace AppDomainExercise
{
    public class FooByRef : MarshalByRefObject, IFoo
    {
        public FooByRef()
        {
            Console.WriteLine("FooByRef");
        }

        public void WriteDomainInfo()
        {
            Console.WriteLine("Domain name: " + AppDomain.CurrentDomain.FriendlyName);
        }
    }
}
