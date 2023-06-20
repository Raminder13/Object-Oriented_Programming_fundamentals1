using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    public class Product
    {
        public string Name { get; }
        public decimal Price { get; }
        public string Code { get; }

        public Product(string name, decimal price, string code)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Product name cannot be empty.");
            }

            if (string.IsNullOrEmpty(code))
            {
                throw new ArgumentException("Product code cannot be empty.");
            }

            if (price <= 0)
            {
                throw new ArgumentException("Product price must be a positive value.");
            }

            Name = name;
            Price = price;
            Code = code;
        }
    }
}
