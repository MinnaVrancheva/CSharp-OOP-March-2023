using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Computers.Tests
{
    public class Tests
    {
        private Computer computer;
        private ComputerManager manager;

        [SetUp]
        public void Setup()
        {
            computer = new Computer("Asus", "Z", 2500);
            manager = new ComputerManager();
        }

        [TearDown]
        public void TearDown()
        {
            computer = null;
            manager = null;
        }

        [Test]
        public void ComputerConstructor_CreatesNewComputer()
        {
            computer = new Computer("Asus", "Z", 2500);

            Assert.That(computer, Is.Not.Null);
            Assert.That(computer.Manufacturer, Is.EqualTo("Asus"));
            Assert.That(computer.Model, Is.EqualTo("Z"));
            Assert.That(computer.Price, Is.EqualTo(2500));
        }

        [Test]
        public void ComputerManagerConstructor_CreatesNewCollectionOfComputers()
        {
            Assert.That(manager.Computers.Count, Is.EqualTo(0));
        }

        [Test]
        public void Count_CountsTheComputersCorrectly()
        {
            manager.AddComputer(computer);
            Assert.That(manager.Count, Is.EqualTo(1));
        }

        [Test]
        public void AddComputer_WorksProperly()
        {
            manager.AddComputer(computer);

            Assert.That(manager.Computers.Count, Is.EqualTo(1));
        }

        [Test]
        public void AddComputer_ThrowsWhenManufacturerAndModelExists()
        {
            Computer computer2 = new Computer("Asus", "Z", 2500);
            manager.AddComputer(computer);

            Assert.Throws<ArgumentException>(
                () => manager.AddComputer(computer2),
                $"This computer already exists.");
        }

        [Test]
        public void RemoveComputer_RemovesComputerSuccessfully()
        {
            Computer computer2 = new Computer("Dell", "M", 2500);
            manager.AddComputer(computer);
            manager.AddComputer(computer2);

            Assert.That(manager.RemoveComputer("Asus", "Z"), Is.EqualTo(computer));

        }

        [Test]
        public void GetComputer_ThrowsIfNonExistingComputer()
        {
            manager.AddComputer(computer);

            ArgumentException exception = Assert
               .Throws<ArgumentException>(() => manager.GetComputer("Dell", "M"));

            Assert.That(exception.Message, Is.EqualTo("There is no computer with this manufacturer and model."));
        }

        [Test]
        public void GetComputer_GetsTheCorrectComputer()
        {
            manager.AddComputer(computer);

            Assert.That(manager.GetComputer("Asus", "Z"), Is.EqualTo(computer));
        }

        [Test]
        public void GetComputerByManufacturer_ReturnsCorrectCompueter()
        {
            Computer computer2 = new Computer("Dell", "M", 2500);
            manager.AddComputer(computer);
            manager.AddComputer(computer2);

            List<Computer> computers = new List<Computer> { computer2 };
            var sortedComputers = manager.GetComputersByManufacturer("Dell");

            Assert.AreEqual(computers, sortedComputers);
        }

        [Test]
        public void ValidateNullValue_Test()
        {
            manager.AddComputer(computer);

            Assert.Throws<ArgumentNullException>(
                () => manager.RemoveComputer(null, null));

            ArgumentNullException exception = Assert
               .Throws<ArgumentNullException>(() => manager.AddComputer(null));

            Assert.That(exception.Message, Is.EqualTo("Can not be null! (Parameter 'computer')"));
        }
    }
}