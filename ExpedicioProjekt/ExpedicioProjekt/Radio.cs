using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpedicioProjekt
{
    internal class Radio
    {
        private string fajl;
        List<Amator> amatorok = new List<Amator>();

        public Radio(string fajl)
        {
            this.fajl = fajl;

            Beolvas();
        }

        internal void Megfejt()
        {
            var napok =
                from amator in amatorok
                group amator by amator.nap into groups
                orderby groups.Key
                select new { nap = groups.Key };

            List<string> megfejtettuzenetek = new List<string>();
            foreach (var nap in napok)
            {
                var aznapiuzenetek = amatorok.Where(a => a.nap == nap.nap).ToList();
                Console.WriteLine("\n{0}", nap.nap);
                string megfejtettuzenet = "";
                string elsouzenet = aznapiuzenetek[0].uzenet;
                for (int i = 0; i < elsouzenet.Length; i++)
                {
                    char karakter = elsouzenet[i];
                    megfejtettuzenet += Megfejtes(karakter, aznapiuzenetek, i);

                }
                megfejtettuzenetek.Add(megfejtettuzenet);
            }
            foreach (var uzenet in megfejtettuzenetek)
            {
                Console.WriteLine(uzenet);
            }
        }

        private string Megfejtes(char karakter, List<Amator> aznapiuzenetek, int i)
        {
            string vissza = "#";
            if (karakter != '#')
            {
                vissza = karakter.ToString();
            }
            else
            {
                for (int j = 1; j < aznapiuzenetek.Count; j++)
                {
                    if (aznapiuzenetek[j].uzenet[i] != '#')
                    {
                        vissza = aznapiuzenetek[j].uzenet[i].ToString();
                        break;
                    }
                }
            }
            return vissza;
        }

        private void Beolvas()
        {
            string[] sorok = File.ReadAllLines(fajl);
            int nap = 0;
            int amator = 0;

            for (int i = 0; i < sorok.Length; i++)
            {
                int mutato = i % 2;
                if (mutato == 0)
                {
                    string[] oszlopok = sorok[i].Split(' ');
                    nap = int.Parse(oszlopok[0]);
                    amator = int.Parse(oszlopok[1]);
                }
                else
                {
                    string uzenet = sorok[i];
                    amatorok.Add(new Amator(nap, amator, uzenet));
                }
            }
        }
    }
}
