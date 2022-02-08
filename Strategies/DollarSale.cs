using System;
using System.Collections.Generic;
using System.Linq;

namespace vending_machine.Strategies
{
    class DollarSale : ISalesStrategy
    {
        public void ChangeBack(decimal balance)
        {
            Console.WriteLine("\n\n\nMachine has: 20 cent, 50 cent, 1 dollar, 5 dollar.");
            decimal leftOnBalance = 0m;
            decimal sumOfChange = 0m;
            decimal change_20 = 20m; decimal change_50 = 50m; decimal change_100 = 100m; decimal change_500 = 500m;
            int counter_20 = 0; int counter_50 = 0; int counter_100 = 0; int counter_500 = 0;
            while (balance != 0)
            {
                if (balance < change_20)
                {
                    leftOnBalance += balance;
                    balance = 0;
                }
                else if (balance >= change_20 && balance < change_50)
                {
                    balance -= 20;
                    sumOfChange += 20;
                    counter_20 += 1;
                }
                else if (balance >= change_50 && balance < change_100)
                {
                    balance -= 50;
                    sumOfChange += 50;
                    counter_50 += 1;
                }
                else if (balance >= change_100 && balance < change_500)
                {
                    balance -= 100;
                    sumOfChange += 100;
                    counter_100 += 1;
                }
                else if (balance >= change_500)
                {
                    balance -= 500;
                    sumOfChange += 500;
                    counter_500 += 1;
                }
            }
            Console.WriteLine($"\n\tYour Change\n20 cent - {counter_20}\n50 cent - {counter_50}\n1 Dollar Bill - {counter_100}\n5 Dollar Bill - {counter_500}");
            Console.WriteLine($"Your Total change is - {sumOfChange} Cent\n\nMoney left on Balance: {leftOnBalance} Cent");
        }

        public decimal MoneyAndBarcodeIN(decimal balance, string barCode, List<ItemsToDrop> list)
        {
            foreach (var item in list)
            {
                if (barCode == item.Code)
                {
                    if (balance > item.Price && item.ItemsCount > 0)
                    {
                        item.ItemsCount--;
                        balance -= item.Price;
                        Console.WriteLine($"\n   Purchased successfully\nClaim your item - \"{item.itemsName}\"\n" +
                            $"Balance: {balance} Cent");
                        break;
                    }
                    else if (balance < item.Price)
                    {
                        Console.WriteLine("\nNot Enough Money!");
                        Console.WriteLine($"Balance: {balance}");
                        break;
                    }
                    else if (item.ItemsCount <= 0)
                    {
                        Console.WriteLine($"\nMachine has run out of {item.itemsName}!");
                        break;
                    }
                }
            }
            return balance;
        }

        public void PrintByCurrency(List<ItemsToDrop> list)
        {
            foreach (var item in list)
            {
                Console.WriteLine($"{item.itemsName} - {item.Code} - {item.Price} Cent - {item.ItemsCount}");
            }
        }
        public void Discount(List<ItemsToDrop> list, ItemsType type)
        {
            foreach (var item in list)
            {
                if(item.itemsType == type && type == ItemsType.Chocolate) item.Price = item.Price * 8/10;

                else if (item.itemsType == type && type == ItemsType.Drink) item.Price = item.Price * 8 / 10;

                else if (item.itemsType == type && type == ItemsType.Chips) item.Price = item.Price * 9 / 10;
            }
        }
        public void EndDiscount(List<ItemsToDrop> list, ItemsType type)
        {
            foreach (var item in list)
            {
                if (item.itemsType == type && type == ItemsType.Chocolate) item.Price = item.Price * 10/8;

                else if (item.itemsType == type && type == ItemsType.Drink) item.Price = item.Price * 10/8;

                else if (item.itemsType == type && type == ItemsType.Chips) item.Price = item.Price * 10/9;
            }
        }
    }
}