using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blackjack_oop
{
    internal class Dealer
    {
        public List<string> Karty_v_ruce { get; set; }

        public int Hodnota_karet { get; set; }

        public int VratHodnutuKaretVRuce()
        {
            Hodnota_karet = 0;
            foreach (string k in Karty_v_ruce)
            {
                Karta karta_hrace = new Karta();
                karta_hrace.Hodnota = k[0];
                karta_hrace.Barva = k[1];
                int hodnota = karta_hrace.VratHodnotu(Hodnota_karet);
                Hodnota_karet += hodnota;
                if (k[0] == 'A')
                {
                    if (Hodnota_karet > 21)
                    {
                        Hodnota_karet -= 10;
                    }
                }
            }
            return Hodnota_karet;
        }

        public void VypisKartyDealera()
        {
            Console.Write("Dealerovi Karty: ");
            foreach (string k in Karty_v_ruce)
            {
                Console.Write(k + " ");
            }
        }
    }
}
