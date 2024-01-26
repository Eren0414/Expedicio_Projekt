using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpedicioProjekt
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string fajl = "veetel.txt";

            Radio r = new Radio(fajl);

            r.Statisztika();

            Console.WriteLine("A megfejtett üzenetet a 'veetelMegfejtett.txt' fájlban találod.");
            Console.WriteLine("Statisztika kiírva a 'napiStatisztika.txt' fájlba.");


            Console.ReadLine();
        }
    }
}

