using System;
using VehiclesExtension.Generate.Interfaces;
using VehiclesExtension.Models;
using VehiclesExtension.Models.Interfaces;

namespace VehiclesExtension.GenerateVehicle;

public class GenerateVehicle : IGenerateVehicle
{
    public IVehicle generatedVehicles(string type, double fuelQuantity, double fuelConsumption, double tankCapacity)
    {
        switch (type)
        {
            case "Car":
                return new Car(fuelQuantity, fuelConsumption, tankCapacity);
            case "Truck":
                return new Truck(fuelQuantity, fuelConsumption, tankCapacity);
            case "Bus":
                return new Bus(fuelQuantity, fuelConsumption, tankCapacity);
                default:
                throw new ArgumentException("Invalid vehicle type");
        }
    }

}
