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
            throw new ApplicationException();
            Console.Read();
        }
    }
}
