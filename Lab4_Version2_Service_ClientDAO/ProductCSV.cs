using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Service_ClientDAO
{
    public class ProductCSV : IProduct
    {
        public string Path { get; private set; }
        public List<Product> Products { get; private set; }

        public ProductCSV(string Path)
        {
            this.Path = Path.Trim();
        }

        private static Product Piece(string str)
        {
            try
            {
                string[] pieces = str.Split(';');
                int[] marketsID = pieces[1].Split(',').Select(int.Parse).ToArray();
                int[] counts = pieces[2].Split(',').Select(int.Parse).ToArray();
                string[] costsSTr = pieces[3].Split(',');
                for (int i = 0; i < costsSTr.Count(); i++)
                {
                    costsSTr[i] = costsSTr[i].Replace('.', ',');
                }
                double[] costs = costsSTr.Select(double.Parse).ToArray();
                return new Product(pieces[0].Trim(), marketsID.ToList(), counts.ToList(), costs.ToList());
            }
            catch (IndexOutOfRangeException)
            {
                throw new IndexOutOfRangeException($"Во входном файле market.csv ошибка в количестве входных данных!");
            }
        }

        public void Connect()
        {
            this.Products = new List<Product>();
            string[] patrs;
            try
            {
                patrs = File.ReadAllLines(this.Path);
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException($"Файл {Path} не найден!");
            }

            foreach (var s in patrs)
            {
                Products.Add(Piece(s));
            }
        }

        public static void Swap<T>(IList<T> list, int indexA, int indexB)
        {
            T tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
        }

        public Product SortByCostsOneProduct(string ProductName)
        {
            this.Connect();
            int index;
            Product request;
            foreach (var p in this.GetAllProducts())
            {
                if (p.Name == ProductName)
                {
                    index = Products.IndexOf(p);
                    request = new Product(Products[index]);
                    for (int i = 0; i < request.Cost.Count; i++)
                    {
                        for (int j = request.Cost.Count - 1; j > i; j--)
                        {
                            if (request.Cost[j - 1] > request.Cost[j])
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
            this.SaveChanges();
            return null;
        }

        public void DeleteProductFromMarket(int MarketID, string Name)
        {
            this.Connect();
            bool flag = true;
            for (var i = 0; i < Products.Count; i++)
            {
                var product = Products[i];
                if (product.ShopID.Contains(MarketID) && product.Name == Name)
                {
                    flag = false;
                    if (product.ShopID.Count() == 1)
                    {
                        Products.Remove(product);
                    }
                    else
                    {
                        int index = product.ShopID.IndexOf(MarketID);
                        product.Count.Remove(product.Count[index]);
                        product.Cost.Remove(product.Cost[index]);
                        product.ShopID.Remove(product.ShopID[index]);
                    }
                }
            }
            if (flag)
                Console.WriteLine($"Продукт {Name} в магазине с Id {MarketID} не существовал");
            this.SaveChanges();
        }

        public void DeleteProduct(string Name)
        {
            this.Connect();
            bool flag = true;
            for (var i = 0; i < Products.Count; i++)
            {
                var product = Products[i];
                if (product.Name == Name)
                {
                    Products.Remove(product);
                    flag = false;
                }
            }
            if (flag)
                Console.WriteLine($"Продукт {Name} не существовал");
            this.SaveChanges();
        }
        public List<Product> GetAllProducts()
        {
            this.Connect();
            return this.Products;
        }

        public List<Product> GetAllProductsInShop(int ID)
        {
            this.Connect();
            bool flag = true;
            List<Product> prodList = new List<Product>();
            foreach (var p in Products)
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
            this.SaveChanges();
            return prodList;
        }

        public void MakeProduct(Product NewProduct)
        {
            this.Connect();
            bool flag = true;
            foreach (var s in Products)
                if (s.Name == NewProduct.Name )
                    flag = false;
            if (flag)
                Products.Add(NewProduct);
            else
                Console.WriteLine($"Продукт с именем {NewProduct.Name} уже существует");
            this.SaveChanges();
        }

        public void UpdateProduct(string Name, int ShopID, int NewCount, double NewCost)
        {
            this.Connect();
            bool flag = true;
            foreach (var p in Products)
            {
                if (p.Name == Name)
                {
                    flag = false;
                    if (p.ShopID.Contains(ShopID))
                    {
                        int poss = p.ShopID.IndexOf(ShopID);
                        p.Count[poss] = NewCount;
                        p.Cost[poss] = NewCost;
                    }
                    else
                    {
                        p.ShopID.Add(ShopID);

                        p.Count.Add(NewCount);
                        p.Cost.Add(NewCost);

                    }
                }
            }
            if (flag)
                Console.WriteLine($"Продукт с именем {Name} не существует");
            this.SaveChanges();
        }


        public Product GetProduct(string Name)
        {
            this.Connect();
            bool flag = true;
            foreach (var p in Products)
            {
                if (p.Name == Name)
                {
                    flag = false;
                    return p;
                }
            }
            if (flag)
                Console.WriteLine($"Товара с названием {Name} не существует");
            this.SaveChanges();
            return null;
        }

        public Product GetProduct(string Name, int ID)
        {
            this.Connect();
            bool flag = true;
            foreach (var p in Products)
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
            this.SaveChanges();
            return null;
        }

        public void SaveChanges()
        {
            string st = "";
            foreach (var s in Products)
            {
                st = st + s.ToString() + '\n';
            }
            File.WriteAllText(Path, st);
        }
    }
}
