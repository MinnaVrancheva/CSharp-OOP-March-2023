using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildFarm.Models.Food;

namespace WildFarm.Models.Animal
{
    public class Tiger : Feline
    {
        private const double IncreasedWeight = 1.00;
        public Tiger(string name, double weight, string livingRegion, string breed)
            : base(name, weight, livingRegion, breed, IncreasedWeight) { }

        public override IReadOnlyCollection<Type> FoodTypes 
            => new HashSet<Type>() { typeof(Meat)};

        public override string AskForFood()
        {
            return $"ROAR!!!";
        }
    }
}
