﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Models.Contracts.Heroes
{
    public class Barbarian : Hero
    {
        public Barbarian(string name, int health, int armour)
            : base(name, health, armour) { }
    }
}
