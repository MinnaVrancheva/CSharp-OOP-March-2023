using Raiding.Core.Interfaces;
using Raiding.Factory;
using Raiding.Factory.Interfaces;
using Raiding.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raiding.Core
{
    public class Engine : IEngine
    {
        private readonly IHeroFactory heroFactory;
        private readonly ICollection<IBaseHero> heroes;

        public Engine(IHeroFactory heroFactory)
        {
            this.heroFactory = heroFactory;
            heroes = new List<IBaseHero>();
        }

        public void Run()
        {
            int numberOfInputs = int.Parse(Console.ReadLine());

            while (numberOfInputs > 0)
            {
                string name = Console.ReadLine();
                string type = Console.ReadLine();

                try
                {
                    heroes.Add(heroFactory.baseHero(type, name));
                    numberOfInputs--;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            foreach (IBaseHero hero in heroes)
            {
                Console.WriteLine(hero.CastAbility());
            }

            int bossPower = int.Parse(Console.ReadLine());

            if (heroes.Sum(hero => hero.Power) >= bossPower)
            {
                Console.WriteLine("Victory!");
            }
            else
            {
                Console.WriteLine("Defeat...");
            }
        }
    }
}
