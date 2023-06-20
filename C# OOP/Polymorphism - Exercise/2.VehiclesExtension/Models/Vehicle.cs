using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehiclesExtension.Models.Interfaces;

namespace VehiclesExtension.Models
{
    public abstract class Vehicle : IVehicle
    {
        private double increasedConsumption;
        private double fuelQuantity;

        protected Vehicle(double fuelQuantity, double fuelConsumption, double tankCapacity, double increasedConsumption)
        {
            TankCapacity = tankCapacity;
            FuelQuantity = fuelQuantity;
            FuelConsumption = fuelConsumption;
            this.increasedConsumption = increasedConsumption;
        }

        public double FuelQuantity
        {
            get => fuelQuantity;
            private set
            {
                if (TankCapacity < value)
                {
                    fuelQuantity = 0;
                }
                else
                {
                    fuelQuantity = value;
                }
            }
        }

        public double FuelConsumption { get; private set; }

        public double TankCapacity { get; private set;}

        public string Drive(double distance, bool isIncreasedConsumption = true)
        {
            double consumption = isIncreasedConsumption
                ? FuelConsumption + increasedConsumption
                : FuelConsumption;

            if (FuelQuantity < consumption * distance)
            {
                throw new ArgumentException($"{this.GetType().Name} needs refueling");
            }

            FuelQuantity -= consumption * distance;
            return $"{this.GetType().Name} travelled {distance} km";
        }

        public virtual void Refuel(double amountToRefuel)
        {
            if (amountToRefuel <= 0)
            {
                throw new ArgumentException($"Fuel must be a positive number");
            }

            if (amountToRefuel + fuelQuantity > TankCapacity)
            {
                throw new ArgumentException($"Cannot fit {amountToRefuel} fuel in the tank");
            }
            FuelQuantity += amountToRefuel;
        }

        public override string ToString()
        {
            return $"{this.GetType().Name}: {FuelQuantity:F2}";
        }
    }
}
