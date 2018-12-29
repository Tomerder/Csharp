using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shape
{
    class Program
    {
        static void Main(string[] args)
        {

           

            int x;
            int y;
            int width;
            int height;
            Console.WriteLine("Please enter x coordinate for the rectangle");
            string input;
            input = Console.ReadLine();
            x = int.Parse(input);
            
            Console.WriteLine("Please enter y coordinate for the rectangle");
            input = Console.ReadLine();
            y = int.Parse(input);

            Console.WriteLine("Please enter width coordinate for the rectangle");
            input = Console.ReadLine();
            width  = int.Parse(input);

             Console.WriteLine("Please enter height coordinate for the rectangle");
            input = Console.ReadLine();
            height  = int.Parse(input);

            Rectangle r1 = new Rectangle(x,y,width,height);
           

            Console.WriteLine("After assigning the rectangle: ");
            Console.WriteLine(r1);

            Rectangle r2 = new Rectangle(7,7,10,10);
            Console.WriteLine("Second Rectangle is: " + r2);
            r2.move(10, 10);
            Console.WriteLine("After moving the second Rectangle to 10 10: " + r2);

            if (r1 == r2)
                Console.WriteLine("== between two rectangles returned true");
            else
                Console.WriteLine("== between two rectangles returned false");

            Circle c1 = new Circle(5, 5, 10);
            Console.WriteLine("added a circle : " + c1);
            c1.move(7, 7);
            Console.WriteLine("after moving the circle to 7,7" + c1);

            CompoShape cs = new CompoShape(5,5,5);
            Console.WriteLine("Compound Shape before adding r1 and r2 and c1 " + cs);
            cs.add(r1);
            cs.add(r2);
            cs.add(c1);

            Console.WriteLine("Compound Shape after adding r1 and r2 and c1 " + cs);
            cs.move(20, 20);
            Console.WriteLine("Compound Shape after moving it to 20 20  " + cs);

            Console.WriteLine("Shape in comboShape cell 1 is: " + cs[1]);

            //cs.sortShapes();



        }
    }
}
