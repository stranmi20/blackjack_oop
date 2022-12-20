using blackjack_oop;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using static System.Formats.Asn1.AsnWriter;

int moznost;
bool menu = true;
//Hlavni Loop Menu
while (menu)
{
    //Menu
    Console.Clear();
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
        //Hra
        case '1':
            //Nastaveni Penez
            int penize = 1000;
            Console.Clear();
            //Zalozeni Nove Hry
            Hra hra = new Hra();
            //Zavolani Metody Nove Hry
            Hrac hrac = hra.NewGame();
            //Zavolani Noveho Kola
            hra.NewRound(hrac, penize);
            bool dalsi_kolo = true;
            //Loop Pro Nove Kolo
            while (dalsi_kolo)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                //Kontrola Jestli Hrac Nema 0 Penez
                if (hrac.Penize == 0)
                {
                    Console.WriteLine("Mate 0 Penez");
                    Console.WriteLine();
                    Console.WriteLine("Zmacknete ENTER Pro Vstupu Do Menu");
                    Console.ReadLine();
                    break;
                }
                Console.Write("Chcete hrat dalsi kolo? (a/n) >> ");
                char odpoved = Console.ReadKey().KeyChar;
                //Pokud ANO
                if (odpoved == 'a')
                {
                    //Zavola Se Nove Kolo Kam Se Poslou Penize A Hrac
                    hra.NewRound(hrac, hrac.Penize);
                }
                //Pokud NE
                else if (odpoved == 'n')
                {
                    break;
                }
            }
            Console.ResetColor();
            break;
        //Zebricek
        case '2':
            Hra hra2 = new Hra();
            hra2.ZebricekProVypis();
            break;
        case '3':
            Console.Clear();
            Console.WriteLine("Každý hráč na začátku hry obdrží dvě karty a pak mu krupiér nabízí další karty. Hráč se po každé rozhoduje, zda bude chtít další, nebo ne. Základní princip hry je, že hráč chce mít hodnotu karet blíže 21 než krupiér, ale přitom 21 nepřekročit. Vyhrává ten, kdo má po ukončení hry v ruce nejvyšší součet, aniž by překročil 21. Hráč, který má v ruce součet karet větší než 21, je takzvaně „trop“ neboli „přes“.");
            Console.WriteLine("Karty od 2 do 10 mají při počítání stejnou hodnotu, jaká je uvedena na kartě, karty J, Q, K (spodek, královna a král) mají hodnotu 10. Eso (A) se může počítat podle vlastního uvážení za 1 nebo za 11. Barvy karet nemají žádný význam. Pokud má hráč eso počítané za 11 bodů, nazývá se součet „měkký“, protože při tažení další karty se hráč nikdy nemůže dostat „přes“; pokud ne, nazývá se součet „tvrdý“ (např. měkkých 16 nebo tvrdých 17).");
            Console.WriteLine("Základním cílem je mít více bodů než krupiér. Pokud je hráč „trop“, vždy prohrává, a to i tehdy, pokud je „trop“ i krupiér, což znamená, že blackjack upřednostňuje krupiéra. Pokud mají hráč i krupiér stejný počet bodů, končí hra nerozhodně a nikdo nevyhrává. Každý hráč hraje samostatně proti krupiérovi, je tedy možné, aby v průběhu jedné hry krupiér s některými hráči vyhrál, s některými prohrál a s jinými remizoval.");
            Console.WriteLine();
            Console.WriteLine("Rozdávání karet");
            Console.WriteLine("Po umístění sázek (na tzv. boxy) začne krupiér rozdávat karty. Minimální vsazená částka se v různých kasinech liší a vždy je u stolu vyznačena. Krupiér rozdává ze svého pohledu ve směru hodinových ručiček tak, že každému hráči a sobě dá jednu kartu a pak ještě každému hráči jednu. Všechny karty se rozdávají lícem nahoru, takže všichni vidí jejich hodnoty. Nikdo kromě krupiéra se nesmí karet dotýkat.");
            Console.WriteLine("Po tomto rozdání karet se začne krupiér ve stejném směru postupně ptát hráčů, co chtějí se svými kartami provést a zároveň jim nahlásí součet jejich karet. Hráč může chtít další kartu, zůstat stát, nebo hrát double. Krupiér podle pokynů hráče provádí všechny kroky až do doby, dokud hráč nezůstane stát nebo není „trop“. Jakmile je hráč „trop“, krupiér okamžitě sebere vsazenou sázku.");
            Console.WriteLine("Po dokončení hry všech hráčů krupiér rozdá karty sobě a podle výsledku vyplatí sázky nebo sebere prohrávající vklady.");
            Console.WriteLine();
            Console.WriteLine("Hra dealera");
            Console.WriteLine("Krupiér začíná hrát poté, co všichni hráči u stolu dohrají své hry. Na rozdíl od ostatních hráčů je při volbě dalšího postupu výrazně omezen. Pokud je jeho součet menší než 17, musí vždy vzít další kartu, pokud je 17 a výše, nesmí brát další kartu, musí zůstat stát a ukončit tak hru. (V některých kasinech krupiér táhne další kartu i při měkkém součtu 17.) Krupiér nemá možnost zahlásit ani double ani.");
            Console.WriteLine("Pokud krupiér překročí 21, vyhrávají všichni hráči, kteří sami 21 nepřekročili. Pokud krupiér nepřekročí hodnotu 21, vyhrávají ti hráči, kteří mají vyšší počet bodů. Všechny výhry jsou vyplaceny v poměru 1 : 1. Hráči, kteří mají stejný počet bodů jako krupiér, dostanou své sázky zpět.");
            Console.WriteLine("Pokud má krupiér black jack, prohrávají svou sázku všichni hráči s výjimkou těch, kteří sami mají black jack – ti končí nerozhodně (stand off).");
            Console.WriteLine();
            Console.WriteLine("Hra hráče");
            Console.WriteLine("Hráč, který je na řadě má při hře následující možnosti:");
            Console.WriteLine("Hit: Vzít od krupiéra další kartu.");
            Console.WriteLine("Double: Zdvojnásobit vsazenou částku. V takovém případě dostane od krupiéra ještě jednu kartu a hra končí. Někdy lze sázku zdvojnásobit pouze v okamžiku, kdy má hráč pouze jednu kartu.");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Zmacknete ENTER Pro Navrat Do Menu");
            Console.ReadLine();
            Console.ResetColor();
            break;
        //Konec
        case '4':
            menu = false;
            Console.Clear();
            Console.WriteLine("Dekujeme Za Zahrani!");
            Console.ReadLine();
            break;
        default:
            Console.Clear();
            continue;
    }
}            
