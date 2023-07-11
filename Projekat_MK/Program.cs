using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Projekat_MK
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Korisnik korisnik = new Korisnik();
            string input;
            Console.WriteLine("Dostupne naredbe:");
            Console.WriteLine("login, where, go, create, list, print, find, findDat, exit, logout\n");
            string putanjaProjekta = Directory.GetCurrentDirectory();
            do
            {
                Console.WriteLine("\nUnesite naredbu:");
                input = (Console.ReadLine()).Trim(); 
                string[] argumenti = input.Split(" ");
                for (int i = 0; i < Provjere.BrojacRijeci(argumenti); i++)
                {
                    argumenti[i] = argumenti[i].Trim(); 
                }


                argumenti[0] = argumenti[0].ToLower();



                if (argumenti[0].Equals("login") && Provjere.BrojacRijeci(argumenti) == 1 && !korisnik.IsLogged())
                {
                    Directory.SetCurrentDirectory(putanjaProjekta);
                    korisnik.Login();

                }
                else if (argumenti[0].Equals("login") && Provjere.BrojacRijeci(argumenti) > 1)
                    Console.WriteLine("Previse argumenata za 'login' komandu");



                else if (argumenti[0].Equals("logout") && korisnik.IsLogged() && Provjere.BrojacRijeci(argumenti) == 1)
                {
                    korisnik.Logout();
                    Console.WriteLine("Uspjesno odjavljen!");
                }
                else if (argumenti[0].Equals("logout") && Provjere.BrojacRijeci(argumenti) > 1)
                    Console.WriteLine("Previse argumenata za 'logout' komandu");



                else if (argumenti[0].Equals("where") && korisnik.IsLogged() && Provjere.BrojacRijeci(argumenti) == 1)
                {
                    korisnik.Where_GetUserDir();
                }
                else if (argumenti[0].Equals("where") && korisnik.IsLogged() && Provjere.BrojacRijeci(argumenti) > 1)
                    Console.WriteLine("Previse argumenata za 'where' komandu");



                else if (argumenti[0].Equals("go") && korisnik.IsLogged() && Provjere.BrojacRijeci(argumenti) == 2)
                {
                    korisnik.Go(argumenti[1]);
                }
                else if (korisnik.IsLogged() && Provjere.BrojacRijeci(argumenti) > 2 && argumenti[0].Equals("go"))
                    Console.WriteLine("Previse argumenata za komandu 'go'");
                else if (Provjere.BrojacRijeci(argumenti) == 1 && argumenti[0].Equals("go") && korisnik.IsLogged())
                    Console.WriteLine("'go' zahtijeva i putanju kao argument\ngo putanja");




                else if (argumenti[0].Equals("create") && (korisnik.IsLogged() && Provjere.BrojacRijeci(argumenti) == 2 || Provjere.BrojacRijeci(argumenti) == 3))
                {
                    korisnik.CreateFileorDir(argumenti);
                }
                else if ((korisnik.IsLogged() && Provjere.BrojacRijeci(argumenti) == 1) && argumenti[0].Equals("create"))
                    Console.WriteLine("Nedovoljno argumenata za komandu 'create'!");
                else if ((korisnik.IsLogged() && Provjere.BrojacRijeci(argumenti) > 1) && argumenti[0].Equals("create"))
                    Console.WriteLine("Previse argumenata za komandu 'create' !");



                else if ((argumenti[0].Equals("list") && korisnik.IsLogged() && (Provjere.BrojacRijeci(argumenti) == 2 || Provjere.BrojacRijeci(argumenti) == 1)))
                {
                    if (Provjere.BrojacRijeci(argumenti) == 1)
                    {
                        int counter = 0;
                        Korisnik.ListIspisi(korisnik.GetUserDir(), counter);
                    }
                    else if (Provjere.BrojacRijeci(argumenti) == 2)
                    {
                        if (Directory.Exists(argumenti[1]))
                        {
                            int counter = 0;
                            Korisnik.ListIspisi(argumenti[1], counter);
                        }
                        else
                        {
                            Console.WriteLine("Trazeni direktorijum ne postoji!");
                        }
                    }
                }
                else if ((korisnik.IsLogged() && Provjere.BrojacRijeci(argumenti) > 2) && argumenti[0].Equals("list"))
                    Console.WriteLine("Previse argumenata za komandu list");




                else if (korisnik.IsLogged() && Provjere.BrojacRijeci(argumenti) == 2 && argumenti[0].Equals("print"))
                {
                    Korisnik.PrintTextFajl(argumenti[1]);
                }
                else if (korisnik.IsLogged() && Provjere.BrojacRijeci(argumenti) == 1 && argumenti[0].Equals("print"))
                    Console.WriteLine("Nedovoljno argumenata za komandu Print, potrebno je navesti naziv datoteke");
                else if (korisnik.IsLogged() && Provjere.BrojacRijeci(argumenti) > 2 && argumenti[0].Equals("print"))
                    Console.WriteLine("Previse argumenata za komandu 'Print'");




                else if (korisnik.IsLogged() && (Provjere.BrojacRijeci(argumenti)) >= 3 && argumenti[0].Equals("find"))
                {
                    string tekst = Provjere.vratiText(argumenti);
                    string datoteka = argumenti.Last();
                    Korisnik.Find(tekst, datoteka);
                }
                else if (korisnik.IsLogged() && (Provjere.BrojacRijeci(argumenti)) < 3 && argumenti[0].Equals("find"))
                    Console.WriteLine("Nedovoljno argumenata za komandu find");





                else if (korisnik.IsLogged() && (Provjere.BrojacRijeci(argumenti) == 3 && argumenti[0].Equals("finddat")))
                {
                   
                        bool nadjeno = false;
                        Korisnik.FindDat(argumenti[1], argumenti[2], ref nadjeno);
                        if (!nadjeno)
                        {
                            Console.WriteLine("Datoteka nije nadjena!");
                        }
                    

                }
                else if ((Provjere.BrojacRijeci(argumenti) == 2 || Provjere.BrojacRijeci(argumenti) == 1) && argumenti[0].Equals("finddat") && korisnik.IsLogged())
                    Console.WriteLine("Funkcija podrazumjeva tri parametra, pokusajte:\n finddat (datoteka) (putanja)");
                else if ((Provjere.BrojacRijeci(argumenti) > 3 && korisnik.IsLogged() && argumenti[0].Equals("finddat")))
                    Console.WriteLine("Previse argumenata za komandu FindDat");


                else
                {
                    if (Provjere.AlreadyLoggedIn(input, korisnik))
                    {
                        Console.WriteLine("Korisnik je vec prijavljen.Prvo se odjavite pomocu logout.");
                        Console.WriteLine("Da li zelite da se odjavite (DA/NE)");
                        string izbor = Console.ReadLine().ToUpper().Trim();
                        if (izbor.Equals("DA"))
                        { korisnik.Logout(); Console.WriteLine("Korisnik je odjavljen."); }
                        else if (izbor.Equals("NE")) Console.WriteLine("Korisnik nije odjavljen.");
                        else
                            Console.WriteLine("Nepostojeca opcija. Pokusajte ponovo.");
                    }
                    else if (Provjere.TrebaPrijava(argumenti, korisnik))
                    {
                        Console.WriteLine("Korisnik se mora prijaviti da bi koristio ovu komandu.\nPrijavite se pomocu 'login'");
                    }
                    else if (!argumenti[0].Equals("exit") && Provjere.BrojacRijeci(argumenti) >= 1) Console.WriteLine("Nepostojeca komanda. Pokusajte ponovo!");
                }
            } while (!input.ToLower().Equals("exit"));
        }
    }
}