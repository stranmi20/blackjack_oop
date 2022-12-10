using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

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

        public List<string> Shuffle()
        {
            Karty = Karty.OrderBy(_ => rng.Next()).ToList();
            return Karty;
        }

        public void Pridani_karty_k_hraci(Hrac hrac)
        {
            hrac.Karty_v_ruce.Add(Karty[0]);
            Karty.Remove(Karty[0]);
        }

        public void Pridani_karty_k_dealerovi(Dealer dealer)
        {
            dealer.Karty_v_ruce.Add(Karty[0]);
            Karty.Remove(Karty[0]);
        }
    }
}
