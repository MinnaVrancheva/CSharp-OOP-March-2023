namespace BorderControl;

public class StartUp
{
    static void Main(string[] arg)
    {
        List<IIdentifier> people = new List<IIdentifier>();

        string command; 

        while ((command = Console.ReadLine()) != "End")
        {
            string[] input = command.Split();

            if (input.Length == 3)
            {
                IIdentifier citizen = new Citizen(input[2], input[0], int.Parse(input[1]));
                people.Add(citizen);
            }
            else
            {
                IIdentifier robot = new Robot(input[1], input[0]);
                people.Add(robot);
            }
        }

        string fakeId = Console.ReadLine();

        foreach (IIdentifier citizen in people)
        {
            if (citizen.Id.EndsWith(fakeId))
            {
                Console.WriteLine(citizen.Id);
            }
        }
    }
}
