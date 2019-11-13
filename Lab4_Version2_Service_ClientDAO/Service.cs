using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_ClientDAO
{
    public static class Service
    {
        private static IMarket MarketDAO;
        private static IProduct ProductDAO;

        public static void StartWorkWith(string path)
        {
            string[] parts;
            try
            {
                parts = File.ReadAllLines(path);
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException($"Файл {path} не найден!");
            }
            try
            {
                if (parts[0] == "CSV")
                {
                    string[] piece = parts[1].Split(' ');
                    MarketDAO = new MarketCSV(piece[0]);
                    ProductDAO = new ProductCSV(piece[1]);
                }
                else if (parts[0] == "DB")
                {
                    string[] piece = parts[1].Split(' ');
                    MarketDAO = new MarketDB(piece[0]);
                    ProductDAO = new ProductDB(piece[0]);
                }
            }
            catch(ArgumentOutOfRangeException)
            {
                throw new ArgumentOutOfRangeException("Ошибка при обработке файла-настроек (settings.txt) подключения к ДАО");
            }
        }

        public static void DeleteMarket(int MarketID)
        {
            MarketDAO.DeleteMarket(MarketID);
        }

        public static void DeleteProduct(string Name)
        {
            ProductDAO.DeleteProduct(Name);
        }

        public static void DeleteProductFromMarket(int ID, string Name)
        {
            ProductDAO.DeleteProductFromMarket(ID, Name);
        }

        public static List<Market> GetAllMarkets()
        {
            return MarketDAO.GetAllMarkets();
        }

        public static List<Product> GetAllProducts()
        {
            return ProductDAO.GetAllProducts();
        }

        public static List<Product> GetAllProductsInShop(int ID)
        {
            return ProductDAO.GetAllProductsInShop(ID);
        }

        public static void MakeMarket(Market NewMarket)
        {
            MarketDAO.MakeMarket(NewMarket);
        }

        public static void MakeProduct(Product NewProduct)
        {
            ProductDAO.MakeProduct(NewProduct);
        }

        public static Product GetProduct(string Name)
        {
            return ProductDAO.GetProduct(Name);
        }

        public static Product GetProduct(string Name, int ID)
        {
            return ProductDAO.GetProduct(Name, ID);
        }


        public static void UpdateMarket(int MarketID, string Name, string Adress)
        {
            MarketDAO.UpdateMarket(MarketID, Name, Adress);
        }

        public static void UpdateProduct(string Name, int ShopID, int NewCount, double NewCost)
        {
            ProductDAO.UpdateProduct(Name, ShopID, NewCount, NewCost);
        }

        public static Product SortByCostOneProduct(string Name)
        {
            return ProductDAO.SortByCostsOneProduct(Name);
        }
    }
}
