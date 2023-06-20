using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildFarm.Core.Interfaces;
using WildFarm.Factories.Interfaces;
using WildFarm.Models.Interfaces;

namespace WildFarm.Core
{
    public class Engine : IEngine
    {
        private readonly IAnimalFactory animalFactory;
        private readonly IFoodFactory foodFactory;
        private readonly ICollection<IAnimal> animals;

        public Engine(
            IAnimalFactory animalFactory, 
            IFoodFactory foodFactory)
        {
            this.animalFactory = animalFactory;
            this.foodFactory = foodFactory;
            animals = new List<IAnimal>();
        }

        public void Run()
        {
            string command;

            while ((command = Console.ReadLine()) != "End")
            {
                IAnimal animal = null;
                IFood food;

                try
                {
                    string[] animalArg = command
                        .Split(" ", StringSplitOptions.RemoveEmptyEntries); 
                    string[] foodArg = Console.ReadLine()
                        .Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    string foodType = foodArg[0];
                    int foodQuantity = int.Parse(foodArg[1]);
                    
                    animal = animalFactory.CreateAnimal(animalArg);
                    food = foodFactory.CreateFood(foodType, foodQuantity);
                    Console.WriteLine(animal.AskForFood());

                    animal.Eat(food);
                    
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                animals.Add(animal);
            }

            foreach (IAnimal animal in animals)
            {
                Console.WriteLine(animal);
            }
        }
    }
}
