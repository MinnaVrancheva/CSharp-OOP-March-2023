using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
    public class Rectangle : Shape
    {
        private double height;
        private double width;

        public Rectangle(double height, double width)
        {
            Height = height;
            Width = width;
        }

        public double Height { get { return this.height; } private set { this.height = value; } }
        public double Width { get { return this.width; } private set { this.width = value; } }

        public override double CalculateArea() => this.Width * this.Height;

        public override double CalculatePerimeter() => 2 * (this.Width + this.Height);
    }
}
