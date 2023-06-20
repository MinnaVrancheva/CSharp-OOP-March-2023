using EasterRaces.Models.Races.Contracts;
using EasterRaces.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasterRaces.Repositories
{
    public class RaceRepository : IRepository<IRace>
    {
        private List<IRace> races;

        public RaceRepository()
        {
            races = new List<IRace>();
        }
        public void Add(IRace model)
        {
            races.Add(model);
        }

        public IReadOnlyCollection<IRace> GetAll()
        {
            return this.races;
        }

        public IRace GetByName(string name)
        {
            return races.FirstOrDefault(x => x.Name == name);
        }

        public bool Remove(IRace model)
        {
            return races.Remove(model);
        }
    }
}
