using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace blackjack_oop
{
    internal class Karta
    {
        
        //Nastaveni Vlastnosti Karty
        public char Hodnota { get; set; }

        public char Barva { get; set; }

        //Metoda Pro Vraceni Hodnoty Karty
        public int VratHodnotu(int hodnota_karet)
        {
            if (Hodnota == '1' || Hodnota == 'J' || Hodnota == 'K' || Hodnota == 'Q')
            {
                return 10;
            } 
            if(Hodnota == 'A')
            {
                return 11;
            }
            //Prevedeni z char na int
            return CharUnicodeInfo.GetDecimalDigitValue(Hodnota);

        }
    }
}
