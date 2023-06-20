using VehiclesExtension.Models.Interfaces;

namespace VehiclesExtension.Generate.Interfaces;

public interface IGenerateVehicle
{
    IVehicle generatedVehicles(string type, double fuelQuantity, double fuelConsumption, double tankCapacity);
}
