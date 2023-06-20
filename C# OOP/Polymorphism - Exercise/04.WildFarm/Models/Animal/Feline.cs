using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildFarm.Models.Interfaces;

namespace WildFarm.Models.Animal
{
    public abstract class Feline : Mammal, IFeline
    {
        protected Feline(string name, double weight, string livingRegion, string breed, double increasedWeight)
            : base(name, weight, livingRegion, increasedWeight)
        {
            Breed = breed;
        }

        public string Breed { get; private set; }

        public override string ToString()
        {
            return base.ToString() + $"{Breed}, {Weight}, {LivingRegion}, {FoodEaten}]";
        }
    }
}
