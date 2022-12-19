using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blackjack_oop
{
    internal class Dealer
    {
        //Nastaveni Vlastnosti Dealera
        public List<string> Karty_v_ruce { get; set; }

        public int Hodnota_karet { get; set; }

        //Metoda Pro Vraceni Hodnoty Karet Dealera
        public int VratHodnutuKaretVRuce()
        {
            //Pocitani karet
            Hodnota_karet = 0;
            foreach (string k in Karty_v_ruce)
            {
                Karta karta_hrace = new Karta();
                karta_hrace.Hodnota = k[0];
                karta_hrace.Barva = k[1];
                int hodnota = karta_hrace.VratHodnotu(Hodnota_karet);
                Hodnota_karet += hodnota;
            }

            foreach (string k in Karty_v_ruce)
            {
                //Pokud Ma ESO
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

        //Metoda Pro Vypis Karet dealera
        public void VypisKartyDealera()
        {
            Console.Write("Dealerovi Karty: ");
            foreach (string k in Karty_v_ruce)
            {
                Console.Write(k + " ");
            }
        }

        //Metoda Pro Kontrolu Blackjacku
        public bool KontrolaBlackjacku()
        {
            if (Hodnota_karet == 21)
            {
                foreach (string k in Karty_v_ruce)
                {
                    if (k[0] == 'A')
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
