namespace DatabaseExtended.Tests
{
    using NUnit.Framework;
    using ExtendedDatabase;
    using System;

    [TestFixture]
    public class ExtendedDatabaseTests
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
            database.Add(new Person(5, "Sasho"));
            int expectedCount = 1;
            Person result = database.FindById(5);

            Assert.AreEqual(expectedCount, database.Count);
            Assert.AreEqual("Sasho", result.UserName);
            Assert.AreEqual(5, result.Id);
        }

        [Test]
        public void Add_ThrowsWhenCountIsMoreThan16()
        {
            Person[] people = CreateFullArray();
            Database database = new Database(people);

            InvalidOperationException exception = Assert
                .Throws<InvalidOperationException>(
                () => database.Add(new Person(17, "Sisi")));

            Assert.That(exception.Message, Is.EqualTo("Array's capacity must be exactly 16 integers!"));
        }

        private Person[] CreateFullArray()
        {
            Person[] result = new Person[16];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new Person(i, i.ToString());
            }

            return result;
        }

        [Test]
        public void Add_ThrowsIfPersonIdIsNotUnique()
        {
            database.Add(new Person(5, "Sasho"));

            InvalidOperationException exception = Assert
                .Throws<InvalidOperationException>(
                () => database.Add(new Person(5, "Itso")));

            InvalidOperationException exception1 = Assert
                .Throws<InvalidOperationException>(
                () => database.Add(new Person(6, "Sasho")));

            Assert.That(exception.Message, Is.EqualTo("There is already user with this Id!"));
            Assert.That(exception1.Message, Is.EqualTo("There is already user with this username!"));

        }

        [Test]
        public void Constructor_CreatesDatabaseCollectionCorrectly()
        {
            database = new Database(new Person(5, "Sasho"), new Person(6, "Itso"));
            Person first = database.FindById(5);
            Person second = database.FindById(6);

            Assert.AreEqual("Sasho", first.UserName);
            Assert.AreEqual("Itso", second.UserName);
        }

        [Test]
        public void Remove_ThrowsWhenCollectionIsEmpty()
        {
            Assert.Throws<InvalidOperationException>(
                () => database.Remove());
        }

        [Test]
        public void Remove_RemovesLastElementSuccessfully()
        {
            database = new Database(new Person(5, "Sasho"), new Person(6, "Itso"));
            Person first = database.FindById(5);
            database.Remove();

            Assert.AreEqual(1, database.Count);
            Assert.AreEqual("Sasho", first.UserName);

            InvalidOperationException exception = Assert
                .Throws<InvalidOperationException>(() => database.FindByUsername("Itso"));

            Assert.That(exception.Message, Is.EqualTo("No user is present by this username!"));
        }

        [Test]
        public void FindByUserName_ThrowsWhenNameIsNull()
        {
            ArgumentNullException exception = Assert
                .Throws<ArgumentNullException>(
                () => database.FindByUsername(null));

            ArgumentNullException exception1 = Assert
                .Throws<ArgumentNullException>(
                () => database.FindByUsername(string.Empty));

            Assert.That(exception.ParamName, Is.EqualTo("Username parameter is null!"));
            Assert.That(exception1.ParamName, Is.EqualTo("Username parameter is null!"));


        }

        [Test]
        public void FindByUserName_ThrowsWhenNoUserIsPresentByThisUserName()
        {
            InvalidOperationException exception = Assert
                .Throws<InvalidOperationException>(() => database.FindByUsername("Gosho"));

            Assert.That(exception.Message, Is.EqualTo("No user is present by this username!"));
        }

        [Test] 
        public void FindByUserName_WorksCorrectly()
        {
            database = new Database(new Person(5, "Sasho"));
            Person person = database.FindByUsername("Sasho");

            Assert.That(person.UserName, Is.EqualTo("Sasho"));
            Assert.That(person.Id, Is.EqualTo(5));
        }

        public void FindByID_ThrowsWhenIDIsNegative()
        {
            ArgumentOutOfRangeException exception = Assert
                .Throws<ArgumentOutOfRangeException>(() => database.FindById(-5));

            Assert.That(exception.Message, Is.EqualTo("Id should be a positive number!"));
        }

        [Test]
        public void FindByID_ThrowsWhenNoUserIsPresentByThisID()
        {
            InvalidOperationException exception = Assert
                .Throws<InvalidOperationException>(() => database.FindById(5));

            Assert.That(exception.Message, Is.EqualTo("No user is present by this ID!"));
        }

        [Test]
        public void FindByID_WorksCorrectly()
        {
            database = new Database(new Person(5, "Sasho"));
            Person person = database.FindById(5);

            Assert.That(person.UserName, Is.EqualTo("Sasho"));
            Assert.That(person.Id, Is.EqualTo(5));
        }
    }
}