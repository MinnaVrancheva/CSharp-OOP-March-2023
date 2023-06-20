
namespace CustomRandomList;
public class StartUp
{
    static void Main(string[] arg)
    {
        RandomList random = new RandomList();

        random.Add("1");
        random.Add("2");
        random.Add("3");

        Console.WriteLine(random.RandomString());
    }
}
