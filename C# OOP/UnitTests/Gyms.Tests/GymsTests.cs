using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gyms.Tests
{
    public class GymsTests
    {
        string name = "NewHorizon";
        int capacity = 2;
        Gym gym;

        [SetUp]
        public void SetUp()
        {
            Gym gym = new Gym(name, capacity);
        }

        [Test]
        public void Athlete_ConstructorCorrectlyCreatesNewAthlete()
        {
            Athlete athlete = new Athlete("Janis");

            Assert.AreEqual("Janis", athlete.FullName);
            Assert.IsFalse(athlete.IsInjured);
        }

        [Test]
        public void Gym_ConstructorCorrectlyCreatesNewGym()
        {
            Gym gym = new Gym(name, capacity);
            Assert.That(gym.Name, Is.EqualTo(name));
            Assert.That(gym.Capacity, Is.EqualTo(capacity));
        }

        [Test]
        public void Gym_ConstructorCorrectlyCreatesListOfAthletes()
        {
            Gym gym = new Gym(name, capacity);
            Athlete athlete = new Athlete("Janis");

            gym.AddAthlete(athlete);
            int expectedCount = 1;

            Assert.That(gym.Count, Is.EqualTo(expectedCount));
        }

        [Test]
        public void GymName_ThrowsWhenNullOrEmpty()
        {
            Assert.Throws<ArgumentNullException>(
                () => new Gym(null, 5),
                $"Invalid gym name.");
        }

        [Test]
        public void GymCapacity_ThrowsWhenBelowZero()
        {
            Assert.Throws<ArgumentException>(
                () => new Gym(name, -5),
                $"Invalid gym capacity.");
        }

        [Test]
        public void AddAthlete_ThrowsWhenCapacityFull()
        {
            Gym gym = new Gym(name, capacity);
            Athlete athlete1 = new Athlete("Janis");
            Athlete athlete2 = new Athlete("Joplin");
            Athlete athlete3 = new Athlete("Franklin");
            gym.AddAthlete(athlete2);
            gym.AddAthlete(athlete1);

            Assert.Throws<InvalidOperationException>(
                () => gym.AddAthlete(athlete3),
                $"The gym is full.");
        }

        [Test]
        public void RemoveAthlete_ThrowsWhenAthleteDoesNotExist()
        {
            Gym gym = new Gym(name, capacity);
            Athlete athlete1 = new Athlete("Janis");
            gym.AddAthlete(athlete1);

            Assert.Throws<InvalidOperationException>(
                () => gym.RemoveAthlete("Joplin"),
                $"The athlete Joplin doesn't exist.");
        }

        [Test]
        public void RemoveAthlete_WorksAsExpected()
        {
            Gym gym = new Gym(name, capacity);
            Athlete athlete1 = new Athlete("Janis");
            gym.AddAthlete(athlete1);
            gym.RemoveAthlete("Janis");

            Assert.That(gym.Count, Is.EqualTo(0));
        }

        [Test]
        public void IsInjured_ThrowsWhenThereIsNoSuchAthlete()
        {
            Gym gym = new Gym(name, capacity);
            Athlete athlete1 = new Athlete("Janis");
            gym.AddAthlete(athlete1);

            Assert.Throws<InvalidOperationException>(
                () => gym.InjureAthlete("Joplin"),
                $"The athlete Joplin doesn't exist.");
        }

        [Test]
        public void IsInjured_WorksAsExpected()
        {
            Gym gym = new Gym(name, capacity);
            Athlete athlete1 = new Athlete("Janis");
            gym.AddAthlete(athlete1);

            var athleteInjured = gym.InjureAthlete("Janis");

            Assert.AreEqual(athlete1, athleteInjured);
            Assert.AreEqual(true, athlete1.IsInjured);
            Assert.AreEqual("Janis", athlete1.FullName);
        }

        [Test]
        public void Gym_Report()
        {
            Gym gym = new Gym(name, capacity);
            Athlete athlete1 = new Athlete("Janis");
            Athlete athlete2 = new Athlete("Joplin");
            List<Athlete> athletes = new List<Athlete>()
            {
                athlete1, athlete2
            };

            gym.AddAthlete(athlete1);
            gym.AddAthlete(athlete2);

            string expectedOutput = $"Active athletes at {gym.Name}: {string.Join(", ", athletes.Where(x => !x.IsInjured).Select(f => f.FullName))}";

            Assert.That(gym.Report(), Is.EqualTo(expectedOutput));
        }
    }
}
