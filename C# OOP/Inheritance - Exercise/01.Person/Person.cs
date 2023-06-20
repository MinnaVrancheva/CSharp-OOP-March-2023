using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Person
{
    public class Person
    {
        private string name;
        private int age;

        public Person(string name, int age)
        {
            this.Name = name;
            this.Age = age;
        }

        public string Name { get; set; }
        public int Age { get; set; }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(string.Format("Name: {0}, Age: {1}",
                this.Name,
                this.Age));

            return sb.ToString();
        }
    }
}
