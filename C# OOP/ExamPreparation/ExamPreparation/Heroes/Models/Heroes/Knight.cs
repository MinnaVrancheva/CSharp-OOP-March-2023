using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Models.Contracts.Heroes
{
    public class Knight : Hero
    {
        public Knight(string name, int health, int armour)
            : base(name, health, armour) { }
    }
}
