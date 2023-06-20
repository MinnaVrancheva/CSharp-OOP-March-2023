using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildFarm.Models.Interfaces;

namespace WildFarm.Models.Animal
{
    public abstract class Mammal : Animal, IMammal
    {
        protected Mammal(string name, double weight, string livingRegion, double increasedWeight) 
            : base(name, weight, increasedWeight)
        {
            LivingRegion = livingRegion;
        }

        public string LivingRegion {get; private set;}
    }
}
