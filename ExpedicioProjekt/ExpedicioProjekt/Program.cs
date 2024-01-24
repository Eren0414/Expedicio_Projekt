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
            string inputFile = "veetel.txt";
            string outputFile = "veetelMegfejtett.txt";

            string[] lines = File.ReadAllLines(inputFile, Encoding.UTF8);

            var napok = new Dictionary<int, List<string>>();

            foreach (var line in lines)
            {
                string[] lineParts = line.Split();

                // Ellenőrzés a sor hosszára
                if (lineParts.Length >= 3)
                {
                    // Csak a számokat tartjuk meg az első oszlopban
                    string numericPart = new string(lineParts[0].Where(c => char.IsDigit(c)).ToArray());

                    // Ellenőrzés az első oszlop formátumára
                    if (int.TryParse(numericPart, out int napSzama))
                    {
                        string uzenet = lineParts[2];

                        if (!napok.ContainsKey(napSzama))
                        {
                            napok[napSzama] = new List<string>();
                        }

                        napok[napSzama].Add(uzenet);
                    }
                    else
                    {
                        Console.WriteLine($"Hiba a sorban: Az első oszlop nem érvényes szám.");
                    }
                }
                else
                {
                    Console.WriteLine($"Hiba a sorban: Nem megfelelő formátum.");
                }
            }

            using (StreamWriter writer = new StreamWriter(outputFile, false, Encoding.UTF8))
            {
                foreach (var nap in napok)
                {
                    int napSzama = nap.Key;
                    List<string> uzenetek = nap.Value;

                    // Összefésüljük az azonos napszámú üzeneteket
                    string egyesitettUzenet = OsszefesuldAzonosNapUzeneteit(uzenetek);

                    // Hiányzó betűk behelyettesítése
                    string visszafejtettUzenet = EgeszitsdKiUzenetet(egyesitettUzenet);

                    // Az üzenet utáni részt levágjuk
                    int indexOfDollar = visszafejtettUzenet.IndexOf('$');
                    if (indexOfDollar != -1)
                    {
                        visszafejtettUzenet = visszafejtettUzenet.Substring(0, indexOfDollar);
                    }

                    // Ellenőrzés, hogy az üzenet üres-e
                    if (!string.IsNullOrEmpty(visszafejtettUzenet))
                    {
                        writer.WriteLine($"{napSzama};{visszafejtettUzenet}");
                    }
                }
            }

            Console.WriteLine("A visszafejtett üzenetek el lettek mentve a veetelMegfejtett.txt fájlba.");

            Console.ReadLine();
        }

        static string EgeszitsdKiUzenetet(string uzenet)
        {
            StringBuilder kiegeszitettUzenet = new StringBuilder();
            int hianyzoIndex = 0;

            for (int i = 0; i < uzenet.Length; i++)
            {
                if (uzenet[i] == '#')
                {
                    // Hiányzó betűk behelyettesítése
                    if (hianyzoIndex < uzenet.Length)
                    {
                        kiegeszitettUzenet.Append(uzenet[hianyzoIndex]);
                        hianyzoIndex++;
                    }
                }
                else
                {
                    // Egyéb karakterek egyszerű másolása
                    kiegeszitettUzenet.Append(uzenet[i]);
                }
            }

            return kiegeszitettUzenet.ToString();
        }

        static string OsszefesuldAzonosNapUzeneteit(List<string> uzenetek)
        {
            StringBuilder egyesitettUzenet = new StringBuilder();

            // A leghosszabb üzenet hosszáig iterálunk
            for (int i = 0; i < uzenetek.Max(u => u.Length); i++)
            {
                // Az összes nap uzenetének i. karakterét veszi
                char karakter = uzenetek.Where(u => i < u.Length).Select(u => u[i]).FirstOrDefault();

                // Az egyesített üzenethez hozzáadjuk a karaktert
                egyesitettUzenet.Append(karakter);
            }

            return egyesitettUzenet.ToString();
        }
    }
}

