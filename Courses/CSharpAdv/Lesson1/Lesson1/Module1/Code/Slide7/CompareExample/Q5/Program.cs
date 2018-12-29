using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q2
{
    class Program
    {
        static void Main(string[] args)
        {
            Manager m1 = Manager.Instance();
            Manager m2 = Manager.Instance();
        }
    }

    class Manager
    {
        private static Manager _manager = new Manager(); // go to DB and get some data or go to network...

        private Manager()
        {
            Console.WriteLine("instance ctor called");
        }

        public static Manager Instance()
        {
            return _manager;
        }
    }
}
