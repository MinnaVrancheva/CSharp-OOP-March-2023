using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayCelebrations
{
    public class Pet : IBirthdate
    {
        public Pet(string birthdate, string name)
        {
            Birthdate = birthdate;
            Name = name;
        }

        public string Birthdate { get; private set; }
        public string Name { get; private set; }
    }
}
