﻿using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChristmasPastryShop.Models.Cocktails
{
    public abstract class Cocktail : ICocktail
    {
        private string name;
        private string size;
        private double price;

        public Cocktail(string name, string size, double price)
        {
            Name = name;
            Size = size;
            Price = price;
        }

        public string Name
        {
            get { return name; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.NameNullOrWhitespace);
                }
                name = value;
            }
        }

        public string Size { get { return size; } private set { size = value; } }

        public double Price
        {
            get { return price; }
            private set
            {
                if (this.Size == "Small")
                {
                    value /= 3;
                }
                else if (this.Size == "Middle")
                {
                    value = (value / 3) * 2;
                }
                price = value;
            }
        }

        public override string ToString()
        {
            return $"{this.Name} ({this.Size}) - {this.Price:F2} lv";
        }
    }
}
