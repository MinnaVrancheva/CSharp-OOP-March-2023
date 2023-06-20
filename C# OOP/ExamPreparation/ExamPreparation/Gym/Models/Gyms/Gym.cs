using Gym.Models.Athletes.Contracts;
using Gym.Models.Equipment.Contracts;
using Gym.Models.Gyms.Contracts;
using Gym.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gym.Models.Gyms
{
    public abstract class Gym : IGym
    {
        private string name;
        ICollection<IEquipment> equipment;
        ICollection<IAthlete> athletes;
        
        public Gym(string name, int capacity)
        {
            this.Name = name;
            Capacity = capacity;

            equipment = new List<IEquipment>();
            athletes = new List<IAthlete>();
        }

        public string Name
        {
            get { return name; }
            private set
            {
                if(string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidGymName);
                }
                name = value;
            }
        }

        public int Capacity { get ; protected set; } 

        public double EquipmentWeight => Equipment.Sum(x => x.Weight);

        public ICollection<IEquipment> Equipment => this.equipment;

        public ICollection<IAthlete> Athletes => this.athletes;

        public void AddAthlete(IAthlete athlete)
        {
            if (athletes.Count == Capacity)
            {
                throw new InvalidOperationException(ExceptionMessages.NotEnoughSize);
            }
            Athletes.Add(athlete);
        }

        public void AddEquipment(IEquipment equipment)
        {
            Equipment.Add(equipment);
        }

        public void Exercise()
        {
            foreach (var athlete in Athletes)
            {
                athlete.Exercise();
            }
        }
        
        public bool RemoveAthlete(IAthlete athlete)
        {
            return Athletes.Remove(athlete);
        }

        public string GymInfo()
        {
            StringBuilder sb = new StringBuilder();
            string athletes = Athletes.Count == 0 ? "No athletes" : string.Join(", ", Athletes.Select(x => x.FullName));

            sb.AppendLine($"{Name} is a {GetType().Name}:");
            sb.AppendLine($"Athletes: {athletes}");
            sb.AppendLine($"Equipment total count: {Equipment.Count}");
            sb.AppendLine($"Equipment total weight: {Equipment.Sum(x => x.Weight):F2} grams");

            return sb.ToString().TrimEnd();
        }
    }
}
