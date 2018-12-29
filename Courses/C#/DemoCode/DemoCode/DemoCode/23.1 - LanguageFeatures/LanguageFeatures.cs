using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Reflection;
using System.Threading;
using System.Linq.Expressions;

namespace Ch2_LanguageFeatures
{
    class LanguageFeatures
    {
        private static int CalcSalary()
        {
            return 0;
        }

        static void Main(string[] args)
        {
            DeveloperProductivity();

            Extensibility();

            FunctionalProgramming();

            QueryOperators();
        }

        private static void DeveloperProductivity()
        {
            Person p = new Person
            {
                Id = 2,
                Name = "John Doe"
            };

            List<int> nums = new List<int>
            {
                5, 3, 20
            };
            Dictionary<string, int> dict = new Dictionary<string, int>
            {
                { "A", 1 },
                { "B", 2 }
            };

            MyCollection coll = new MyCollection
            {
                1,2,3
            };

            MyCollection2 coll2 = new MyCollection2
            {
                1,2,3
            };

            var i = 5;
            var s = "Hello";
            var e = new Employee();
            var dict2 = new Dictionary<int, List<string>>();

            Console.WriteLine(i.GetType().Name);
            Console.WriteLine(s.GetType().Name);

            var pi = new
            {
                Name = "John",
                Pay = CalcSalary()
            };
            Console.WriteLine(pi.GetType().FullName);

            var pi2 = new
            {
                Name = 5,
                Pay = 3.0f
            };
            Console.WriteLine(pi2.GetType().FullName);
        }

        private static void Extensibility()
        {
            string str = Console.ReadLine();
            Console.WriteLine(str.ToFloat().Round());

            int i = 5;  //int implements IComparable<int>
            i.IsGreaterThan(4); //Extensions.IsGreaterThan<int>
            i.IsEqualTo(6); //Extensions.IsEqualTo<int>

            Button_TemplateMethod buttonTemplate = new ConcreteButton_TemplateMethod();
            buttonTemplate.Click();

            Button_Events buttonEvents = new Button_Events();
            buttonEvents.OnClicking += delegate { Console.WriteLine("Button.OnClicking"); };
            buttonEvents.OnClicked += delegate { Console.WriteLine("Button.OnClicked"); };
            buttonEvents.Click();

            Button_Partial buttonPartial = new Button_Partial();
            buttonPartial.Click();
        }

        private static void FunctionalProgramming()
        {
            //C# 1.0 version
            MyConverter converter1 = new MyConverter(Converters.IntToStringConverter);
            Console.WriteLine(converter1(5));

            //C# 2.0 version - method group type inference
            MyConverter converter2a = Converters.IntToStringConverter;
            Console.WriteLine(converter2a(5));
            
            //C# 2.0 version - anonymous method
            MyConverter converter2b = delegate(int i) { return i.ToString(); };
            Console.WriteLine(converter2b(5));

            //C# 3.0 version - lambda expression with explicit argument type
            MyConverter converter3a = (int x) => x.ToString();
            Console.WriteLine(converter3a(5));
            
            //C# 3.0 version - lambda expression with explicit variable
            Func<int, string> converter3b = x => x.ToString();
            Console.WriteLine(converter3b(5));

            Action<int> print = i => Console.WriteLine(i);

            bool stop = false;
            Thread t = new Thread(delegate() {
                while (!stop){
                    Console.WriteLine("Thread working...");
                    Thread.Sleep(1000);
                }
            });
            t.Start();
            Console.ReadKey();
            stop = true;
            t.Join();

            List<int> numbers = new List<int> { 1, 2, 3, 4 };
            numbers.Filter(i => i % 2 == 0).ForEach(Console.WriteLine);

            //This prints 100, 100 times (because of capturing).
            //To fix, declare int copy = i inside the loop
            //and use the copy inside the lambda.
            for (int i = 0; i < 100; ++i)
            {
                ThreadPool.QueueUserWorkItem((o) =>
                {
                    Thread.Sleep(100);
                    Console.WriteLine(i);
                });
            }
            Console.ReadKey();

            Expression<Func<int, int, int>> mul = (x, y) => x * y;
            Func<int, int, int> compiled = mul.Compile();
            Console.WriteLine(compiled(5, 3));

            //Constructing the tree by hand:
            ParameterExpression xParameter = Expression.Parameter(typeof(int), "x");
            Expression<Func<int, int>> square =
                Expression.Lambda<Func<int,int>>(
                    Expression.Multiply(xParameter, xParameter),
                    xParameter);
            Console.WriteLine(square.Compile()(5));
        }
        
        private static void QueryOperators()
        {
            var customers = new[] {
                new { Name = "John", Balance = 100, Id = 1611 },
                new { Name = "Mary", Balance = -20, Id = 1444 },
                new { Name = "Bob", Balance = 40, Id = 1233 }
            };
            var query = from cust in customers
                        where cust.Balance > 0
                        orderby cust.Name
                        select new { cust.Name, cust.Balance };
            query.ToList().ForEach(cust =>
                Console.WriteLine("We appreciate customer {0} with balance {1}", cust.Name, cust.Balance));
        }
    }

    #region Developer Productivity

    public class Employee { }

    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    class MyCollection2 : IEnumerable
    {
        public void Add(int i) { }

        #region IEnumerable Members

        public IEnumerator GetEnumerator()
        {
            return null;
        }

        #endregion
    }

    class MyCollection : ICollection<int>
    {

        #region ICollection<int> Members

        public void Add(int item)
        {
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(int item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(int[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(int item)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable<int> Members

        public IEnumerator<int> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    #endregion

    #region Functional Programming

    static class Converters
    {
        public static string IntToStringConverter(int i)
        {
            return i.ToString();
        }
    }

    static class ListExtensions
    {
        public static List<int> Filter(this List<int> list, Predicate<int> filter)
        {
            List<int> filtered = new List<int>();
            list.ForEach(i =>
            {
                if (filter(i))
                    filtered.Add(i);
            });
            return filtered;
        }
    }

    //C# 1.0 version
    public delegate string MyConverter(int num);

    //C# 2.0 generic version
    public delegate TOutput MyConverter<TInput, TOutput>(TInput input);
    //There's also a Converter<TInput, TOutput> built-in delegate

    //C# 3.0 version - part of System.Core
    //public delegate TResult Func<T, TResult>(T arg);

    #endregion

    #region Extensibility

    #region Template Method

    public abstract class Button_TemplateMethod //Could be made non-abstract
    {
        public void Click()
        {
            OnClicking();
            Console.WriteLine("Button.Click work");
            OnClicked();
        }
        protected abstract void OnClicking();   //Could be made virtual with an empty implementation
        protected abstract void OnClicked();    //Could be made virtual with an empty implementation
    }
    public sealed class ConcreteButton_TemplateMethod : Button_TemplateMethod
    {
        protected override void OnClicking()
        {
            Console.WriteLine("ConcreteButton.OnClicking");
        }

        protected override void OnClicked()
        {
            Console.WriteLine("ConcreteButton.OnClicked");
        }
    }

    #endregion

    #region Events

    public sealed class Button_Events
    {
        public void Click()
        {
            OnClicking(this, EventArgs.Empty);
            Console.WriteLine("Button.Click work");
            OnClicked(this, EventArgs.Empty);
        }
        public event EventHandler OnClicking = delegate { };
        public event EventHandler OnClicked = delegate { };
    }

    #endregion

    #region Partial Methods

    public sealed partial class Button_Partial
    {
        public void Click()
        {
            OnClicking();
            Console.WriteLine("Button.Click work");
            OnClicked();
        }
        partial void OnClicking();
        partial void OnClicked();
    }
    partial class Button_Partial    //Second part, could be in separate .cs file
    {                               //but within the SAME assembly
        partial void OnClicking()
        {
            Console.WriteLine("Button.OnClicking");
        }
        //partial void OnClicked()
        //{
        //    Console.WriteLine("Button.OnClicked");
        //}
    }

    #endregion

    #region Extension Methods

    static class Extensions
    {
        public static float ToFloat(this string s)
        {
            return float.Parse(s);
        }
        public static int Round(this float f)
        {
            return (int)Math.Round(f, MidpointRounding.AwayFromZero);
        }

        //Generic extension methods
        public static bool IsGreaterThan<T>(this IComparable<T> left, T right)
        {
            return left.CompareTo(right) > 0;
        }
        public static bool IsEqualTo<T>(this IComparable<T> left, T right)
        {
            return left.CompareTo(right) == 0;
        }
    }

    #endregion

    #endregion
}
