using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_ClientDAO
{
    interface IProduct
    {
        List<Product> GetAllProducts();
        void MakeProduct(Product NewProduct);
        void UpdateProduct(string Name, int ShopID);
        void UpdateProduct(string Name, int ShopID, int NewCount, double NewCost);
        void DeleteProductFromMarket(int ID, string Name);
        void DeleteProduct(string Name);
        List<Product> GetAllProductsInShop(int ID);
        Product GetProduct(string Name);
        Product SortByCostsOneProduct(string ProductName);
    }
}
