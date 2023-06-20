using NUnit.Framework;
using System;

namespace FootballTeam.Tests
{
    public class Tests
    {
        private FootballPlayer player;
        private FootballTeam team;

        [SetUp]
        public void Setup()
        {
            player = new FootballPlayer("Gosho", 19, "Goalkeeper");
            team = new FootballTeam("Arsenal", 16);
        }

        [TearDown]
        public void TearDown()
        {
            player = null;
            team = null;
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void FootballPlayerConstructor_ThrowsWhenNameIsNullOrEmpty(string name)
        {
            ArgumentException exception = Assert
               .Throws<ArgumentException>(() => new FootballPlayer(name, 19, "Goalkeeper"));

            Assert.That(exception.Message, Is.EqualTo("Name cannot be null or empty!"));
        }

        [Test]
        public void FootballPlayerConstructor_ThrowsWhenPositionIsInvalid()
        {
            ArgumentException exception = Assert
               .Throws<ArgumentException>(() => new FootballPlayer("Gosho", 19, "goalkeeper"));

            Assert.That(exception.Message, Is.EqualTo("Invalid Position"));
        }

        [Test]
        [TestCase(0)]
        [TestCase(22)]
        public void FootballPlayerConstructor_ThrowsWhenPlayerNumberIsBelow1AndMoreThan21(int number)
        {
            ArgumentException exception = Assert
               .Throws<ArgumentException>(() => new FootballPlayer("Gosho", number, "Goalkeeper"));

            Assert.That(exception.Message, Is.EqualTo("Player number must be in range [1,21]"));
        }

        [Test]
        public void FootballPlayerConstructor_CreatesNewPlayer()
        {
            player = new FootballPlayer("Gosho", 19, "Goalkeeper");

            Assert.That(player.Name, Is.EqualTo("Gosho"));
            Assert.That(player.PlayerNumber, Is.EqualTo(19));
            Assert.That(player.Position, Is.EqualTo("Goalkeeper"));
            Assert.That(player.ScoredGoals, Is.EqualTo(0));
        }

        [Test]
        public void FootballPlayerScore_IncreasesTheNumberOfGoals()
        {
            player.Score();

            Assert.That(player.ScoredGoals, Is.EqualTo(1));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void FootballTeamConstructor_ThrowsWhenNameIsNullOrEmpty(string name)
        {
            ArgumentException exception = Assert
               .Throws<ArgumentException>(() => new FootballTeam(name, 16));

            Assert.That(exception.Message, Is.EqualTo("Name cannot be null or empty!"));
        }

        [Test]
        public void FootballPlayerConstructor_ThrowsWhenCapacityIsLessThan15()
        {
            ArgumentException exception = Assert
               .Throws<ArgumentException>(() => new FootballTeam("Arsenal", 14));

            Assert.That(exception.Message, Is.EqualTo("Capacity min value = 15"));
        }

        [Test]
        public void FootbalTeamConstructor_CreatesNewTeam()
        {
            team = new FootballTeam("Arsenal", 16);

            Assert.That(team.Name, Is.EqualTo("Arsenal"));
            Assert.That(team.Capacity, Is.EqualTo(16));
            Assert.That(team.Players.Count, Is.EqualTo(0));
        }

        [Test]
        public void AddNewPlayer_ThrowsIfCountIsBiggerThanCapacity()
        {
            FootballPlayer player2 = new FootballPlayer("Gosho", 19, "Goalkeeper");
            FootballPlayer player3 = new FootballPlayer("Gosho", 19, "Goalkeeper");
            FootballPlayer player4 = new FootballPlayer("Gosho", 19, "Goalkeeper");
            FootballPlayer player5 = new FootballPlayer("Gosho", 19, "Goalkeeper");
            FootballPlayer player6 = new FootballPlayer("Gosho", 19, "Goalkeeper");
            FootballPlayer player7 = new FootballPlayer("Gosho", 19, "Goalkeeper");
            FootballPlayer player8 = new FootballPlayer("Gosho", 19, "Goalkeeper");
            FootballPlayer player9 = new FootballPlayer("Gosho", 19, "Goalkeeper");
            FootballPlayer player10 = new FootballPlayer("Gosho", 19, "Goalkeeper");
            FootballPlayer player11 = new FootballPlayer("Gosho", 19, "Goalkeeper");
            FootballPlayer player12 = new FootballPlayer("Gosho", 19, "Goalkeeper");
            FootballPlayer player13 = new FootballPlayer("Gosho", 19, "Goalkeeper");
            FootballPlayer player14 = new FootballPlayer("Gosho", 19, "Goalkeeper");
            FootballPlayer player15 = new FootballPlayer("Gosho", 19, "Goalkeeper");
            FootballPlayer player16 = new FootballPlayer("Gosho", 19, "Goalkeeper");
            FootballPlayer player17 = new FootballPlayer("Gosho", 19, "Goalkeeper");

            team.AddNewPlayer(player);
            team.AddNewPlayer(player2);
            team.AddNewPlayer(player3);
            team.AddNewPlayer(player4);
            team.AddNewPlayer(player5);
            team.AddNewPlayer(player6);
            team.AddNewPlayer(player7);
            team.AddNewPlayer(player8);
            team.AddNewPlayer(player9);
            team.AddNewPlayer(player10);
            team.AddNewPlayer(player11);
            team.AddNewPlayer(player12);
            team.AddNewPlayer(player13);
            team.AddNewPlayer(player14);
            team.AddNewPlayer(player15);
            team.AddNewPlayer(player16);

            string message = "No more positions available!";

            Assert.That(team.AddNewPlayer(player17), Is.EqualTo(message));
        }

        [Test]
        public void AddNewPlayer_SuccessfullyAddsNewPlayer()
        {
            string meaasge = $"Added player {player.Name} in position {player.Position} with number {player.PlayerNumber}";

            Assert.That(team.AddNewPlayer(player), Is.EqualTo(meaasge));
        }

        [Test]
        public void PickPlayer_ReturnsTheCorrectPlayer()
        {
            FootballPlayer player2 = new FootballPlayer("Tosho", 14, "Goalkeeper");
            FootballPlayer player3 = new FootballPlayer("Pesho", 19, "Goalkeeper");
            FootballPlayer player4 = new FootballPlayer("Vesho", 19, "Goalkeeper");

            team.AddNewPlayer(player);
            team.AddNewPlayer(player2);
            team.AddNewPlayer(player3);
            team.AddNewPlayer(player4);

            Assert.That(team.PickPlayer("Tosho"), Is.EqualTo(player2));
            Assert.That(team.PickPlayer("Tosho").Position, Is.EqualTo(player2.Position));
            Assert.That(team.PickPlayer("Tosho").PlayerNumber, Is.EqualTo(player2.PlayerNumber));
        }

        [Test]
        public void PlayerScore_PicksUpTheCorrectPlayerAndIncreasesItsScoredGoals()
        {
            FootballPlayer player2 = new FootballPlayer("Tosho", 14, "Goalkeeper");
            FootballPlayer player3 = new FootballPlayer("Pesho", 10, "Goalkeeper");
            FootballPlayer player4 = new FootballPlayer("Vesho", 8, "Goalkeeper");

            team.AddNewPlayer(player);
            team.AddNewPlayer(player2);
            team.AddNewPlayer(player3);
            team.AddNewPlayer(player4);

            string message = $"{player3.Name} scored and now has 1 for this season!";

            Assert.That(team.PlayerScore(10), Is.EqualTo(message));
            Assert.That(player3.ScoredGoals, Is.EqualTo(1));
        }
    }
}