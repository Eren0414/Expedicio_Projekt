using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpedicioProjekt
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string valami = "ablskgpjt";
            string valami2 = "ablskgpjt";
            string megfejtes = "";
            for (int i = 0; i < valami.Length; i++) {
                Console.WriteLine(valami[i]);
                Console.WriteLine(valami2[i]);
                megfejtes += valami[i];
            }
            Console.WriteLine(megfejtes);
            Console.ReadLine();
           
        }
    }
}
