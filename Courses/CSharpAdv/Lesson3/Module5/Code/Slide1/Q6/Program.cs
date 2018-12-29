using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q6
{
    class Publisher
    {
        public event Action<int> Del;

        public void Publish(int i)
        {
            Del?.Invoke(5);

            //if (Del != null)
            //{
            //    Del(5);
            //}
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Publisher p = new Publisher();
            p.Del += F;
            p.Del += F;
            p.Publish(7);
        }

        static void F(int i)
        {
            Console.WriteLine(i);
        }
    }
}
