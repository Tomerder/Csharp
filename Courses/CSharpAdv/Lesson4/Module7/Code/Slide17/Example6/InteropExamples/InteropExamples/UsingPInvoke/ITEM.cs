using System.Runtime.InteropServices;

namespace UsingPInvoke
{
    [StructLayout(LayoutKind.Sequential)]
    struct ITEM
    {
        public ITEM(uint data)
        {
            dwData = data;
        }
        public uint dwData;
    }
}
