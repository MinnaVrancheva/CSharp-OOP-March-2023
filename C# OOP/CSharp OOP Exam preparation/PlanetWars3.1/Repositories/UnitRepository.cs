using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetWars.Repositories
{
    public class UnitRepository : IRepository<IMilitaryUnit>
    {
        private List<IMilitaryUnit> militaryUnits;
        public UnitRepository()
        {
            this.militaryUnits = new List<IMilitaryUnit>();
        }
        public IReadOnlyCollection<IMilitaryUnit> Models => this.militaryUnits;

        public void AddItem(IMilitaryUnit model)
        {
            militaryUnits.Add(model);
        }

        public IMilitaryUnit FindByName(string name)
        {
            return militaryUnits.FirstOrDefault(x => x.GetType().Name == name);
        }

        public bool RemoveItem(string name)
        {
            if (militaryUnits.Any(x => x.GetType().Name == name))
            {
                militaryUnits.Remove(FindByName(name));
                return true;
            }
            return false;
        }
    }
}
