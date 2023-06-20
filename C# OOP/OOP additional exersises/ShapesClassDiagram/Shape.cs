using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapesClassDiagram
{
    public abstract class Shape
    {
        protected Location location;

        public double Area()
        {
            return 0.0;
        }

        public double Perimeter()
        {
            return 0.0;
        }

        public string ToString()
        {
            return string.Empty;
        }
    }
}
