using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetWars.Repositories
{
    public class WeaponRepository : IRepository<IWeapon>
    {
        private List<IWeapon> weapons;

        public WeaponRepository()
        {
            this.weapons = new List<IWeapon>();
        }
        public IReadOnlyCollection<IWeapon> Models => this.weapons;

        public void AddItem(IWeapon model)
        {
            this.weapons.Add(model);
        }

        public IWeapon FindByName(string name) => weapons.FirstOrDefault(w => w.GetType().Name == name);

        public bool RemoveItem(string name)
        {
            if (this.weapons.Any(w => w.GetType().Name == name))
            {
                weapons.Remove(FindByName(name));
                return true;
            }
            return false;
        }
    }
}
