using EasterRaces.Core.Contracts;
using EasterRaces.Models.Cars;
using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Models.Drivers;
using EasterRaces.Models.Drivers.Contracts;
using EasterRaces.Models.Races;
using EasterRaces.Models.Races.Contracts;
using EasterRaces.Repositories;
using EasterRaces.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasterRaces.Core.Entities
{
    public class ChampionshipController : IChampionshipController
    {
        private CarRepository cars;
        private DriverRepository drivers;
        private RaceRepository races;

        public ChampionshipController()
        {
            cars = new CarRepository();
            drivers = new DriverRepository();
            races = new RaceRepository();
        }
        public string AddCarToDriver(string driverName, string carModel)
        {
            IDriver driver = drivers.GetByName(driverName);
            if (driver == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.DriverNotFound, driverName));
            }

            ICar car = cars.GetByName(carModel);
            if (car == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.CarNotFound, carModel));
            }

            driver.AddCar(car);
            return string.Format(OutputMessages.CarAdded, driverName, carModel);
        }

        public string AddDriverToRace(string raceName, string driverName)
        {
            IRace race = races.GetByName(raceName);
            if (race == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceNotFound, raceName));
            }

            IDriver driver = drivers.GetByName(driverName);
            if (driver == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.DriverNotFound, driverName));
            }

            race.AddDriver(driver);
            return string.Format(OutputMessages.DriverAdded, driverName, raceName);
        }

        public string CreateCar(string type, string model, int horsePower)
        {
            if (cars.GetByName(model) != null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CarExists, model));
            }

            ICar car;
            if (type == "Muscle")
            {
                car = new MuscleCar(model, horsePower);
            }
            else
            {
                car = new SportsCar(model, horsePower);
            }

            cars.Add(car);
            return string.Format(OutputMessages.CarCreated, car.GetType().Name, model);
        }

        public string CreateDriver(string driverName)
        {
            IDriver driver = new Driver(driverName);

            if (drivers.GetByName(driverName) != null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.DriversExists, driverName));
            }

            drivers.Add(driver);
            return string.Format(OutputMessages.DriverCreated, driverName);
        }

        public string CreateRace(string name, int laps)
        {
            IRace race = new Race(name, laps);

            if (races.GetByName(name) != null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceExists, name));
            }

            races.Add(race);
            return string.Format(OutputMessages.RaceCreated, name);
        }

        public string StartRace(string raceName)
        {
            IRace race = races.GetByName(raceName);
            if (race == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceNotFound, raceName));
            }

            if (race.Drivers.Count < 3)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceInvalid, raceName, 3));
            }

            int counter = 1;
            StringBuilder sb = new StringBuilder();

            foreach (var driver in race.Drivers.OrderByDescending(x => x.Car.CalculateRacePoints(race.Laps)).Take(3))
            {
                if (counter == 1)
                {
                    sb.AppendLine(string.Format(OutputMessages.DriverFirstPosition, driver.Name, race.Name));
                }
                else if(counter == 2)
                {
                    sb.AppendLine(string.Format(OutputMessages.DriverSecondPosition, driver.Name, race.Name));
                }
                else if (counter == 3)
                {
                    sb.AppendLine(string.Format(OutputMessages.DriverThirdPosition, driver.Name, race.Name));
                }
                counter++;
            }

            races.Remove(race);
            return sb.ToString().TrimEnd();
        }
    }
}
