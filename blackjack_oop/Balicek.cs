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
        //Nastaveni Vlastnosti Balicku
        public List<string> Karty { get; set; }

        //Metoda Pro Vytvoreni Balicku
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

        //Random
        private static Random rng = new Random();

        //Metoda Pro Zamichani Balicku
        public List<string> Shuffle()
        {
            Karty = Karty.OrderBy(_ => rng.Next()).ToList();
            return Karty;
        }

        //Metoda Pro Pridani Karty K Hraci
        public void Pridani_karty_k_hraci(Hrac hrac)
        {
            hrac.Karty_v_ruce.Add(Karty[0]);
            Karty.Remove(Karty[0]);
        }

        //Metoda Pro Pridani Karty K Dealerovi
        public void Pridani_karty_k_dealerovi(Dealer dealer)
        {
            dealer.Karty_v_ruce.Add(Karty[0]);
            Karty.Remove(Karty[0]);
        }
    }
}
