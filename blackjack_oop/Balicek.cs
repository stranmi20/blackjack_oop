using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blackjack_oop
{
    internal class Balicek
    {
        public List<string> Karty { get; set; }

        public void Vypis_pocet_karet()
        {
            Console.WriteLine("Pocet karet v balicku: " + Karty.Count);
        }

        public void PridaniKartyDoBaliku(string hodnota,string barva)
        {
            Karty.Add(hodnota + barva);
        }

        private static Random rng = new Random();
        public static void Shuffle<T>(IList<T> Karty)
        {
            int n = Karty.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = Karty[k];
                Karty[k] = Karty[n];
                Karty[n] = value;
            }
        }
    }
}
