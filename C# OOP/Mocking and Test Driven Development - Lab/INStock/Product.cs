using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INStock
{
    public class Product : IProduct
    {
        private string label;
        private decimal price;
        private int quantity;

        public Product(string label, decimal price, int quantity)
        {
            Label = label;
            Price = price;
            Quantity = quantity;
        }

        public string Label
        {
            get { return label; }
            private set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Product label cannot be empty!");
                }
                label = value;
            }
        }

        public decimal Price
        {
            get { return price; }
            private set
            {
                if(value <= 0)
                {
                    throw new ArgumentException("Price of the product must be positive number!");
                }
                price = value;
            }
        }

        public int Quantity
        {
            get { return quantity; }
            private set
            {
                if(value < 1)
                {
                    throw new ArgumentException("Product quantity must be at least one");
                }
                quantity = value;
            }
        }

        public override int GetHashCode()
        {
            return this.label.GetHashCode();
        }
    }
}
