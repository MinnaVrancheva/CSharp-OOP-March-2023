namespace Telephony;

public class StartUp
{
    static void Main(string[] arg)
    {
        string[] phoneNumbers = Console.ReadLine()
            .Split();

        string[] urls = Console.ReadLine()
            .Split();

        ICallable callable;

        foreach (string phoneNumber in phoneNumbers)
        {
            if (phoneNumber.Length == 10)
            {
                callable = new Smartphone();
            }
            else
            {
                callable = new StationaryPhone();
            }

            try
            {
                Console.WriteLine(callable.Call(phoneNumber));
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        IBrowsable browsable = new Smartphone();

        foreach (string url in urls)
        {
            try
            {
                Console.WriteLine(browsable.Browse(url));
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
