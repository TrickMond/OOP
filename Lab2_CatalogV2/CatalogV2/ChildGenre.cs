using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogV2
{
    class ChildGenre
    {
        public Genre ParentGenre { get; protected set; }
        public string Name { get; protected set; }
        
        public ChildGenre(string genreName, Genre parent)
        {
            this.Name = genreName;
            this.ParentGenre = parent;
        }

    }
}
