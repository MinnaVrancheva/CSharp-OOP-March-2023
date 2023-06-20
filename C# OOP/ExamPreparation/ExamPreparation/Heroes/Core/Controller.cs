using Heroes.Core.Contracts;
using Heroes.Models;
using Heroes.Models.Contracts;
using Heroes.Models.Contracts.Heroes;
using Heroes.Models.Weapons;
using Heroes.Repositories;
using Heroes.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heroes.Core
{
    public class Controller : IController
    {
        private HeroRepository heroes;
        private WeaponRepository weapons;

        public Controller()
        {
            this.heroes = new HeroRepository();
            this.weapons = new WeaponRepository();
        }

        public string CreateHero(string type, string name, int health, int armour)
        {
            IHero hero = this.heroes.FindByName(name);

            if (hero != null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.HeroAlreadyExists, name));
            }

            if (heroes.Models.GetType().Name == null)
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidHeroType);
            }

            
            if(type == "Knight")
            {
                hero = new Knight(name, health, armour);
                heroes.Add(hero);
                return string.Format(OutputMessages.SuccessfullyAddedKnight, name);
            }
            else
            {
                hero = new Barbarian(name, health, armour);
                heroes.Add(hero);
                return string.Format(OutputMessages.SuccessfullyAddedBarbarian, name);
            }
        }

        public string CreateWeapon(string type, string name, int durability)
        {
            IWeapon weapon = this.weapons.FindByName(name);

            if (weapon != null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.WeaponAlreadyExists, name));
            }

            if(weapons.Models.GetType().Name == null)
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidWeaponType);
            }

            
            if(type == "Mace")
            {
                weapon = new Mace(name, durability);
            }
            else
            {
                weapon = new Claymore(name, durability);
            }

            weapons.Add(weapon);
            return $"A {type.ToLower()} {name} is added to the collection.";

        }
        public string AddWeaponToHero(string weaponName, string heroName)
        {
            IWeapon weapon = this.weapons.FindByName(weaponName);

            if (weapon == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.WeaponDoesNotExists, weaponName));
            }

            IHero hero = this.heroes.FindByName(heroName);

            if (hero == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.HeroDoesNotExists, heroName));
            }       
            
            if (hero.Weapon != null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.HeroHasWeapon, heroName));
            }

            //IWeapon weapon = this.weapons.FindByName(weaponName);
            hero.AddWeapon(weapon);
            weapons.Remove(weapon);
            return $"Hero {heroName} can participate in battle using a {hero.Weapon.GetType().Name.ToLower()}.";
        }

        public string StartBattle()
        {
            IMap map = new Map();
            ICollection<IHero> players = heroes.Models.Where(p => p.IsAlive && p.Weapon != null).ToList();
            return map.Fight(players);
        }
        public string HeroReport()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var hero in heroes.Models.OrderBy(x => x.GetType().Name).ThenByDescending(x => x.Health).ThenBy(x => x.Name))
            {
                sb.AppendLine($"{hero.GetType().Name}: {hero.Name}");
                sb.AppendLine($"--Health: {hero.Health}");
                sb.AppendLine($"--Armour: {hero.Armour}");
                sb.AppendLine(hero.Weapon != null ? $"--Weapon: {hero.Weapon.Name}" : "--Weapon: Unarmed");
            }
            return sb.ToString().TrimEnd();
        }
    }
}
