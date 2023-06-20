using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetWars.Models.Weapons
{
    public abstract class Weapon : IWeapon
    {
        private double price;
        private int destructionLevel;

        public Weapon(int destructionLevel, double price)
        {
            DestructionLevel = destructionLevel;
            Price = price;
        }
        public double Price
        {
            get => price; 
            private set
            {
                price = value;
            }
        }

        public int DestructionLevel
        {
            get => this.destructionLevel;
            private set
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException(string.Format(ExceptionMessages.TooLowDestructionLevel));
                }
                if (value > 10)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.TooHighDestructionLevel));
                }
                this.destructionLevel = value;
            }
        }
    }
}
