namespace FightingArena.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class WarriorTests
    {
        private Warrior warrior;

        [SetUp]
        public void SetUp()
        {
            warrior = new Warrior("Lili", 15, 45);
        }

        [TearDown]
        public void TearDown()
        {
            warrior = null;
        }

        [Test]
        public void Constructor_CreatesNewWarrior()
        {
            warrior = new Warrior("Lili", 15, 45);

            Assert.That(warrior.Name, Is.EqualTo("Lili"));
            Assert.That(warrior.Damage, Is.EqualTo(15));
            Assert.That(warrior.HP, Is.EqualTo(45));
        }

        [Test]
        [TestCase(null)]
        [TestCase(" ")]
        public void Name_ThrowsWhenNameIsNullOrWhiteSpace(string name)
        {
            ArgumentException exception = Assert
                .Throws<ArgumentException>(() => new Warrior(name, 15, 45));

            Assert.That(exception.Message, Is.EqualTo("Name should not be empty or whitespace!"));
        }

        [Test]
        [TestCase(0)]
        [TestCase(-5)]
        public void Name_ThrowsWhenDamageIsLessOrEqualToZero(int damage)
        {
            ArgumentException exception = Assert
                .Throws<ArgumentException>(() => new Warrior("Lili", damage, 45));

            Assert.That(exception.Message, Is.EqualTo("Damage value should be positive!"));
        }

        [Test]
        public void Name_ThrowsWhenHPIsLessOrEqualToZero()
        {
            ArgumentException exception = Assert
                .Throws<ArgumentException>(() => new Warrior("Lili", 15, -5));

            Assert.That(exception.Message, Is.EqualTo("HP should not be negative!"));
        }

        [Test]

        public void Atack_ThrowsWhenAttackerHPLessThan30()
        {
            var attacker = new Warrior("Sensei", 15, 29);

            InvalidOperationException exception = Assert
               .Throws<InvalidOperationException>(() => attacker.Attack(warrior));

            Assert.That(exception.Message, Is.EqualTo("Your HP is too low in order to attack other warriors!"));
        }

        [Test]
        public void Atack_ThrowsWhenDefenderHPLessThan30()
        {
            var defender = new Warrior("Sensei", 15, 29);

            InvalidOperationException exception = Assert
               .Throws<InvalidOperationException>(() => warrior.Attack(defender));

            Assert.That(exception.Message, Is.EqualTo("Enemy HP must be greater than 30 in order to attack him!"));
        }

        [Test]
        public void Atack_ThrowsWhenDefenderStrongerThanAttackerDamage()
        {
            var defender = new Warrior("Sensei", 50, 80);

            InvalidOperationException exception = Assert
               .Throws<InvalidOperationException>(() => warrior.Attack(defender));

            Assert.That(exception.Message, Is.EqualTo("You are trying to attack too strong enemy"));
        }

        [Test]
        public void Attack_Succeeded()
        {
            var defender = new Warrior("Sensei", 15, 33);

            warrior.Attack(defender);

            Assert.That(warrior.HP, Is.EqualTo(30));
            Assert.That(defender.HP, Is.EqualTo(18));
        }

        [Test]
        public void Attack_KillsTheDefender()
        {
            var attacker = new Warrior("Lili", 35, 50);
            var defender = new Warrior("Sensei", 15, 33);

            attacker.Attack(defender);

            Assert.That(attacker.HP, Is.EqualTo(35));
            Assert.That(defender.HP, Is.EqualTo(0));
        }
    }
}