using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.SymbolStore;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace blackjack_oop
{
    internal class Hra
    {

        //Metoda Pro Novou Hru
        public Hrac NewGame()
        {
            bool hra = true;
            //Hlavni Loop Metody
            while (hra)
            {
                //Nastaveni Nicku
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Napiste vas nick: ");
                string nick = Console.ReadLine();
                bool nick_check = KontrolaNicku(nick);

                //Kontrola Nicku
                if (nick_check == false)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Nick musi byt delsi nez 3 znaky!");
                }
                else
                {

                    // Nastaveni Nicku K Hraci
                    Hrac hrac = new Hrac();
                    hrac.Nick = nick;
                    
                    hra = false;
                    return hrac;
                }
            }

            return null;
        }

        //Metoda Pro Nove Kolo
        public void NewRound(Hrac hrac, int penize)
        {
            //Vytvoreni Listu
            List<string> karty = new List<string>();
            List<string> hrac_karty_v_ruce = new List<string>();
            List<string> dealer_karty_v_ruce = new List<string>();

            //Vytvoreni Balicku
            Balicek balicek = new Balicek();
            balicek.Karty = karty;
            hrac.Karty_v_ruce = hrac_karty_v_ruce;
            
            bool round = true;

            //Hlavni Loop Kola
            while (round)
            {
                //Sazka
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                hrac.Penize = penize;
                Console.WriteLine("Pocet penez: " + hrac.Penize);
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Vsadte sazku >> ");
                Int32.TryParse(Console.ReadLine(), out int sazka);
                bool sazka_check = KontrolaSazky(sazka, penize);

                //Kontrola Sazky
                if (sazka_check == false)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Zadejte platnou sazku");
                    Console.ResetColor();
                }
                else
                {
                    //Nastaveni Penez
                    Console.Clear();
                    
                    hrac.Sazka = sazka;

                    hrac.Penize = hrac.OdectiPenize();

                    //Tvorba Balicku + Michani
                    balicek.VytvorBalicek();
                    balicek.Karty = balicek.Shuffle();

                    //Pridani Karet K Hracovi
                    balicek.Pridani_karty_k_hraci(hrac);
                    balicek.Pridani_karty_k_hraci(hrac);

                    //Vraceni Hodnoty Karet Hrace
                    hrac.Hodnota_karet = hrac.VratHodnotuKaretVRuce();

                    //Vytvoreni Dealera a jeho ruky
                    Dealer dealer = new Dealer();
                    dealer.Karty_v_ruce = dealer_karty_v_ruce;

                    //Pridani Karet K Dealerovi
                    balicek.Pridani_karty_k_dealerovi(dealer);
                    balicek.Pridani_karty_k_dealerovi(dealer);

                    //Vraceni Hodnoty Karet Dealera
                    dealer.Hodnota_karet = dealer.VratHodnutuKaretVRuce();

                    //Vypis hry
                    VypisHry(hrac, dealer);

                    
                    if(KontrolaBlackjacku(hrac, dealer))
                    {
                        break;
                    }

                    
                    bool dalsi_karta = true;
                    //loop pro dalsi kartu
                    while (dalsi_karta)
                    {
                        dalsi_karta = DalsiKarta(hrac, dealer, balicek);
                    }
                   
                    //vyhodnoceni hry
                    if(Rozhodovani(hrac, dealer, balicek))
                    {
                        break;
                    }
                }
            }
        }

        //Metoda Pro Kontrola Nicku
        public bool KontrolaNicku(string nick)
        {
            if (nick.Length <= 3)
            {
                return false;
            }

            return true;
        }

        //Metoda Pro Kontrolu Sazky
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


        //Metoda Pro Vypis Hry
        public void VypisHry(Hrac hrac, Dealer dealer)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Pocet penez: " + hrac.Penize);
            Console.WriteLine();       
            hrac.VypisKarty();
            Console.WriteLine();           
            hrac.Hodnota_karet = hrac.VratHodnotuKaretVRuce();
            Console.WriteLine("Hodnota Karet: " + hrac.Hodnota_karet);
            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Karty Dealera: ");
            Console.Write(dealer.Karty_v_ruce[0]);
            Console.WriteLine();
            Console.WriteLine();
            Console.ResetColor();
        }


        //Metoda Pro Vypis Po Konci Kola
        public void VypisPoHre(Hrac hrac, Dealer dealer)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine("Pocet penez: " + hrac.Penize);          
            hrac.VypisKarty();
            Console.WriteLine();
            hrac.Hodnota_karet = hrac.VratHodnotuKaretVRuce();
            Console.WriteLine("Hodnota Karet: " + hrac.Hodnota_karet);
            Console.WriteLine();
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            dealer.VypisKartyDealera();
            Console.WriteLine();        
            dealer.Hodnota_karet = dealer.VratHodnutuKaretVRuce();           
            Console.WriteLine("Hodnota Dealerovych Karet: " + dealer.Hodnota_karet);
            Console.WriteLine();
            Console.ResetColor();
            ZapisDoZebricku(hrac);
        }

        public void ZapisDoZebricku(Hrac hrac)
        {
            try
            {
                string cesta = @"zebricek.csv";
            
                //Pokud csv file neexistuje vytvori ho
                if (!File.Exists(cesta))
                {
                    File.Create(cesta).Dispose();
                }
                //Otevre se soubor pro cteni
                StreamReader sr = new StreamReader(cesta);

                List<string[]> data = new List<string[]>();

                //Ulozi cely text do arraye
                string line;
                bool nick_exist = false;
                //cteni souboru
                while ((line = sr.ReadLine()) != null)
                {
                    string[] fields = line.Split(';');
                    //prepis existujiciho nicku
                    Int32.TryParse(fields[1], out int value);
                    if (fields[0] == hrac.Nick && value < hrac.Penize) {
                    
                        fields[1] = hrac.Penize.ToString();
                        nick_exist = true;
                    } else if (fields[0] == hrac.Nick && value >= hrac.Penize)
                    {
                        nick_exist = true;
                    }
                    data.Add(fields);
                }
                //pokud je novy hrac
                if (!nick_exist)
                {
                    string[] pole = new string[2];
                    pole[0] = hrac.Nick;
                    pole[1] = hrac.Penize.ToString();
                    data.Add(pole);
                }

                //Zavre se
                sr.Close();

                //Otvre se soubor pro psani
                StreamWriter sw = new StreamWriter(cesta);

                //vypis do csv souboru
                foreach (string[] udaj in data)
                {
                    string newline = string.Format("{0};{1}", udaj[0], udaj[1]);
                    sw.WriteLine(newline);
                }

                //Zavre se
                sw.Close();
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("Nastala Neocekavana Chyba 2");
                Console.ReadLine();
            }
           
        }

        //metoda pro vyhodnoceni hry
        public bool Rozhodovani( Hrac hrac, Dealer dealer, Balicek balicek)
        {
            //Pokud Ma Hrac Hodnotu Karet Nad 21
            if (hrac.Hodnota_karet > 21)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Prohrali Jste!");
                Console.WriteLine("Hodnota Vasich Karet Je Nad 21");
                VypisPoHre(hrac, dealer);
                return true;
            }

            //Kontrola Jestli Dealer Nema BlackJack
            if (dealer.KontrolaBlackjacku())
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Prohrali jste!");
                Console.WriteLine("Dealer Ma Blackjack!");
                VypisPoHre(hrac, dealer);
                return true;
            }

            //Loop Na To Kdyz Dealer Ma Hodnotu Karet Pod 17 
            while (dealer.Hodnota_karet < 17)
            {
                balicek.Pridani_karty_k_dealerovi(dealer);
                dealer.Hodnota_karet = dealer.VratHodnutuKaretVRuce();
            }

            //Pokud Ma Dealer Hodnotu Karet Min Nebo Presne 21 A Hrac Nema Nad 21
            if (dealer.Hodnota_karet <= 21 && hrac.Hodnota_karet <= 21)
            {

                //Pokud Hrac Ma Vetsi Hodnotu Karet Nez Dealer
                if (hrac.Hodnota_karet > dealer.Hodnota_karet)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Vyhrali Jste!");
                    Console.WriteLine("Mate Vetsi Hodnotu Karet Nez Dealer!");
                    hrac.Penize = hrac.VyhrajPrachy();
                    VypisPoHre(hrac, dealer);
                    return true;
                }

                //Pokud Maji Stejnou Hodnotu Karet
                if (hrac.Hodnota_karet == dealer.Hodnota_karet)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Remiza!");
                    Console.WriteLine("Mate Stejnou Hodnotu Karet Jak Dealer!");
                    hrac.Penize = hrac.VratPrachy();
                    VypisPoHre(hrac, dealer);
                    return true;
                }

                //Pokud Ma Hrac Mensi Hodnotu Karet Nez Dealer
                if (hrac.Hodnota_karet < dealer.Hodnota_karet)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Prohrali jste!");
                    Console.WriteLine("Mate Mensi Hodnotu Karet Nez Dealer!");
                    VypisPoHre(hrac, dealer);
                    return true;
                }
            }

            //Pokud Ma Dealer Nad 21
            if (dealer.Hodnota_karet > 21)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Vyhrali Jste!");
                Console.WriteLine("Dealer Ma Nad 21!");
                hrac.Penize = hrac.VyhrajPrachy();
                VypisPoHre(hrac, dealer);
                return true;
            }
            return false;
        }

        //metoda pro dalsi kartu
        public bool DalsiKarta(Hrac hrac, Dealer dealer, Balicek balicek)
        {
            bool dalsi_karta_bool = true;
            //Loop Pro Dalsi Kartu
            while (dalsi_karta_bool)
            {
                //Dalsi Karta
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Chcete Dalsi Kartu? / Nebo Chcete Dat DoubleDown (a/n/d) >> ");
                Console.ResetColor();
                char dalsi_karta = Console.ReadKey().KeyChar;
                //Pokud ANO
                if (dalsi_karta == 'a')
                {
                    Console.Clear();
                    balicek.Pridani_karty_k_hraci(hrac);
                    hrac.Hodnota_karet = hrac.VratHodnotuKaretVRuce();
                    VypisHry(hrac, dealer);
                    //pokud ma hrac vetsi hodnotu karet nez 21
                    if(hrac.Hodnota_karet > 21)
                    {
                        return false;
                    }
                    return true;
                    //DOUBLE DOWN
                }
                else if (dalsi_karta == 'd')
                {
                    //KONTROLA PENEZ
                    if (hrac.Penize < hrac.Sazka)
                    {
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Nemate Dostatek Penez!");
                        Console.ResetColor();
                        
                    }
                    else
                    {
                        hrac.Sazka = hrac.Sazka * 2;
                        hrac.Penize = hrac.OdectiPenize();
                        balicek.Pridani_karty_k_hraci(hrac);
                        hrac.Hodnota_karet = hrac.VratHodnotuKaretVRuce();
                        VypisHry(hrac, dealer);
                        //kdyz ma vetsi hodnotu karet nez 21
                        if (hrac.Hodnota_karet > 21)
                        {
                            return false;
                        }
                        return false;
                    }
                    //POKUD NE
                }
                else if (dalsi_karta == 'n')
                {
                    return false;
                }
            }
            return false;
        }

        //metoda pro kontrolovani blackjacku
        public bool KontrolaBlackjacku(Hrac hrac, Dealer dealer)
        {
            //Kontrola Jestli Hrac I Dealer Nemaji BlackJack
            if (hrac.KontrolaBlackjacku() && dealer.KontrolaBlackjacku())
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Remiza");
                Console.WriteLine("Oba Mate Blackjack!");
                Console.ResetColor();
                hrac.Penize = hrac.VratPrachy();
                VypisPoHre(hrac, dealer);
                return true;
            }

            //Kontrola Jestli Hrac Nema BlackJack
            if (hrac.KontrolaBlackjacku())
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Vyhrali jste!");
                Console.WriteLine("Mate Blackjack!");
                hrac.Penize = hrac.VyhrajPrachy() + hrac.Sazka;
                VypisPoHre(hrac, dealer);
                return true;
            }

            //Kontrola Jestli Dealer Nema Blackjack
            if (dealer.KontrolaBlackjacku())
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Prohrali Jste!");
                Console.WriteLine("Dealer Ma Blackjack");
                VypisPoHre(hrac, dealer);
                return true;
            }
            return false;
        }

        public void ZebricekProVypis()
        {
            try
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Zebricek 5 nejlepsich");
                Console.WriteLine("Jmeno - Penize");
                Console.ResetColor();
                Console.WriteLine();
                string cesta = @"zebricek.csv";

                //Pokud csv file neexistuje vytvori ho
                if (!File.Exists(cesta))
                {
                    File.Create(cesta).Dispose();
                }
                //Otevre se soubor pro cteni
                StreamReader sr = new StreamReader(cesta);

                List<Tuple<string, int>> data = new List<Tuple<string, int>>();

                //Ulozi cely text do arraye
                string line;
                //cteni souboru
                while ((line = sr.ReadLine()) != null)
                {
                    string[] fields = line.Split(';');
                    Int32.TryParse(fields[1], out int value);
                    string jmeno = fields[0];
                    Tuple<string, int> tuple = new Tuple<string, int>(jmeno, value);
                    data.Add(tuple);
                }

                data = data.OrderByDescending(x => x.Item2).ToList();
                var prvnichpet = data.Take(5);

                foreach (Tuple<string, int> dat in prvnichpet)
                {
                    Console.Write(dat.Item1);
                    Console.Write(" - ");
                    Console.Write(dat.Item2);
                    Console.WriteLine();
                    Console.WriteLine();
                }

                sr.Close();

                Console.WriteLine();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Zmacknete ENTER pro navrat do menu");
                Console.ReadLine();
                Console.ResetColor();
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("Nastala Neocekavana Chyba 1");
            }
        }
    }
}
