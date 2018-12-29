using System;

namespace MarshalingDifferences
{
    /// <summary>
    /// A marshal-by-reference Rectangle class that demonstrates marshal-by-reference
    /// behavior.  When passed across application domain boundaries, a proxy (reference)
    /// to the object is passed without copying its contents.
    /// </summary>
    class RectangleMBR : MarshalByRefObject
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public RectangleMBR()
            : this(10, 10)
        {
        }

        public RectangleMBR(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public int Area { get { return Width * Height; } }

        public override string ToString()
        {
            return String.Format("w={0}, h={1}, wxh={2}", Width, Height, Area);
        }
    }
}
