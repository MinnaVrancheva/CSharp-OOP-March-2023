
using ShoppingSpree.Models;

namespace ShoppingSpree
{
    public class StartUp
    {
        static void Main(string[] arg)
        {
            List<Person> personList = new List<Person>();
            List<Product> productList = new List<Product>();

            try
            {
                string[] personNamesPairs = Console.ReadLine()
                    .Split(";", StringSplitOptions.RemoveEmptyEntries);

                foreach (string name in personNamesPairs)
                {
                    string[] values = name.Split("=", StringSplitOptions.RemoveEmptyEntries);

                    Person person = new Person(values[0], decimal.Parse(values[1]));
                    personList.Add(person);

                }
                    
                string[] productNamesPairs = Console.ReadLine()
                                .Split(separator: ";", StringSplitOptions.RemoveEmptyEntries);

                foreach (string name in productNamesPairs)
                {
                    string[] values = name.Split("=", StringSplitOptions.RemoveEmptyEntries);

                    Product products = new Product(values[0], decimal.Parse(values[1]));
                    productList.Add(products);

                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            string input;

            while ((input = Console.ReadLine()) != "END")
            {
                string personName = input.Split(" ", StringSplitOptions.RemoveEmptyEntries)[0];
                string productName = input.Split(" ", StringSplitOptions.RemoveEmptyEntries)[1];

                Person person = personList.FirstOrDefault(x => x.Name == personName);
                Product product = productList.FirstOrDefault(x => x.Name == productName);

                if (person is not null && product is not null)
                {
                    Console.WriteLine(person.Add(product));
                }
            }

            Console.WriteLine(string.Join(Environment.NewLine, personList));
        }
    }
}

