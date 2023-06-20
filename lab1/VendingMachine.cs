using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    public class VendingMachine
    {
        private static int nextSerialNumber = 1;
        private readonly string barcode;

        public int SerialNumber { get; }
        public string Barcode => barcode;
        private Dictionary<int, int> MoneyFloat { get; }
        private Dictionary<Product, int> Inventory { get; }

        public VendingMachine(string barcode)
        {
            if (string.IsNullOrEmpty(barcode))
            {
                throw new ArgumentException("Barcode cannot be empty.");
            }

            this.barcode = barcode;
            SerialNumber = nextSerialNumber++;
            MoneyFloat = new Dictionary<int, int>();
            Inventory = new Dictionary<Product, int>();
        }

        public string StockItem(Product product, int quantity)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            if (quantity < 0)
            {
                throw new ArgumentException("Quantity cannot be negative.");
            }

            if (Inventory.ContainsKey(product))
            {
                Inventory[product] += quantity;
            }
            else
            {
                Inventory[product] = quantity;
            }

            return $"Product '{product.Name}' (Code: {product.Code}) stocked. Price: ${product.Price}, Quantity: {Inventory[product]}";
        }

        public string StockFloat(int moneyDenomination, int quantity)
        {
            if (moneyDenomination <= 0)
            {
                throw new ArgumentException("Money denomination must be a positive value.");
            }

            if (quantity < 0)
            {
                throw new ArgumentException("Quantity cannot be negative.");
            }

            if (MoneyFloat.ContainsKey(moneyDenomination))
            {
                MoneyFloat[moneyDenomination] += quantity;
            }
            else
            {
                MoneyFloat[moneyDenomination] = quantity;
            }

            return $"Stocked {quantity} ${moneyDenomination} coins in the float.";
        }

        public string VendItem(string code, List<int> money)
        {
            if (string.IsNullOrEmpty(code))
            {
                throw new ArgumentException("Code cannot be empty.");
            }

            Product product = GetProductByCode(code);

            if (product == null)
            {
                return $"Error: No item with code '{code}'.";
            }

            int itemQuantity = Inventory[product];
            if (itemQuantity == 0)
            {
                return $"Error: Item '{product.Name}' with code '{product.Code}' is out of stock.";
            }

            decimal totalPrice = product.Price;

            decimal totalMoney = 0;
            foreach (int denomination in money)
            {
                totalMoney += denomination;
            }

            if (totalMoney < totalPrice)
            {
                return $"Error: Insufficient money provided. The price of '{product.Name}' is ${totalPrice}.";
            }

            decimal changeAmount = totalMoney - totalPrice;

            string changeString = GetChangeString(changeAmount);
            Inventory[product]--;

            return $"Please enjoy your '{product.Name}' and take your change of ${changeAmount}. {changeString}";
        }

        private Product GetProductByCode(string code)
        {
            foreach (var kvp in Inventory)
            {
                if (kvp.Key.Code == code)
                {
                    return kvp.Key;
                }
            }

            return null;
        }

        private string GetChangeString(decimal amount)
        {
            string changeString = "Change returned: ";
            int[] denominations = new int[] { 20, 10, 5, 2, 1 };

            foreach (int denomination in denominations)
            {
                if (amount >= denomination && MoneyFloat.ContainsKey(denomination) && MoneyFloat[denomination] > 0)
                {
                    int numCoins = (int)(amount / denomination);
                    numCoins = Math.Min(numCoins, MoneyFloat[denomination]);
                    amount -= numCoins * denomination;
                    MoneyFloat[denomination] -= numCoins;
                    changeString += $"{numCoins} ${denomination} coins, ";
                }
            }

            return changeString.TrimEnd(',', ' ');
        }
    }
}
