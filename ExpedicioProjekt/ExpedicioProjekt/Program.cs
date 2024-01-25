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

            var napok = new List<Amatorok>();

            foreach (var line in lines)
            {
                // Csak a számokat és a speciális karaktereket hagyjuk meg
                string numericAndSpecialChars = new string(line.Where(c => char.IsDigit(c) || IsSpecialCharacter(c)).ToArray());

                // Ellenőrzés az első oszlop formátumára
                if (!string.IsNullOrEmpty(numericAndSpecialChars) && int.TryParse(numericAndSpecialChars, out int napSzama))
                {
                    string[] lineParts = line.Split();

                    // Ellenőrzés a sor hosszára
                    if (lineParts.Length >= 3)
                    {
                        string visszafejtettUzenet = Radio.DecodeVeetelMessage(lineParts[2]);

                        if (!string.IsNullOrEmpty(visszafejtettUzenet))
                        {
                            napok.Add(new Amatorok { NapSzama = napSzama, VisszafejtettUzenet = visszafejtettUzenet });
                        }
                        else
                        {
                            Console.WriteLine($"Hiba a sorban: Nem sikerült visszafejteni az üzenetet.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Hiba a sorban: Nem megfelelő formátum.");
                    }
                }
                else
                {
                    Console.WriteLine($"Hiba a sorban: Az első oszlop nem érvényes szám.");
                }
            }

            using (StreamWriter writer = new StreamWriter(outputFile, false, Encoding.UTF8))
            {
                foreach (var nap in napok)
                {
                    writer.WriteLine($"{nap.NapSzama};{nap.VisszafejtettUzenet}");
                }
            }

            Console.WriteLine("A visszafejtett üzenetek el lettek mentve a veetelMegfejtett.txt fájlba.");
            Console.ReadLine();
        }

        static bool IsSpecialCharacter(char c)
        {
            // Itt hozzáadhatsz más karaktereket, ha szükséges
            return c == '#' || c == '$';
        }
    }
}

