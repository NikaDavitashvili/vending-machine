using System;
using System.Collections.Generic;
using System.Linq;
using vending_machine.Strategies;

namespace vending_machine
{
    public class Program
    {
        static void Main(string[] args)
        {
            VendingMachine vending_machine = new VendingMachine();
            vending_machine.StartBuyingSnacks();
        }
    }
}