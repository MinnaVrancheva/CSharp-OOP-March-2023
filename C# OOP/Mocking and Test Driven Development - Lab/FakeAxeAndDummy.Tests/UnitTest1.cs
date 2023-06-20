namespace FakeAxeAndDummy.Tests
{
    using NUnit.Framework;
    using System;
    public class Tests
    {
        private Hero hero;
        private Dummy dummy;
        private Axe axe;

        [SetUp]
        public void Setup()
        {
            hero = new Hero("Svetlin");
            dummy = new Dummy(5, 10);
            axe = new Axe(3, 12);
        }

        [Test]
        public void Test1()
        {
            
        }
    }
}