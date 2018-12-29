using System;
using System.Threading.Tasks;

namespace TaskException
{
    class Program
    {
        static void Main(string[] args)
        {
            Task t = Task.Run(() =>
            {
                throw new ApplicationException();
            });
            //try
            //{
            //    t.Wait();
            //}
            //catch (AggregateException ex)
            //{
            //    foreach (Exception e in ex.InnerExceptions)
            //    {
            //        Console.WriteLine(e.GetType());
            //    }
            //}
            Console.Read();
        }
    }
}
