using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace blackjack_oop
{
    internal class Hra
    {
        public Hrac NewGame()
        {
            bool hra = true;

            while (hra)
            {
                Console.Write("Napiste vas nick: ");
                string nick = Console.ReadLine();
                bool nick_check = KontrolaNicku(nick);
                if (nick_check == false)
                {
                    Console.Clear();
                    Console.WriteLine("Nick musi byt delsi nez 3 znaky!");
                }
                else
                {
                    Hrac hrac = new Hrac();
                    hrac.Nick = nick;
                    
                    hra = false;
                    return hrac;
                }
            }

            return null;
        }

        public void NewRound(Hrac hrac)
        {
            List<string> karty = new List<string>();

            Balicek balicek = new Balicek();
            balicek.Karty = karty;
            int penize = 1000;
            bool round = true;
            while (round)
            {
                Console.Write("Vsadte sazku: ");
                Int32.TryParse(Console.ReadLine(), out int sazka);
                bool sazka_check = KontrolaSazky(sazka, penize);
                if (sazka_check == false)
                {
                    Console.Clear();
                    Console.WriteLine("Zadejte platnou sazku");
                }
                else
                {

                    hrac.Penize = penize;
                    hrac.Sazka = sazka;
                    Console.WriteLine(hrac.Nick);
                    penize = hrac.OdectiPenize();
                    Console.WriteLine(penize);
                    balicek.VytvorBalicek();
                    balicek.Karty = balicek.Shuffle();
                }
            }
           
        }

        public bool KontrolaNicku(string nick)
        {
            if (nick.Length <= 3)
            {
                return false;
            }

            return true;
        }

        public bool KontrolaSazky(int sazka, int penize)
        {
            if (sazka <= 0)
            {
                return false;
            }
            if (sazka > penize)
            {
                return false;
            }

            return true;
        }
    }
}
