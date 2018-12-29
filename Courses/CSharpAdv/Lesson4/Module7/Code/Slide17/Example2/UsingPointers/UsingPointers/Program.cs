
namespace UsingPointers
{
    class Program
    {
        static void Main(string[] args)
        {
            int y = 9;
            Func(y);
        }

        public static unsafe void Func(int x)
        {
            int* px = &x;
        }
    }
}
