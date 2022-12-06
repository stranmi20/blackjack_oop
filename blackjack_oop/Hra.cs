using blackjack_oop;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

Balicek balicek = new Balicek();

Karta karta = new Karta();
karta.Hodnota = "A";
karta.Barva = "Srdcova";

balicek.PridaniKartyDoBaliku(karta.Hodnota, karta.Barva);

int penize = 1000;
bool hra = true;
int moznost = 5;
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
            while (hra)
            {
                Console.Write("Napiste vas nick: ");
                string nick = Console.ReadLine();
                if (nick.Length <= 3)
                {
                    Console.Clear();
                    Console.WriteLine("Nick musi byt delsi nez 3 znaky!");
                }
                else
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

            }
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
