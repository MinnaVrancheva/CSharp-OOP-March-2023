namespace FightingArena.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class ArenaTests
    {
        private Arena arena;

        [SetUp]
        public void SetUp()
        {
            arena = new Arena();
        }

        [TearDown]
        public void TearDown()
        {
            arena = null;
        }

        [Test]
        public void Constructor_CreatesNewEmptyReadOnlyCollection()
        {
            arena = new Arena();
            Assert.AreEqual(0, arena.Count);
        }

        [Test]
        public void Enroll_ThrowsWhenWarriorNameIsNotUnique()
        {
            arena.Enroll(new Warrior("Lili", 5, 10));

            InvalidOperationException exception = Assert
                .Throws<InvalidOperationException>(() => arena.Enroll(new Warrior("Lili", 7, 12)));

            Assert.That(exception.Message, Is.EqualTo("Warrior is already enrolled for the fights!"));
        }

        [Test]
        public void Enroll_EnrollsNewWarrior()
        {
            arena.Enroll(new Warrior("Lili", 5, 10));

            Assert.AreEqual(1, arena.Count);
        }

        [Test]
        [TestCase("Lili", "Sensei")]
        public void Fight_ThrowsWhenDefenderMissing(string warriorOneName, string warriorTwoName)
        {
            arena.Enroll(new Warrior("Lili", 5, 10));

            InvalidOperationException exception = Assert
                .Throws<InvalidOperationException>(() => arena.Fight(warriorOneName, warriorTwoName));

            Assert.That(exception.Message, Is.EqualTo("There is no fighter with name Sensei enrolled for the fights!"));
        }

        [Test]
        [TestCase("Lili", "Sensei")]
        public void Fight_ThrowsWhenAttackerIsMissing(string warriorOneName, string warriorTwoName)
        {
            arena.Enroll(new Warrior("Sensei", 5, 10));

            InvalidOperationException exception = Assert
                .Throws<InvalidOperationException>(() => arena.Fight(warriorOneName, warriorTwoName));

            Assert.That(exception.Message, Is.EqualTo("There is no fighter with name Lili enrolled for the fights!"));
        }

        [Test]
        public void Fight_WarriorsEngageInBattleSuccessfully()
        {
            var attacker = new Warrior("Sensei", 15, 48);
            var deffender = new Warrior("Lili", 15, 52);

            arena.Enroll(attacker);
            arena.Enroll(deffender);

            arena.Fight(attacker.Name, deffender.Name);

            Assert.That(attacker.HP, Is.EqualTo(33));
            Assert.That(deffender.HP, Is.EqualTo(37));
        }
    }
}
