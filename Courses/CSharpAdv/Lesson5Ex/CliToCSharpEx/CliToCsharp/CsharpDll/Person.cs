
namespace CsharpDll
{
    public class Person
    {
        private string _firstName;

        public string FirstName
        {
            get { return _firstName; }
        }

        private string _lastName;

        public string LastName
        {
            get { return _lastName; }
        }

        public Person(string firstName, string lastName)
        {
            _firstName = firstName;
            _lastName = lastName;
        }

        public string GetFullName()
        {
            return _firstName + " " + _lastName;
        }
    }
}
