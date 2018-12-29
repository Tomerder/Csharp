using System;
using System.Collections.Generic;

namespace Delegates
{
    class Garage
    {
        private List<Car> _cars = new List<Car>();

        public Garage()
        {
        }

        public void Add(Car car)
        {
            _cars.Add(car);
        }

        public void WashCars()
        {
            foreach (Car car in _cars)
            {
                if (car.IsDirty)
                {
                    // Wash the car.
                    car.IsDirty = false;
                }
            }
        }

        // The Garage class wants to provide an extensibility point
        //  for other developers that want to specify other operations
        //  that should be applied for all cars.  Here's how it's done:
        //
        public delegate void CarAction(Car car);

        // The following method takes a parameter of the delegate type
        //  we just defined, and invokes the delegate for each car in
        //  the garage.
        //
        public void PerformCustomOperation(CarAction carAction)
        {
            foreach (Car car in _cars)
            {
                carAction(car);
            }
        }
    }
}
