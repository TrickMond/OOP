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
            
            Console.WriteLine(Manager.WhereTheCheapestProducts(new List<string> { "Fanta", "Chocolate Alenka" }, new List<int> { 1, 1000 }));
        }
    }
}
