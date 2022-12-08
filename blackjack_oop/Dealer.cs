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
            foreach (string a in Karty_v_ruce)
            {
                Karta karta_dealera = new Karta();
                karta_dealera.Hodnota = a[0];
                karta_dealera.Barva = a[1];
                int hodnota_dealera = karta_dealera.VratHodnotu(Hodnota_karet);
                Hodnota_karet += hodnota_dealera;
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
