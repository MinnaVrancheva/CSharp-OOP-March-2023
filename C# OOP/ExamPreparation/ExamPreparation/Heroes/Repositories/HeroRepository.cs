using Heroes.Models.Contracts;
using Heroes.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heroes.Repositories
{
    public class HeroRepository : IRepository<IHero>
    {
        private List<IHero> heroes;
        public HeroRepository()
        {
            this.heroes = new List<IHero>();
        }
        public IReadOnlyCollection<IHero> Models => this.heroes;

        public void Add(IHero model)
        {
            heroes.Add(model);
        }

        public IHero FindByName(string name) => heroes.FirstOrDefault(h => h.Name == name);

        public bool Remove(IHero model)
        {
            return heroes.Remove(model);
            //string name = model.GetType().Name;
            //if(heroes.Any(h => h.GetType().Name == name))
            //{
            //    return true;
            //}
            //return false;
        }
    }
}
