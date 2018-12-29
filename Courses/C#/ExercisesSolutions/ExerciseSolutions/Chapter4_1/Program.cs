
namespace Chapter4Exercise1
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
 
        }
    }
}
