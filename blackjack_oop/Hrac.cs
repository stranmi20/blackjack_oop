using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace blackjack_oop
{
    internal class Hrac
    {
        //Nastaveni Vlastnosti Hrace
        public string Nick { get; set; }
        public List<string> Karty_v_ruce { get; set; }
        public int Penize { get; set; }

        public int Sazka { get; set; }

        public int Hodnota_karet { get; set; }

        //Metoda Pro Odecteni Penez
        public int OdectiPenize()
        {
            return Penize - Sazka;
        }

        //Metoda Pro Vyhru Penez
        public int VyhrajPrachy()
        {
            return Penize + Sazka * 2;
        }

        //Metoda Pro Vraceni Vkladu
        public int VratPrachy()
        {
            return Penize + Sazka;
        }

        //Metoda Pro Vypis Karet V Ruce
        public void VypisKarty()
        {
            Console.Write("Vase Karty: ");
            foreach (string k in Karty_v_ruce)
            {
                Console.Write(k + " ");
            }
        }

        //Metoda Pro Vraceni Hodnoty Co Ma V Ruce
        public int VratHodnotuKaretVRuce()
        {
            Hodnota_karet = 0;
            foreach (string k in Karty_v_ruce)
            {
                Karta karta_hrace = new Karta();
                karta_hrace.Hodnota = k[0];
                karta_hrace.Barva = k[1];
                int hodnota = karta_hrace.VratHodnotu(Hodnota_karet);
                Hodnota_karet += hodnota;
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

        //Metoda Pro Kontrolu Jestli Nema Blackjack
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
