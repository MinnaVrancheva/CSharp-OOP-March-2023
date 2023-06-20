using VehiclesExtension.Core;
using VehiclesExtension.Core.Interfaces;
using VehiclesExtension.Generate.Interfaces;
using VehiclesExtension.GenerateVehicle;

IGenerateVehicle generateVehicle = new GenerateVehicle();
IEngine engine = new Engine(generateVehicle);
engine.Run();
