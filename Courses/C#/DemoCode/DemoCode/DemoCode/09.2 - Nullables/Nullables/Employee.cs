using System;

namespace Nullables
{
    // This is the definition of an employee.  It's a value
    //  type that contains a name and an id.
    //
    struct Employee
    {
        public string Name;
        public int Id;

        public Employee(string name, int id)
        {
            Name = name;
            Id = id;
        }
    }
}
