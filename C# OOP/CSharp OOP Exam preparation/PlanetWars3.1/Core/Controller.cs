using PlanetWars.Core.Contracts;
using PlanetWars.Models.MilitaryUnits;
using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Models.Planets;
using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Models.Weapons;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories;
using PlanetWars.Utilities.Messages;
using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace PlanetWars.Core
{
    public class Controller : IController
    {
        private PlanetRepository planets;

        public Controller()
        {
            this.planets = new PlanetRepository();
        }
                
        public string AddUnit(string unitTypeName, string planetName)
        {
            IPlanet planet = this.planets.FindByName(planetName);

            if(this.planets.FindByName(planetName) == default)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnexistingPlanet, planetName));
            }

            if(unitTypeName != nameof(AnonymousImpactUnit) &&
                unitTypeName != nameof(SpaceForces) &&
                unitTypeName != nameof(StormTroopers))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.ItemNotAvailable, unitTypeName));
            }

            if (planet.Army.Any(x => x.GetType().Name == unitTypeName))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnitAlreadyAdded, unitTypeName, planetName));
            }

            IMilitaryUnit unit;

            if (unitTypeName == nameof(AnonymousImpactUnit))
            {
                unit = new AnonymousImpactUnit();
            }
            else if (unitTypeName == nameof(SpaceForces))
            {
                unit = new SpaceForces();
            }
            else
            {
                unit = new StormTroopers();
            }
            
            planet.Spend(unit.Cost);
            planet.AddUnit(unit);
            return string.Format(OutputMessages.UnitAdded, unitTypeName, planetName);
        }

        public string AddWeapon(string planetName, string weaponTypeName, int destructionLevel)
        {
            IPlanet planet = this.planets.FindByName(planetName);

            if (planet == default)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnexistingPlanet, planetName));
            }

            if(planet.Weapons.Any(w => w.GetType().Name == weaponTypeName))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.WeaponAlreadyAdded, weaponTypeName));
            }

            if (weaponTypeName != nameof(BioChemicalWeapon) &&
                weaponTypeName != nameof(NuclearWeapon) &&
                weaponTypeName != nameof(SpaceMissiles))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.ItemNotAvailable, weaponTypeName));
            }

            IWeapon weapon;
            if (weaponTypeName == nameof(SpaceMissiles))
            {
                weapon = new SpaceMissiles(destructionLevel);
            }
            else if(weaponTypeName == nameof(NuclearWeapon))
            {
                weapon = new NuclearWeapon(destructionLevel);
            }
            else
            {
                weapon = new BioChemicalWeapon(destructionLevel);
            }
            
            planet.Spend(weapon.Price);
            planet.AddWeapon(weapon);
            return string.Format(OutputMessages.WeaponAdded, planetName, weaponTypeName);
        }

        public string CreatePlanet(string name, double budget)
        {
            IPlanet planet = new Planet(name, budget);

            if (this.planets.FindByName(name) != null)
            {
                return string.Format(OutputMessages.ExistingPlanet, name);
            }
            else
            {
                planets.AddItem(planet);
                return string.Format(OutputMessages.NewPlanet, name);
            }
        }

        public string ForcesReport()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"***UNIVERSE PLANET MILITARY REPORT***");

            foreach (var planet in this.planets.Models.OrderByDescending(p => p.MilitaryPower).ThenBy(x => x.Name))
            {
                sb.AppendLine(planet.PlanetInfo());
            }

            return sb.ToString().TrimEnd();
        }

        public string SpaceCombat(string planetOne, string planetTwo)
        {
            IPlanet firstPlanet = planets.FindByName(planetOne);
            IPlanet secondPlanet = planets.FindByName(planetTwo);

            double firstPlanetHalfBudget = firstPlanet.Budget / 2;
            double secondPlanetHalfBudget = secondPlanet.Budget / 2;

            double firstPlanetProfit = firstPlanet.Army.Sum(x => x.Cost) + 
                                            firstPlanet.Weapons.Sum(x => x.Price);
            double secondPlanetProfit = secondPlanet.Army.Sum(x => x.Cost) +
                                            secondPlanet.Weapons.Sum(x => x.Price);

            bool firstPlanetHasNuclearWeapon = firstPlanet.Weapons.Any(x => x.GetType().Name == nameof(NuclearWeapon));
            bool secondPlanetHasNuclearWeapon = secondPlanet.Weapons.Any(x => x.GetType().Name == nameof(NuclearWeapon));

            if (firstPlanet.MilitaryPower > secondPlanet.MilitaryPower)
            {
                FirstPlanetWins(planetTwo, firstPlanet, firstPlanetHalfBudget, secondPlanetHalfBudget, secondPlanetProfit);
                return string.Format(OutputMessages.WinnigTheWar, planetOne, planetTwo);
            }
            else if (firstPlanet.MilitaryPower < secondPlanet.MilitaryPower)
            {
                SecondPlanetWins(planetOne, secondPlanet, firstPlanetHalfBudget, secondPlanetHalfBudget);
                return string.Format(OutputMessages.WinnigTheWar, planetTwo, planetOne);
            }
            else 
            {
                if ((!firstPlanetHasNuclearWeapon && !secondPlanetHasNuclearWeapon) || (firstPlanetHasNuclearWeapon && secondPlanetHasNuclearWeapon)) 
                {
                    firstPlanet.Spend(firstPlanetHalfBudget);
                    secondPlanet.Spend(secondPlanetHalfBudget);
                    return string.Format(OutputMessages.NoWinner);
                }
                else if (firstPlanetHasNuclearWeapon)
                {
                    FirstPlanetWins(planetTwo, firstPlanet, firstPlanetHalfBudget, secondPlanetHalfBudget, secondPlanetProfit);
                    return string.Format(OutputMessages.WinnigTheWar, planetOne, planetTwo);
                }
                else
                {
                    SecondPlanetWins(planetTwo, secondPlanet, firstPlanetHalfBudget, secondPlanetHalfBudget);
                    return string.Format(OutputMessages.WinnigTheWar, planetTwo, planetOne);
                }
            }
        }

        private void SecondPlanetWins(string planetOne, IPlanet secondPlanet, double firstPlanetHalfBudget, double secondPlanetHalfBudget)
        {
            secondPlanet.Spend(secondPlanetHalfBudget);
            secondPlanet.Profit(firstPlanetHalfBudget);
            secondPlanet.Profit(firstPlanetHalfBudget);
            planets.RemoveItem(planetOne);
        }

        private void FirstPlanetWins(string planetTwo, IPlanet firstPlanet, double firstPlanetHalfBudget, double secondPlanetHalfBudget, double secondPlanetProfit)
        {
            firstPlanet.Spend(firstPlanetHalfBudget);
            firstPlanet.Profit(secondPlanetHalfBudget);
            firstPlanet.Profit(secondPlanetProfit);
            planets.RemoveItem(planetTwo);
        }

        public string SpecializeForces(string planetName)
        {
            IPlanet planet = this.planets.FindByName(planetName);

            if(planet == default)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnexistingPlanet, planetName));
            }

            if (planet.Army.Count == 0)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.NoUnitsFound));
            }

            planet.Spend(1.25);
            planet.TrainArmy();

            return string.Format(OutputMessages.ForcesUpgraded, planetName);
        }
    }
}
