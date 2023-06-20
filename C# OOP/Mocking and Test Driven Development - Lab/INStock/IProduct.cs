using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INStock
{
    public interface IProduct
    {
        public string Label { get; }
        public decimal Price { get; }
        public int Quantity { get; }

    }
}
