using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogV2
{
    class Program
    {
        static void Main(string[] args)
        {
            //Жанры
            Genre pop = new Genre("Pop");
            Genre kPop = new Genre("K-Pop", "Pop");

            //Исполнители
            var art1 = new Artist("Mabel");
            var art2 = new Artist("Dubee");

            //Альбомы
            Song s1 = new Song(pop, "Just A Friend", art1, 2019);
            Song s2 = new Song(pop, "OK(Anxiety Anthem)", art1, 2019);
            Song s3 = new Song(pop, "Mad Love", art1, 2019);

            Album a1 = new Album(new List<Song>() { s1, s2, s3 }, "Stripped Session");

            Song s4 = new Song(kPop, "Purpose", art2, 2018);
            Song s5 = new Song(kPop, "Butterfly", art2, 2018);

            Album a2 = new Album(new List<Song>() { s4, s5 }, "Day N Night");

            //Сборники
            Song s6 = new Song(pop, "XXXXXXXX", art1, 2018);

            Compilation c1 = new Compilation(new List<Song>() { s1, s4, s6 }, "Compilation by Day");

            Cotalog c = new Cotalog(new List<Album>() { a1, a2 }, new List<Compilation>() { c1 });
            c.Show();

            string artist = "Mabel";
            Int32 year = 2018;
            string gen = "K-Pop";
            string name = "day";


            //Запросы

            var req1 = c.Artist(artist);

            var req2 = c.Year(year);

            var req3 = c.Genre(gen);

            var req4 = c.Name(name);

            var req5 = c.GenreAndYear("Pop", 2018);

            Console.WriteLine('\n'+artist);
            foreach (var s in req1)
            {
                Console.WriteLine(s.ToString());
            }
            Console.WriteLine('\n'+year.ToString());
            foreach (var s in req2)
            {
                Console.WriteLine(s.ToString());
            }
            Console.WriteLine('\n'+gen);
            foreach (var s in req3)
            {
                Console.WriteLine(s.ToString());
            }
            Console.WriteLine('\n' + name);
            foreach (var s in req4.Item1)
            {
                Console.WriteLine(s.ToString());
            }
            foreach (var s in req4.Item2)
            {
                Console.WriteLine(s.ToString());
            }
            Console.WriteLine('\n' + gen + " " + year);
            foreach (var s in req3)
            {
                Console.WriteLine(s.ToString());
            }
            Console.ReadLine();
        }
    }
}
