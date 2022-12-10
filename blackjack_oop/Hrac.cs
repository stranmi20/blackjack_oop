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
        public string Nick { get; set; }
        public List<string> Karty_v_ruce { get; set; }
        public int Penize { get; set; }

        public int Sazka { get; set; }

        public int Hodnota_karet { get; set; }

        public int OdectiPenize()
        {
            return Penize - Sazka;
        }

        public int VyhrajPrachy()
        {
            return Penize + Sazka * 2;
        }

        public int VratPrachy()
        {
            return Penize + Sazka;
        }
        public void VypisKarty()
        {
            Console.Write("Vase Karty: ");
            foreach (string k in Karty_v_ruce)
            {
                Console.Write(k + " ");
            }
        }

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
