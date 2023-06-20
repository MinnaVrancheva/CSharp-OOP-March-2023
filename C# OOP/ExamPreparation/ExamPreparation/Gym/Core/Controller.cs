using Gym.Core.Contracts;
using Gym.Models.Athletes;
using Gym.Models.Athletes.Contracts;
using Gym.Models.Equipment;
using Gym.Models.Equipment.Contracts;
using Gym.Models.Gyms;
using Gym.Models.Gyms.Contracts;
using Gym.Repositories;
using Gym.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gym.Core
{
    public class Controller : IController
    {
        private EquipmentRepository equipment;
        private List<IGym> gyms;

        public Controller()
        {
            equipment = new EquipmentRepository();
            gyms = new List<IGym>();
        }
        public string AddAthlete(string gymName, string athleteType, string athleteName, string motivation, int numberOfMedals)
        {
            IAthlete athlete;
            if(athleteType != nameof(Boxer) && athleteType != nameof(Weightlifter))
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAthleteType);
            }
            if (athleteType == nameof(Boxer))
            {
                athlete = new Boxer(athleteName, motivation, numberOfMedals);
            }
            else
            {
                athlete = new Weightlifter(athleteName, motivation, numberOfMedals);
            }

            IGym gym = gyms.FirstOrDefault(x => x.Name == gymName);
            if(athleteType == nameof(Boxer) && gym.GetType().Name == nameof(BoxingGym))
            {
                gym.AddAthlete(athlete);
            }
            else if (athleteType == nameof(Weightlifter) && gym.GetType().Name == nameof(WeightliftingGym))
            {
                gym.AddAthlete(athlete);
            }
            else
            {
                return OutputMessages.InappropriateGym;
            }

            return string.Format(OutputMessages.EntityAddedToGym, athleteType, gymName);
        }

        public string AddEquipment(string equipmentType)
        {
            IEquipment newEquipment;
            if (equipmentType == nameof(BoxingGloves))
            {
                newEquipment = new BoxingGloves();
            }
            else if (equipmentType == nameof(Kettlebell))
            {
                newEquipment = new Kettlebell();
            }
            else   
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidEquipmentType);
            }

            equipment.Add(newEquipment);
            return string.Format(OutputMessages.SuccessfullyAdded, equipmentType);
        }

        public string AddGym(string gymType, string gymName)
        {
            IGym gym;
            if(gymType == nameof(BoxingGym))
            {
                gym = new BoxingGym(gymName);
            }
            else if (gymType == nameof(WeightliftingGym))
            {
                gym = new WeightliftingGym(gymName);
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidGymType);
            }

            gyms.Add(gym);
            return string.Format(OutputMessages.SuccessfullyAdded, gymType);
        }

        public string EquipmentWeight(string gymName)
        {
            IGym gym = gyms.FirstOrDefault(gyms => gyms.Name == gymName);
            double weight = gym.EquipmentWeight;

            return string.Format(OutputMessages.EquipmentTotalWeight, gymName, weight);
        }

        public string InsertEquipment(string gymName, string equipmentType)
        {
            IEquipment newEquipment = equipment.FindByType(equipmentType);
            if(newEquipment == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InexistentEquipment, equipmentType));
            }

            IGym gym = gyms.FirstOrDefault(x => x.Name == gymName);
            gym.AddEquipment(newEquipment);
            equipment.Remove(newEquipment);

            return string.Format(OutputMessages.EntityAddedToGym, equipmentType, gymName);
        }

        public string TrainAthletes(string gymName)
        {
            IGym gym = gyms.FirstOrDefault(gyms => gyms.Name == gymName);
            gym.Exercise();

            return String.Format(OutputMessages.AthleteExercise, gym.Athletes.Count());
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var gym in gyms)
            {
                sb.AppendLine(gym.GymInfo());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
