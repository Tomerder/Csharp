
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
            fixed (int* pa = a)
            {
                *pa = 17;
            }

            fixed (int* p = &a[1])
            {
                *p = 12;
            }
        }
    }
}
