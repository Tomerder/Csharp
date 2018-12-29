using System;

namespace MarshalingDifferences
{
    /// <summary>
    /// A serializable Rectangle class that demonstrates marshal-by-value
    /// behavior.  When passed across application domain boundaries, this
    /// class is copied using serialization.
    /// </summary>
    [Serializable]
    class RectangleMBV
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public RectangleMBV()
            : this(10, 10)
        {
        }

        public RectangleMBV(int width, int height)
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
