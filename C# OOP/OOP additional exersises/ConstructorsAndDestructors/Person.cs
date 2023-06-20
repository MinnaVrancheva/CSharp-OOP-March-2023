using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructorsAndDestructors
{
    public class Person
    {
        public Person(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public override string ToString()
        {
            return $"Hello! My name is {Name}";
        }

        ~Person()
        {
            Name = string.Empty;
        }
    }
}
