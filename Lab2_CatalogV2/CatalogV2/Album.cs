using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogV2
{
    class Album : Compilation
    {
        private string artist;
        private Int32 year;
        private Genre genre;
        
        public Album (Song song, string albumName)
            : base (song, albumName)
        {
            this.artist = song.Artist();
            this.year = song.Year();
            this.genre = song.Genre();
        }

        public Album (List<Song> songs, string albumName)
            : base(songs, albumName)
        {
            this.artist = songs[songs.Count - 1].Artist();
            this.year = songs[songs.Count - 1].Year();
            this.genre = songs[songs.Count - 1].Genre();
        }

        public override string ToString()
        {
            string str = String.Format($"А: {this.name} | И: {this.artist} | Г: {this.year} | Ж: {this.genre.ToString()}");
            foreach (Song song in this.songs)
            {
                str = str + "\n\t" + song.Name();
            }
            return str;
        }


        public Int32 Year()
        {
            return this.year;
        }

        public string Artist()
        {
            return this.artist;
        }

        public Genre Genre()
        {
            return this.genre;
        }

    }
}
