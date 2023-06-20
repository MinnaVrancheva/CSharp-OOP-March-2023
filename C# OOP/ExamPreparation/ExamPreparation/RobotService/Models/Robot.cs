using RobotService.Models.Contracts;
using RobotService.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Models
{
    public abstract class Robot : IRobot
    {
        private string model;
        private int batteryCapacity;
        private int batteryLevel;
        private int convertionCapacityIndex;
        private List<int> interfaceStandards;
        public Robot(string model, int batteryCapacity, int conversionCapacityIndex)
        {
            Model = model;
            BatteryCapacity = batteryCapacity;
            this.batteryLevel = batteryCapacity;
            this.convertionCapacityIndex = conversionCapacityIndex;
            interfaceStandards = new List<int>();
        }

        public string Model
        {
            get { return model; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.ModelNullOrWhitespace);
                }
                model = value;
            }
        }

        public int BatteryCapacity
        {
            get { return batteryCapacity; }
            protected set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.BatteryCapacityBelowZero);
                }
                batteryCapacity = value;
            }
        }

        public int BatteryLevel => this.batteryLevel;

        public int ConvertionCapacityIndex => this.convertionCapacityIndex;

        public IReadOnlyCollection<int> InterfaceStandards => this.interfaceStandards;

        public void Eating(int minutes)
        {
            int energy = ConvertionCapacityIndex * minutes;

            if (energy > BatteryCapacity - BatteryLevel)
            {
                batteryLevel = batteryCapacity;
            }
            else
            {
                batteryLevel += energy;
            }
        }

        public bool ExecuteService(int consumedEnergy)
        {
            if (batteryLevel >= consumedEnergy)
            {
                batteryLevel -= consumedEnergy;
                return true;
            }
            return false;
        }

        public void InstallSupplement(ISupplement supplement)
        {
            interfaceStandards.Add(supplement.InterfaceStandard);
            BatteryCapacity -= supplement.BatteryUsage;
            batteryLevel -= supplement.BatteryUsage;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            string supplemetsInstalled = interfaceStandards.Any() ? string.Join(", ", interfaceStandards) : "none"; 

            sb.AppendLine($"{this.GetType().Name} {Model}");
            sb.AppendLine($"--Maximum battery capacity: {BatteryCapacity}");
            sb.AppendLine($"--Current battery level: {BatteryLevel}");
            sb.AppendLine($"--Supplement installed: {supplemetsInstalled}");

            return sb.ToString().TrimEnd();
        }
    }
}
