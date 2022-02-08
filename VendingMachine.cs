using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vending_machine.Strategies;

namespace vending_machine
{
    public class VendingMachine
    {
        public ISalesStrategy salesStrategy;
        public List<ItemsToDrop> ItemsList { get; set; } = new List<ItemsToDrop>();
        public ItemsToDrop itemsToDrop { get; set; }
        public string Currency { get; set; }
        public decimal Purchased { get; set; }
        public decimal Balance { get; set; }
        public string Code { get; set; }

        public void SetStrategy(ISalesStrategy salesStrategy)
        {
            this.salesStrategy = salesStrategy;
        }

        public void PrintItems(List<ItemsToDrop> list)
        {
            Console.WriteLine("\n   Item - Code - Price - Item Quantity");
            Console.WriteLine(new string('_', 40));
            this.salesStrategy.PrintByCurrency(list);
        }

        public void BuySnacks(decimal money, string barCode, List<ItemsToDrop> list)
        {
            foreach(var item in list)
            {
                if (barCode == item.Code && Balance > item.Price) Purchased = item.Price;
            }
            Balance = money;
            Balance = this.salesStrategy.MoneyAndBarcodeIN(Balance, barCode, list);
        }

        public void GiveChange()
        {
            this.salesStrategy.ChangeBack(Balance);
        }


        public int DiscountOnTypes(List<ItemsToDrop> list)
        {
            int randForDiscount = new Random().Next(0, 30);
            if(randForDiscount < 10)
            {
                Console.WriteLine("We have 20% discount on Chocolates.");
                this.salesStrategy.Discount(list, ItemsType.Chocolate);
            }
            else if (randForDiscount >= 10 && randForDiscount < 20)
            {
                Console.WriteLine("We have 20% discount on Drinks.");
                this.salesStrategy.Discount(list, ItemsType.Drink);
            }
            else if (randForDiscount >= 20 && randForDiscount < 30)
            {
                Console.WriteLine("We have 10% discount on Chips.");
                this.salesStrategy.Discount(list, ItemsType.Chips);
            }

            return randForDiscount;
        }
        public void EndDiscountForCurrentCustomer(List<ItemsToDrop> list, int randForDiscount)
        {
            if (randForDiscount < 10)
            {
                this.salesStrategy.EndDiscount(list, ItemsType.Chocolate);
            }
            else if (randForDiscount >= 10 && randForDiscount < 20)
            {
                this.salesStrategy.EndDiscount(list, ItemsType.Drink);
            }
            else if (randForDiscount >= 20 && randForDiscount < 30)
            {
                this.salesStrategy.EndDiscount(list, ItemsType.Chips);
            }
        }


        public void ConvertIntoDollar(List<ItemsToDrop> list)
        {
            foreach (var item in list)
            {
                item.Price = item.Price* 3/10;
            }
        }
        public void ConvertIntoLari(List<ItemsToDrop> list)
        {
            foreach (var item in list)
            {
                item.Price = item.Price * 10/3;
            }
        }

        public void FillSnacks(List<ItemsToDrop> list)
        {
            foreach(var item in list)
            {
                if(item.ItemsCount <= 0)
                {
                    item.ItemsCount = new Random().Next(5, 10);
                    Console.WriteLine($"\nVending machine run out of - {item.itemsName}!\nIt was refilled by - {item.ItemsCount}");
                }
            }
        }

        public bool MoneyAndCodeCheck(decimal balance, string code)
        {
            if (balance <= 0)
            {
                Console.WriteLine("ERROR, wrong money input");
                return false;
            }
            else if (code != "A001" && code != "A002" && code != "A003" &&
                     code != "B001" && code != "B002" && code != "B003" &&
                     code != "C001" && code != "C002" && code != "C003")
            {
                Console.WriteLine("ERROR, wrong code input!");
                return false;
            }
            return true;
        }

        public string CheckCurrency(List<ItemsToDrop> list)
        {
            if (list[0].Price < 50m) return "Dollar";
            else return "Lari";
        }

        public void SnacksIntoMachine(List<ItemsToDrop> list)
        {
            List<string> codesList = new List<string>() { "A001", "A002", "A003", "B001", "B002", "B003", "C001", "C002", "C003" };
            var rnd = new Random();
            var shuffledList = codesList.OrderBy(item => rnd.Next());
            int index = 0;
            foreach (var item in shuffledList)
            {
                list[index].Code = item;
                index++;
            }
        }

        public void StartBuyingSnacks()
        {
            VendingMachine vend = new VendingMachine();
            vend.ItemsList.Add(new ItemsToDrop
            {
                itemsType = ItemsType.Chocolate,
                itemsName = Items.Snickers,
                Price = 150
            });
            vend.ItemsList.Add(new ItemsToDrop
            {
                itemsType = ItemsType.Chocolate,
                itemsName = Items.Twix,
                Price = 150
            });
            vend.ItemsList.Add(new ItemsToDrop
            {
                itemsType = ItemsType.Chocolate,
                itemsName = Items.KitKat,
                Price = 130
            });
            vend.ItemsList.Add(new ItemsToDrop
            {
                itemsType = ItemsType.Drink,
                itemsName = Items.Coke,
                Price = 150
            });
            vend.ItemsList.Add(new ItemsToDrop
            {
                itemsType = ItemsType.Drink,
                itemsName = Items.Fanta,
                Price = 130
            });
            vend.ItemsList.Add(new ItemsToDrop
            {
                itemsType = ItemsType.Drink,
                itemsName = Items.Sprite,
                Price = 120
            });
            vend.ItemsList.Add(new ItemsToDrop
            {
                itemsType = ItemsType.Chips,
                itemsName = Items.Lays,
                Price = 80
            });
            vend.ItemsList.Add(new ItemsToDrop
            {
                itemsType = ItemsType.Chips,
                itemsName = Items.Doritos,
                Price = 100
            });
            vend.ItemsList.Add(new ItemsToDrop
            {
                itemsType = ItemsType.Chips,
                itemsName = Items.Pringles,
                Price = 50
            });

            Console.WriteLine("\tVending Machine");
            SnacksIntoMachine(vend.ItemsList);

            string stopProgramme = string.Empty;

            while(stopProgramme != "y")
            {
                Console.Write("Pick Currency (Lari/USD): ");
                string currencyChoice = Convert.ToString(Console.ReadLine());
                if (currencyChoice.ToLower() == "usd")
                {
                    //strategiis sheqmna dolaristvis, fasebis morgeba valutaze, fasdaklebebis gaketeba, monacemebis gamotana.
                    vend.SetStrategy(new DollarSale());
                    string checker = CheckCurrency(vend.ItemsList);
                    if (checker == "Lari")
                    {
                        vend.ConvertIntoDollar(vend.ItemsList);
                    }
                    int randForDiscount = vend.DiscountOnTypes(vend.ItemsList);
                    vend.PrintItems(vend.ItemsList);

                    //Pirveli archeva. shemodis tanxa da item-is kodi.
                    bool cont = true;
                    while (cont)
                    {
                        Console.Write("\nEnter the money: ");
                        vend.Balance = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter Code of Item: ");
                        vend.Code = Convert.ToString(Console.ReadLine());
                        if(MoneyAndCodeCheck(vend.Balance, vend.Code))
                        {
                            cont = false;
                            vend.BuySnacks(vend.Balance, vend.Code, vend.ItemsList);
                            vend.FillSnacks(vend.ItemsList);
                        }
                    }

                    //Momdevno archevnebi, sanam momxmarebels ar shewyveta ar moundeba. shemodis mxolod item-is kodi.
                    bool continueOperations = true;
                    while (continueOperations)
                    {
                        Console.Write("\nDo you want to purchase anything else?(Y/y to continue)\nAnswer: ");
                        string toContinue = Convert.ToString(Console.ReadLine());
                        if (toContinue.ToLower() == "y")
                        {
                            Console.Write("Enter Code of Item: ");
                            vend.Code = Convert.ToString(Console.ReadLine());
                            if (MoneyAndCodeCheck(vend.Balance, vend.Code))
                            {
                                vend.BuySnacks(vend.Balance, vend.Code, vend.ItemsList);
                                vend.FillSnacks(vend.ItemsList);
                            }
                        }
                        else
                        {
                            continueOperations = false;
                        }
                    }

                    //xurdis dabruneba.
                    vend.GiveChange();
                    Console.WriteLine("\n\n");

                    //fasdaklebebis moxsna, im shemtxvevashi, tu mimdinare momxmarebelma operaciebi shewyvita.
                    vend.EndDiscountForCurrentCustomer(vend.ItemsList, randForDiscount);

                    //kitxva unda tu ara mtliani programis dasruleba.
                    Console.Write("Do you want to end programme?(Y/y to end)\nAnswer: ");
                    stopProgramme = Convert.ToString(Console.ReadLine()).ToLower();
                    if(stopProgramme == "y")
                    {
                        Console.WriteLine(new string('_', 40));
                        Console.WriteLine("\tEnd Of Programme!");
                    }
                }
                else if (currencyChoice.ToLower() == "lari")
                {
                    //strategiis sheqmna laristvis, fasebis morgeba valutaze, fasdaklebebis gaketeba, monacemebis gamotana.
                    vend.SetStrategy(new LariSale());
                    string checker = CheckCurrency(vend.ItemsList);
                    if (checker == "Dollar")
                    {
                        vend.ConvertIntoLari(vend.ItemsList);
                    }
                    int randForDiscount = vend.DiscountOnTypes(vend.ItemsList);
                    vend.PrintItems(vend.ItemsList);

                    //Pirveli archeva. shemodis tanxa da item-is kodi.
                    bool cont = true;
                    while (cont)
                    {
                        Console.Write("\nEnter the money: ");
                        vend.Balance = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter Code of Item: ");
                        vend.Code = Convert.ToString(Console.ReadLine());
                        if (MoneyAndCodeCheck(vend.Balance, vend.Code))
                        {
                            cont = false;
                            vend.BuySnacks(vend.Balance, vend.Code, vend.ItemsList);
                            vend.FillSnacks(vend.ItemsList);
                        }
                    }

                    //Momdevno archevnebi, sanam momxmarebels ar shewyveta ar moundeba. shemodis mxolod item-is kodi.
                    bool continueOperations = true;
                    while (continueOperations)
                    {
                        Console.Write("\nDo you want to purchase anything else?(Y/y to continue)\nAnswer: ");
                        string toContinue = Convert.ToString(Console.ReadLine());
                        if (toContinue.ToLower() == "y")
                        {
                            Console.Write("Enter Code of Item: ");
                            vend.Code = Convert.ToString(Console.ReadLine());
                            if (MoneyAndCodeCheck(vend.Balance, vend.Code))
                            {
                                vend.BuySnacks(vend.Balance, vend.Code, vend.ItemsList);
                                vend.FillSnacks(vend.ItemsList);
                            }
                        }
                        else
                        {
                            continueOperations = false;
                        }
                    }

                    //xurdis dabruneba.
                    vend.GiveChange();
                    Console.WriteLine("\n\n");

                    //fasdaklebebis moxsna, im shemtxvevashi, tu mimdinare momxmarebelma operaciebi shewyvita.
                    vend.EndDiscountForCurrentCustomer(vend.ItemsList, randForDiscount);

                    //kitxva unda tu ara mtliani programis dasruleba.
                    Console.Write("Do you want to end programme?(Y/y to end)\nAnswer: ");
                    stopProgramme = Convert.ToString(Console.ReadLine()).ToLower();
                    if (stopProgramme == "y")
                    {
                        Console.WriteLine(new string('_', 40));
                        Console.WriteLine("\t  End Of Programme!");
                    }

                    
                }
                else
                {
                    Console.WriteLine("ERROR, enter valid currency(Lari/USD)\n");
                }
            }
        }

    }
    public enum ItemsType
    {
        Chocolate,
        Drink,
        Chips
    }

    public enum Items
    {
        Snickers,
        Twix,
        KitKat,
        Coke,
        Fanta,
        Sprite,
        Lays,
        Doritos,
        Pringles
    }

    public class ItemsToDrop
    {
        public string Code { get; set; }
        public ItemsType itemsType { get; set; }
        public Items itemsName { get; set; }
        public decimal Price { get; set; }
        public int ItemsCount = new Random().Next(5, 10);
    }
}