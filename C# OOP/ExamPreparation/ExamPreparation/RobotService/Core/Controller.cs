using RobotService.Core.Contracts;
using RobotService.Models;
using RobotService.Models.Contracts;
using RobotService.Repositories;
using RobotService.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Core
{
    public class Controller : IController
    {
        private SupplementRepository supplements;
        private RobotRepository robots;

        public Controller()
        {
            this.supplements = new SupplementRepository();
            this.robots = new RobotRepository();
        }
        public string CreateRobot(string model, string typeName)
        {
            IRobot robot;
            if (typeName == nameof(DomesticAssistant))
            {
                robot = new DomesticAssistant(model);
            }
            else if (typeName == nameof(IndustrialAssistant))
            {
                robot = new IndustrialAssistant(model);
            }
            else
            {
                return string.Format(OutputMessages.RobotCannotBeCreated, typeName);
            }
            robots.AddNew(robot);
            return string.Format(OutputMessages.RobotCreatedSuccessfully, typeName, model);
        }

        public string CreateSupplement(string typeName)
        {
            ISupplement supplement;
            if (typeName == nameof(SpecializedArm))
            {
                supplement = new SpecializedArm();
            }
            else if (typeName == nameof(LaserRadar))
            {
                supplement = new LaserRadar();
            }
            else
            {
                return String.Format(OutputMessages.SupplementCannotBeCreated, typeName);
            }

            supplements.AddNew(supplement);
            return string.Format(OutputMessages.SupplementCreatedSuccessfully, typeName);
        }

        public string PerformService(string serviceName, int intefaceStandard, int totalPowerNeeded)
        {
            var selectedRobots = robots.Models().Where(x => x.InterfaceStandards.Contains(intefaceStandard)).OrderByDescending(x => x.BatteryLevel).ToList();

            if (selectedRobots.Count == 0)
            {
                return string.Format(OutputMessages.UnableToPerform, intefaceStandard);
            }

            int sumOfbatteryLevels = selectedRobots.Sum(x => x.BatteryLevel);

            if (sumOfbatteryLevels < totalPowerNeeded)
            {
                return string.Format(OutputMessages.MorePowerNeeded, serviceName, totalPowerNeeded - sumOfbatteryLevels);
            }

            int robotsCounter = 0;

            foreach (var robot in selectedRobots)
            {
                robotsCounter++;

                if (robot.BatteryLevel >= totalPowerNeeded)
                {
                    robot.ExecuteService(totalPowerNeeded);
                    break;
                }
                else
                {
                    totalPowerNeeded -= robot.BatteryLevel;
                    robot.ExecuteService(robot.BatteryLevel);
                }
            }
            
            return string.Format(OutputMessages.PerformedSuccessfully, serviceName, robotsCounter);
        }

        public string Report()
        {
            var selectedRobots = robots.Models().OrderByDescending(x => x.BatteryLevel).ThenBy(x => x.BatteryCapacity).ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var robot in selectedRobots)
            {
                sb.AppendLine(robot.ToString());
            }

            return sb.ToString().TrimEnd();
        }

        public string RobotRecovery(string model, int minutes)
        {
            var selectedRobots = robots.Models().Where(x => x.Model == model && x.BatteryLevel < x.BatteryCapacity / 2).ToList();
            int count = 0;

            foreach (var robot in selectedRobots)
            {
                robot.Eating(minutes);
                count++;
            }

            return string.Format(OutputMessages.RobotsFed, count);
        }

        public string UpgradeRobot(string model, string supplementTypeName)
        {
            ISupplement supplement = supplements.Models().FirstOrDefault(x => x.GetType().Name == supplementTypeName);
            int interfaceValue = supplement.InterfaceStandard;
            
            var selectedRobots = robots.Models().Where(x => !x.InterfaceStandards.Contains(interfaceValue) && x.Model == model).ToList();

            if (selectedRobots.Count == 0)
            {
                return string.Format(OutputMessages.AllModelsUpgraded, model);
            }

            IRobot selectedRobot = selectedRobots.FirstOrDefault();
            selectedRobot.InstallSupplement(supplement);
            supplements.RemoveByName(supplementTypeName);

            return string.Format(OutputMessages.UpgradeSuccessful, model, supplementTypeName);
        }
    }
}
