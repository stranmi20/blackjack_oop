using blackjack_oop;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

int moznost;
bool menu = true;
while (menu)
{
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("************BLACKJACK****************");
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
            int penize = 1000;
            menu = false;
            Console.Clear();
            Hra hra = new Hra();
            Hrac hrac = hra.NewGame();
            hra.NewRound(hrac, penize);
            bool dalsi_kolo = true;
            while (dalsi_kolo)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine();
                Console.Write("Chcete hrat dalsi kolo? (a/n) >> ");
                
                char odpoved = Console.ReadKey().KeyChar;
                if (odpoved == 'a')
                {
                    hra.NewRound(hrac, hrac.Penize);
                }
                else if (odpoved == 'n')
                {
                    
                    menu = true;
                    dalsi_kolo = false;
                }
                else
                {
                    continue;
                }
            }
            Console.ResetColor();
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
