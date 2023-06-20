namespace FoodShortage;

public class Citizen : IIdentifier, IBirthdate, IBuyer
{
    private const int InitialFood = 10;
    public Citizen(string name, int age, string id, string birthdate)
    {
        Name = name;
        Age = age;
        Id = id;
        Birthdate = birthdate;
    }
    public string Id { get; private set; }
    public string Name { get; private set; }
    public int Age { get; private set; }
    public string Birthdate { get; private set; }
    public int Food {get; private set; }

    public void BuyFood()
    {
        Food += InitialFood;
    }
}
