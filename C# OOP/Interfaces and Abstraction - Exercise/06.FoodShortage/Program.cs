namespace FoodShortage;

public class StartUp
{
    static void Main(string[] arg)
    {
        int numberOfPeople = int.Parse(Console.ReadLine());
        List<IBuyer> buyerList = new List<IBuyer>();

        for (int i = 0; i < numberOfPeople; i++)
        {
            string[] input = Console.ReadLine()
                .Split();

            if (input.Length == 4)
            {
                IBuyer citizen = new Citizen(input[0], int.Parse(input[1]), input[2], input[3]);
                buyerList.Add(citizen);
            }
            else
            {
                IBuyer rebel = new Rebel(input[0], int.Parse(input[1]), input[2]);
                buyerList.Add(rebel);
            }
        }

        string command;

        while ((command = Console.ReadLine()) != "End")
        {
            buyerList.FirstOrDefault(b => b.Name == command)?.BuyFood();
        }

        Console.WriteLine(buyerList.Sum(x => x.Food));
    }
}
