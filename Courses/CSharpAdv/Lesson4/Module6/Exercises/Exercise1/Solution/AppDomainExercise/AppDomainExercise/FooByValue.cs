using System;

namespace AppDomainExercise
{
    [Serializable]
    public class FooByValue : IFoo
    {
        public FooByValue()
        {
            Console.WriteLine("FooByValue");
        }

        public void WriteDomainInfo()
        {
            Console.WriteLine("Domain name: " + AppDomain.CurrentDomain.FriendlyName);
        }
    }
}
