using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    public class Product
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public string Code { get; set; }

        public Product(string name, int price, string code)
        {
            Name = name;
            Price = price;
            Code = code;
        }
    }
}
