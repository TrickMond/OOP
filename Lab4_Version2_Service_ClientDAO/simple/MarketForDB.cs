using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace Service_ClientDAO
{
    [Table(Name = "MarketForDB")]
    class MarketForDB
    {
        [Column(Name = "IdMarket", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        [Column(Name = "NameMarket")]
        public string Name { get; set; }
        [Column(Name = "AddressMarket")]
        public string Address { get; set; }
        public override string ToString()
        {
            return String.Format($"{Id};{Name};{Address}");
        }
    }
}
