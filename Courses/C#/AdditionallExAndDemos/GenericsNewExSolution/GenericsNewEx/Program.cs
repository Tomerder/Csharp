using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenericsNewEx
{
    class Program
    {
        static void Main(string[] args)
        {
            Person p1 = new Person("John", 22, 10000);
            Person p2 = new Person("Albert", 42, 5000);
            Person p3 = new Person("Zvi", 20, 2000);
            Console.WriteLine(p1);

            PersonContainer pc = new PersonContainer();
            pc.addPerson(p1);
            pc.addPerson(p2);
            pc.addPerson(p3);
            pc.printContainer();

            Person pFound = pc.findPerson((p) => (p.Salary == 2000));
            Console.WriteLine("Person found is: " + pFound);
            pc.sort();
            pc.printContainer();
            pc.sort((pa,pb)=>(pa.Age.CompareTo(pb.Age)));
            pc.printContainer();
            
        }
    }
}
