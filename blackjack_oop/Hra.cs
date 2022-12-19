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
                    hrac.Penize = penize;
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

                    
                    //Kontrola Jestli Hrac I Dealer Nemaji BlackJack
                    if (hrac.KontrolaBlackjacku() && dealer.KontrolaBlackjacku())
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("Remiza");
                        Console.WriteLine("Oba Mate Blackjack!");
                        Console.ResetColor();
                        VypisPoHre(hrac, dealer);
                        break;
                    }

                    //Kontrola Jestli Hrac Nema BlackJack
                    if (hrac.KontrolaBlackjacku())
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Vyhrali jste!");
                        Console.WriteLine("Mate Blackjack!");
                        hrac.Penize = hrac.VyhrajPrachy();
                        VypisPoHre(hrac, dealer);
                        break;
                    }

                    bool dalsi_karta_bool = true;
                    bool prohra_hrac_nad_21 = false;
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

                            //Kontrola Jestli Nema Hrac Hodnotu Karet Nad 21
                            if (hrac.Hodnota_karet > 21)
                            {
                                prohra_hrac_nad_21 = true;
                                break;
                            }
                            //DOUBLE DOWN
                        } else if (dalsi_karta == 'd') {
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
                                hrac.Penize = hrac.OdectiPenize();
                                balicek.Pridani_karty_k_hraci(hrac);
                                hrac.Hodnota_karet = hrac.VratHodnotuKaretVRuce();
                                VypisHry(hrac, dealer);
                                if (hrac.Hodnota_karet > 21)
                                {
                                    prohra_hrac_nad_21 = true;
                                }
                                break;
                            }
                        //POKUD NE
                        } else if (dalsi_karta == 'n')
                        {
                            break;
                        }
                        //Pokud Napise Neco Jineho
                        else
                        {
                            continue;
                        }
                    }

                    
                    //Pokud Ma Hrac Hodnotu Karet Nad 21
                    if (prohra_hrac_nad_21 == true)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Prohrali Jste!");
                        Console.WriteLine("Hodnota Vasich Karet Je Nad 21");
                        VypisPoHre(hrac, dealer);
                        break;
                    }

                    //Kontrola Jestli Dealer Nema BlackJack
                    if (dealer.KontrolaBlackjacku())
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Prohrali jste!");
                        Console.WriteLine("Dealer Ma Blackjack!");
                        VypisPoHre(hrac, dealer);
                        break;
                    }

                    //Loop Na To Kdyz Dealer Ma Hodnotu Karet Pod 17 
                    while (dealer.Hodnota_karet < 17)
                    {
                        balicek.Pridani_karty_k_dealerovi(dealer);
                        dealer.Hodnota_karet = dealer.VratHodnutuKaretVRuce();
                    }
                            
                    //Pokud Ma Dealer Hodnotu Karet Min Nebo Presne 21 A Hrac Nema Nad 21
                    if (dealer.Hodnota_karet <= 21 && prohra_hrac_nad_21 == false)
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
                            break;
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
                            break;
                        }

                        //Pokud Ma Hrac Mensi Hodnotu Karet Nez Dealer
                        if (hrac.Hodnota_karet < dealer.Hodnota_karet)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Prohrali jste!");
                            Console.WriteLine("Mate Mensi Hodnotu Karet Nez Dealer!");                              
                            VypisPoHre(hrac, dealer);
                            break;
                        }
                    }

                    //Pokud Ma Dealer Nad 21
                    if(dealer.Hodnota_karet > 21)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Vyhrali Jste!");
                        Console.WriteLine("Dealer Ma Nad 21!");
                        hrac.Penize = hrac.VyhrajPrachy();
                        VypisPoHre(hrac, dealer);                     
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
            Console.WriteLine("Hodnota Dealerovich Karet: " + dealer.Hodnota_karet);
            Console.WriteLine();
            Console.ResetColor();
            ZapisDoZebricku(hrac);
        }

        public void ZapisDoZebricku(Hrac hrac)
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
            while ((line = sr.ReadLine()) != null)
            {
                string[] fields = line.Split(';');

                if (fields[0] == hrac.Nick) {
                    
                    fields[1] = hrac.Penize.ToString();
                }
                data.Add(fields);
            }

            //Zavre se
            sr.Close();

            //Otvre se soubor pro psani
            StreamWriter sw = new StreamWriter(cesta);

            string[] hracs = new string[2];

            foreach (string[] udaj in data)
            {
                string newline = string.Format("{0};{1}", udaj[0], udaj[1]);
                sw.WriteLine(newline);
            }

            //Zavre se
            sw.Close();
        }
    }
}
