using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace Service_ClientDAO
{
    [Table(Name = "ProductForDB")]
    class ProductForDB
    {
        [Column(Name = "NameProduct", IsPrimaryKey = true)]
        public string Name { get; set; }
        [Column(Name = "IdMarket", IsPrimaryKey = true)]
        public int ShopID { get; set; }
        [Column(Name = "CounstProduct")]
        public int Count { get; set; }
        [Column(Name = "CostProduct")]
        public double Cost { get; set; }

        public override string ToString()
        {
            return String.Format($"{Name};{ShopID};{Count};{Cost}");
        }
    }
}
