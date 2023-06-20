using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildFarm.Models.Interfaces;

namespace WildFarm.Models.Animal
{
    public abstract class Bird : Animal, IBird
    {
        protected Bird(string name, double weight, double wingSize, double increasedWeight)
            : base(name, weight, increasedWeight) 
        {
            WingSize = wingSize;
        }

        public double WingSize {get; private set;}

        public override string ToString()
        {
            return base.ToString() + $"{WingSize}, {Weight}, {FoodEaten}]";
        }
    }
}
