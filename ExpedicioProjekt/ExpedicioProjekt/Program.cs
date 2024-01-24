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

            var napok = new Dictionary<int, StringBuilder>();

            foreach (var line in lines)
            {
                string[] parts = line.Split(' ');

                if (parts.Length >= 3 && int.TryParse(parts[0], out int napSzama))
                {
                    string decryptedMessage = Radio.DecodeVeetelMessage(parts[2]);

                    if (napok.ContainsKey(napSzama))
                    {
                        // Azonos nap esetén az üzenetet hozzáadjuk a már meglévőhöz
                        napok[napSzama].Append(decryptedMessage);
                    }
                    else
                    {
                        // Új nap esetén létrehozzuk a nap üzenetét
                        napok.Add(napSzama, new StringBuilder(decryptedMessage));
                    }
                }
                else
                {
                    Console.WriteLine($"Hiba a sorban: Nem megfelelő formátum.");
                }
            }

            using (StreamWriter writer = new StreamWriter(outputFile, false, Encoding.UTF8))
            {
                foreach (var kvp in napok)
                {
                    // A helyes sorrendben írjuk ki a napokat és azokat az üzeneteket, amiket összefűztünk
                    writer.WriteLine($"{kvp.Key} {kvp.Value}");
                }
            }

            Console.WriteLine("A visszafejtett üzenetek el lettek mentve a veetelMegfejtett.txt fájlba.");
            Console.ReadLine();
        }
    }        
}

