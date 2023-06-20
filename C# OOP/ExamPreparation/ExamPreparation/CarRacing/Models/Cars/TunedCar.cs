using System;
using System.Collections.Generic;
using System.Text;

namespace CarRacing.Models.Cars
{
    public class TunedCar : Car
    {
        private const double TunedCarFuelAvailable = 65;
        private const double TunedCarFuelConsumtpionPerRace = 7.5;
        public TunedCar(string make, string model, string vin, int horsePower)
            : base(make, model, vin, horsePower, TunedCarFuelAvailable, TunedCarFuelConsumtpionPerRace) { }

        public override void Drive()
        {
            base.Drive();
            double reducedHorsePower = HorsePower - (HorsePower * 0.03);
            HorsePower = (int)reducedHorsePower;
        }
    }
}
