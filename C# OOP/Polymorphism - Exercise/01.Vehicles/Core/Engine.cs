using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicles.Core.Interfaces;
using Vehicles.Models;

namespace Vehicles.Core
{
    public class Engine : IEngine
    {
        public void Run()
        {
            string[] carInput = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);

            Car car = new Car(double.Parse(carInput[1]), double.Parse(carInput[2]));

            string[] truckInput = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);

            Truck truck = new Truck(double.Parse(truckInput[1]), double.Parse(truckInput[2]));

            int numberOfCommands = int.Parse(Console.ReadLine());

            for (int i = 0; i < numberOfCommands; i++)
            {
                try
                {
                    string command = Console.ReadLine();

                    switch (command.Split()[0])
                    {
                        case "Drive":
                            if (command.Split()[1] == "Car")
                            {
                                Console.WriteLine(car.Drive(double.Parse(command.Split()[2])));
                            }
                            else
                            {
                                Console.WriteLine(truck.Drive(double.Parse(command.Split()[2])));
                            }
                            break;
                        case "Refuel":
                            if (command.Split()[1] == "Car")
                            {
                                car.Refuel(double.Parse(command.Split()[2]));
                            }
                            else
                            {
                                truck.Refuel(double.Parse(command.Split()[2]));
                            }
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Console.WriteLine(car.ToString());
            Console.WriteLine(truck.ToString());
        }
    }
}
