using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_ClientDAO
{
    public class Product
    {
        public string Name { get; set; }
        public List<int> ShopID { get; set; }
        public List<int> Count { get; set; }
        public List<double> Cost { get; set; }

        public Product(string Name, List<int> ShopID, List<int> Count, List<double> Cost)
        {
            this.Name = Name;
            this.ShopID = ShopID;
            this.Count = Count;
            this.Cost = Cost;
        }

        public Product (string Name, int ShopID, int Count, double Cost)
        {
            this.Name = Name;
            this.ShopID = new List<int>() { ShopID };
            this.Count = new List<int>() { Count };
            this.Cost = new List<double>() { Cost };
        }

        public Product(Product copyProduct)
        {
            this.Name = copyProduct.Name;
            this.ShopID = copyProduct.ShopID;
            this.Count = copyProduct.Count;
            this.Cost = copyProduct.Cost;
        }

        public override string ToString()
        {
            string strCount = "", strCost = "", strID = "";
            for (int i = 0; i < this.Count.Count(); i++)
            {
                if (this.Count.Count() - 1 == i)
                {
                    strCount = strCount + this.Count[i];
                    strCost = strCost + this.Cost[i];
                    strID = strID + this.ShopID[i];
                }
                else
                {
                    strCount = strCount + this.Count[i] + " , ";
                    strCost = strCost + this.Cost[i] + " , ";
                    strID = strID + this.ShopID[i] + " , ";
                }
            }
            return String.Format($"{Name};{strID};{strCount};{strCost}");
        }
    }
}
