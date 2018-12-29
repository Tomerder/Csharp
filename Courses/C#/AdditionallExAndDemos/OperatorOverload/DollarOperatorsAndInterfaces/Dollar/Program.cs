using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Dollar
{
    class Program
    {
        static void Main(string[] args)
        {

            Bank bank = new Bank();
            bank.add(new Dollar(1));
            bank.add(new Dollar(2));
            bank.add(new Dollar(3));
            bank.add(new Dollar(4));
            bank.add(new Dollar(5));
            bank.add(new Dollar(6));
          
            foreach (Dollar d in bank)
                Console.WriteLine("Dollar in bank: " + d.Value);

            IEnumerator myenum1 =   bank.GetEnumerator();
            IEnumerator myenum2 = bank.GetEnumerator();
            IEnumerator myenum3 = bank.GetEnumerator();

            myenum1.MoveNext();
            Console.WriteLine("Dollar in bank according to enumerator1: " + ((Dollar)(myenum1.Current)).Value);

            myenum2.MoveNext();
            myenum2.MoveNext();
            myenum2.MoveNext();
            Console.WriteLine("Dollar in bank according to enumerator2: " + ((Dollar)(myenum2.Current)).Value);


            myenum3.MoveNext();
            myenum3.MoveNext();
            myenum3.MoveNext();
            myenum3.MoveNext();
            Console.WriteLine("Dollar in bank according to enumerator3: " + ((Dollar)(myenum3.Current)).Value);
            Console.WriteLine("Dollar in bank according to enumerator1: " + ((Dollar)(myenum1.Current)).Value);




        }
    }
}
