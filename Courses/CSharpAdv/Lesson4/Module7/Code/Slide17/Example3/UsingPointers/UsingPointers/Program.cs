
namespace UsingPointers
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = {1, 2, 3};
            Func(arr);
        }

        public static unsafe void Func(int[] a)
        {
            int* pa = a; //This does NOT compile!
            int* p = &a[1];
        }
    }
}
