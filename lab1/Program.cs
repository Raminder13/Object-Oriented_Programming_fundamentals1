namespace lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            VendingMachine vendingMachine = new VendingMachine(1);

            Product coke = new Product("Coke", 2, "A1");
            Product sprite = new Product("Sprite", 3, "A2");
            Product fanta = new Product("Fanta", 3, "A3");

            vendingMachine.StockItem(coke, 10);
            vendingMachine.StockItem(sprite, 10);
            vendingMachine.StockItem(fanta, 10);

            vendingMachine.StockFloat(1, 10);
            vendingMachine.StockFloat(2, 10);
            vendingMachine.StockFloat(5, 10);
            vendingMachine.StockFloat(10, 10);
            vendingMachine.StockFloat(20, 10);
            vendingMachine.StockFloat(50, 10);
            vendingMachine.StockFloat(100, 10);

            List<int> money = new List<int>();

            money.Add(20);
            money.Add(10);
            money.Add(5);            

            Console.WriteLine(vendingMachine.VendItem("A1", money));
        }
    }
}
