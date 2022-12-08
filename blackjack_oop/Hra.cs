using System;
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

        public void NewRound(Hrac hrac)
        {
            List<string> karty = new List<string>();
            List<string> hrac_karty_v_ruce = new List<string>();
            List<string> dealer_karty_v_ruce = new List<string>();

            Balicek balicek = new Balicek();
            balicek.Karty = karty;
            hrac.Karty_v_ruce = hrac_karty_v_ruce;
            int penize = 1000;
            int hodnota_karet_hrace = 0;
            int hodnota_karet_dealera = 0;
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
                    Console.Clear();
                    hrac.Penize = penize;
                    hrac.Sazka = sazka;

                    penize = hrac.OdectiPenize();

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

                    VypisHry(hrac, penize, dealer);

                    char dalsi_karta;
                    bool konec = true;
                    bool dalsi_karta_bool = true;
                    do
                    {
                        Console.WriteLine();
                        Console.WriteLine("Chcete Dalsi Kartu? (a/n)");
                        dalsi_karta = Console.ReadKey().KeyChar;
                        if (dalsi_karta == 'a')
                        {
                            Console.Clear();
                            balicek.Pridani_karty_k_hraci(hrac);
                            VypisHry(hrac, penize, dealer);
                            dalsi_karta_bool = KontrolaHodnoty(hrac);
                            if (!dalsi_karta_bool)
                            {
                                dalsi_karta = 'n';
                            }
                        } else if (dalsi_karta == 'n')
                        {
                            konec = true;
                            break;
                        }
                        else
                        {
                            dalsi_karta = 'a';
                        }
                    } while (dalsi_karta == 'a');
                    if (konec)
                    {
                        if (!dalsi_karta_bool)
                        {
                            Console.Clear();
                            VypisHry(hrac, penize, dealer);
                        }

                        if (dealer.Hodnota_karet < 17)
                        {
                            balicek.Pridani_karty_k_dealerovi(dealer);
                        }

                        if (hrac.Hodnota_karet > dealer.Hodnota_karet)
                        {
                            Console.Clear();
                            Console.WriteLine("Vyhrali jste!");
                        }
                        if (hrac.Hodnota_karet == dealer.Hodnota_karet)
                        {
                            Console.Clear();
                            Console.WriteLine("Remiza!");
                        }
                        if (hrac.Hodnota_karet < dealer.Hodnota_karet)
                        {
                            Console.Clear();
                            Console.WriteLine("Prohrali jste!");
                        }
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

        public bool KontrolaHodnoty(Hrac hrac)
        {
            if (hrac.Hodnota_karet > 21)
            {
                return false;
            }
            return true;
        }

        public void VypisHry(Hrac hrac, int penize, Dealer dealer)
        {
            Console.WriteLine("Pocet penez: " + penize);
            hrac.VypisKarty();
            Console.WriteLine();
            hrac.Hodnota_karet = hrac.VratHodnotuKaretVRuce();
            Console.WriteLine("Hodnota Karet: " + hrac.Hodnota_karet);
            Console.Write("Karty Dealera: ");
            Console.Write(dealer.Karty_v_ruce[0]);
            Console.WriteLine();
        }
    }
}
