using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_ClientDAO
{
    public static class Manager
    {
        public static void MakeMarket(Market NewMarket)
        {
            Service.MakeMarket(NewMarket);
        }

        public static void MakeProduct(Product NewProduct)
        {
            Service.MakeProduct(NewProduct);
        }

        public static void OrderProduct(int ID, string Name, int Count)
        {
            try
            {
                Service.UpdateProduct(Name, ID, Service.GetProduct(Name, ID).Count[0] + Count, Service.GetProduct(Name, ID).Cost[0]);
            }
            catch(NullReferenceException)
            {
                Console.WriteLine($"Установите стоимость товара {Name} в магазине {ID}");
                double temp = Convert.ToDouble(Console.ReadLine());
                Service.UpdateProduct(Name, ID, Count, temp);
            }
        }

        public static double? BuyProduct(int ID, string Name, int Count)
        {
            try
            {
                if (Count >= Service.GetProduct(Name, ID).Count[0])
                {
                    double temp = Service.GetProduct(Name, ID).Cost[0];
                    int countTemp = Service.GetProduct(Name, ID).Count[0];
                    Service.DeleteProductFromMarket(ID, Name);
                    return temp * countTemp;
                }
                else
                {
                    Service.UpdateProduct(Name, ID, Service.GetProduct(Name, ID).Count[0] - Count, Service.GetProduct(Name, ID).Cost[0]);
                    return Service.GetProduct(Name, ID).Cost[0] * Count;
                }
            }
            catch(NullReferenceException)
            {
                Console.WriteLine($"Ошибка при поиске: магазина {ID} или продукта в нём с именем \"{Name}\" не существует");
                return null;
            }
        }

        public static List<Product> WhatCanIBuy(int ID, double Money)
        {
            List<Product> request = new List<Product>();
            foreach (var s in Service.GetAllProductsInShop(ID))
                if (s.Cost[0] > Money)
                    continue;
                else
                {
                    int count = (int)Math.Truncate(Money / s.Cost[0]);
                    if (count > s.Count[0])
                        count = s.Count[0];
                    double cost = (double)count * s.Cost[0];
                    request.Add(new Product(s.Name, ID, count, cost));
                }
            return request;
        }

        public static int? WhereTheCheapestProducts(List<string> productsName, List<int> productsCounts)
        {
            List<int> ShopListID = new List<int>();
            for (int i = 0; i < productsName.Count(); i++)
            {
                try
                {
                    Product p = Service.SortByCostOneProduct(productsName[i]);
               
                for (int j = 0; j < p.Count.Count; j++)
                {
                    if (p.Count[j] >= productsCounts[i])
                    {
                        ShopListID.Add(p.ShopID[j]);
                        if (ShopListID.Where(item => item == p.ShopID[j]).Count() == productsCounts.Count())
                            return p.ShopID[j];
                    }
                }
                }
                catch (NullReferenceException)
                {
                    throw new NullReferenceException($"В ДАО нет продукта с именем {productsName[i]}");
                }
            }
            Console.WriteLine("Ни в одном магазине нет продуктов с заданным количеством");
            return null;
        }
    }
}
