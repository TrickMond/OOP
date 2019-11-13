using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_ClientDAO
{
    public class Market
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }

        public Market(int ID, string Name, string Adress)
        {
            this.ID = ID;
            this.Name = Name;
            this.Adress = Adress;
        }

        public override string ToString()
        {
            return String.Format($"{ID};{Name};{Adress}");
        }
    }
}
