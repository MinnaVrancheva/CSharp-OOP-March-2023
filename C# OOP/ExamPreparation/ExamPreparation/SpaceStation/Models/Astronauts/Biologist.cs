using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceStation.Models.Astronauts
{
    public class Biologist : Astronaut
    {
        private const int BiologistOxygen = 70;
        public Biologist(string name)
            : base(name, BiologistOxygen) { }

        public override void Breath()
        {
            Oxygen = Oxygen - 5 > 0 ? Oxygen - 5 : 0;
        }
    }
}
