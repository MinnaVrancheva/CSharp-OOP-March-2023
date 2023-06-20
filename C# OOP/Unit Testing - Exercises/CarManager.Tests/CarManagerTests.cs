namespace CarManager.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class CarManagerTests
    {
        private Car car;
        [SetUp]
        public void SetUp()
        {
            car = new Car("Ford", "Mustang", 11.2, 75);
        }

        [TearDown]
        public void TearDown()
        {
            car = null;
        }

        [Test]
        public void Constructor_CreatesCar()
        {
            car = new Car("Ford", "Mustang", 11.2, 75);

            Assert.That(car.Make, Is.EqualTo("Ford"));
            Assert.That(car.Model, Is.EqualTo("Mustang"));
            Assert.That(car.FuelConsumption, Is.EqualTo(11.2));
            Assert.That(car.FuelCapacity, Is.EqualTo(75));
            Assert.That(car.FuelAmount, Is.EqualTo(0));
        }

        [Test]
        public void Constructor_CreateCarThrowsIfMakeIsNull()
        {
            Assert.Throws<ArgumentException>(
                () => new Car(null, "Mustang", 11.2, 75),
                $"Make cannot be null or empty!");
        }

        [Test]
        public void Constructor_CreateCarThrowsIfMakeIsEmpty()
        {
            Assert.Throws<ArgumentException>(
                () => new Car(string.Empty, "Mustang", 11.2, 75),
                $"Make cannot be null or empty!");
        }

        [Test]
        public void Constructor_CreateCarThrowsIfModelIsNull()
        {
            Assert.Throws<ArgumentException>(
                () => new Car("Ford", null, 11.2, 75),
                $"Model cannot be null or empty!");
        }

        [Test]
        public void Constructor_CreateCarThrowsIfModelIsEmpty()
        {
            Assert.Throws<ArgumentException>(
                () => new Car("Ford", string.Empty, 11.2, 75),
                $"Make cannot be null or empty!");
        }

        [Test]

        [TestCase(0)]
        [TestCase(-5)]
        public void Constructor_CreateCarThrowsIfFuelConsumptionIsEqualOrLessThanZero(double fuelConsumption)
        {
            ArgumentException exception = Assert
                .Throws<ArgumentException>(() => new Car("Ford", "Mustang", fuelConsumption, 75));

            Assert.That(exception.Message, Is.EqualTo("Fuel consumption cannot be zero or negative!"));
        }

        [Test]

        [TestCase(0)]
        [TestCase(-6)]
        public void Constructor_CreateCarThrowsIfFuelAmountIsEqualOrLessThanZero(double fuelCapacity)
        {
            ArgumentException exception = Assert
                .Throws<ArgumentException>(() => new Car("Ford", "Mustang", 11.2, fuelCapacity));

            Assert.That(exception.Message, Is.EqualTo("Fuel capacity cannot be zero or negative!"));
        }

        [Test]
        [TestCase(0)]
        [TestCase(-6)]
        public void Refuel_ThrowsWhenFuelToRefuelIsLessThanZero(double fuelToRefuel)
        {
            ArgumentException exception = Assert
                .Throws<ArgumentException>(() => car.Refuel(fuelToRefuel));

            Assert.That(exception.Message, Is.EqualTo("Fuel amount cannot be zero or negative!"));
        }

        [Test]
        public void Refuel_RefuelsTheCarWhenEqualToFuelAmount()
        {
            car.Refuel(34);

            Assert.AreEqual(34, car.FuelAmount);
        }

        [Test]
        public void Refuel_RefuelsTheCarWhenEqualToFuelCapacity()
        {
            car.Refuel(78);

            Assert.AreEqual(75, car.FuelAmount);
        }

        [Test]
        public void Drive_ThrowsWhenFuelNeededIsMoreThanFuelAmount()
        {
            InvalidOperationException exception = Assert
                .Throws<InvalidOperationException>(() => car.Drive(12));

            Assert.That(exception.Message, Is.EqualTo("You don't have enough fuel to drive!"));
        }

        [Test]
        public void Drive_WorksAsExpected()
        {
            car.Refuel(20);
            car.Drive(100);
            Assert.AreEqual(8.8, car.FuelAmount);
        }
    }
}