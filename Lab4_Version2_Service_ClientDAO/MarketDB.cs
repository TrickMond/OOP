using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;

namespace Service_ClientDAO
{
    class MarketDB : IMarket
    {
        public string Path { get; private set; }
        private DataContext db;

        public MarketDB(string path)
        {
            this.Path = path;
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=" + path + ";Integrated Security=True";
            this.db = new DataContext(connectionString);
        }

        public void DeleteMarket(int MarketID)
        {
            var temp = from markets in db.GetTable<MarketForDB>()
                       where markets.Id == MarketID
                       select markets;
            foreach (var market in temp)
            {
                db.GetTable<MarketForDB>().DeleteOnSubmit(market);
            }
            db.SubmitChanges();
        }

        public List<Market> GetAllMarkets()
        {
            List<Market> request = new List<Market>();
            var temp = db.GetTable<MarketForDB>();
            foreach (var t in temp)
                request.Add(new Market (t.Id, t.Name, t.Address));
            return request;
        }

        public void MakeMarket(Market NewMarket)
        {
            db.GetTable<MarketForDB>().InsertOnSubmit(new MarketForDB { Name = NewMarket.Name, Address = NewMarket.Adress, Id = NewMarket.ID });
            db.SubmitChanges();
        }


        public void UpdateMarket(int MarketID, string Name, string Adresss)
        {
            var temp = from markets in db.GetTable<MarketForDB>()
                       where markets.Id == MarketID
                       select markets;
            foreach (var t in temp)
            {
                t.Name = Name;
                t.Address = Adresss;
            }
        }
    }
}
