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

            try
            {
                string[] lines = File.ReadAllLines(inputFile, Encoding.UTF8);

                var napok = new Amatorok[lines.Length];

                for (int i = 0; i < lines.Length; i++)
                {
                    string[] lineParts = lines[i].Split();

                    // Ellenőrzés a sor hosszára
                    if (lineParts.Length >= 3)
                    {
                        if (int.TryParse(lineParts[0], out int napSzama))
                        {
                            string eredetiUzenet = lineParts[2];

                            napok[i] = new Amatorok { NapSzama = napSzama, EredetiUzenet = eredetiUzenet };

                            // Ellenőrzés, hogy a VisszafejtettUzenet ne legyen null
                            napok[i].VisszafejtettUzenet = Radio.DecodeVeetelMessage(lines[i]) ?? string.Empty;
                        }
                        else
                        {
                            Console.WriteLine($"Hiba a(z) {i + 1}. sorban: Az első oszlop nem érvényes szám.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Hiba a(z) {i + 1}. sorban: Nem megfelelő formátum.");
                    }
                }

                using (StreamWriter writer = new StreamWriter(outputFile, false, Encoding.UTF8))
                {
                    foreach (var nap in napok)
                    {
                        // Ellenőrzés, hogy az üzenet üres-e
                        if (!string.IsNullOrEmpty(nap.VisszafejtettUzenet))
                        {
                            writer.WriteLine($"{nap.NapSzama};{nap.VisszafejtettUzenet}");
                        }
                    }
                }

                Console.WriteLine("A visszafejtett üzenetek el lettek mentve a veetelMegfejtett.txt fájlba.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba történt: {ex.Message}");
            }

            Console.ReadLine();
        }
    }
    }
}
