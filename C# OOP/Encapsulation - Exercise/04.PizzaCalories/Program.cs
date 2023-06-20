using PizzaCalories.Models;

namespace PizzaCalories
{
    public class StartUp
    {
        static void Main(string[] arg)
        {
            try
            {
                string pizzaName = Console.ReadLine().Split()[1];
                string[] doughInfo = Console.ReadLine().Split();

                Dough dough = new(doughInfo[1], doughInfo[2], double.Parse(doughInfo[3]));

                Pizza pizza = new(pizzaName, dough);

                string input;

                while ((input = Console.ReadLine()) != "END")
                {
                    string[] toppingInfo = input.Split();

                    Topping topping = new(toppingInfo[1], double.Parse(toppingInfo[2]));

                    pizza.AddToppings(topping);
                }

                Console.WriteLine(pizza);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }
    }
}
