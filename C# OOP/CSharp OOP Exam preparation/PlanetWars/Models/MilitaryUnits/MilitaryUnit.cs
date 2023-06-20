using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetWars.Models.MilitaryUnits
{
    public abstract class MilitaryUnit : IMilitaryUnit
    {
        private double cost;
        private int enduranceLevel;

        public MilitaryUnit(double cost)
        {
            this.cost = cost;
            this.enduranceLevel = 1;
        }
        public double Cost => this.cost;

        public int EnduranceLevel => this.enduranceLevel;

        public void IncreaseEndurance()
        {
            enduranceLevel += 1;

            if (enduranceLevel > 20)
            {
                enduranceLevel = 20;
                throw new ArgumentException(string.Format(ExceptionMessages.EnduranceLevelExceeded));
            }
        }
    }
}
