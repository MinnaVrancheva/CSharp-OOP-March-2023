using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildFarm.Models.Food;

namespace WildFarm.Models.Animal
{
    public class Cat : Feline
    {
        private const double IncreasedWeight = 0.30;
        public Cat(string name, double weight, string livingRegion, string breed)
            : base(name, weight, livingRegion, breed, IncreasedWeight) { }

        public override IReadOnlyCollection<Type> FoodTypes 
            => new HashSet<Type>() { typeof(Vegetable), typeof(Meat) };

        public override string AskForFood()
        {
            return $"Meow";
        }
    }
}
