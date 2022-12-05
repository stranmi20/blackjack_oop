using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blackjack_oop
{
    internal class Karta
    {
        public string Hodnota { get; set; }

        public string Barva { get; set; }

        public void VypisKartu()
        {
            Console.WriteLine(this.Hodnota + this.Barva);
        }
    }
}
