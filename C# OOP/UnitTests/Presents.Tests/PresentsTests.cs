namespace Presents.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class PresentsTests
    {
        private Present present;
        private Bag bag;

        [SetUp]
        public void SetUp()
        {
            present = new Present("Wonderful", 10);
            bag = new Bag();
        }

        [TearDown]
        public void TearDown()
        {
            present = null;
            bag = null;
        }

        [Test]
        public void Present_ConstructorCreatesNewPresent()
        {
            present = new Present("Wonderful", 10);

            Assert.That(present.Name, Is.EqualTo("Wonderful"));
            Assert.That(present.Magic, Is.EqualTo(10));
        }

        [Test]
        public void BagConstructor_CreatesNewEmptyListOfPresents()
        {
            bag = new Bag();
            Assert.AreEqual(0, bag.GetPresents().Count);
        }

        [Test]
        [TestCase(null)]
        public void Create_ThrowsWhenPresentIsNull(Present present)
        {
            ArgumentNullException exception = Assert
                .Throws<ArgumentNullException>(() => bag.Create(present));

            Assert.That(exception.ParamName, Is.EqualTo("Present is null"));
        }

        [Test]
        public void Create_ThrowsWhenPresentAlreadyExists()
        {
            present = new Present("Wonderful", 10);
            bag.Create(present);

            InvalidOperationException exception = Assert
                .Throws<InvalidOperationException>(() => bag.Create(present));

            Assert.That(exception.Message, Is.EqualTo("This present already exists!"));
        }

        [Test]
        public void Create_SuccessfullyAddsNewPresentToTheCollection()
        {
            present = new Present("Wonderful", 10);

            string message = $"Successfully added present {present.Name}.";

            Assert.That(bag.Create(present), Is.EqualTo(message));
            Assert.That(present.Name, Is.EqualTo("Wonderful"));
            Assert.That(present.Magic, Is.EqualTo(10));

            Assert.AreEqual(1, bag.GetPresents().Count);
        }

        [Test]
        public void Remove_RemovesThePresent()
        {
            present = new Present("Wonderful", 10);
            bag.Create(present);

            Assert.That(bag.Remove(present), Is.EqualTo(true));
        }

        [Test]
        public void GetPresentWithLeastMagic_GetsThePresentWithLeastMagic()
        {
            present = new Present("Wonderful", 10);
            Present present2 = new Present("Excellent", 12);
            bag.Create(present);
            bag.Create(present2);

            Assert.That(bag.GetPresentWithLeastMagic, Is.EqualTo(present));
        }

        [Test]
        public void GetPresent_ReturnsPresentWithGivenName()
        {
            present = new Present("Wonderful", 10);
            Present present2 = new Present("Excellent", 12);
            bag.Create(present);
            bag.Create(present2);

            Assert.That(bag.GetPresent("Wonderful"), Is.EqualTo(present));
        }
    }
}
