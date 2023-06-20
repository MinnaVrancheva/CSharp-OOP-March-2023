using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildFarm.Models.Food;

namespace WildFarm.Models.Animal
{
    public class Dog : Mammal
    {
        private const double IncreasedWeight = 0.40;
        public Dog(string name, double weight, string livingRegion)
            : base(name, weight, livingRegion, IncreasedWeight) { }

        public override IReadOnlyCollection<Type> FoodTypes => new HashSet<Type>() { typeof(Meat)};

        public override string AskForFood()
        {
            return $"Woof!";
        }

        public override string ToString()
        {
            return base.ToString() + $"{Weight}, {LivingRegion}, {FoodEaten}]";
        }
    }
}
