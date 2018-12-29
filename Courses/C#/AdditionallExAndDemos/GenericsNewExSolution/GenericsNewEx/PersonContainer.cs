using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenericsNewEx
{
    public class PersonContainer
    {
        private List<Person> _pList = new List<Person>();

        public void addPerson(Person p)
        {
            _pList.Add(p);
        }
        public void sort()
        {
            _pList.Sort();
        }
        public void sort(Comparison<Person> comp)
        {
            _pList.Sort(comp);
        }
        public void printContainer()
        {
            Console.WriteLine("This is the List:");
            Console.WriteLine("-----------------");
            foreach (Person p in _pList)
                Console.WriteLine(p);
        }
        public Person findPerson(Predicate<Person> cond)
        {
            return _pList.Find(cond);
            
        }
    }
}
