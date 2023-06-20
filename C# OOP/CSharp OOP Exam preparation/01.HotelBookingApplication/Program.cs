using BookingApp.Core;
using BookingApp.Core.Contracts;

namespace BookingApp;

public class StartUp
{
    public static void Main()
    {
        IEngine engine = new Engine();
        engine.Run();
    }
}
