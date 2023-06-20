namespace Interfaces
{
    public class PhotoBookTest
    {
        static void Main(string[] arg)
        {
            Car car = new Car(0);

            int fuel = int.Parse(Console.ReadLine());

            if (car.Refuel(fuel))
            {
                car.Drive();
            }
        }
    }
}

