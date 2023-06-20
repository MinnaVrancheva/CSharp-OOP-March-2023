using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Robots.Tests
{
    public class RobotsTests
    {
        int capacity = 2;
        string name = "Svetlio";
        int maximumBattery = 5;
        RobotManager robotManager;
        Robot robot;

        [SetUp]
        public void SetUp()
        {
            robotManager = new RobotManager(capacity);
            robot = new Robot(name, maximumBattery);
        }

        [Test]
        public void Robot_ConstructorCorrectlyCreatesNewRobot()
        {
            Robot robot = new Robot(name, maximumBattery);
            robotManager.Add(robot);

            Assert.That(robot.Name, Is.EqualTo(name));
            Assert.That(robot.MaximumBattery, Is.EqualTo(maximumBattery));
            Assert.That(robotManager.Count, Is.EqualTo(1));
        }

        [Test]
        public void RobotManager_ConstructorCorrectlyCreatesNewRobotManager()
        {
            Assert.AreEqual(capacity, robotManager.Capacity);
        }

        [Test]
        public void Capacity_ThrowsWhenValueIsBelowZero()
        {
            int newCapacity = -1;

            Assert.Throws<ArgumentException>(
                () => new RobotManager(newCapacity),
                $"Invalid capacity!");
        }

        [Test]
        public void Count_Test()
        {
            robotManager.Add(robot);
            int expectedCount = 1;

            Assert.That(robotManager.Count, Is.EqualTo(expectedCount));
        }

        [Test]
        public void Add_ThrowsWhenRobotExists()
        {
            robotManager.Add(robot);
            Robot robot2 = new Robot(name, 4);

            Assert.Throws<InvalidOperationException>(
                () => robotManager.Add(robot2),
                $"There is already a robot with name {robot.Name}!");
        }

        [Test]
        public void Add_ThrowsWhenCapacityReached()
        {
            Robot robot2 = new Robot("Maxim", 4);
            robotManager.Add(robot);
            robotManager.Add(robot2);
            Robot robot3 = new Robot("Jeki", 2);

            Assert.Throws<InvalidOperationException>(
                () => robotManager.Add(robot3),
                $"Not enough capacity!");

        }

        [Test]
        public void Remove_ThrowsWhenRobotDoesNotExist()
        {
            robotManager.Add(robot);
            Assert.Throws<InvalidOperationException>(
                () => robotManager.Remove("Maxim"),
                $"Robot with the name Maxim doesn't exist!");
        }

        [Test]
        public void Remove_WorksCorrectly()
        {
            robotManager.Add(robot);
            robotManager.Remove("Svetlio");

            Assert.That(robotManager.Count, Is.EqualTo(0)); 
        }

        [Test]
        public void Work_ThrowsWhenRobotDoesNotExist()
        {
            string job = "construction";
            int batteryUsage = 3;

            Robot robot = new Robot(name, maximumBattery);
            robotManager.Add(robot);

            Assert.Throws<InvalidOperationException>(
                () => robotManager.Work("Maxim", job, batteryUsage),
                $"Robot with the name Maxim doesn't exist!");
        }

        [Test]
        public void Work_ThrowsWhenBatteryIsLowerThanBatteryUsage()
        {
            int batteryUsage = 7;
            robotManager.Add(robot);

            Assert.Throws<InvalidOperationException>(
                () => robotManager.Work(name, "Nothing new", batteryUsage),
                $"{robot.Name} doesn't have enough battery!");
        }

        [Test]
        public void Work_WorksAsExpected()
        {
            int batteryUsage = 3;
            int expectedBattery = robot.Battery - batteryUsage;

            robotManager.Add(robot);
            robotManager.Work(name, "Getting Ready", batteryUsage);

            Assert.That(robot.Battery, Is.EqualTo(expectedBattery));
        }

        [Test]
        public void Charge_ThrowsWhenRobotDoesNotExist()
        {
            Assert.Throws<InvalidOperationException>(
                () => robotManager.Charge("Misho"),
                $"Robot with the name Misho doesn't exist!");
        }

        [Test]
        public void Charge_ChargesTheRobotBattery()
        {
            robotManager.Add(robot);
            robotManager.Work(name, "testing", 3);

            robotManager.Charge(name);
            Assert.That(robot.Battery, Is.EqualTo(5));
        }
    }
}
