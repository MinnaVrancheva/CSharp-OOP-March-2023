using Heroes.Models.Contracts;
using Heroes.Models.Contracts.Heroes;
using Heroes.Repositories;
using Heroes.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heroes.Models
{
    public class Map : IMap
    {
        //private IRepository<IHero> heroes;

        //public Map()
        //{
        //    this.heroes = new HeroRepository();
        //}
        public string Fight(ICollection<IHero> players)
        {
            List<IHero> barbarians = new List<IHero>();
            List<IHero> knights = new List<IHero>();
            
            foreach (var hero in players)
            {
                if (hero.GetType().Name == "Barbarian")
                {
                    barbarians.Add(hero);
                }
                else
                {
                    knights.Add(hero);
                }
            }

            int barbarianCasualties = 0;
            int knightCasualties = 0;

            while (barbarians.Any(x => x.IsAlive == true) && knights.Any(x => x.IsAlive == true))
            {
                foreach (var knight in knights.Where(x => x.IsAlive))
                {
                    foreach (var barbarian in barbarians.Where(x => x.IsAlive))
                    {
                        if (knight.Weapon.Durability == 0)
                        {
                            continue;
                        }
                        barbarian.TakeDamage(knight.Weapon.DoDamage());
                        if(!barbarian.IsAlive)
                        {
                            barbarianCasualties++;
                        }
                    }
                }

                //barbariansInitialCount += barbarians.RemoveAll(b => b.IsAlive == false);

                foreach (var barbarian in barbarians.Where(x => x.IsAlive))
                {
                    foreach (var knight in knights.Where(x => x.IsAlive))
                    {
                        if (barbarian.Weapon.Durability == 0)
                        {
                            continue;
                        }
                        knight.TakeDamage(barbarian.Weapon.DoDamage());
                        if(!knight.IsAlive)
                        {
                            knightCasualties++;
                        }    
                    }
                }

                //knightsInitialCount = knights.RemoveAll(k => k.IsAlive == false);

                //for (int i = 0; i < knights.Count; i++)
                //{
                //    for (int j = 0; j < barbarians.Count; j++)
                //    {
                //        if (knights[i].Weapon.Durability == 0)
                //        {
                //            knights[i] = knights[i + 1];
                //        }

                //        barbarians[j].TakeDamage(knights[i].Weapon.DoDamage());
                //        if (barbarians[j].IsAlive == false)
                //        {
                //            barbarians.Remove(barbarians[j]);
                //            //j = j - 1;
                //        }
                //    }
                //}

                //for (int i = 0; i < barbarians.Count; i++)
                //{
                //    if(barbarians[i].Weapon.Durability == 0)
                //    {
                //        barbarians[i] = barbarians[i + 1];
                //    }

                //    for (int j = 0; j < knights.Count; j++)
                //    {
                //        knights[j].TakeDamage(barbarians[i].Weapon.DoDamage());
                //        if (knights[j].IsAlive == false)
                //        {
                //            knights.Remove(knights[j]);
                //            //j = j - 1;
                //        }
                //    }
                //}


            }

            if (knights.Count > 0)
            {
                return $"The knights took {knightCasualties} casualties but won the battle.";
            }
            else
            {
                return $"The knights took {barbarianCasualties} casualties but won the battle.";
            }
        }
    }
}
