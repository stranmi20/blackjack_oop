﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        private int OdectiPenize()
        {
            return Penize - Sazka;
        }

        

    }
}