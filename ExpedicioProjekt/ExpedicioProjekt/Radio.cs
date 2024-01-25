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
            return encodedMessage.Replace("#", "a").Replace("$", ".");
        }
    }
}
