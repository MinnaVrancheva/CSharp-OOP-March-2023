using NUnit.Framework;
using System;

namespace RepairShop.Tests
{
    public class Tests
    {
        public class RepairsShopTests
        {
          
            [Test]
            public void Car_ConstructorSetsNewCarCorrectly()
            {
                Car car = new Car("Pegeout", 10);
                string expectedModel = "Pegeout";
                int numberOfIssues = 10;

                Assert.That(car.CarModel == expectedModel);
                Assert.That(car.NumberOfIssues == numberOfIssues);
            }

            [Test]
            public void Garage_ConstructorCreatesNewCarCorrectly()
            {
                Garage newGarage = new Garage("Maistori", 5);
                string expectedName = "Maistori";
                int numberOfMechanics = 5;

                Assert.That(newGarage.Name == expectedName);
                Assert.That(newGarage.MechanicsAvailable == numberOfMechanics);
            }

            [Test]
            public void Garage_ConstructorCorrectlyCreatesNewCollection()
            {
                Car car = new Car("Pegeout", 10);
                Garage newGarage = new Garage("Maistori", 5);
                newGarage.AddCar(car);
                int expectedCount = 1;

                
                Assert.That(newGarage.CarsInGarage, Is.EqualTo(expectedCount));
            }

            [Test]
            public void ConstructorThrowsInvalidNameExceptionWhenNameIsNull()
            {
                Assert.Throws<ArgumentNullException>(
                    () => new Garage(null, 5), 
                    $"Invalid garage name.");
            }

            [Test]
            public void ConstructorThrowsWhenNoAvailableMechanics()
            {
                Assert.Throws<ArgumentException>(
                    () => new Garage("Maistori", 0), 
                    $"At least one mechanic must work in the garage.");
            }

            [Test]
            public void AddCar_ThrowsWhenNoAvailableMechanics()
            {
                Car car = new Car("Pegeout", 10);
                Car car2 = new Car ("Mercedes", 1);
                Garage newGarage = new Garage("Maistori", 1);
                newGarage.AddCar(car);

                Assert.Throws<InvalidOperationException>(
                    () => newGarage.AddCar(car2),
                    $"No mechanic available.");
            }

            [Test]
            public void FixCar_ThrowsWhenNoCarForFixing()
            {
                Car car = new Car("Pegeout", 10);
                Garage newGarage = new Garage("Maistori", 1);
                newGarage.AddCar(car);
                string car2Name = "Mercedes";

                Assert.Throws<InvalidOperationException>(
                    () => newGarage.FixCar(car2Name),
                    $"The car {car2Name} doesn't exist.");
            }

            [Test]
            public void RemoveFixedCar_ThrowsWhenNoCarToRemove()
            {
                Car car = new Car("Pegeout", 10);
                Garage newGarage = new Garage("Maistori", 1);
                newGarage.AddCar(car);

                Assert.Throws<InvalidOperationException>(
                    () => newGarage.RemoveFixedCar(),
                    $"No fixed cars available.");
            }
 
        }
    }
}