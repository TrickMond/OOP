using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogV2
{
    class Cotalog
    {
        private List<Album> albums;
        private List<Compilation> cmps;

        public Cotalog(List<Album> albums = null, List<Compilation> cmps = null)
        {
            this.albums = albums;
            this.cmps = cmps;
        }

        public void Show()
        {
            foreach (var a in albums)
            {
                Console.WriteLine(a.ToString() + "\n");
            }
            foreach (var c in cmps)
            {
                Console.WriteLine(c.ToString() + "\n");
            }
        }

        public List<Song> Artist(string art)
        {
            List<Song> songs = new List<Song>();
            foreach (var a in this.albums)
            {
                if (a.Artist() == art)
                {
                    songs.AddRange(a.SongList());
                }
            }
            for (int i = 0; i < cmps.Count; i++)
            {
                List<Song> so;
                for (int j = 0; j < cmps[i].Count(); j++)
                {
                    so = cmps[i].SongList();
                    if (so[j].Artist() == art && !songs.Contains(so[j]))
                    {
                        songs.Add(so[j]);
                    }
                }
            }
            return songs;
        }

        public List<Song> Year(Int32 year)
        {
            List<Song> songs = new List<Song>();
            foreach (var a in this.albums)
            {
                if (a.Year() == year)
                {
                    songs.AddRange(a.SongList());
                }
            }
            for (int i = 0; i < cmps.Count; i++)
            {
                List<Song> so;
                for (int j = 0; j < cmps[i].Count(); j++)
                {
                    so = cmps[i].SongList();
                    if (so[j].Year() == year && !songs.Contains(so[j]))
                    {
                        songs.Add(so[j]);
                    }
                }
            }
            return songs;
        }

        public List<Song> Genre(string gen)
        {
            List<Song> songs = new List<Song>();
            foreach (var a in this.albums)
            {
                if (a.Genre().IsRelative(gen))
                {
                    songs.AddRange(a.SongList());
                }
            }
            for (int i = 0; i < cmps.Count; i++)
            {
                List<Song> so;
                for (int j = 0; j < cmps[i].Count(); j++)
                {
                    so = cmps[i].SongList();
                    if (so[j].Genre().IsRelative(gen) && !songs.Contains(so[j]))
                    {
                        songs.Add(so[j]);
                    }
                }
            }
            return songs;
        }



        public Tuple<List<Album>, List<Compilation>> Name(string name)
        {
            List<Album> albu = new List<Album>();
            List<Compilation> cm = new List<Compilation>();
            string n = name.ToUpper();
            foreach (var alb in albums)
            {
                if (alb.Name().ToUpper().Contains(n))
                {
                    albu.Add(alb);
                }
            }
            foreach (var cmp in cmps)
            {
                if (cmp.Name().ToUpper().Contains(n))
                {
                    cm.Add(cmp);
                }
            }
            return Tuple.Create(albu, cm);
        }
        public List<Song> GenreAndYear(string g, Int32 y)
        {
            List<Song> req = new List<Song>();
            var listG = this.Genre(g);
            var listY = this.Year(y);
            foreach (var ge in listG)
                if (ge.Year() == y)
                    req.Add(ge);
            foreach (var ye in listY)
                if (ye.Genre().ToString() == g)
                    req.Add(ye);
            return (req);
        }
    }
}
