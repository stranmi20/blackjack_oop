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
        public List<string> Karty_v_ruce { get; set; }
        public int Penize { get; set; }

        public int Sazka { get; set; }

        public int OdectiPenize()
        {
            return Penize - Sazka;
        }

    }
}
