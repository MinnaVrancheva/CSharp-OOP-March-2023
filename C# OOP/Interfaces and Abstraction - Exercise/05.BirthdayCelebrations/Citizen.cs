namespace BirthdayCelebrations;

public class Citizen : IIdentifier, IBirthdate
{
    public Citizen(string birthdate, string id, string name, int age)
    {
        Birthdate = birthdate;
        Id = id;
        Name = name;
        Age = age;
    }
    public string Id { get; private set; }
    public string Name { get; private set; }
    public int Age { get; private set; }
    public string Birthdate { get; private set; }
}
