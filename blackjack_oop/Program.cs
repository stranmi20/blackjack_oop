using blackjack_oop;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

int moznost;
bool menu = true;
while (menu)
{
    Console.WriteLine("************BLACKJACK****************");
    Console.WriteLine("MENU");
    Console.WriteLine("1 - Hra");
    Console.WriteLine("2 - zebricek");
    Console.WriteLine("3 - Pravidla");
    Console.WriteLine("4 - Konec");
    Console.WriteLine("Vyberte moznost");
    Console.Write(">>");
    moznost = Console.ReadKey().KeyChar;
    switch (moznost)
    {
        case '1':
            menu = false;
            Console.Clear();
            Hra hra = new Hra();
            Hrac hrac = hra.NewGame();
            hra.NewRound(hrac);
            /*
            while (hra)
            {
                Console.Write("Vsadte sazku: ");
                Int32.TryParse(Console.ReadLine(), out int sazka);

                Hrac hrac = new Hrac();
                hrac.Nick = nick;
                hrac.Penize = penize;
                hrac.Sazka = sazka;

                bool kontrola_sazky = hrac.KontrolaSazky();
                if (kontrola_sazky == false)
                {
                    Console.Clear();
                    Console.WriteLine("Zadejte platnou sazku");
                }
                else
                {
                    penize = hrac.OdectiPenize();
                    balicek.Vypis_pocet_karet();
                }
            }
            */
            break;
        case '2':
            menu = false;
            Console.Clear();
            Console.WriteLine("L");
            Console.ReadLine();
            break;
        case '3':
            menu = false;
            Console.Clear();
            Console.WriteLine("Im racist");
            Console.ReadLine();
            break;
        case '4':
            menu = false;
            Console.Clear();
            Console.WriteLine("kys");
            Console.ReadLine();
            break;
        default:
            Console.Clear();
            continue;
    }
}            
