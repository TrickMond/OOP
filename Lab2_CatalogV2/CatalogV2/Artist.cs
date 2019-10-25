using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogV2
{
    class Artist
    {
        public string Name { get; protected  set; }
        public List<string> AdditionalData { get; protected set; }

        public Artist(string artistName)
        {
            this.Name = artistName;
            this.AdditionalData = new List<string>();
        }

        public Artist(string artistName, List<string> strs)
        {
            this.Name = artistName;
            this.AdditionalData = strs;
        }

        public override string ToString()
        {
            string str = String.Format($"{ Name }");
            if (AdditionalData.Count != 0)
            {
                str = str + "\n\t Дополнительная информация";
                foreach (var st in AdditionalData)
                {
                    str = str + "\n\t\t- st";
                }
            }
            return str;
        }
    }
}
