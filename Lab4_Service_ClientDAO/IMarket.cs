using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_ClientDAO
{
    interface IMarket
    {
        List<Market> GetAllMarkets();
        void MakeMarket(Market NewMarket);
        void UpdateMarket(int MarketID, string Name, string Adresss);
        void DeleteMarket(int MarketID);
        void SaveChanges();
    }
}
