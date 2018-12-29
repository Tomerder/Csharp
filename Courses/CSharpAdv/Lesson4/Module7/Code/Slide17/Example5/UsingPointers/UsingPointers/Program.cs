
namespace UsingPointers
{
    class Program
    {
        static void Main(string[] args)
        {
            Func();
        }

        public static unsafe void Func()
        {
            int[] a = new int[3];
            int[] b = new int[4];
            string s = "Hello";
            fixed (int* pa = a, pb = b)
            {
                *pa = 17;
                *pb = 18;
                //Like C and C++, assigning a String, no need for '&' 
                fixed (char* ps = s)
                {
                    *ps = 'Y'; 
                }
            }
        }
    }
}
