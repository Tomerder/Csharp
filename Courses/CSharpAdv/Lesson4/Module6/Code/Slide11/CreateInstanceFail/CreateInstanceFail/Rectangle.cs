using System;

namespace CreateInstanceFail
{
    /// <summary>
    /// A sample object used for instantiation in a different application domain.
    /// </summary>
    [Serializable]
    class Rectangle
    {
        private readonly int _width, _height;

        public Rectangle()
            : this(10, 10)
        {
            Console.WriteLine("Rectangle constructor executes, app domain name: " + AppDomain.CurrentDomain.FriendlyName);
        }

        public Rectangle(int width, int height)
        {
            _width = width;
            _height = height;
        }

        public int Area { get { return _width * _height; } }
    }
}
