using System;
using System.Collections.Generic;
using System.Text;

namespace CarRacing.Models.Cars
{
    public class SuperCar : Car
    {
        private const double SuperCarFuelAvailable = 80;
        private const double SuperCarFuelConsumtpionPerRace = 10;
        public SuperCar(string make, string model, string vin, int horsePower)
            : base(make, model, vin, horsePower, SuperCarFuelAvailable, SuperCarFuelConsumtpionPerRace) { }
    }
}
