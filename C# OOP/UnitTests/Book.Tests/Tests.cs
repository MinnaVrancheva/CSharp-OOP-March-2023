using System;
using NUnit.Framework;

namespace Book.Tests
{
    public class Tests
    {
        [Test]
        public void Constructor_SetsNewBookCorrectly()
        {
            Book newBook = new Book("Mravkite", "Makiaveevich");
            string author = "Makiaveevich";
            string name = "Mravkite";

            Assert.That(newBook.Author, Is.EqualTo(author));
            Assert.That(newBook.BookName, Is.EqualTo(name));
        }

        [Test]
        public void Constructor_CorrectlyCreatesNewFootnoteDictionary()
        {
            Book newBook = new Book("Mravkite", "Makiaveevich");
            //Dictionary<int, string> footnote = new Dictionary<int, string>();
            //footnote.Add(1, "newFootnote");
            newBook.AddFootnote(1, "newFootnote");

            Assert.That(newBook.FootnoteCount, Is.EqualTo(1));
        }

        [Test]
        public void Constructor_ThrowsWhenNameIsNullOrEmpty()
        {
            Assert.Throws<ArgumentException>(
                    () => new Book(null, "Makiaveevich"),
                    $"Invalid {nameof(Book.BookName)}!");
        }

        [Test]
        public void Constructor_ThrowsWhenAuthorIsNullOrEmpty()
        {
            Assert.Throws<ArgumentException>(
                    () => new Book("Mravkite", null),
                    $"Invalid {nameof(Book.Author)}!");
        }

        [Test]
        public void AddFootnote_ThrowsWhenFootnoteKeyAlreadyExists()
        {
            Book newBook = new Book("Mravkite", "Makiaveevich");
            newBook.AddFootnote(1, "newFootnote");

            Assert.Throws<InvalidOperationException>(
                () => newBook.AddFootnote(1, "secondFootnote"),
                $"Footnote already exists!");
        }

        [Test]
        public void FindFootnote_CorrectlyFindsTheFootnote()
        {
            Book newBook = new Book("Mravkite", "Makiaveevich");
            newBook.AddFootnote(1, "newFootnote");

            string expectedText = "newFootnote";
            int footnoteNumber = 1;

            string expectedMessage = $"Footnote #{footnoteNumber}: {expectedText}";

            Assert.AreEqual(expectedMessage, newBook.FindFootnote(footnoteNumber));
        }

        [Test]
        public void AlterFootnote_CorrectlyAltersFootnote()
        {
            Book newBook = new Book("Mravkite", "Makiaveevich");
            newBook.AddFootnote(1, "newFootnote");
            newBook.AlterFootnote(1, "alteredText");

            string expectedText = "alteredText";
            int footnoteNumber = 1;

            string expectedMessage = $"Footnote #{footnoteNumber}: {expectedText}";
            string result = newBook.FindFootnote(footnoteNumber);

            Assert.AreEqual(expectedMessage, result);
        }

        [Test]
        public void FindFootnote_ThrowsWhenKeyCannotBeFound()
        {
            Book newBook = new Book("Mravkite", "Makiaveevich");
            newBook.AddFootnote(1, "newFootnote");

            Assert.Throws<InvalidOperationException>(
                () => newBook.FindFootnote(2),
                $"Footnote doesn't exists!");
        }

        [Test]
        public void AlterFootnote_ThrowsWhenKeyCannotBeFound()
        {
            Book newBook = new Book("Mravkite", "Makiaveevich");
            newBook.AddFootnote(1, "newFootnote");

            Assert.Throws<InvalidOperationException>(
                () => newBook.AlterFootnote(2, "Altered footnote"),
                $"Footnote doesn't exists!");
        }
    }
}