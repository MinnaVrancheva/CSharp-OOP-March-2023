namespace Database.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class DatabaseTests
    {
        private Database database;

        [SetUp]
        public void SetUp()
        {
            database = new Database();
        }

        [TearDown]
        public void TearDown()
        {
            database = null;
        }

        [Test]
        public void Add_AddsNewElement()
        {
            database.Add(6);
            int expectedCount = 1;
            int[] result = database.Fetch();

            Assert.AreEqual(expectedCount, result.Length);
            Assert.AreEqual(6, result[0]);
            Assert.AreEqual(expectedCount, database.Count);
        }

        [Test]
        public void Add_ThrowsWhenCountIsDifferentThan16()
        {
            database = new Database(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);

            Assert.Throws<InvalidOperationException>(
                () => database.Add(17),
                $"Array's capacity must be exactly 16 integers!");
        }

        [Test]
        public void Count_CreatesDatabaseWithGivenCount()
        {
            database = new Database(1, 2, 3, 4, 5);

            Assert.AreEqual(5, database.Count);
        }

        [Test]
        public void Remove_ThrowsWhenCollectionIsEmpty()
        {
            Assert.Throws<InvalidOperationException>(
                () => database.Remove(),
                $"The collection is empty!");
        }

        [Test]
        public void Remove_RemovesLastElementSuccessfully()
        {
            var database = new Database(1, 2, 3, 4, 5);
            var result = database.Fetch();
            database.Remove();

            Assert.AreEqual(4, database.Count);
            Assert.AreEqual(5, result.Length);
            Assert.AreEqual(1, result[0]);
        }

        [Test]
        public void Fetch_WorksCorrectly()
        {
            database = new Database(1, 2, 3);
            var result = database.Fetch();

            Assert.That(new int[] { 1, 2, 3 }, Is.EquivalentTo(result));
        }
    }
}
