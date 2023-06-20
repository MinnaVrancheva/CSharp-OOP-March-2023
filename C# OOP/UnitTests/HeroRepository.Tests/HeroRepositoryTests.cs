using System;
using System.Collections.Generic;
using NUnit.Framework;
public class HeroRepositoryTests
{
    string name = "NewHorizon";
    int level = 2;
    Hero hero;
    HeroRepository data; 

    [SetUp]
    public void SetUp()
    {
        Hero hero = new Hero(name, level);
        data = new HeroRepository();
    }

    [Test]
    public void Hero_ConstructorCorrectlyCreatesNewHero()
    {
        Hero hero = new Hero(name, level);

        Assert.That(hero.Name, Is.EqualTo(name));
        Assert.That(hero.Level, Is.EqualTo(level));
    }

    [Test]
    public void HeroRepository_ConstructorCorrectlyCreatesNewList()
    {
        Hero hero = new Hero(name, level);
        data.Create(hero);
        Assert.That(data.Heroes.Count, Is.EqualTo(1));
    }

    [Test]
    public void Create_ThrowsWhenHeroIsNull()
    {
        Hero heroNew = null;
        Assert.Throws<ArgumentNullException>(
            () => data.Create(heroNew),
            $"Hero is null");
    }

    [Test]
    public void Create_ThrowsWhenHeroAlreadyExists()
    {
        Hero hero = new Hero(name, level);
        data.Create(hero);
        Assert.Throws<InvalidOperationException>(
            () => data.Create(hero),
            $"Hero with name {hero.Name} already exists");
    }

    [Test]
    public void Create_SuccessfullyCreatesNewHero()
    {
        Hero hero = new Hero(name, level);
        string result = $"Successfully added hero {hero.Name} with level {hero.Level}";

        Assert.That(data.Create(hero), Is.EqualTo(result));
    }

    [Test]
    public void Remove_ThrowsWhenNameIsNullOrWhitespace()
    {
        Hero hero = new Hero(name, level);
        data.Create(hero);
        Assert.Throws<ArgumentNullException>(
            () => data.Remove(null),
            $"Name cannot be null");
    }

    [Test]
    public void Remove_SuccessfullyRemoveHero()
    {
        Hero hero = new Hero(name, level);
        data.Create(hero);
        data.Remove(name);

        Assert.That(data.Heroes.Count, Is.EqualTo(0));
    }

    [Test]
    public void GetHeroWithHighestLevel_WorksAsExpected()
    {
        Hero hero = new Hero(name, level);
        Hero heroNew = new Hero("Simpson", 5);

        data.Create(heroNew);
        data.Create(hero);

        Assert.That(data.GetHeroWithHighestLevel, Is.EqualTo(heroNew));
    }

    [Test]
    public void GetHero_WorksAsExpected()
    {
        Hero hero = new Hero(name, level);
        Hero heroNew = new Hero("Simpson", 5);

        data.Create(heroNew);
        data.Create(hero);

        Assert.That(data.GetHero("Simpson"), Is.EqualTo(heroNew));
    }
}
