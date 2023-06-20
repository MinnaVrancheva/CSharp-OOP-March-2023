using NUnit.Framework;
using System;

namespace VehicleGarage.Tests
{
    public class Tests
    {
        private Vehicle vehicle;
        private Garage garage;

        [SetUp]
        public void Setup()
        {
            vehicle = new Vehicle("Ford", "Mustang", "SA5555SA", 50);
            garage = new Garage(2);
        }

        [TearDown]
        public void TearDown()
        {
            vehicle = null;
            garage = null;
        }

        [Test]
        public void VehicleConstructor_CreatesNewVehicle()
        {
            vehicle = new Vehicle("Ford", "Mustang", "SA5555SA", 50);

            Assert.That(vehicle, Is.Not.Null);
            Assert.That(vehicle.Brand, Is.EqualTo("Ford"));
            Assert.That(vehicle.Model, Is.EqualTo("Mustang"));
            Assert.That(vehicle.LicensePlateNumber, Is.EqualTo("SA5555SA"));
            Assert.That(vehicle.IsDamaged, Is.EqualTo(false));
        }

        [Test]
        public void GarageConstructor_CreatesNewListOfVehicles()
        {
            Assert.That(garage.Vehicles.Count, Is.EqualTo(0));
        }

        [Test]
        public void GarageConstructor_CreatesNewGarage()
        {
            garage = new Garage(2);

            Assert.That(vehicle, Is.Not.Null);
            Assert.That(garage.Capacity, Is.EqualTo(2));
            Assert.That(garage.Vehicles.Count, Is.EqualTo(0));
        }

        [Test]
        public void AddVehicle_ReturnFalseWhenCapacityIsReached()
        {
            Vehicle vehicle2 = new Vehicle("Mercedes", "E-Class", "CA5555CA", 55);
            garage.AddVehicle(vehicle);
            garage.AddVehicle(vehicle2);

            Assert.That(garage.AddVehicle(new Vehicle("Ford", "S", "RT7887RT", 74)), Is.False);
        }

        [Test]
        public void AddVehicle_ReturnFalseWhenLicensePlateIsTheSame()
        {
            Vehicle vehicle2 = new Vehicle("Mercedes", "E-Class", "SA5555SA", 55);
            garage.AddVehicle(vehicle);
            garage.AddVehicle(vehicle2);

            Assert.That(garage.AddVehicle(vehicle2), Is.False);
        }

        [Test]
        public void AddVehicle_AddsNewVehicle()
        {            
            Assert.That(garage.AddVehicle(vehicle), Is.True);
        }

        [Test]
        public void ChargeVehicles_chargesVehicleBattery()
        {
            garage.AddVehicle(vehicle);
            garage.DriveVehicle("CA5555CA", 50, false);

            Assert.AreEqual(vehicle.BatteryLevel, 50);

            garage.ChargeVehicles(50);
            
            Assert.AreEqual(vehicle.BatteryLevel, 100);
        }

        [Test]
        public void ChargeVehicles_ChargesAllVehicles()
        {
            Vehicle vehicle2 = new Vehicle("Mercedes", "E-Class", "CA5555CA", 55);
            garage.AddVehicle(vehicle);
            garage.AddVehicle(vehicle2);

            garage.DriveVehicle("SA5555SA", 50, false);
            garage.DriveVehicle("CA5555CA", 50, false);

            int count = 2;

            Assert.That(garage.ChargeVehicles(50), Is.EqualTo(count));
        }

        //[Test]
        //public void DriveVehicle_ReturnsNull()
        //{
        //    garage.AddVehicle(vehicle);
        //    garage.DriveVehicle("SA5555SA", 50, true);
        //    //var message = "";

        //    //Assert.Null(vehicle);
        //    Assert.That(message, Is.Null);

        //}

        [Test]
        public void DriveVehicle_chargesVehicleBattery()
        {
            garage.AddVehicle(vehicle);
            garage.DriveVehicle("SA5555SA", 50, false);

            Assert.AreEqual(vehicle.BatteryLevel, 50);
            Assert.AreEqual(vehicle.IsDamaged, false);

        }

        [Test]
        public void DriveVehicle_setsIsDamagedCorrectly()
        {
            garage.AddVehicle(vehicle);
            garage.DriveVehicle("SA5555SA", 50, true);

            Assert.AreEqual(vehicle.BatteryLevel, 50);
            Assert.AreEqual(vehicle.IsDamaged, true);

        }



        [Test]
        public void RepairVehicles_DoesNotDriveDamagedVehicle()
        {
            string message = $"Vehicles repaired: 1";

            garage.AddVehicle(vehicle);
            garage.DriveVehicle("SA5555SA", 50, true);

            Assert.That(garage.RepairVehicles(), Is.EqualTo(message));
        }
    }
}