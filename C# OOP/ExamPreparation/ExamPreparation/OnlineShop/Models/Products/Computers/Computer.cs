using OnlineShop.Common.Constants;
using OnlineShop.Models.Products.Components;
using OnlineShop.Models.Products.Peripherals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineShop.Models.Products.Computers
{
    public abstract class Computer : Product, IComputer
    {
        private List<IComponent> components;
        private List<IPeripheral> peripherals;
        protected Computer(int id, string manufacturer, string model, decimal price, double overallPerformance) 
            : base(id, manufacturer, model, price, overallPerformance)
        {
            components = new List<IComponent>();
            peripherals = new List<IPeripheral>();
        }

        public IReadOnlyCollection<IComponent> Components => this.components.AsReadOnly();

        public IReadOnlyCollection<IPeripheral> Peripherals => this.peripherals.AsReadOnly();

        public override double OverallPerformance
        {
            get 
            {
                if (components.Count == 0)
                {
                    return base.OverallPerformance;
                }
                else
                {
                    return base.OverallPerformance + components.Average(x => x.OverallPerformance);
                }
            }
            
        }

        public override decimal Price
        {
            get
            {
                return base.Price + components.Sum(x => x.Price) + peripherals.Sum(x => x.Price);
            }
        }

        public void AddComponent(IComponent component)
        {
            if (components.Any(x => x.GetType().Name == component.GetType().Name))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.ExistingComponent, components.GetType().Name, this.GetType().Name, this.Id));
            }

            components.Add(component);
        }

        public void AddPeripheral(IPeripheral peripheral)
        {
            if (peripherals.Any(x => x.GetType().Name == peripheral.GetType().Name))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.ExistingPeripheral, peripheral.GetType().Name, this.GetType().Name, this.Id));
            }

            peripherals.Add(peripheral);
        }

        public IComponent RemoveComponent(string componentType)
        {
            IComponent component = components.FirstOrDefault(x => x.GetType().Name == componentType);

            if (!components.Any() || component == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.NotExistingComponent, components.GetType().Name, this.GetType().Name, this.Id));
            }

            components.Remove(component);
            return component;
        }

        public IPeripheral RemovePeripheral(string peripheralType)
        {
            IPeripheral peripheral = peripherals.FirstOrDefault(x => x.GetType().Name == peripheralType);

            if (!peripherals.Any() || peripheral == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.NotExistingPeripheral, peripheral.GetType().Name, this.GetType().Name, this.Id));
            }

            peripherals.Remove(peripheral);
            return peripheral;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(base.ToString());
            sb.AppendLine($" Components ({components.Count}):");

            if (components.Count > 0)
            {
                foreach (var component in components)
                {
                    sb.AppendLine($"  {component}");
                }
            }
            
            double averagePerformance = peripherals.Any() ? peripherals.Average(x => x.OverallPerformance) : 0;

            sb.AppendLine($" Peripherals ({peripherals.Count}); Average Overall Performance ({averagePerformance:F2}):");

            if (peripherals.Count > 0)
            {
                foreach (var peripheral in peripherals)
                {
                    sb.AppendLine($"  {peripheral}");
                }
            }

            return sb.ToString().TrimEnd();
        }
    }
}
