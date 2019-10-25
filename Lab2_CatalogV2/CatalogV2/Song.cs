using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogV2
{
    class Song
    {
        private Genre g;
        private string name;
        private Artist artist;
        private Int32 year;

        public Song (Genre genre, string songsName,  Artist artistName = null, Int32 releasedAt = 0)
        {
            this.artist = artistName;
            this.g = genre;
            this.name = songsName;
            this.year = releasedAt;
        }

        public override string ToString()
        {
            return String.Format($"{this.name} | {this.artist} | {this.year}");
        }

        public Genre Genre()
        {
            return this.g;
        }

        public string StrGenre()
        {
            return this.g.ToString();
        }

        public string Name()
        {
            return this.name;
        }

        public string Artist()
        {
            return this.artist.ToString();
        }

        public Int32 Year()
        {
            return this.year;
        }
    }
}
