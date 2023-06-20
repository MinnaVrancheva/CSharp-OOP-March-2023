namespace ConstructorsAndDestructors;

public class StartUp
{
    static void Main(string[] arg)
    {
        int prompts = 3;
        Person[] persons = new Person[prompts];

        for (int i = 0; i < prompts; i++)
        {
            persons[i] = new Person(Console.ReadLine());
        }

        for (int i = 0; i < prompts; i++)
        {
            Console.WriteLine(persons[i].ToString());
        }
    }
}

