using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaCalories.Models
{
    public class Topping
    {

        private double ToppingCaloriesPerGram = 2;
        private readonly Dictionary<string, double> toppingTypes;

        private string toppingType;
        private double weight;
        public Topping(string toppingType, double weight)
        {
            toppingTypes =
                new Dictionary<string, double>() { { "meat", 1.2 }, { "veggies", 0.8 }, { "cheese", 1.1 }, { "sauce", 0.9 } };

            ToppingType = toppingType;
            Weight = weight;
        }

        public string ToppingType
        {
            get { return toppingType; }
            private set
            {
                if (!toppingTypes.ContainsKey(value.ToLower()))
                {
                    throw new ArgumentException($"Cannot place {value} on top of your pizza.");
                }
                toppingType = value;
            }
        }

        public double Weight
        {
            get
            { return weight; }
            private set
            {
                if (value < 1 || value > 50)
                {
                    throw new ArgumentException($"{toppingType} weight should be in the range [1..50].");
                }
                weight = value;
            }
        }

        public double Calories
        {
            get
            {
                double typeModifier = toppingTypes[ToppingType.ToLower()];

                return ToppingCaloriesPerGram * weight * typeModifier;
            }
        }
    }
}
