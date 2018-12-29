using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenericsNewEx
{
    public class PersonContainer
    {
        // Students: TODO : add a member which is
        // a List<Person>

        public void addPerson(Person p)
        {
            // Students: TODO: add the person to the list
        }
        public void sort()
        {
            // Students: TODO: make the list sorted
            // using the List Sort method
        }
        public void printContainer()
        {
            // Students: TODO: print the list
        }
        public Person findPerson(Predicate<Person> cond)
        {
            return null;
            // Students: TODO: find a person
            // in the list given the predicate.
            // Use list Find method.
        }
    }
}
