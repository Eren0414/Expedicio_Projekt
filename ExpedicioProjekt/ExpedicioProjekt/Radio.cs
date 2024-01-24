using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpedicioProjekt
{
    internal class Radio
    {
        public static string DecodeVeetelMessage(string encodedMessage)
        {
            string[] parts = encodedMessage.Split('#');

            // Ellenőrzés, hogy van-e "#" a szövegben
            if (parts.Length > 1)
            {
                // Az üzenet az első rész
                string message = parts[1];

                // A maradék részek a napok
                string[] days = parts.Skip(2).ToArray();

                foreach (string day in days)
                {
                    // A nap számának meghatározása
                    if (int.TryParse(day, out int dayNumber))
                    {
                        // Az üzenet kinyerése és eltávolítása a nap részből
                        string[] dayParts = day.Split('$');
                        string decryptedMessage = dayParts[0];

                        // Ha az üzenet hossza kisebb, mint a kinyert szöveg hossza,
                        // akkor csak az üzenet végéig veszünk karaktereket
                        if (decryptedMessage.Length < message.Length)
                        {
                            message = message.Substring(0, decryptedMessage.Length);
                        }

                        // Az üzenet helyettesítése a nap részben
                        dayParts[0] = decryptedMessage;

                        // A nap visszafejtett üzenetének összefűzése
                        string decryptedDay = string.Join("$", dayParts);

                        // Az eredeti üzenetben a nap rész helyettesítése a visszafejtett nappal
                        parts[dayNumber + 1] = decryptedDay;
                    }
                }

                // Az összefűzés az új üzenetekkel
                return string.Join("#", parts);
            }

            // Ha nincs "#" a szövegben, akkor visszatérünk az eredeti szöveggel
            return encodedMessage;
        }
    }
}
