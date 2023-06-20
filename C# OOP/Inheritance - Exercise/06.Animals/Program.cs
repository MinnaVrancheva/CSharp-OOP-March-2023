
namespace Animals
{
    public class StartUp
    {
        public static void Main(string[] arg)
        {
            string input;

            while ((input = Console.ReadLine()) != "Beast!")
            {
                string[] input2 = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                try
                {
                    switch (input)
                    {
                        case "Dog":
                            Dog dog = new Dog(input2[0], int.Parse(input2[1]), input2[2]);
                            PrintAnimal(input, dog);
                            break;
                        case "Frog":
                            Frog frog = new Frog(input2[0], int.Parse(input2[1]), input2[2]);
                            PrintAnimal(input, frog);
                            break;
                        case "Cat":
                            Cat cat = new Cat(input2[0], int.Parse(input2[1]), input2[2]);
                            PrintAnimal(input, cat);
                            break;
                        case "Tomcat":
                            Tomcat tomcat = new Tomcat(input2[0], int.Parse(input2[1]));
                            PrintAnimal(input, tomcat);
                            break;
                        case "Kitten":
                            Kitten kittens = new Kitten(input2[0], int.Parse(input2[1]));
                            PrintAnimal(input, kittens);
                            break;
                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static void PrintAnimal<T>(string input, T animal) where T : Animal
        {
            Console.WriteLine(input);
            Console.WriteLine(animal.ToString());
        }
    }
}

