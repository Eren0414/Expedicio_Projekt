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
        public static string DecodeVeetelMessage(string input)
        {
            string chars = input.Split()[1];

            StringBuilder decoded = new StringBuilder();

            foreach (char c in chars)
            {
                if (c == '#')
                {
                    decoded.Append(' ');
                }
                else if (c == '$')
                {
                    decoded.Append(char.ToUpper(c));
                }
                else
                {
                    decoded.Append(c);
                }
            }

            return decoded.ToString();
        }
    }
}
