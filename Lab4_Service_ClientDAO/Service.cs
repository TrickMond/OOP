using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_ClientDAO
{
    public static class Service
    {
        private static MarketCSV MarketCSV = new MarketCSV("shops.csv");
        private static ProductCSV ProductCSV = new ProductCSV("products.csv");

        public static void DeleteMarket(int MarketID)
        {
            MarketCSV.DeleteMarket(MarketID);
        }

        public static void DeleteProduct(string Name)
        {
            ProductCSV.DeleteProduct(Name);
        }

        public static void DeleteProductFromMarket(int ID, string Name)
        {
            ProductCSV.DeleteProductFromMarket(ID, Name);
        }

        public static List<Market> GetAllMarkets()
        {
            return MarketCSV.GetAllMarkets();
        }

        public static List<Product> GetAllProducts()
        {
            return ProductCSV.GetAllProducts();
        }

        public static List<Product> GetAllProductsInShop(int ID)
        {
            return ProductCSV.GetAllProductsInShop(ID);
        }

        public static void MakeMarket(Market NewMarket)
        {
            MarketCSV.MakeMarket(NewMarket);
        }

        public static void MakeProduct(Product NewProduct)
        {
            ProductCSV.MakeProduct(NewProduct);
        }

        public static Product GetProduct(string Name)
        {
            return ProductCSV.GetProduct(Name);
        }

        public static Product GetProduct(string Name, int ID)
        {
            return ProductCSV.GetProduct(Name, ID);
        }

        public static void SaveChanges()
        {
            ProductCSV.SaveChanges();
            MarketCSV.SaveChanges();
        }

        public static void UpdateMarket(int MarketID, string Name, string Adress)
        {
            MarketCSV.UpdateMarket(MarketID, Name, Adress);
        }

        public static void UpdateProduct(string Name, int ShopID)
        {
            ProductCSV.UpdateProduct(Name, ShopID);
        }

        public static void UpdateProduct(string Name, int ShopID, int NewCount, double NewCost)
        {
            ProductCSV.UpdateProduct(Name, ShopID, NewCount, NewCost);
        }

        public static Product SortByCostOneProduct(string Name)
        {
            return ProductCSV.SortByCostsOneProduct(Name);
        }
    }
}
