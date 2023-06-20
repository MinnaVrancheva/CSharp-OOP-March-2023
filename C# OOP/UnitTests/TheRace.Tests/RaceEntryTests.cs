using NUnit.Framework;
using System;
using TheRace;

namespace TheRace.Tests
{
    public class RaceEntryTests
    {
        private UnitCar car;
        private UnitDriver driver;
        private RaceEntry race;

        [SetUp]
        public void Setup()
        {
            car = new UnitCar("Porshe", 430, 4500);
            driver = new UnitDriver("Ivan", car);
            race = new RaceEntry();
        }

        [TearDown]
        public void TearDown()
        {
            car = null;
            driver = null;
            race = null;
        }

        [Test]
        public void CarConstructor_CreatesNewCar()
        {
            car = new UnitCar("Porshe", 430, 4500);

            Assert.That(car.Model, Is.EqualTo("Porshe"));
            Assert.That(car.HorsePower, Is.EqualTo(430));
            Assert.That(car.CubicCentimeters, Is.EqualTo(4500));
        }

        [Test]
        public void UnitDriverConstructor_ThrowsWhenNameIsNull()
        {
            Assert.Throws<ArgumentNullException>(
                () => new UnitDriver(null, car));
        }

        [Test]
        public void UnitDriverConstructor_CreatesNewDriver()
        {
            car = new UnitCar("Porshe", 430, 4500);
            driver = new UnitDriver("Ivan", car);

            Assert.That(driver.Car.Equals(car), Is.True);
            Assert.That(driver.Name, Is.EqualTo("Ivan"));
        }

        [Test]
        public void RaceEntryConstructor_CreatesNewDriverCollection()
        {
            Assert.That(race.Counter, Is.EqualTo(0));
        }

        [Test]
        public void AddDriver_ThrowsWhenNameIsNull()
        {
            InvalidOperationException exception = Assert
               .Throws<InvalidOperationException>(() => race.AddDriver(null));

            Assert.That(exception.Message, Is.EqualTo("Driver cannot be null."));
        }

        [Test]
        public void AddDriver_ThrowsWhenNameAlreadyExists()
        {
            race.AddDriver(driver);

            InvalidOperationException exception = Assert
               .Throws<InvalidOperationException>(() => race.AddDriver(driver));

            Assert.That(exception.Message, Is.EqualTo($"Driver {driver.Name} is already added."));
        }

        [Test]
        public void AddDriver_SuccessfullyAddsNewDriver()
        {
            string message = $"Driver {driver.Name} added in race.";

            Assert.That(race.AddDriver(driver), Is.EqualTo(message));
            Assert.That(race.Counter, Is.EqualTo(1));
            Assert.That(driver.Name, Is.EqualTo("Ivan"));

        }

        [Test]
        public void CalculateAverageHorsePower_ThrowsWhenLessThanMinPartisipants()
        {
            race.AddDriver(driver);

            InvalidOperationException exception = Assert
               .Throws<InvalidOperationException>(() => race.CalculateAverageHorsePower());

            Assert.That(exception.Message, Is.EqualTo($"The race cannot start with less than 2 participants."));
        }

        [Test]
        public void CalculateAverageHorsePower_CalculatesCorrectly()
        {
            UnitCar car2 = new UnitCar("Mustang", 410, 4100);
            UnitDriver driver2 = new UnitDriver("Sasho", car2);
            race.AddDriver(driver);
            race.AddDriver(driver2);

            double avgHorsePower = 420;

            Assert.That(race.CalculateAverageHorsePower, Is.EqualTo(avgHorsePower));
        }
    }
}