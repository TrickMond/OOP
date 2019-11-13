using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Service_ClientDAO
{
    public class MarketCSV : IMarket
    {
        public string Path { get; private set; }
        public List<Market> Markets { get; private set; }

        public MarketCSV(string CSVsPath)
        {
            this.Path = CSVsPath.Trim();
        }

        private static Market Piece(string str)
        {
            string[] pieces = str.Split(';');
            try
            {
                return new Market(Convert.ToInt32(pieces[0].Trim()), pieces[1].Trim(), pieces[2].Trim());
            }
            catch (IndexOutOfRangeException)
            {
                throw new IndexOutOfRangeException($"Во входном файле market.csv ошибка входных данных!");
            }
        }

        public void Connect()
        {
            this.Markets = new List<Market>();
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
                Markets.Add(Piece(s));
            }
        }


        public void DeleteMarket(int MarketID)
        {
            this.Connect();
            bool flag = true;
            for (var i = 0; i < Markets.Count; i++)
            {
                var m = Markets[i];
                if (m.ID == MarketID)
                {
                    flag = false;
                    Markets.Remove(m);
                }
            }
            if (flag)
                Console.WriteLine($"Магазина с {MarketID} не сущевствует");
            this.SaveChanges();
        }

        public List<Market> GetAllMarkets()
        {
            this.Connect();
            return this.Markets;
        }

        public void MakeMarket(Market NewMarket)
        {
            this.Connect();
            bool flag = true;
            foreach (var v in Markets)
                if (v.ID == NewMarket.ID)
                    flag = false;
            if (flag)
            {
                Markets.Add(NewMarket);
                this.SaveChanges();
            }
            else
                Console.WriteLine($"Магазин с ID {NewMarket.ID} уже сущевствует. Магазин {NewMarket.Name} создан не был");
        }

        public void UpdateMarket(int MarketID, string Name = "" , string Adress = "")
        {
            this.Connect();
            foreach (var m in Markets)
            {
                if (m.ID == MarketID)
                {
                    if (m.Name != "")
                        m.Name = Name;
                    if (m.Adress != "")
                        m.Adress = Adress;
                }
            }
            this.SaveChanges();
        }

        public void SaveChanges()
        {
            string st = "";
            foreach (var s in Markets)
            {
                st = st + s.ToString() + '\n';
            }
            File.WriteAllText(Path, st);
        }
    }
}
