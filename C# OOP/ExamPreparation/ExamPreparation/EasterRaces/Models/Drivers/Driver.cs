using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Models.Drivers.Contracts;
using EasterRaces.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasterRaces.Models.Drivers
{
    public class Driver : IDriver
    {
        private string name;
        private ICar car;

        public Driver(string name)
        {
            Name = name;
        }

        public string Name
        {
            get { return name; }
            private set
            {
                if (string.IsNullOrEmpty(value) || value.Length < 5)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidName, value, 5));
                }
                name = value;
            }
        }

        public ICar Car
        {
            get { return this.car; }
            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(ExceptionMessages.CarInvalid);
                }
                car = value;
            }
        }

        public int NumberOfWins {get; private set;}

        public bool CanParticipate => this.Car != null ? true : false;

        public void AddCar(ICar car)
        {
            this.Car = car;
        }

        public void WinRace()
        {
            this.NumberOfWins++;
        }
    }
}
