using System;
using System.Reflection;

[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyKeyFile("GraphicsLib.snk")]

namespace GraphicsLib
{
    public class Rectangle
    {
        private int _width;
        private int _height;

        public Rectangle(int width, int height)
        {
            _width = width;
            _height = height;
        }

        public int Area
        {
            get { return _width * _height; }
        }
    }
}
