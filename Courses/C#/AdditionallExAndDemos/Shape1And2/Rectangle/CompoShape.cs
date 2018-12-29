using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shape
{
    class CompoShape : Shape
    {
        Shape[] _sArray;
        private int _numShapes = 0;
        private static int defualtSize = 10;

        public CompoShape()
            : base()
        {
            _sArray = new Shape[defualtSize];
        }
        public CompoShape(int x, int y,int size )
            : base(x,y)
        {
            _sArray = new Shape[size];
        }
        public void add(Shape s)
        {
            if (_numShapes == _sArray.Length)
            {
                Console.WriteLine("Sorry, can not add any more Shapes");
                return;
            }
            _sArray[_numShapes++] = s;

        }
        public override float Area
        {
            get
            {
                float tempArea = 0;
                for (int i = 0; i < _numShapes; i++)
                    tempArea = tempArea + _sArray[i].Area;

                return tempArea;
            }
        }

        public override void resize(int percent)
        {

           
            for (int i = 0; i < _numShapes; i++)
                _sArray[i].resize(percent);

           
        }

        public override void move(int newX, int newY)
        {
            base.move(newX, newY);
            for (int i = 0; i < _numShapes; i++)
                _sArray[i].move(newX, newY);
            
        }
        public override string ToString()
        {
            string rString = "Centeral Point is: " + base.ToString();
            for (int i = 0; i < _numShapes; i++)
                rString = rString + "\n" + "Shape number: " + i + " " + _sArray[i].ToString();

            return rString;
        } 

        public Shape this[int index]
        {
            get
            {
                if (index >= 0 && index < _numShapes)
                    return _sArray[index];
                else
                    return null;
            }
        }

        // demonstrate Sorting shapes array (show the usage of IComparable for shape)
        public void sortShapes()
        {
            Array.Sort(_sArray);
        }
       
    }
}
