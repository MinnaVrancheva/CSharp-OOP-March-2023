using NUnit.Framework;
using System;

namespace BankSafe.Tests
{
    public class BankVaultTests
    {
        private Item item;
        private BankVault bankVault;

        [SetUp]
        public void Setup()
        {
            bankVault = new BankVault();
            item = new Item("Svetlio", "2");
        }

        [TearDown]
        public void TearDown()
        {
            bankVault = null;
            item = null;
        }

        [Test]
        public void ItemConstructor_CorrectlyCreatesNewItem()
        {
            item = new Item("Svetlio", "2");
            Assert.That(item.Owner, Is.EqualTo("Svetlio"));
            Assert.That(item.ItemId, Is.EqualTo("2"));
        }

        [Test]
        public void BankVaultConstructor_CreatesNewDictionary()
        {
            Assert.That(bankVault.VaultCells.Count, Is.EqualTo(12));
        }

        [Test]
        public void AddItem_ThrowsWhenVaultCellDoesNotExist()
        {
            ArgumentException exception = Assert
               .Throws<ArgumentException>(() => bankVault.AddItem("D", item));

            Assert.That(exception.Message, Is.EqualTo("Cell doesn't exists!"));
        }

        [Test]
        public void AddItem_ThrowsIfCellAlreadyTaken()
        {
            Item item2 = new Item("Valio", "3");
            bankVault.AddItem("A1", item);

            ArgumentException exception = Assert
              .Throws<ArgumentException>(() => bankVault.AddItem("A1", item2));

            Assert.That(exception.Message, Is.EqualTo("Cell is already taken!"));
        }

        [Test]
        public void AddItem_ThrowsIfItemAlreadyAdded()
        {
            bankVault.AddItem("A1", item);

            InvalidOperationException exception = Assert
              .Throws<InvalidOperationException>(() => bankVault.AddItem("A2", item));

            Assert.That(exception.Message, Is.EqualTo("Item is already in cell!"));
        }

        [Test]
        public void AddItem_SuccessfullyAddsNewItem()
        {
            string message = $"Item:{item.ItemId} saved successfully!";

            Assert.That(bankVault.AddItem("A1", item), Is.EqualTo(message));
        }

        [Test]
        public void RemoveItem_ThrowsWhenVaultCellDoesNotExist()
        {
            ArgumentException exception = Assert
               .Throws<ArgumentException>(() => bankVault.RemoveItem("D", item));

            Assert.That(exception.Message, Is.EqualTo("Cell doesn't exists!"));
        }

        [Test]
        public void RemoveItem_ThrowsWhenItemDoesNotExist()
        {
            bankVault.AddItem("A1", item);
            Item item2 = new Item("Valio", "3");

            ArgumentException exception = Assert
               .Throws<ArgumentException>(() => bankVault.RemoveItem("A1", item2));

            Assert.That(exception.Message, Is.EqualTo("Item in that cell doesn't exists!"));
        }

        [Test]
        public void RemoveItem_SuccessfullyRemovesItem()
        {
            bankVault.AddItem("A1", item);
            string message = $"Remove item:{item.ItemId} successfully!";

            Assert.That(bankVault.RemoveItem("A1", item), Is.EqualTo(message));
        }
    }
}