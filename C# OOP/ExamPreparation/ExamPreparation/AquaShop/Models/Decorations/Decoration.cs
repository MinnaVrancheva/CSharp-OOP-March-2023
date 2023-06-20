using AquaShop.Models.Decorations.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquaShop.Models.Decorations
{
    public abstract class Decoration : IDecoration
    {
        private int comfort;
        private decimal price;

        public Decoration(int comfort, decimal price)
        {
            Comfort = comfort;
            Price = price;
        }

        public int Comfort
        {
            get { return comfort; }
            protected set { comfort = value; }
        }

        public decimal Price
        {
            get { return price; }
            protected set { price = value; }
        }
    }
}
