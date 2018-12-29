using System;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Task[] tasks = new Task[10];

            for (int i = 0; i < 10; i++)
            {
                tasks[i] = new Task(j => Console.WriteLine(j), i);
                tasks[i].Start();
            }

            Task.WaitAll(tasks);
            Console.WriteLine("I'm done");
        }
    }
}
