using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

            using (StreamWriter writer = new StreamWriter("veetelMegfejtett.txt"))
            {
                for (int i = 0; i < megfejtettuzenetek.Count; i++)
                {
                    string farkasOutput = (megfejtettuzenetek[i].Contains("/") ? megfejtettuzenetek[i].Split('/')[0] + "/" + megfejtettuzenetek[i].Split('/')[1] : "");
                    writer.WriteLine($"{i + 1}: {megfejtettuzenetek[i]} {farkasOutput}");
                }
            }
        }

        private static string Megfejtes(char karakter, List<Amator> aznapiuzenetek, int i)
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

        internal void Statisztika()
        {
            var napok =
                from amator in amatorok
                group amator by amator.nap into groups
                orderby groups.Key
                select new { nap = groups.Key, Amatorok = groups.ToList() };

            using (StreamWriter writer = new StreamWriter("napiStatisztika.txt"))
            {
                foreach (var nap in napok)
                {
                    int amatorokSzama = nap.Amatorok.Count;
                    int felnottFarkas = 0;
                    int gyerekFarkas = 0;

                    foreach (var amator in nap.Amatorok)
                    {
                        Match match = Regex.Match(amator.uzenet, @"(\d+)/(\d+)");

                        if (match.Success)
                        {
                            string x = match.Value;
                            felnottFarkas = int.Parse(match.Groups[1].Value);
                            gyerekFarkas = int.Parse(match.Groups[2].Value);
                        }
                    }

                    string farkasOutput = $"{(felnottFarkas > 0 ? felnottFarkas.ToString() : "-")};{(gyerekFarkas > 0 ? gyerekFarkas.ToString() : "-")}";

                    writer.WriteLine($"{nap.nap};{amatorokSzama};{farkasOutput}");
                }
            }
        }
    }
}
