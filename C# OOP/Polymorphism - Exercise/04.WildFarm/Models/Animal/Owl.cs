using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildFarm.Models.Food;

namespace WildFarm.Models.Animal
{
    public class Owl : Bird
    {
        private const double IncreasedWeight = 0.25;
        public Owl(string name, double weight, double wingSize)
            : base(name, weight, wingSize, IncreasedWeight) { }

        public override IReadOnlyCollection<Type> FoodTypes 
            => new HashSet<Type>() { typeof(Meat)};

        public override string AskForFood()
        {
            return $"Hoot Hoot";
        }
    }
}
