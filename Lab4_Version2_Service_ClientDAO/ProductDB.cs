using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;

namespace Service_ClientDAO
{
    class ProductDB : IProduct
    {
        public string Path { get; private set; }
        private DataContext db;

        public ProductDB(string path)
        {
            this.Path = path;
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=" + path + ";Integrated Security=True";
            this.db = new DataContext(connectionString);
        }

        public void DeleteProduct(string Name)
        {
            var temp = from deletedProduct in db.GetTable<ProductForDB>()
                       where deletedProduct.Name == Name
                       select deletedProduct;
            foreach (var dp in temp)
            {
                db.GetTable<ProductForDB>().DeleteOnSubmit(dp);
            }
            db.SubmitChanges();
        }

        public void DeleteProductFromMarket(int ID, string Name)
        {
            var temp = from deletedProduct in db.GetTable<ProductForDB>()
                       where deletedProduct.Name == Name && deletedProduct.ShopID == ID
                       select deletedProduct;
            foreach (var dp in temp)
            {
                db.GetTable<ProductForDB>().DeleteOnSubmit(dp);
            }
            db.SubmitChanges();
        }

        public List<Product> GetAllProducts()
        {
            List<Product> request = new List<Product>();
            var temp = from product in db.GetTable<ProductForDB>()
                       orderby product.Name
                       select product;
            var t = temp.ToList();
            try
            {
                List<int> Counts = new List<int>() { t[0].Count };
                List<int> ShopIds = new List<int>() { t[0].ShopID };
                List<double> Costs = new List<double>() { t[0].Cost };
                for (int i = 1; i < t.Count(); i++)
                {
                    if (t[i - 1].Name == t[i].Name)
                    {
                        Counts.Add(t[i].Count);
                        ShopIds.Add(t[i].ShopID);
                        Costs.Add(t[i].Cost);
                    }
                    else
                    {
                        request.Add(new Product(t[i - 1].Name, ShopIds, Counts, Costs));
                        Counts = new List<int>() { t[i].Count };
                        ShopIds = new List<int>() { t[i].ShopID };
                        Costs = new List<double>() { t[i].Cost };
                    }
                }
                request.Add(new Product(t[t.Count() - 1].Name, ShopIds, Counts, Costs));
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new ArgumentOutOfRangeException("В таблице базы данных ProductDB нет ни одной записи!");
            }
            return request;
        }

        public List<Product> GetAllProductsInShop(int ID)
        {
            bool flag = true;
            List<Product> prodList = new List<Product>();
            foreach (var p in this.GetAllProducts())
            {
                if (p.ShopID.Contains(ID))
                {
                    flag = false;
                    int i = p.ShopID.IndexOf(ID);
                    prodList.Add(new Product(p.Name, new List<int> { p.ShopID[i] }, new List<int> { p.Count[i] }, new List<double> { p.Cost[i] }));
                }
            }
            if (flag)
                Console.WriteLine($"Магазин с Id {ID} не существует");
            return prodList;
        }

        public Product GetProduct(string Name)
        {
            bool flag = true;
            foreach (var p in this.GetAllProducts())
            {
                if (p.Name == Name)
                {
                    flag = false;
                    return p;
                }
            }
            if (flag)
                Console.WriteLine($"Товара с названием {Name} не существует");
            return null;
        }

        public Product GetProduct(string Name, int ID)
        {
            bool flag = true;
            foreach (var p in this.GetAllProducts())
            {
                if (p.Name == Name)
                    for (int i = 0; i < p.ShopID.Count(); i++)
                    {
                        if (p.ShopID[i] == ID)
                        {
                            flag = false;
                            return new Product(p.Name, new List<int> { ID }, new List<int> { p.Count[i] }, new List<double> { p.Cost[i] });
                        }
                    }
            }
            if (flag)
                Console.WriteLine($"Товара с названием {Name} в магазине {ID} не существует");
            return null;
        }

        public void MakeProduct(Product NewProduct)
        {
            var temp = from t in db.GetTable<MarketForDB>()
                       where NewProduct.ShopID.Contains(t.Id)
                       select t;
            foreach (var t in temp)
                Console.WriteLine(t);
            if (temp.Count() == NewProduct.ShopID.Count())
                for (int i = 0; i < NewProduct.Cost.Count(); i++)
                {
                    db.GetTable<ProductForDB>().InsertOnSubmit(new ProductForDB { Name = NewProduct.Name, Count = NewProduct.Count[i], Cost = NewProduct.Cost[i], ShopID = NewProduct.ShopID[i] });
                }
            else
                throw new ArgumentNullException("Магазина с заданым ID не существует. Товар не был создан!");
            db.SubmitChanges();
        }

        public static void Swap<T>(IList<T> list, int indexA, int indexB)
        {
            T tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
        }

        public Product SortByCostsOneProduct(string ProductName)
        {
            int index;
            Product request;
            var t = this.GetAllProducts();
            foreach (var p in t)
            {
                if (p.Name == ProductName)
                {
                    index = t.IndexOf(p);
                    request = new Product(t[index]);
                    for (int i = 0; i < request.Cost.Count; i++)
                    {
                        for (int j = request.Cost.Count - 1; j > i; j--)
                        {
                            if (request.Cost[i] > request.Cost[j])
                            {
                                Swap<double>(request.Cost, i, j);
                                Swap<int>(request.Count, i, j);
                                Swap<int>(request.ShopID, i, j);
                            }
                        }
                    }
                    return request;
                }
            }
            return null;
        }

        public void UpdateProduct(string Name, int ShopID, int NewCount, double NewCost)
        {

            var temp = from product in db.GetTable<ProductForDB>()
                       where product.Name == Name && product.ShopID == ShopID
                       select product;
            foreach (var t in temp)
            {
                t.Count = NewCount;
                t.Cost = NewCost;
            }
            db.SubmitChanges();
        }
    }
}
