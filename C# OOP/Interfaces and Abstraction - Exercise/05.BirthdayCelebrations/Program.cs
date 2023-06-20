namespace BirthdayCelebrations;

public class StartUp
{
    static void Main(string[] arg)
    {
        List<IBirthdate> birthdates = new List<IBirthdate>();

        string input;

        while ((input = Console.ReadLine()) != "End")
        {
            string command = input.Split()[0];

            if (command == "Citizen")
            {
                IBirthdate citizen = new Citizen(input.Split()[4], input.Split()[3], input.Split()[1], int.Parse(input.Split()[2]));
                birthdates.Add(citizen);
            }
            else if (command == "Robot")
            {
                IIdentifier identifier = new Robot(input.Split()[2], input.Split()[1]);
            }
            else if (command == "Pet")
            {
                IBirthdate pet = new Pet(input.Split()[2], input.Split()[1]);
                birthdates.Add(pet);
            }
        }

        string specifiedBirthdate = Console.ReadLine();

        foreach (var birthdate in birthdates)
        {
            if (birthdate.Birthdate.EndsWith(specifiedBirthdate))
            {
                Console.WriteLine(birthdate.Birthdate);
            }
        }
    }
}
