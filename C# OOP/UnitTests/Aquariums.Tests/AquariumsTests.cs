namespace Aquariums.Tests
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    public class AquariumsTests
    {
        private Fish fish;
        private Aquarium aquarium;
        

        [SetUp]
        public void SetUp()
        {
            fish = new Fish("Elen");
            aquarium = new Aquarium("Exol", 2);
        }

        [TearDown]
        public void TearDown()
        {
            fish = null;
            aquarium = null;
        }

        [Test]
        public void Fish_Constructor_CreatesNewFish()
        {
            fish = new Fish("Elen");

            Assert.That(fish.Name, Is.EqualTo("Elen"));
            Assert.That(fish.Available, Is.True);
        }

        [Test]
        public void Aquarium_Constructor_CreatesNewAquarium()
        {
            aquarium = new Aquarium("Exol", 2);

            Assert.AreEqual(aquarium.Name, "Exol");
            Assert.AreEqual(aquarium.Capacity, 2);
            Assert.AreEqual(aquarium.Count, 0);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void Aquarium_Constructor_ThrowsWhenNameIsIsNullOrEmpty(string name)
        {
            Assert.Throws<ArgumentNullException>(
                () => new Aquarium(name, 2));
        }

        [Test]
        public void Aquarium_Constructor_ThrowsWhenCapacityIsBelowZero()
        {
            ArgumentException exception = Assert
               .Throws<ArgumentException>(() => new Aquarium("Exol", -2));

            Assert.That(exception.Message, Is.EqualTo("Invalid aquarium capacity!"));
        }

        [Test]
        public void Add_ThrowsWhenAquariunIsFull()
        {
            Fish fish2 = new Fish("Sisi");

            aquarium.Add(fish);
            aquarium.Add(fish2);

            InvalidOperationException exception = Assert
               .Throws<InvalidOperationException>(() => aquarium.Add(new Fish("Stenli")));

            Assert.That(exception.Message, Is.EqualTo("Aquarium is full!"));
        }

        [Test]
        public void Add_AddsNewFishSuccessfully()
        {
            aquarium.Add(fish);

            Assert.AreEqual(fish.Name, "Elen");
            Assert.AreEqual(aquarium.Count, 1);
        }

        [Test]
        public void RemoveFish_ThrowsWhenFishNameIsNull()
        {
            aquarium.Add(fish);

            InvalidOperationException exception = Assert
               .Throws<InvalidOperationException>(() => aquarium.RemoveFish("Stenli"));

            Assert.That(exception.Message, Is.EqualTo("Fish with the name Stenli doesn't exist!"));
        }

        [Test]
        public void RemoveFish_RemovesTheFishSuccessfully()
        {
            Fish fish2 = new Fish("Sisi");
            aquarium.Add(fish);
            aquarium.Add(fish2);

            aquarium.RemoveFish("Elen");

            Assert.AreEqual(aquarium.Count, 1);
        }

        [Test]
        public void SellFish_ThrowsWhenFishNameIsNull()
        {
            aquarium.Add(fish);

            InvalidOperationException exception = Assert
               .Throws<InvalidOperationException>(() => aquarium.SellFish("Stenli"));

            Assert.That(exception.Message, Is.EqualTo("Fish with the name Stenli doesn't exist!"));
        }

        [Test]
        public void SellFish_RemovesTheFishSuccessfully()
        {
            Fish fish2 = new Fish("Sisi");
            aquarium.Add(fish);
            aquarium.Add(fish2);

            Assert.AreEqual(aquarium.SellFish("Elen"), fish);
            Assert.AreEqual(aquarium.Count, 2);
            Assert.That(fish.Available, Is.False);
        }

        [Test]
        public void ReportTest()
        {
            aquarium.Add(fish);
            string message = $"Fish available at {aquarium.Name}: {fish.Name}";

            Assert.AreEqual(message, aquarium.Report());
        }
    }
}
