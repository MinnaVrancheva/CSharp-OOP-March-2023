namespace InheritanceOfObjects;

public class StartUp
{
    static void Main(string[] arg)
    {
        int inputs = 3;
        Person[] person = new Person[inputs];

        for (int i = 0; i < inputs; i++)
        {
            if (i == 0)
            {
                person[i] = new Teacher(Console.ReadLine());
            }
            else
            {
                person[i] = new Student(Console.ReadLine());
            }
        }

        for (int i = 0; i < inputs; i++)
        {
            if (i == 0)
            {
                ((Teacher)person[i]).Explain();
            }
            else
            {
                ((Student)person[i]).Study();
            }
        }
    }
}
