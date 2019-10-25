using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogV2
{
    class Compilation
    {
        protected List<Song> songs;
        protected string name;

        public Compilation(Song song, string compilationName)
        {
            this.songs = new List<Song>() { song };
            this.name = compilationName;
        }

        public Compilation(List<Song> songs, string compilationName)
        {
            this.songs = songs;
            this.name = compilationName;
        }

        public string Name()
        {
            return this.name;
        }

        public List<Song> SongList()
        {
            return this.songs;
        }

        public override string ToString()
        {
            string str = String.Format($"Сборник: {this.name}");
            foreach (Song song in this.songs)
            {
                str = str + "\n\t" + String.Format($"Н: {song.Name()} | И: {song.Artist()} | Г: {song.Year().ToString()} | Ж: {song.Genre().ToString()}");
            }
            return str;
        }

        public Int32 Count()
        {
            return this.songs.Count;
        }
    }
}
