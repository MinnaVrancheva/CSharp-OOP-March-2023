using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INStock
{
    public class ProductStock : IEnumerable<Product>
    {
        public ProductStock()
        {
            this.ProductPerLabel = new Dictionary<string, Product>();
            this.Products = new List<IProduct>();
            this.ProductsSortedPerPrice = new SortedList<decimal, HashSet<Product>>();
            this.ProductsSortedPerQuantity = new SortedList<int, HashSet<Product>>();
        }

        public Dictionary<string, Product> ProductPerLabel { get; }
        public List<IProduct> Products { get; }
        public SortedList<decimal, HashSet<Product>> ProductsSortedPerPrice { get; }
        public SortedList<int, HashSet<Product>> ProductsSortedPerQuantity { get; }

        public int Count => this.ProductPerLabel.Count;

        public void Add(Product product)
        {
            this.ProductPerLabel.Add(product.Label, product);
            this.Products.Add(product);

            if (!ProductsSortedPerPrice.ContainsKey(product.Price))
            {
                this.ProductsSortedPerPrice.Add(product.Price, new HashSet<Product>());
                this.ProductsSortedPerPrice[product.Price].Add(product);
            }
            else
            {
                this.ProductsSortedPerPrice[product.Price].Add(product);
            }

            if (!ProductsSortedPerQuantity.ContainsKey(product.Quantity))
            {
                this.ProductsSortedPerQuantity.Add(product.Quantity, new HashSet<Product>());
                this.ProductsSortedPerQuantity[product.Quantity].Add(product);
            }
            else
            {
                this.ProductsSortedPerQuantity[product.Quantity].Add(product);
            }
        }

        public bool Contains(Product product)
        {
            return ProductPerLabel.ContainsKey(product.Label);
        }

        public IProduct Find(int index)
        {
            if (index < 0 || index >= this.Products.Count)
            {
                throw new IndexOutOfRangeException("The index is outside of the range");
            }
            return this.Products[index];
        }

        public IProduct FindByLabel(string label)
        {
            Product product = ProductPerLabel.FirstOrDefault(x => x.Key == label).Value;

            if(product == null)
            {
                throw new ArgumentException("Product with that label does not exist!");
            }

            return product;
        }

        public List<IProduct> FindAllInPriceRange(decimal lowestPrice, decimal highestPrice)
        {
            List<IProduct> sortedProduct = new List<IProduct>();
            IList<decimal> priceList = ProductsSortedPerPrice.Keys;

            if (priceList[0] > highestPrice && priceList.Last() < lowestPrice)
            {
                return sortedProduct;
            }
            
            foreach (decimal price in priceList)
            {
                if (price >= lowestPrice && price <= highestPrice)
                {
                    sortedProduct.AddRange(ProductsSortedPerPrice[price]);
                }
            }

            priceList.Reverse();
            return sortedProduct;
        }

        public ICollection<Product> FindAllByPrice(decimal price)
        {
            var priceKeys = ProductsSortedPerPrice.Keys;

            if (priceKeys.Contains(price) == false)
            {
                return new HashSet<Product>();
            }

            return ProductsSortedPerPrice[price];
        }

        
        public Product FindMostExpensiveProducts()
        {
            HashSet<Product> products = ProductsSortedPerPrice.Values.Last();
            return products.First();
        }

        public ICollection<Product> FindAllByQuantity(int quantity)
        {
            var quantityKeys = ProductsSortedPerQuantity.Keys;

            if(quantityKeys.Contains(quantity) == false)
            {
                return new HashSet<Product>();
            }

            return ProductsSortedPerQuantity[quantity];
        }

        public IEnumerator<Product> GetEnumerator()
        {
            foreach ((string label, var product) in this.ProductPerLabel)
            {
                yield return product;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IProduct this[int index]
        {
            get => this.Find(index);
            set
            {
                if (index >= 0 && index <= this.Products.Count)
                {
                    this.Products[index] = value;
                }
            }
        }

    }
}
