using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public class Car : IVehiculo
    {
        public Car(int fuel)
        {
            Fuel = fuel;
        }

        public int Fuel { get; set; }
        public void Drive()
        {
            if (Fuel > 0)
            {
                Console.WriteLine("Driving");
            }
        }

        public bool Refuel(int amountToRefuel)
        {
            Fuel += amountToRefuel;
            return true;
        }
    }
}
