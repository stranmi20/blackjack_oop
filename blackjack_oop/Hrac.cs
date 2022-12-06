using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace blackjack_oop
{
    internal class Hrac
    {
        public string Nick { get; set; }
        public List<Array> Karty { get; set; }
        public int Penize { get; set; }

        public int Sazka { get; set; }

        public void VypisKarty()
        {
            for (int i = 0; i < Karty.Count(); i++)
            {
                Console.WriteLine(i);
            }
        }

        public int OdectiPenize()
        {
            return Penize - Sazka;
        }

        public bool KontrolaSazky()
        {
            if (Sazka < 0)
            {
                return false;
            } else if (Sazka > Penize)
            {
                return false;
            } else if (String.IsNullOrEmpty(Sazka.ToString()))
            {
                return false;
            } else if (Regex.IsMatch(Sazka.ToString(), @"^[a-zA-Z]+$"))
            {
                return false;
            } else
            {
                return true;
            }
        }

        

    }
}
