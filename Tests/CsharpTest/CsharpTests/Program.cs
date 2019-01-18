using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpTests
{
    class Program
    {
        static void Main(string[] args)
        {
            //test ref/value (Dictionary,String,int...)
            TestParamsTypes();          
        }

        private static void TestParamsTypes()
        {
            int num = 5;
            TestIntParamByValue(num);
            Console.WriteLine("Int Param was not changed because int is value type: " + num);

            num = 5;
            TestIntParamByRef(ref num);
            Console.WriteLine("Int Param was  changed because passed as ref: " + num);

            string str = "XXX";
            TestStrParamByValue(str);
            Console.WriteLine("Str Param was not changed because string is mutable: " + str);

            str = "XXX";
            TestStrParamByRef(ref str);
            Console.WriteLine("Str Param was  changed because passed as ref: " + str);

            A a = new A(5);
            TestAParamByValue(a);
            Console.WriteLine("A Param was changed because A is ref type: " + a);

            a.a = 5;
            TestAParamByRef(ref a);
            Console.WriteLine("A Param was realocated because A is ref type passed by ref: " + a);

        }

        static void TestIntParamByValue(int _num)
        {
            _num = 7;
        }

        static void TestIntParamByRef(ref int _num)
        {
            _num = 7;
        }

        static void TestStrParamByValue(string _str)
        {
            _str = "YYY";
        }

        static void TestStrParamByRef(ref string _str)
        {
            _str = "YYY";
        }

        static void TestAParamByValue(A _a)
        {
            _a.a = 7;
            _a = new A(77);
        }

        static void TestAParamByRef(ref A _a)
        {
            _a.a = 7;
            _a = new A(77);
        }
    }


    public class A
    {
        public int a;

        public A(int _num)
        {
            a = _num;
        }

        public override string ToString()
        {
            return a.ToString();
        }
    }
}
