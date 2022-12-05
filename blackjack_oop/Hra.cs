using System;
using System.Reflection.Metadata.Ecma335;

namespace blackjack_oop
{
    class Hra
    {
        static void Main()
        {
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
                                Hrac hrac = new Hrac();
                                hrac.Nick = nick;
                                hrac.Penize = penize;
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
        }
    }
}
