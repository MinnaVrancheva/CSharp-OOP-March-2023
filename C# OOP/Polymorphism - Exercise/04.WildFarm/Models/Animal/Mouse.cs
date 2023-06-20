using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildFarm.Models.Food;

namespace WildFarm.Models.Animal
{
    public class Mouse : Mammal
    {
        private const double IncreasedWeight = 0.10;
        public Mouse(string name, double weight, string livingRegion)
            : base(name, weight, livingRegion, IncreasedWeight) { }

        public override IReadOnlyCollection<Type> FoodTypes 
            => new HashSet<Type>() { typeof(Vegetable), typeof(Fruit) };
        public override string AskForFood()
        {
            return $"Squeak";
        }

        public override string ToString()
        {
            return base.ToString() + $"{Weight}, {LivingRegion}, {FoodEaten}]";
        }
    }
}
