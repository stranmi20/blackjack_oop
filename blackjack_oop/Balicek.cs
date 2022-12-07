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
        public List<string> VytvorBalicek()
        {
            string[] hodnoty = new string[] { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
            string[] barva = new string[] { "♥", "♦", "♠", "♣" };

            for (int i = 0; i < 4; i++)
            {
                foreach (string hod in hodnoty)
                {
                    foreach (string bar in barva)
                    {
                        string hod_bar = hod + bar;
                        Karty.Add(hod_bar);
                    }
                }
            }
            return Karty;
        }

        private static Random rng = new Random();

        public void Shuffle(List<string> balik)
        {
            for (int n = balik.Count - 1; n > 0; --n)
            {
                int k = rng.Next(n + 1);
                int temp = balik[n];
                balik[n] = balik[k];
                balik[k] = temp;
            }
        }
    }
}
