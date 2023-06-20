using NUnit.Framework;
using System;

namespace PlanetWars.Tests
{
    public class UnitTests
    {
        [TestFixture]
        public class PlanetWarsTests
        {
            [Test]
            public void ConstructorShouldSetNameCorrectly()
            {
                var planet = new Planet("Mars", 560);
                var expectedName = "Mars";

                Assert.That(planet.Name, Is.EqualTo(expectedName));
            }

            [Test]
            public void ConstructorThrowsInvalidNameException()
            {
                Assert.Throws<ArgumentException>(() => new Planet(null, 120), $"Invalid Planet Name");
            }

            [Test]
            public void ConstructorThrowsExceptionInvalidBudget()
            {
                Assert.Throws<ArgumentException>(() => new Planet("Mars", -5), $"Budget cannot drop below Zero!");
            }

            [Test]
            public void ConstructorCorrectlyCreatesCollectionOfWeapons()
            {
                var planet = new Planet("Mars", 345);
                Assert.That(planet.Weapons.Count, Is.EqualTo(0));
            }

            [Test]
            public void ConstructorWeaponCorrectlyAddsNewWeapon()
            {
                var weapon = new Weapon("pw", 3.40, 50);

                Assert.That(weapon.Name, Is.EqualTo("pw"));
                Assert.That(weapon.DestructionLevel, Is.EqualTo(50));
                Assert.That(weapon.Price, Is.EqualTo(3.40));
            }

            [Test]
            public void AddWeapon_WeaponIsAddedToPlanetCollectionOfWeapons()
            {
                var planet = new Planet("Mars", 887);
                var weapon = new Weapon("pw", 25, 8);

                planet.AddWeapon(weapon);

                Assert.That(planet.Weapons.Count, Is.EqualTo(1));
            }

            [Test]
            public void AddWeapon_AlreadyAddedWeapon()
            {
                var planet = new Planet("Mars", 887);
                var weapon = new Weapon("pw", 25, 8);
                planet.AddWeapon(weapon);
                Assert.Throws<InvalidOperationException>(
                    () => planet.AddWeapon(weapon), 
                    $"There is already a {weapon.Name}");
            }

            [Test]
            public void MilitaryPowerRatioIsSetCorrectly()
            {
                var planet = new Planet("Mars", 887);
                var weaponOne = new Weapon("pw", 25, 8);
                var weaponTwo = new Weapon("pp", 5.5, 30);

                planet.AddWeapon(weaponOne);
                planet.AddWeapon(weaponTwo);

                Assert.That(planet.MilitaryPowerRatio, Is.EqualTo(38));
            }

            [Test]
            public void BudgetIncreasedWithGivenAmount()
            {
                var planet = new Planet("Mars", 887);
                planet.SpendFunds(87);

                Assert.That(planet.Budget, Is.EqualTo(800));
            }

            [Test]
            public void BudgetCannotDropBelowZero()
            {
                var planet = new Planet("Mars", 887);

                Assert.Throws<InvalidOperationException>(
                    () => planet.SpendFunds(900), 
                    $"Not enough funds to finalize the deal.");
            }

            [Test]
            public void Weapon_IncreaseDesctructionLevelWorksProperly()
            {
                var weaponOne = new Weapon("pw", 25, 8);
                weaponOne.IncreaseDestructionLevel();

                Assert.That(weaponOne.DestructionLevel, Is.EqualTo(9));
            }
            [Test]
            public void WeaponIsNuclearWorksProperly()
            {
                var weaponNuclear = new Weapon("Nuclear", 55.5, 55);
                var weaponNotNuclear = new Weapon("NotNuclear", 20, 2);
                
                Assert.That(weaponNuclear.IsNuclear, Is.EqualTo(true));
                Assert.That(weaponNotNuclear.IsNuclear, Is.EqualTo(false));
            }

            [Test]
            public void RemoveWeaponWorksProperly()
            {
                var planet = new Planet("NewPlanet", 1500);
                var weaponOne = new Weapon("WeaponOne", 20, 2);
                var weaponTwo = new Weapon("WeaponTwo", 20, 3);

                planet.AddWeapon(weaponOne);
                planet.AddWeapon(weaponTwo);

                Assert.That(planet.MilitaryPowerRatio, Is.EqualTo(5));

                planet.RemoveWeapon("WeaponOne");

                Assert.That(planet.MilitaryPowerRatio, Is.EqualTo(3));
                Assert.That(planet.Weapons.Count, Is.EqualTo(1));
            }

            [Test]
            public void UpgradeWeaponWorksProperly()
            {
                var planet = new Planet("Mars", 887);
                var weaponOne = new Weapon("pw", 25, 8);
                planet.AddWeapon(weaponOne);
                planet.UpgradeWeapon("pw");

                Assert.That(planet.MilitaryPowerRatio, Is.EqualTo(9));
            }
            [Test]
            public void UpgradeWeaponDoesNotExist()
            {
                var planet = new Planet("Mars", 887);
                Assert.Throws<InvalidOperationException>(
                    () => planet.UpgradeWeapon("WeaponDoesNotExist"), 
                    $"NotAddedWeapon does not exist in the weapon repository of {planet.Name}");
            }

            [Test]
            public void DestructOpponent_Throws_IfOpponentIsTooStrong()
            {
                var planetOne = new Planet("Mars", 887);
                var planetTwo = new Planet("Venera", 500);

                var weaponOne = new Weapon("WeaponOne", 20, 2);
                var weaponTwo = new Weapon("WeaponTwo", 30, 5);
                var weaponThree = new Weapon("WeaponThree", 20, 2);

                planetOne.AddWeapon(weaponOne);
                planetOne.AddWeapon(weaponThree);
                planetTwo.AddWeapon(weaponTwo);
                                
                Assert.Throws<InvalidOperationException>(
                    () => planetOne.DestructOpponent(planetTwo),
                    $"{planetTwo.Name} is too strong to declare war to!");
            }
            [Test]
            public void DestructOpponentWorksProperly()
            {
                var planetOne = new Planet("Mars", 1500);
                var planetTwo = new Planet("Venera", 2000);

                var weaponOne = new Weapon("WeaponOne", 20, 2);
                var weaponTwo = new Weapon("WeaponTwo", 30, 5);
                var weaponThree = new Weapon("WeaponThree", 20, 4);


                planetOne.AddWeapon(weaponOne);
                planetOne.AddWeapon(weaponThree);
                planetTwo.AddWeapon(weaponTwo);

                var expectedResult = "Venera is destructed!";

                Assert.That(planetOne.DestructOpponent(planetTwo), Is.EqualTo(expectedResult));
            }

            [Test]
            public void WeaponPriceCannotBeNegative()
            {
                //var weapon = new Weapon("pw", -5, 8);

                Assert.Throws<ArgumentException>(
                    () => new Weapon("pw", -5, 8), 
                    "Price cannot be negative.");

                
            }
        }
    }
}

