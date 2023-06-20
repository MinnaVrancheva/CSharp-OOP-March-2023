using Heroes.Repositories;
using Heroes.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Models.Contracts.Heroes
{
    public abstract class Hero : IHero
    {
        private string name;
        private int health;
        private int armour;
        private IWeapon weapon;

        public Hero(string name, int health, int armour)
        {
            Name = name;
            Health = health;
            Armour = armour;
        }

        public string Name
        {
            get { return name; }
            private set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidHeroName);
                }
                name = value;
            }
        }

        public int Health
        {
            get { return health; }
            private set
            {
                if(value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.TooLowHeroHealth);
                }
                health = value;
            }
        }

        public int Armour
        {
            get { return armour; }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.TooLowHeroArmour);
                }
                armour = value;
            }
        }

        public IWeapon Weapon
        {
            get { return weapon; }
            private set
            {
                if(value == null)
                {
                    throw new ArgumentException(ExceptionMessages.WeaponIsNull);
                }
                weapon = value;
            }
        }

        public bool IsAlive
        {
            get
            {
                if(health <= 0)
                {
                    return false;
                }
                return true;
            }
            
        }

        public void AddWeapon(IWeapon weapon)
        {
            Weapon = weapon;
        }

        public void TakeDamage(int points)
        {
            this.armour -= points;

            if(armour <= 0 )
            {
                int pointsLeft = Math.Abs(armour);
                armour = 0;
                this.health -= pointsLeft;

                if(health <= 0)
                {
                    health = 0;
                }
            }
        }
    }
}
