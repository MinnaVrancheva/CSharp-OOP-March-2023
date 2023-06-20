using PlanetWars.Models.MilitaryUnits;
using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Models.Weapons;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories;
using PlanetWars.Repositories.Contracts;
using PlanetWars.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetWars.Models.Planets
{
    public class Planet : IPlanet
    {
        private UnitRepository units;
        private WeaponRepository weapons;
        private string name;
        private double budget;

        public Planet(string name, double budget)
        {
            this.units = new UnitRepository();
            this.weapons = new WeaponRepository();
            this.name = name;
            this.budget = budget;
        }
        public string Name
        {
            get { return name; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(ExceptionMessages.InvalidPlanetName);
                }
                name = value;
            }
        }

        public double Budget
        {
            get { return budget; }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidBudgetAmount);
                }
                budget = value;
            }
        }

        public double MilitaryPower => Math.Round(this.CalculateMilitaryPower(), 3);

        public IReadOnlyCollection<IMilitaryUnit> Army => this.units.Models;

        public IReadOnlyCollection<IWeapon> Weapons => this.weapons.Models;

        public void AddUnit(IMilitaryUnit unit)
        {
            units.AddItem(unit);
        }

        public void AddWeapon(IWeapon weapon)
        {
            weapons.AddItem(weapon);
        }

        public string PlanetInfo()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Planet: {this.Name}");
            sb.AppendLine($"--Budget: {this.Budget} billion QUID");
            sb.Append($"--Forces: ");

            if (this.units == null)
            {
                sb.AppendLine($"No units");
            }
            else
            {
                var units = new Queue<string>();
                foreach (var unit in this.Army)
                {
                    units.Enqueue(unit.GetType().Name);
                }

                sb.AppendLine(string.Join(", ", units));
            }

            sb.Append($"Combat equipment: ");

            if (this.Weapons == null)
            {
                sb.AppendLine($"No weapons");
            }
            else
            {
                var weapons = new Queue<string>();
                foreach (var weapon in this.Weapons)
                {
                    weapons.Enqueue(weapon.GetType().Name);
                }
                sb.AppendLine(string.Join(", ", weapons));
            }

            sb.AppendLine($"Military Power: {this.MilitaryPower}");

            return sb.ToString().TrimEnd();
        }

        public void Profit(double amount)
        {
            this.Budget += amount;
        }

        public void Spend(double amount)
        {
            if (budget < amount)
            {
                throw new ArgumentException(ExceptionMessages.UnsufficientBudget);
            }
            this.Budget -= amount;
        }

        public void TrainArmy()
        {
            foreach (var unit in this.Army)
            {
                unit.IncreaseEndurance();
            }
        }

        private double CalculateMilitaryPower()
        {
            double totalAmount = this.units.Models.Sum(s => s.EnduranceLevel) + this.weapons.Models.Sum(s => s.DestructionLevel);

            if (this.units.Models.Any(x => x.GetType().Name == nameof(AnonymousImpactUnit)))
            {
                totalAmount *= 1.30;
            }
            if (this.units.Models.Any(x => x.GetType().Name == nameof(NuclearWeapon)))
            {
                totalAmount *= 1.45;
            }
            return totalAmount;
        }
    }
}
