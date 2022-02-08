using System.Collections.Generic;
namespace vending_machine.Strategies
{
    class NoSalesStrategy : ISalesStrategy
    {
        public void ChangeBack(decimal balance)
        {
            System.Console.WriteLine("ERROR, something wrong!");
        }

        public decimal MoneyAndBarcodeIN(decimal money, string barCode, List<ItemsToDrop> list)
        {
            System.Console.WriteLine("ERROR, something wrong!");
            return 0;
        }

        public void PrintByCurrency(List<ItemsToDrop> list)
        {
            System.Console.WriteLine("ERROR, wrong currency! Choose USD or LARI!");
        }

        public void Discount(List<ItemsToDrop> list, ItemsType type)
        {
            System.Console.WriteLine("ERROR, something wrong!");
        }

        public void EndDiscount(List<ItemsToDrop> list, ItemsType type)
        {
            System.Console.WriteLine("ERROR, something wrong!");
        }
    }
}