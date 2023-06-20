using OnlineShop.Common.Constants;
using OnlineShop.Models.Products.Computers;
using OnlineShop.Models.Products.Peripherals;
using OnlineShop.Models.Products.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineShop.Core
{
    public class Controller : IController
    {
        private List<IComputer> computers;
        private List<IPeripheral> peripherals;
        private List<IComponent> components;

        public Controller()
        {
            computers = new List<IComputer>();
            peripherals = new List<IPeripheral>();
            components = new List<IComponent>();
        }
        public string AddComponent(int computerId, int id, string componentType, string manufacturer, string model, decimal price, double overallPerformance, int generation)
        {
            IComputer computer = computers.FirstOrDefault(x => x.Id == computerId);

            if (computer == null)
            {
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);
            }

            if(components.Any(x => x.Id == id))
            {
                throw new ArgumentException(ExceptionMessages.ExistingComponentId);
            }

            IComponent component;
            switch (componentType)
            {
                case "CentralProcessingUnit":
                    component = new CentralProcessingUnit(id, manufacturer, model, price, overallPerformance, generation); break;
                case "Motherboard":
                    component = new Motherboard(id, manufacturer, model, price, overallPerformance, generation); break;
                case "PowerSupply":
                    component = new PowerSupply(id, manufacturer, model, price, overallPerformance, generation); break;
                case "RandomAccessMemory":
                    component = new RandomAccessMemory(id, manufacturer, model, price, overallPerformance, generation); break;
                case "SolidStateDrive":
                    component = new SolidStateDrive(id, manufacturer, model, price, overallPerformance, generation); break;
                case "VideoCard":
                    component = new VideoCard(id, manufacturer, model, price, overallPerformance, generation); break;
                default: throw new ArgumentException(ExceptionMessages.InvalidComponentType);
            }

            computer.AddComponent(component);
            components.Add(component);
            return string.Format(SuccessMessages.AddedComponent, componentType, id, computerId);
        }

        public string AddComputer(string computerType, int id, string manufacturer, string model, decimal price)
        {
            if (computers.Any(x => x.Id == id))
            {
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);
            }

            IComputer computer;
            if (computerType == nameof(DesktopComputer))
            {
                computer = new DesktopComputer(id, manufacturer, model, price);
            }
            else if (computerType == nameof(Laptop))
            {
                computer = new Laptop(id, manufacturer, model, price);
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.InvalidComputerType);
            }

            computers.Add(computer);
            return string.Format(SuccessMessages.AddedComputer, id);
        }

        public string AddPeripheral(int computerId, int id, string peripheralType, string manufacturer, string model, decimal price, double overallPerformance, string connectionType)
        {
            IComputer computer = computers.FirstOrDefault(x => x.Id == computerId);

            if (computer == null)
            {
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);
            }

            if (peripherals.Any(x => x.Id == id))
            {
                throw new ArgumentException(ExceptionMessages.ExistingPeripheralId);
            }

            IPeripheral peripheral;
            switch (peripheralType)
            {
                case "Headset":
                    peripheral = new Headset(id, manufacturer, model, price, overallPerformance, connectionType);
                    break;
                case "Keyboard":
                    peripheral = new Keyboard(id, manufacturer, model, price, overallPerformance, connectionType);
                    break;
                case "Mouse":
                    peripheral = new Mouse(id, manufacturer, model, price, overallPerformance, connectionType);
                    break;
                case "Monitor":
                    peripheral = new Monitor(id, manufacturer, model, price, overallPerformance, connectionType);
                    break;
                default: throw new ArgumentException(ExceptionMessages.InvalidPeripheralType);
            }

            computer.AddPeripheral(peripheral);
            peripherals.Add(peripheral);
            return string.Format(SuccessMessages.AddedPeripheral, peripheralType, id, computerId);
        }

        public string BuyBest(decimal budget)
        {
            IComputer computer = computers.OrderByDescending(x => x.OverallPerformance).FirstOrDefault(x => x.Price <= budget);

            if (!computers.Any() || computer == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CanNotBuyComputer, budget));
            }

            computers.Remove(computer);
            return computer.ToString();
        }

        public string BuyComputer(int id)
        {
            IComputer computer = computers.FirstOrDefault(x => x.Id == id);

            if (computer == null)
            {
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);
            }

            computers.Remove(computer);
            return computer.ToString();
        }

        public string GetComputerData(int id)
        {
            IComputer computer = computers.FirstOrDefault(x => x.Id == id);

            if (computer == null)
            {
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);
            }

            return computer.ToString();
        }

        public string RemoveComponent(string componentType, int computerId)
        {
            IComputer computer = computers.FirstOrDefault(x => x.Id == computerId);

            if (computer == null)
            {
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);
            }

            IComponent component = components.FirstOrDefault(x => x.GetType().Name == componentType);

            computer.RemoveComponent(componentType);
            components.Remove(component);
            return string.Format(SuccessMessages.RemovedComponent, componentType, component.Id);
        }

        public string RemovePeripheral(string peripheralType, int computerId)
        {
            IComputer computer = computers.FirstOrDefault(x => x.Id == computerId);

            if (computer == null)
            {
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);
            }

            IPeripheral peripheral = peripherals.FirstOrDefault(x => x.GetType().Name == peripheralType);

            computer.RemovePeripheral(peripheralType);
            peripherals.Remove(peripheral);
            return string.Format(SuccessMessages.RemovedPeripheral, peripheralType, peripheral.Id);
        }
    }
}
