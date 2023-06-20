using Formula1.Models.Contracts;
using Formula1.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formula1.Models
{
    public abstract class FormulaOneCar : IFormulaOneCar
    {
        private string model;
        private int horsePower;
        private double engineDisplacement;

        public FormulaOneCar(string model, int horsePower, double engineDisplacement)
        {
            Model = model;
            Horsepower = horsePower;
            EngineDisplacement = engineDisplacement;
        }

        public string Model
        {
            get { return model; }
            private set
            {
                if(string.IsNullOrWhiteSpace(value) || value.Length < 3)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidF1CarModel, model);
                }
                model = value;
            }
        }

        public int Horsepower
        {
            get { return horsePower; }
            private set
            {
                if(value < 900 || value > 1500)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidF1HorsePower, value));
                }
                horsePower = value;
            }
        }

        public double EngineDisplacement
        {
            get { return engineDisplacement; }
            private set
            {
                if(value < 1.6 || value > 2)
                {
                    throw new ArgumentException(String.Format(ExceptionMessages.InvalidF1EngineDisplacement, value));
                }
                engineDisplacement = value;
            }
        }

        public double RaceScoreCalculator(int laps)
        {
            return this.EngineDisplacement / this.Horsepower * laps;
        }
    }
}
