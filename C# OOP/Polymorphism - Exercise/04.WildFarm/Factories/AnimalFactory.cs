using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildFarm.Factories.Interfaces;
using WildFarm.Models.Animal;
using WildFarm.Models.Interfaces;

namespace WildFarm.Factories
{
    public class AnimalFactory : IAnimalFactory
    {
        public IAnimal CreateAnimal(string[] animalArg)
        {
            string animalType = animalArg[0];
            string animalName = animalArg[1];
            double animalWeight = double.Parse(animalArg[2]);

            switch (animalType)
            {
                case "Owl":
                    return new Owl(animalName, animalWeight, double.Parse(animalArg[3]));
                case "Hen":
                    return new Hen(animalName, animalWeight, double.Parse(animalArg[3]));
                case "Mouse":
                    return new Mouse(animalName, animalWeight, animalArg[3]);
                case "Dog":
                    return new Dog(animalName, animalWeight, animalArg[3]);
                case "Cat":
                    return new Cat(animalName, animalWeight, animalArg[3], animalArg[4]);
                case "Tiger":
                    return new Tiger(animalName, animalWeight, animalArg[3], animalArg[4]);
                default:
                    throw new ArgumentException("Invalid animal type");
            }
        }
    }
}
