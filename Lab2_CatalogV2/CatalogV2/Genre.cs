using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogV2
{
    class Genre
    {
        private string name;
        private string parent;

        public Genre (string genreName, string parentsGenresName = null)
        {
            this.name = genreName;
            this.parent = parentsGenresName;
        }

        public override string ToString()
        {
            return this.name;
        }

        public bool IsRelative(string parentName)
        {
            return (this.parent == parentName || this.name == parentName);
        }

        public bool IsRelative(Genre g2)
        {
            return (this.parent == g2.parent && g2.parent != null || this.name == g2.parent || this.parent == g2.name);
        }
    }
}
