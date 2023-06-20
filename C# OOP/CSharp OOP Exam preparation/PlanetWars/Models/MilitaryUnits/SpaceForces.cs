using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetWars.Models.MilitaryUnits
{
    public class SpaceForces : MilitaryUnit
    {
        private const double SpaceForcesCost = 11;
        public SpaceForces()
            : base(SpaceForcesCost) { }
    }
}
