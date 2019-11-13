using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_ClientDAO
{
    class Program
    {
        static void Main(string[] args)
        {
            Manager.StartWorkWith("Settings.txt");

            Console.WriteLine("Markets in DAO:");
            foreach (var temp in Service.GetAllMarkets())
                Console.WriteLine(temp);

            Console.WriteLine("Products in DAO:");
            foreach (var temp in Service.GetAllProducts())
                Console.WriteLine(temp);
            Console.WriteLine();
            

            Console.WriteLine("****Markets in DAO:");
            foreach (var temp in Service.GetAllMarkets())
                Console.WriteLine(temp);

            Console.WriteLine("****Products in DAO:");
            foreach (var temp in Service.GetAllProducts())
                Console.WriteLine(temp);
            Console.WriteLine();

            Manager.OrderProduct(8, "New Product", 100);
            Console.WriteLine("What vcnn i buy");
            foreach (var temp in Manager.WhatCanIBuy(1, 10000))
                Console.WriteLine(temp);
            Console.WriteLine("buy");
            Console.WriteLine(Manager.BuyProduct(8, "New Product", 100));

            Console.WriteLine("\nСамый дешевый список продуктов - количества в магазине с ID");
            Console.WriteLine(Manager.WhereTheCheapestProducts(new List<string> { "Sprite", "TV set PHILIPS" }, new List<int> { 1, 1 }));



            
        }
    }
}
