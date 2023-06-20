using NUnit.Framework;
using System;

namespace SmartphoneShop.Tests
{
    [TestFixture]
    public class SmartphoneShopTests
    {
        private Smartphone phone;
        private Shop shop;

        [SetUp]
        public void SetUp()
        {
            phone = new Smartphone("Nokia", 30);
            shop = new Shop(5);
        }


        private List<Smartphone>? phones;

        [Test]
        public void Constructor_CorrectlyCreatesCollectionOfSmartPhones()
        {
            int expectedCount = 1;
            phones.Add(phone);
            Assert.That(phones.Count, Is.EqualTo(expectedCount));
        }

        [Test]
        public void Shop_ConstructorCorrectlySetsCapacity()
        {
            int capacity = 1;
            var shop = new Shop(capacity);
            Assert.That(shop.Capacity, Is.EqualTo(capacity));
        }

        [Test]
        public void Shop_ConstructorThrowsWhenCapacityBelowZero()
        {
            Assert.Throws<ArgumentException>(() => new Shop(-5));
        }

        [Test]
        public void Shop_CountIsCorrectlySet()
        {
            var shop = new Shop(5);
            shop.Add(new Smartphone("Nokia", 30));
            Assert.That(shop.Count, Is.EqualTo(1));
        }

        
        [Test]
        public void SmartPhone_IsAddedToShopCollectionOfPhones()
        {
            Smartphone phone = new Smartphone("Nokia", 30);
            var shop = new Shop(5);
            shop.Add(phone);
            Assert.That(shop.Count, Is.EqualTo(1));
            Assert.That(phone.ModelName, Is.EqualTo("Nokia"));
            Assert.That(phone.MaximumBatteryCharge, Is.EqualTo(30));
        }

        [Test]
        public void Add_ThrowsWhenModelAlreadyExists()
        {
            var shop = new Shop(5);
            Smartphone phone = new Smartphone("Nokia", 30);
            Smartphone phoneTwo = new Smartphone("Nokia", 55);
            shop.Add(phone);
            Assert.Throws<InvalidOperationException>(
                    () => shop.Add(phoneTwo),
                    $"The phone model {phone.ModelName} already exist.");
        }

        [Test]
        public void Add_ThrowsWhenCapacityReached()
        {
            var shop = new Shop(1);
            Smartphone phone = new Smartphone("Nokia", 30);
            Smartphone phoneTwo = new Smartphone("JS", 55);
            shop.Add(phone);

            Assert.Throws<InvalidOperationException>(
                () => shop.Add(phoneTwo),
                $"The shop is full.");
        }

        [Test]
        public void Remove_WorksCorrectly()
        {
            var shop = new Shop(2);
            Smartphone phone = new Smartphone("Nokia", 30);
            Smartphone phoneTwo = new Smartphone("JS", 55);
            shop.Add(phone);
            shop.Add(phoneTwo);

            shop.Remove("Nokia");
            Assert.That(shop.Count, Is.EqualTo(1));
        }

        [Test]
        public void Remove_ThrowsWhenModelIsInvalid()
        {
            var shop = new Shop(2);
            Smartphone phone = new Smartphone("Nokia", 30);
            shop.Add(phone);
            Assert.Throws<InvalidOperationException>(
                () => shop.Remove("JS"),
                $"The phone model {phone.ModelName} doesn't exist.");
        }
        [Test]
        public void TestPhone_ThrowsWhenModelDoNotExist()
        {
            var shop = new Shop(2);
            Smartphone phone = new Smartphone("Nokia", 30);
            shop.Add(phone);

            Assert.Throws<InvalidOperationException>(
                () => shop.TestPhone("JS", 15),
                $"The phone model {phone.ModelName} doesn't exist.");
        }

        [Test]
        public void TestPhone_WorksProperly()
        {
            var shop = new Shop(2);
            Smartphone phone = new Smartphone("Nokia", 30);
            shop.Add(phone);

            shop.TestPhone("Nokia", 5);
            Assert.That(phone.CurrentBateryCharge, Is.EqualTo(25));
        }

        [Test]
        public void TestPhone_ThrowsWhenNotEnoughBattery()
        {
            var shop = new Shop(2);
            Smartphone phone = new Smartphone("Nokia", 25);
            shop.Add(phone);

            Assert.Throws<InvalidOperationException>(
                () => shop.TestPhone("Nokia", 30),
                $"The phone model {phone.ModelName} is low on batery.");
        }

        [Test]
        public void ChargePhone_ThrowsWhenModelDoNotExist()
        {
            var shop = new Shop(2);
            Smartphone phone = new Smartphone("Nokia", 30);
            shop.Add(phone);

            Assert.Throws<InvalidOperationException>(
                () => shop.ChargePhone("JS"),
                $"The phone model {phone.ModelName} doesn't exist.");
        }
        [Test]
        public void ChargePhone_WorksProperly()
        {
            var shop = new Shop(2);
            Smartphone phone = new Smartphone("Nokia", 30);
            shop.Add(phone);
            phone.CurrentBateryCharge = 30;
            shop.ChargePhone("Nokia");
            Assert.That(phone.CurrentBateryCharge, Is.EqualTo(phone.MaximumBatteryCharge));
        }
    }
}