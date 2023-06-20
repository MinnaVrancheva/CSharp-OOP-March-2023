using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildFarm.Models.Food;

namespace WildFarm.Models.Animal
{
    public class Hen : Bird
    {
        private const double IncreasedWeight = 0.35;
        public Hen(string name, double weight, double wingSize)
            : base(name, weight, wingSize, IncreasedWeight) { }

        public override IReadOnlyCollection<Type> FoodTypes => new HashSet<Type>() { typeof(Vegetable), typeof(Fruit), typeof(Meat), typeof(Seeds) };

        public override string AskForFood()
        {
            return $"Cluck";
        }
    }
}
