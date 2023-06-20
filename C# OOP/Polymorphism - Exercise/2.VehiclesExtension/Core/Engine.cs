using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehiclesExtension.Core.Interfaces;
using VehiclesExtension.Generate.Interfaces;
using VehiclesExtension.Models;
using VehiclesExtension.Models.Interfaces;

namespace VehiclesExtension.Core
{
    public class Engine : IEngine
    {
        private readonly ICollection<IVehicle> vehicles;
        private readonly IGenerateVehicle generateVehicle;
        public Engine(IGenerateVehicle generateVehicle)
        {
            this.generateVehicle = generateVehicle;

            vehicles = new List<IVehicle>();
        }

        public void Run()
        {
            vehicles.Add(PopulateVehicles());
            vehicles.Add(PopulateVehicles());
            vehicles.Add(PopulateVehicles());

            int commandsNumber = int.Parse(Console.ReadLine());

            for (int i = 0; i < commandsNumber; i++)
            {
                try
                {
                    string command = Console.ReadLine();
                    string vehicleType = command.Split()[1];

                    IVehicle vehicle = vehicles.FirstOrDefault(x => x.GetType().Name == vehicleType);

                    switch (command.Split()[0])
                    {
                        case "Drive":
                            Console.WriteLine(vehicle.Drive(double.Parse(command.Split()[2])));
                            break;
                        case "Refuel":
                            vehicle.Refuel(double.Parse(command.Split()[2]));
                            break;
                        case "DriveEmpty":
                            Console.WriteLine(vehicle.Drive(double.Parse(command.Split()[2])), false);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            foreach (IVehicle vehicle in vehicles)
            {
                Console.WriteLine(vehicle.ToString());
            }
        }

        private IVehicle PopulateVehicles()
        {
            string[] vehicleInput = Console.ReadLine()
                            .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            return generateVehicle.generatedVehicles(vehicleInput[0], double.Parse(vehicleInput[1]), double.Parse(vehicleInput[2]), double.Parse(vehicleInput[3]));
        }
    }
}
