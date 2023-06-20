using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaCalories.Models
{
    public class Dough
    {
        private const double DoughCaloriesPerGram = 2;
        private readonly Dictionary<string, double> doughTypeCalories;
        private readonly Dictionary<string, double> doughBakingTechniques;

        private string flourType;
        private string bakingTechnique;
        private double weight;

        public Dough(string flourType, string bakingTechnique, double weight)
        {
            doughTypeCalories =
                new Dictionary<string, double> { { "white", 1.5 }, { "wholegrain", 1.0 } };
            doughBakingTechniques =
                new Dictionary<string, double> { { "crispy", 0.9 }, { "chewy", 1.1 }, { "homemade", 1.0 } };

            FlourType = flourType;
            BakingTechnique = bakingTechnique;
            Weight = weight;
        }
        public double Weight
        {
            get { return weight; }
            private set
            {
                if (value < 1 || value > 200)
                {
                    throw new ArgumentException($"Dough weight should be in the range [1..200].");
                }
                weight = value;
            }
        }
        public string FlourType
        {
            get { return flourType; }
            private set
            {
                if (!doughTypeCalories.ContainsKey(value.ToLower()))
                {
                    throw new ArgumentException($"Invalid type of dough.");
                }

                flourType = value.ToLower();
            }
        }

        public string BakingTechnique
        {
            get { return bakingTechnique; }
            private set
            {
                if (!doughBakingTechniques.ContainsKey(value.ToLower()))
                {
                    throw new ArgumentException($"Invalid type of dough.");
                }

                bakingTechnique = value.ToLower();
            }
        }

        public double Calories
        {
            get
            {
                double typeModifier = doughTypeCalories[FlourType];
                double techniqueModifier = doughBakingTechniques[BakingTechnique];

                return DoughCaloriesPerGram * weight * techniqueModifier * typeModifier;
            }
        }
    }
}
