using Formula1.Models.Contracts;
using Formula1.Repositories.Contracts;
using Formula1.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formula1.Models
{
    public class Pilot : IPilot
    {
        private string fullName;
        private IFormulaOneCar car;
        private int numberOfWins;
        private bool canRace;
        public Pilot(string fullName)
        {
            FullName = fullName;
            NumberOfWins = 0;
            canRace = false;
        }

        public string FullName
        {
            get { return fullName; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 5)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidPilot, value);
                }
                fullName = value;
            }
        }

        public IFormulaOneCar Car
        {
            get { return car; }
            private set
            {
                if(value == null)
                {
                    throw new NullReferenceException(ExceptionMessages.InvalidCarForPilot);
                }
                car = value;
            }
        }
        public int NumberOfWins {get { return numberOfWins; } private set { numberOfWins = value; } }

        public bool CanRace
        {
            get { return canRace; }
            private set
            {
                canRace = value;
            }
        }

        public void AddCar(IFormulaOneCar car)
        {
            Car = car;
            CanRace = true;
        }

        public void WinRace()
        {
            numberOfWins++;
        }

        public override string ToString()
        {
            return $"Pilot {fullName} has {numberOfWins} wins.";
        }
    }
}
