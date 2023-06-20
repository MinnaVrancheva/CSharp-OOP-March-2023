using System;

using WarCroft.Constants;
using WarCroft.Entities.Inventory;
using WarCroft.Entities.Items;

namespace WarCroft.Entities.Characters.Contracts
{
    public abstract class Character
    {
		// TODO: Implement the rest of the class.
		private string name;
		private double health;
		private double armor;

        public Character(string name, double health, double armor, double abilityPoints, IBag bag)
        {
            Name = name;
            
            BaseHealth = health;
            Health = health;

            BaseArmor = armor;
            Armor = armor;
            
            AbilityPoints = abilityPoints;
            Bag = bag;
        }

		public string Name
        {
			get { return name; }
			private set
            {
				if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.CharacterNameInvalid);
                }
                name = value;
            }
        }

        public double Health
        {
            get { return health; }
            set
            {
                if (value > BaseHealth)
                {
                    value = BaseHealth;
                }
                else if (value < 0)
                {
                    value = 0;
                }
                health = value;
            }
        }

        public double Armor
        {
            get { return armor; }
            private set
            {
                if (value < 0)
                {
                    value = 0;
                }
                armor = value;
            }
        }

        public double AbilityPoints { get; set; }

        public IBag Bag { get; set; }

        public double BaseArmor { get; private set; }
        public double BaseHealth { get; private set; }
        public bool IsAlive { get; set; } = true;

		public void EnsureAlive()
		{
			if (!this.IsAlive)
			{
				throw new InvalidOperationException(ExceptionMessages.AffectedCharacterDead);
			}
		}

        public void TakeDamage(double hitPoints)
        {
            if (IsAlive)
            {
                double pointsLeft = Math.Abs(Armor - hitPoints);

                Armor -= hitPoints;
                if (Armor <= 0)
                {
                    Armor = 0;
                    Health -= pointsLeft;

                    if (Health <= 0)
                    {
                        Health = 0;
                        IsAlive = false;
                    }
                }
            }
        }

        public void UseItem(Item item)
        {
            EnsureAlive();
            item.AffectCharacter(this);
        }
    }
}