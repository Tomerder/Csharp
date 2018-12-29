using System;

namespace Delegates
{
    class DelegatesDemo
    {
        static void Main(string[] args)
        {
            Garage garage = new Garage();

            garage.Add(new Car("Mazda", 111));
            garage.Add(new Car("Volvo", 222));

            garage.WashCars();

            // Now we want to apply some custom operation that
            //  we just came up with.  Here's how we do it:
            //
            Garage.CarAction printDetails = new Garage.CarAction(PrintCarDetails);
            garage.PerformCustomOperation(printDetails);

            // There's a short-hand syntax in C# 2.0 to do this:
            //  (It's called "delegate inference.")
            //
            garage.PerformCustomOperation(PrintCarDetails);

            // Anonymous delegates are a C# 2.0 feature as well.
            //  The compiler creates the wrapping class and the
            //  delegate inside it.  Note that the delegate can
            //  refer to variables in the Main function, because
            //  it's defined inside it.  (These variables are
            //  called "captured" or "outer" variables.)
            //
            garage.PerformCustomOperation(delegate(Car car) { car.Travel(10); });
        }

        private static void PrintCarDetails(Car car)
        {
            Console.WriteLine("Car name={0}, license id={1}",
                              car.Name, car.LicenseId);
        }
    }
}
