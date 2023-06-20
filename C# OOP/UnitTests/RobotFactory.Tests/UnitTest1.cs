using NUnit.Framework;

namespace RobotFactory.Tests
{
    public class Tests
    {
        string model = "Babe";
        double price = 2.2;
        int interfaceStandard = 202;

        string factoryName = "NewFactory";
        int capacity = 1;

        string supplementName = "newSupplement";
       

        Robot robot;
        Factory factory;
        Supplement supplement;

        [SetUp]
        public void Setup()
        {
            robot = new Robot(model, price, interfaceStandard);
            factory = new Factory(factoryName, capacity);
            supplement = new Supplement(supplementName, interfaceStandard);
        }

        [Test]
        public void Robot_ConstructorCorrectlyCreatesNewRobot()
        {
            //Robot robot = new Robot(model, price, interfaceStandard);
            factory.ProduceRobot(model, price, interfaceStandard);

            Assert.That(robot.Model, Is.EqualTo(model));
            Assert.That(robot.Price, Is.EqualTo(price));
            Assert.That(robot.InterfaceStandard, Is.EqualTo(interfaceStandard));
            Assert.That(factory.Robots.Count, Is.EqualTo(1));
        }

        [Test]
        public void Robot_ConstructorCorrectlyCreatesNewListOfSupplements()
        {
            robot.Supplements.Add(supplement);
            
            int expectedCount = 1;

            Assert.That(robot.Supplements.Count, Is.EqualTo(expectedCount));

        }

        [Test]
        public void Supplement_ConstructorCorrectlyCreatesNewSupplement()
        {
            supplement = new Supplement(supplementName, interfaceStandard);
            Assert.That(supplement.Name, Is.EqualTo(supplementName));
            Assert.That(supplement.InterfaceStandard, Is.EqualTo(interfaceStandard));
        }

        [Test]
        public void ProduceRobot_CannotCreateNewRobotWhenCapacityReached()
        {
            factory.ProduceRobot(model, price, interfaceStandard);
            string message = "The factory is unable to produce more robots for this production day!";

            Assert.That(factory.ProduceRobot("Maxim", 4, 302), Is.EqualTo(message));
        }

        [Test]
        public void ProduceRobot_producesNewRobotSuccessfully()
        {
            string message = $"Produced --> {robot}";

            Assert.That(factory.ProduceRobot(model, price, interfaceStandard), Is.EqualTo(message));
        }

        [Test]
        public void ProduceSupplement_ProducesNewSupplement()
        {
            string message = $"Supplement: {supplement.Name} IS: {supplement.InterfaceStandard}";

            Assert.That(factory.ProduceSupplement(supplementName, interfaceStandard), Is.EqualTo(message));
        }

        [Test]
        public void UpgradeRobot_UpgradesTheRobotSuccessfully()
        {
            Assert.That(factory.UpgradeRobot(robot, supplement), Is.EqualTo(true));
        }

        [Test]
        public void UpgradeRobot_DoesNotUpgradeMissingRobot()
        {
            factory.UpgradeRobot(robot, supplement);
            Supplement newSupplement = new Supplement("second", 505);

            Assert.That(factory.UpgradeRobot(robot, supplement), Is.EqualTo(false));
            Assert.That(factory.UpgradeRobot(robot, newSupplement), Is.EqualTo(false));
        }

        [Test]
        public void SellRobot_CreatesListOfOrderedRobots()
        {
            int newPrice = 5;
            Robot robot = new Robot("Janis", 1.1, 204);
            factory.ProduceRobot("Janis", 1.1, 204);

            var orderedRobot = factory.SellRobot(newPrice);

            Assert.That(orderedRobot.Model, Is.EqualTo(robot.Model));
            Assert.That(robot.Model, Is.EqualTo("Janis"));

            
        }

        [Test]
        public void FactoryConstructor()
        {
            robot.Supplements.Add(supplement);
            factory.ProduceRobot(model, price, interfaceStandard);

            int expectedCount = 1;

            Assert.That(robot.Supplements.Count, Is.EqualTo(expectedCount));
            Assert.That(factory.Robots.Count, Is.EqualTo(expectedCount));
        }
    }
}