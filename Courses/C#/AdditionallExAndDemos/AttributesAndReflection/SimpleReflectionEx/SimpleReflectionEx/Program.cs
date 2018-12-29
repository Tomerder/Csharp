using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleReflectionEx
{
    public class TestClass
    {
        public void func1() { }
        public void func2() { }
        private int _f1; // just to that we will see them in the test reflector
        private int _f2;

        public void test()
        {
            Console.WriteLine("in test");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {

            TestClass t1 = new TestClass();
            Reflector rf = new Reflector(t1);
            rf.showAllFields();
            rf.showAllMethods();
            rf.activateTestMethod();

            Console.ReadLine();
        }
    }
}
