
using System;

namespace Chapter12Exercise1
{
    class Program
    {
        static void Main(string[] args)
        {
            LinkedList<string> list = new LinkedList<string>();

            for (int i = 0; i < args.Length; ++i)
                list.Add(args[i]);

            list.Print();

            list.RemoveAt(5);
            list.RemoveAt(2);

            list.Print();

            try
            {
                list.RemoveAt(25);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


            try
            {
                list.GetAt(-1);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.WriteLine("love! appears in index: {0}", list.Find("love!"));

        }
    }
}
