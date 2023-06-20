using Easter.Models.Bunnies.Contracts;
using Easter.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easter.Repositories
{
    public class BunnyRepository : IRepository<IBunny>
    {
        private List<IBunny> bunnys;

        public BunnyRepository()
        {
            bunnys = new List<IBunny>();
        }
        public IReadOnlyCollection<IBunny> Models => this.bunnys;

        public void Add(IBunny model)
        {
            bunnys.Add(model);
        }

        public IBunny FindByName(string name)
        {
            return bunnys.FirstOrDefault(x => x.Name == name);
        }

        public bool Remove(IBunny model)
        {
            return this.bunnys.Remove(model);
        }
    }
}
