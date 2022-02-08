using System.Collections.Generic;

namespace vending_machine.Strategies
{
    public interface ISalesStrategy
    {
        public void PrintByCurrency(List<ItemsToDrop> list);
        public decimal MoneyAndBarcodeIN(decimal money, string barCode, List<ItemsToDrop> list);
        public void ChangeBack(decimal balance);
        public void Discount(List<ItemsToDrop> list, ItemsType type);
        public void EndDiscount(List<ItemsToDrop> list, ItemsType type);

    }
}