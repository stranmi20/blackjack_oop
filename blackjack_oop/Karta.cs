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
        public char Hodnota { get; set; }

        public char Barva { get; set; }

        public int VratHodnotu(int hodnota_karet)
        {
            if (Hodnota == '1' || Hodnota == 'J' || Hodnota == 'K' || Hodnota == 'Q')
            {
                return 10;
            } else if(Hodnota == 'A')
            {
                return 11;
            }
            else
            {
                return CharUnicodeInfo.GetDecimalDigitValue(Hodnota);
            }
           
        }
    }
}
