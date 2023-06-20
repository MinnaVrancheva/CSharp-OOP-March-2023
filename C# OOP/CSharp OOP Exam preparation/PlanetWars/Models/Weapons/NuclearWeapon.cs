using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetWars.Models.Weapons
{
    public class NuclearWeapon : Weapon
    {
        private const double NuclearWeaponPrice = 15;
        public NuclearWeapon(int destructionLevel)
            : base(destructionLevel, NuclearWeaponPrice) { }
    }
}
