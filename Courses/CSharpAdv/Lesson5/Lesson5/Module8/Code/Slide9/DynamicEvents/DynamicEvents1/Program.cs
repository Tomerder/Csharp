using System;
using System.Reflection;

namespace DynamicEvents
{
    public delegate string LogMethodDelegate();

    class Program
    {
        static void Main(string[] args)
        {
            Box box = new Box { Name = "Chips", Volume = 2.0m };
            DynamicLog(box, "Log");
            Console.ReadLine();
        }

        private static void DynamicLog(object @object, string name)
        {
            Type t = Assembly.GetExecutingAssembly().GetType("DynamicEvents.Publisher");
            object publisher = Activator.CreateInstance(t);
            EventInfo @event = t.GetEvent("_myEvent");
            
            Type type = @object.GetType();
            MethodInfo logMethod = type.GetMethod(name);
            Delegate method = Delegate.CreateDelegate(@event.EventHandlerType, @object, logMethod);
            @event.AddEventHandler(publisher, method);
        }
    }
}
