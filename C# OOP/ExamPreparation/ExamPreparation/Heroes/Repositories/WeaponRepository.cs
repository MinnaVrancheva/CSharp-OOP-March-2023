using Heroes.Models.Contracts;
using Heroes.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heroes.Repositories
{
    public class WeaponRepository : IRepository<IWeapon>
    {
        private List<IWeapon> weapons;
        public WeaponRepository()
        {
            weapons = new List<IWeapon>();
        }
        public IReadOnlyCollection<IWeapon> Models => this.weapons;

        public void Add(IWeapon model)
        {
            weapons.Add(model);
        }

        public IWeapon FindByName(string name) => weapons.FirstOrDefault(w => w.Name == name);

        public bool Remove(IWeapon model)
        {
            return weapons.Remove(model);
            //string name = model.GetType().Name;

            //if (weapons.Any(w => w.GetType().Name == name))
            //{
            //    weapons.Remove(FindByName(name));
            //    return true;
            //}
            //return false;
        }
    }
}
