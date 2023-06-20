using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildFarm.Models.Interfaces;

namespace WildFarm.Models.Animal
{
    public abstract class Animal : IAnimal
    {
        public double increasedWeight;

        public Animal(string name, double weight, double increasedWeight)
        {
            this.increasedWeight = increasedWeight;
            Name = name;
            Weight = weight;
        }

        public string Name {get; private set;}

        public double Weight { get; private set; }

        public int FoodEaten { get; private set; }
        public abstract IReadOnlyCollection<Type> FoodTypes { get; }

        public abstract string AskForFood();

        public void Eat(IFood food)
        {
            if(!FoodTypes.Any(x => food.GetType().Name == x.Name))
            {
                throw new ArgumentException($"{this.GetType().Name} does not eat {food.GetType().Name}!");
            }

            Weight += food.Quantity * increasedWeight;

            FoodEaten += food.Quantity;
        }

        public override string ToString()
        {
            return $"{GetType().Name} [{Name}, ";
        }
    }
}
