using CarRacing.Models.Cars.Contracts;
using CarRacing.Repositories.Contracts;
using CarRacing.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarRacing.Repositories
{
    public class CarRepository : IRepository<ICar>
    {
        private readonly List<ICar> cars;

        public CarRepository()
        {
            this.cars = new List<ICar>();
        }
        public IReadOnlyCollection<ICar> Models => this.cars;

        public void Add(ICar model)
        {
            if(model == null)
            {
                throw new ArgumentException(ExceptionMessages.InvalidAddCarRepository);
            }
            cars.Add(model);
        }

        public ICar FindBy(string property)
        {
            return cars.FirstOrDefault(x => x.VIN == property);
        }

        public bool Remove(ICar model)
        {
            return this.cars.Remove(model);
        }
    }
}
