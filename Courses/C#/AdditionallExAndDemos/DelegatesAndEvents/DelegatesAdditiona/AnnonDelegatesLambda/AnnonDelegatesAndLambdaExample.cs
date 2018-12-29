using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnnonDelegatesAndLambdaExample
{
    class Program
    {
       static void Main(string[] args)
       {

	        List<int> list = new List<int>() { 10, 11, 2, 5, 6, 22, 7, 91, 3 };

	        Console.WriteLine("Printing the original list");
	        PrintList(list);


		    // demonstrate some methods to define delegates, and pass them
            // to the list FindAll method which expects such predicate

            // define a proper method, lessThanVal and create a delegate instance
            // (with the method as the parameter for ctor) to FindAll in List

            List<int> subList1 = list.FindAll(lessThanVal);

	        Console.WriteLine("Printing SubList1 using the proper lessThanVal 10 method");
	        PrintList(subList1 );

            // instead of creating a proper method, just use an annonymouse delegate!
            List<int> subList2 = list.FindAll(delegate(int item)
                                            {
                                                return item < 10;
                                            });

 	        Console.WriteLine("Printing SubList2 using the annon delegate");
	        PrintList(subList2 );

            // simplest for of an annonymouse delegate: use lambda expression
            // as simple syntax for annon. delegate!
            List<int> subList3 = list.FindAll(x => x < 10);

            Console.WriteLine("Printing SubList3 using the lambda expression");
	        PrintList(subList3 );


            // some more lambda expressions to demo how simple and readable
            // it is to create delegates this way!


            // creating a list with initializers
            List<Person> people = new List<Person>()
            {
                new Person() { Name = "Abby", Age = 65 },
                new Person() { Name = "MiddleMan", Age = 15 },
                new Person() { Name = "Zoe", Age = 20 }
            };
            Console.WriteLine("Printing people list , original list");
	        PrintList(people );

            // one way of using the list Sort is to define a proper delegate
            // based on a proper method
            people.Sort(comparePeopleByName);
            Console.WriteLine("Printing people list sorted by name");
	        PrintList(people );

            // defining the sort method with lambda exp as delegate
            people.Sort((e1, e2) => e1.Name.CompareTo(e2.Name));
            Console.WriteLine("Printing people list sorted by name");
	        PrintList(people );

            people.Sort((e1, e2) => e1.Age.CompareTo(e2.Age));
            Console.WriteLine("Printing people list sorted by Age");
	        PrintList(people );

            // note usage of "var"
            // example of Max method on list, using lambda to define the delegate
            var max = people.Max(x => x.Age);
            var e = people.First(x => x.Age > 20);
        }

       
	   public static int comparePeopleByName (Person p1, Person p2)  {
		    return p1.Name.CompareTo(p2.Name);
        }

        public static bool lessThanVal(int item)
        {
            return item < 10;
        }
	    public static void PrintList<T>(List<T> theList)
        {
		    Console.WriteLine("Printing the list");

		    foreach (var item in theList)
			    Console.WriteLine(item);
	    }
    }

    public class Person
    {
        public string Name { get; set; }
        public float Age { get; set; }

        public override string ToString()
        {
            return string.Format("Name {0}, Age {1}", Name, Age);
        }
    }
}
