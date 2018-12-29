using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dynamic;

namespace DynamicObjectDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            dynamic magicClass = DynamicDictionary.Create();
            magicClass.Name = "My Name";
            magicClass.Age = 25;
            
            magicClass.Print = new Action<string>((string world) => Console.WriteLine(world));
            
            magicClass.SetAge = new Action(() =>
            {
                Console.Write("Enter your age: ");
                magicClass.Age = int.Parse(Console.ReadLine());
            });




            Console.WriteLine(magicClass.Name);
            Console.WriteLine(magicClass.Age);

            magicClass.Print("Hello");

            magicClass.SetAge();
            Console.WriteLine(magicClass.Age);
        }
    }

    public class DynamicDictionary : DynamicObject
    {
        private DynamicDictionary()
        {

        }

        public static dynamic Create()
        {
            return new DynamicDictionary();    
        }

        private Dictionary<string, object> data = new Dictionary<string, object>();

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if (!data.ContainsKey(binder.Name))
            {
                data.Add(binder.Name, value);
            }
            else
            {
                data[binder.Name] = value;
            }

            return true;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (!data.ContainsKey(binder.Name))
            {
                return base.TryGetMember(binder, out result);
            }

            result = data[binder.Name];
            return true;
        }

    }

}
