using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INIReaderV2
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser p = new Parser("simple.txt");
            var s = p.ToSections();
            var st = p.GetValue("int", "StatisterTimeMs", "COMMON");
            foreach (var str in s)
            {
                Console.WriteLine(str);
            }
            Console.WriteLine(st);
        }
    }
}
