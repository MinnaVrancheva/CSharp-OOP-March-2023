namespace Shapes;

public class StartUp
{
    static void Main(string[] arg)
    {
        Shape rect = new Rectangle(10, 20);
        Shape circle = new Circle(30);

        Console.WriteLine(rect.CalculateArea());
        Console.WriteLine(circle.CalculatePerimeter());

        Console.WriteLine(rect.CalculatePerimeter());
        Console.WriteLine(circle.CalculateArea());

    }
}
