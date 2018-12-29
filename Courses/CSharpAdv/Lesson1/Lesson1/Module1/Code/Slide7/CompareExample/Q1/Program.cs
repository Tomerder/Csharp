namespace Q1
{
    class Program
    {
        static void Main(string[] args)
        {
            int numPfPeople = Person.NumOfPeople(); //0
            Person p1 = new Person("Amir");
            numPfPeople = Person.NumOfPeople(); // 1
            Person p2 = new Person("Adler");
            numPfPeople = Person.NumOfPeople(); // 2
            string name = p2.GetName();
        }
    }

    internal class Person
    {
        private string _name;
        private static int _numOfPeople;
        public Person(string name)
        {
            _name = name;
            _numOfPeople++;
        }

        public static int NumOfPeople()
        {
            return _numOfPeople;
        }

        public string GetName()
        {
            return _name;
        }
    }
}
