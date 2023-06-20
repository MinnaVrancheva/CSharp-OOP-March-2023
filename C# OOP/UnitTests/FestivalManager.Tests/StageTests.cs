// Use this file for your unit tests.
// When you are ready to submit, REMOVE all using statements to Festival Manager (entities/controllers/etc)
// Test ONLY the Stage class. 
namespace FestivalManager.Tests
{
    using FestivalManager.Entities;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    [TestFixture]
	public class StageTests
    {
		private Performer performer;
		private Song song;
		private Stage stage;
		TimeSpan duration = new TimeSpan(0,3,08);

		[SetUp]
		public void SetUp()
        {
			performer = new Performer("Bruno", "Mars", 32);
			song = new Song("Back up, back up", duration);
			stage = new Stage();
        }

        [TearDown]
		public void TearDown()
        {
			performer = null;
			song = null;
			stage = null;
        }


		[Test]
	    public void PerformerConstructor_CorrectlyCreatesNewPerformer()
	    {
			performer = new Performer("Bruno", "Mars", 32);
			string performerFullName = "Bruno Mars";
			Assert.That(performer.Age, Is.EqualTo(32));
			Assert.That(performer.FullName, Is.EqualTo(performerFullName));
		}

        [Test]
		public void PerformerConstructorCreatesEmptyCollectionOfSongs()
        {
			Assert.That(performer.SongList, Is.Empty);
        }

        [Test]
		public void Performer_ToString_ReturnsFullName()
        {
			string performerFullName = "Bruno Mars";
			Assert.That(performer.ToString(), Is.EqualTo(performerFullName));
		}

        [Test]
		public void SongConstructor_CorrectlyCreatesNewSong()
        {
			song = new Song("Back up, back up", duration);
			Assert.That(song.Name, Is.EqualTo("Back up, back up"));
			Assert.That(song.Duration, Is.EqualTo(duration));
        }

        [Test]
		public void SongToString_Returns()
        {
			string result = "Back up, back up (03:08)";
			Assert.That(song.ToString(), Is.EqualTo(result));
		}

        [Test]
		public void StageConstructor_CreatesPerformersCollection()
        {
			Assert.That(stage.Performers, Is.Empty);
		}

        [Test]
		public void AddPerformer_ThrowsWhenAgeIsBelow18()
        {
			ArgumentException exception = Assert
			   .Throws<ArgumentException>(() => stage.AddPerformer(new Performer("Bruno", "Mars", 17)));

			Assert.That(exception.Message, Is.EqualTo("You can only add performers that are at least 18."));
		}

        [Test]
		public void AddPerform_SuccessfullyAddsNewPerformer()
        {
			stage.AddPerformer(performer);
			Assert.That(stage.Performers.Count, Is.EqualTo(1));
		}

        [Test]
		public void AddSong_ThrowsWhenDurationIsBelow1Minute()
        {
			TimeSpan duration1 = new TimeSpan(0, 0, 52);

			ArgumentException exception = Assert
			   .Throws<ArgumentException>(() => stage.AddSong(new Song("Back up, back up", duration1)));

			Assert.That(exception.Message, Is.EqualTo("You can only add songs that are longer than 1 minute."));
		}

        [Test]
		public void AddSongToPerformer_SuccessfullyAddsNewSong()
        {
			stage.AddPerformer(performer);
			stage.AddSong(song);
			string message = $"{song} will be performed by {performer}";

			Assert.That(stage.AddSongToPerformer("Back up, back up", "Bruno Mars"), Is.EqualTo(message));
			Assert.That(performer.SongList.Count, Is.EqualTo(1));
			Assert.That(performer.SongList[0], Is.EqualTo(song));
		}

        [Test]
		public void Play_ReturnsPerformerSongsCount()
        {
			performer.SongList.Add(song);
			stage.AddPerformer(performer);

			string message = $"{stage.Performers.Count} performers played {performer.SongList.Count} songs";

			Assert.That(stage.Play(), Is.EqualTo(message));
		}

        [Test]
		public void GetPerformer_ThrowsWhenNoSuchPerformerExists()
        {
			ArgumentException exception = Assert
			   .Throws<ArgumentException>(() => stage.AddSongToPerformer("empty", "Error"));

			Assert.That(exception.Message, Is.EqualTo("There is no performer with this name."));
		}

		[Test]
		public void GetSong_ThrowsWhenNoSuchSongExists()
		{
			stage.AddPerformer(performer);

			ArgumentException exception = Assert
			   .Throws<ArgumentException>(() => stage.AddSongToPerformer("Error", performer.FullName));

			Assert.That(exception.Message, Is.EqualTo("There is no song with this name."));
		}

        [Test]
		public void ValidateNull_Test()
        {
			performer = null;

			ArgumentNullException exception = Assert
			   .Throws<ArgumentNullException>(() => stage.AddPerformer(performer));
		}
	}
}