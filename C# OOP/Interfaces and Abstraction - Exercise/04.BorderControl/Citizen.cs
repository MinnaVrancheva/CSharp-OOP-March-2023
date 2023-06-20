using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BorderControl
{
    public class Citizen : IIdentifier
    {
        public Citizen(string id, string name, int age)
        {
            Id = id;
            Name = name;
            Age = age;
        }
        public string Id { get; private set; }
        public string Name { get; private set; }
        public int Age { get; private set; }
    }
}
