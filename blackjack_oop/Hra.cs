﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.SymbolStore;
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

        public void NewRound(Hrac hrac, int penize)
        {
            List<string> karty = new List<string>();
            List<string> hrac_karty_v_ruce = new List<string>();
            List<string> dealer_karty_v_ruce = new List<string>();

            Balicek balicek = new Balicek();
            balicek.Karty = karty;
            hrac.Karty_v_ruce = hrac_karty_v_ruce;
            
            bool round = true;
            while (round)
            {
                Console.WriteLine("Vsadte sazku >> ");
                Int32.TryParse(Console.ReadLine(), out int sazka);
                bool sazka_check = KontrolaSazky(sazka, penize);
                if (sazka_check == false)
                {
                    Console.Clear();
                    Console.WriteLine("Zadejte platnou sazku");
                }
                else
                {
                    Console.Clear();
                    hrac.Penize = penize;
                    hrac.Sazka = sazka;

                    hrac.Penize = hrac.OdectiPenize();

                    balicek.VytvorBalicek();
                    balicek.Karty = balicek.Shuffle();

                    balicek.Pridani_karty_k_hraci(hrac);
                    balicek.Pridani_karty_k_hraci(hrac);

                    hrac.Hodnota_karet = hrac.VratHodnotuKaretVRuce();

                    Dealer dealer = new Dealer();
                    dealer.Karty_v_ruce = dealer_karty_v_ruce;

                    balicek.Pridani_karty_k_dealerovi(dealer);
                    balicek.Pridani_karty_k_dealerovi(dealer);

                    dealer.Hodnota_karet = dealer.VratHodnutuKaretVRuce();

                    VypisHry(hrac, dealer);

                    char dalsi_karta;
                    bool prohra_hrac_nad_21 = false;
                    do
                    {
                        Console.WriteLine();
                        Console.Write("Chcete Dalsi Kartu? (a/n) >> ");
                        dalsi_karta = Console.ReadKey().KeyChar;
                        if (dalsi_karta == 'a')
                        {
                            Console.Clear();
                            balicek.Pridani_karty_k_hraci(hrac);
                            VypisHry(hrac, dealer);
                            if (hrac.Hodnota_karet > 21)
                            {
                                prohra_hrac_nad_21 = true;
                                break;
                            }
                        } else if (dalsi_karta == 'n')
                        {
                            dalsi_karta = 'n';
                        }
                        else
                        {
                            dalsi_karta = 'a';
                        }
                    } while (dalsi_karta == 'a');

                    

                    if (prohra_hrac_nad_21 == true)
                    {
                        Console.Clear();
                        Console.WriteLine("Prohrali Jste!");
                        Console.WriteLine("Hodnota Vasich Karet Je Nad 21");
                        VypisPoHre(hrac, dealer);
                        hrac.Sazka = 0;
                        round = false;
                    }

                    while (dealer.Hodnota_karet < 17)
                    {
                        balicek.Pridani_karty_k_dealerovi(dealer);
                        dealer.Hodnota_karet = dealer.VratHodnutuKaretVRuce();
                    }

                    if (dealer.Hodnota_karet < 21 && prohra_hrac_nad_21 == false)
                    {
                        if (hrac.Hodnota_karet > dealer.Hodnota_karet)
                        {
                            Console.WriteLine("Vyhrali Jste!");
                            Console.WriteLine("Mate Vetsi Hodnotu Karet Nez Dealer!");                                                        
                            hrac.Penize = hrac.VyhrajPrachy();
                            VypisPoHre(hrac, dealer);
                            round = false;
                        }
                        if (hrac.Hodnota_karet == dealer.Hodnota_karet)
                        {
                            Console.Clear();
                            Console.WriteLine("Remiza!");
                            Console.WriteLine("Mate Stejnou Hodnotu Karet Jak Dealer!");
                            hrac.Penize = hrac.VratPrachy();
                            VypisPoHre(hrac, dealer);                    
                            round = false;
                        }
                        if (hrac.Hodnota_karet < dealer.Hodnota_karet)
                        {
                            Console.Clear();
                            Console.WriteLine("Prohrali jste!");
                            Console.WriteLine("Mate Mensi Hodnotu Karet Nez Dealer!");                              
                            VypisPoHre(hrac, dealer);
                            round = false;
                        }
                    }
                    if(dealer.Hodnota_karet > 21 && round != false)
                    {
                        Console.Clear();
                        Console.WriteLine("Vyhrali Jste!");
                        Console.WriteLine("Dealer Ma Nad 21!");
                        VypisPoHre(hrac, dealer);
                        hrac.Penize = hrac.VyhrajPrachy();
                        round = false;
                    }
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

        public void VypisHry(Hrac hrac, Dealer dealer)
        {
            Console.WriteLine("Pocet penez: " + hrac.Penize);
            hrac.VypisKarty();
            Console.WriteLine();
            hrac.Hodnota_karet = hrac.VratHodnotuKaretVRuce();
            Console.WriteLine("Hodnota Karet: " + hrac.Hodnota_karet);
            Console.Write("Karty Dealera: ");
            Console.Write(dealer.Karty_v_ruce[0]);
            Console.WriteLine();
        }

        public void VypisPoHre(Hrac hrac, Dealer dealer)
        {
            Console.WriteLine("Pocet penez: " + hrac.Penize);
            hrac.VypisKarty();
            Console.WriteLine();
            hrac.Hodnota_karet = hrac.VratHodnotuKaretVRuce();
            Console.WriteLine("Hodnota Karet: " + hrac.Hodnota_karet);
            dealer.VypisKartyDealera();
            Console.WriteLine();
            dealer.Hodnota_karet = dealer.VratHodnutuKaretVRuce();           
            Console.WriteLine("Hodnota Dealerovich Karet: " + dealer.Hodnota_karet);
            Console.WriteLine();
        }
    }
}
