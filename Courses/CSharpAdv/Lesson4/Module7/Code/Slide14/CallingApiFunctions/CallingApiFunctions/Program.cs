using System.Runtime.InteropServices;

namespace CallingApiFunctions
{
    public class Test
    {
        [DllImport("User32.dll", CharSet=CharSet.Auto)]
        public static extern int MessageBox(uint hwnd, string text, string caption, uint type);
    }

    class Program
    {
        static void Main(string[] args)
        {
            Test.MessageBox(0, "Amir", "my caption", 1);
            Test.MessageBox(0, "Adler", "my caption", 4);
        }
    }
}
