using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Projekat_MK
{
    internal class Korisnik
    {
        private bool loggedin;
        private string password;
        private string username;
        private string izbor;
        private string userDir;

        public Korisnik()
        {
            loggedin = false;
            password = null;
            username = null;
            izbor = null;
            userDir = null;
        }

        public bool IsLogged()
        {
            return loggedin;
        }

        public void Logout()
        {
            loggedin = false;
            password = null;
            username = null;
            izbor = null;
            userDir = null;
        }

        public void Login()
        {
            string filePath = @"..\korisnici.txt";
            List<string> linije = File.ReadAllLines(filePath).ToList();
            while (true)
            {
                Console.Write("Unesi username: "); username = Console.ReadLine();
                foreach (string line in linije)
                {
                    string[] tmp = line.Split(";");
                    if (tmp[0].Equals(username))
                    {
                        Console.Write("Unesi password: "); password = Console.ReadLine();
                        if (tmp[1].Equals(password)) { Console.WriteLine("PRIJAVA JE USPJESNA!"); loggedin = true; break; }
                    }
                }
                if (loggedin == true)
                {
                    userDir = Directory.GetCurrentDirectory();// Console.WriteLine(userDir);
                    userDir = Directory.GetParent(userDir).ToString();
                    userDir = userDir + @"\" + username;
                    Directory.CreateDirectory(userDir);
                    Directory.SetCurrentDirectory(userDir);
                    break;
                }
                if (loggedin == false)
                {
                    Console.WriteLine("Neuspjesno prijavljivanje. Pokusajte ponovo? (DA/NE)");
                    izbor = Console.ReadLine().ToUpper().Trim();
                    if (izbor.Equals("NE"))
                        return;
                    else if (izbor.Equals("DA"))
                    { loggedin = false; continue; }
                    else break;
                }
            }
        }

        public void Where_GetUserDir()
        {
            Console.WriteLine(userDir);
        }

        public string GetUserDir()
        {
            return userDir;
        }

        public void Go(string newDirectory)
        {
            if (Directory.Exists(newDirectory))
            {
                userDir = Directory.CreateDirectory(newDirectory).FullName;
                Directory.SetCurrentDirectory(userDir);
                Console.WriteLine("go je uspjesno izvrsen!");
            }
            else if (newDirectory.Contains("-"))
                Console.WriteLine("Nepostojeci parametar!");
            else
                Console.WriteLine("Nepostojeci direktorijum!");
        }
        public void CreateFileorDir(string[] argumenti)
        {
            if (Provjere.BrojacRijeci(argumenti) == 2)
            {
                if (argumenti[1].Equals("-d"))
                {
                    Console.WriteLine("Nepotpun argument, unesite naziv ili lokaciju!");
                    return;
                }
                else
                {
                    string pom = argumenti[1].Split(@"\").Last();
                    //  if (Directory.Exists(argumenti[1]) || Directory.Exists(Directory.GetCurrentDirectory())) //{
                    try {
                        if (Path.HasExtension(argumenti[1]))
                            Console.WriteLine("Kreiran je fajl {0} na lokaciji {1}", pom, File.Create(argumenti[1]).Name);
                    
                   
                        else
                            Console.WriteLine("Kreiran je fajl {0}.dat na lokaciji {1}", pom, File.Create(argumenti[1] + ".dat").Name);
                        // } // else // Console.WriteLine("Ne moze se kreirati fajl, direktorijum ne postoji");
                    }
                    catch (System.IO.DirectoryNotFoundException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            else if (Provjere.BrojacRijeci(argumenti) == 3)
            {
                if (argumenti[1].Equals("-d"))
                {
                    string pom = argumenti[2].Split(@"\").Last();
                    Console.WriteLine("Kreiran je direktorijum {0}", Directory.CreateDirectory(argumenti[2]).FullName);
                }
                else
                    Console.WriteLine("Nepoznat parametar {0}, probajte ponovo! Koristite:\ncreate -d (naziv direktorijuma) - za kreiranje direktorijuma ili\ncreate (nazicDatoteke) - za kreiranje datoteke", argumenti[1]);
            }
        }

        public static void ListIspisi(string sDir, int brojac)
        {
            try
            {
                foreach (string dir in Directory.GetDirectories(sDir))
                {
                    int temp = brojac;

                    while (temp > 0)
                    { 
                        temp--;
                        Console.Write("\t");
                    }
                    Console.WriteLine(dir.Split(@"\").Last());

                    ListIspisi(dir, brojac + 1);
                }

                foreach (string f in Directory.GetFiles(sDir))
                {
                    int temp2 = brojac;
                    while (temp2 > 0) 
                    {
                        temp2--;
                        Console.Write("\t");
                    }
                    Console.WriteLine(Path.GetFileName(f));
                }
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void PrintTextFajl(string fajlDir)
        {
            bool nadjenPrviDio = false; //bice true ako postoji fajl ciji je naziv sve do ekstenzije isti kao fajlDir
            bool drugiDioJeTxt = false; //ako je ekstenzija na inputu .txt postace true
            try { 
            foreach (string f in Directory.GetFiles(Path.GetDirectoryName(Path.GetFullPath(fajlDir)))) //vraca direktorijum u kom se nalazi trazeni fajl i vrti for petlju za svaki fajl
            {
                string pom = Path.GetFileNameWithoutExtension(f).ToLower();
                // Console.WriteLine(pom);
                if (pom.Equals(Path.GetFileNameWithoutExtension(fajlDir).ToLower()))
                {
                    nadjenPrviDio = true;
                    if (Path.HasExtension(fajlDir) && ((Path.GetExtension(fajlDir).ToLower()).Equals(".txt"))) //i onda provjeravamo da li postoji drugi dio i ako postoji da li je on txt
                    {
                        drugiDioJeTxt = true;
                    }
                }
            }
            }
            catch(System.IO.DirectoryNotFoundException)
            {
               // Console.WriteLine(e.Message);
            }
            if (!Path.HasExtension(fajlDir))
            {
                fajlDir = fajlDir + ".txt"; //default pretragu vrsimo po txt fajlovima
            }
            else if ((Path.GetExtension(fajlDir).ToLower()).Equals(".txt"))
                drugiDioJeTxt = true;
            if (File.Exists(fajlDir)) //ako postoji datoteka ili postoji datoteka + .txt (u slucaju da se unese datoteka bez ekstenzije)
            {
                if (!(Path.GetExtension(fajlDir).ToLower()).Equals(".txt"))
                { Console.WriteLine("Navedena datoteka nije tekstualna!"); return; } //slucaj kada datoteka postoji ali nije tekstualna
                try { 
                using (var readtext = new StreamReader(fajlDir))
                {
                    while (!readtext.EndOfStream)
                    {
                        string currentLine = readtext.ReadLine();

                        if (currentLine.Length > 0)
                        {
                            Console.WriteLine(currentLine);
                        }
                        else
                        {
                            Console.WriteLine("");
                        }
                    }
                }
                }
                catch(System.IO.IOException e)
            {
                    System.Console.WriteLine(e.Message);
                    return;
            }
        
                return;
            }
            else if ((nadjenPrviDio) && (!drugiDioJeTxt))
            { Console.WriteLine("Navedena datoteka nije tekstualna!"); return; }
            else Console.WriteLine("Navedena datoteka ne postoji!");
        }

        public static void Find(string tekst, string datotekaDir)
        {
            bool nadjeno = false;

            if (!Path.HasExtension(datotekaDir))
            {
                datotekaDir = datotekaDir + ".txt";
                if (File.Exists(datotekaDir))
                {
                    int brojac = 0;
                    using (var readtext = new StreamReader(datotekaDir))
                    {
                        while (!readtext.EndOfStream)
                        {
                            string currentLine = readtext.ReadLine();
                            brojac++;
                            if (currentLine.Length >= 0)
                            {
                                if (currentLine.Contains(tekst))
                                {
                                    Console.WriteLine("Broj linije u kojoj se tekst nalazi je {0}", brojac);
                                    nadjeno = true;
                                }
                            }
                            else { Console.WriteLine("Nepravilna linija! Tekst nije pronadjen u datoteci!"); }
                        }
                        if (nadjeno)
                            return;
                         else { Console.WriteLine("Tekst nije pronadjen u datoteci!");   return;}

                    }
                }
                else { Console.WriteLine("Trazena datoteka ne postoji!"); return; }
            }
            else if (datotekaDir.Split(".").Last().Equals("txt"))
            {
                if (File.Exists(datotekaDir))
                {
                    int brojac = 0;
                    using (var readtext = new StreamReader(datotekaDir))
                    {
                        while (!readtext.EndOfStream)
                        {
                            string currentLine = readtext.ReadLine();
                            brojac++;
                            if (currentLine.Length >= 0)
                            {
                                if (currentLine.Contains(tekst))
                                {
                                    Console.WriteLine("Broj linije u kojoj se tekst nalazi je {0}", brojac);
                                    nadjeno = true;
                                }
                            }
                            else { Console.WriteLine("Nepravilna Linija! Tekst nije pronadjen u datoteci!"); }
                        }
                        if (nadjeno)
                            return;
                        else { Console.WriteLine("Tekst nije pronadjen u datoteci!"); return; }
                    }
                }
                else if (!File.Exists(datotekaDir))
                {
                    Console.WriteLine("Datoteka ne postoji!");
                    return;
                }
            }
            else if (!datotekaDir.Split(".").Last().Equals("txt"))
            {
                Console.WriteLine("Datoteka nije tekstualna");
                return;
            }
            else { Console.WriteLine("Datoteka nije tekstualna"); return; }
            Console.WriteLine("Tekst nije pronadjen u datoteci!");
            return;
        }
        
        public static void FindDat(string nazivDat, string putanja, ref bool nadjeno)
        {
            try
            {

                foreach (string d in Directory.GetDirectories(putanja))
                {
                    FindDat(nazivDat, d, ref nadjeno);
                }
                foreach (string f in Directory.GetFiles(putanja))
                {
                    if (Path.HasExtension(nazivDat))
                    {
                        if (Path.GetFileName(f).ToLower().Equals(nazivDat.ToLower()))
                        {
                            Console.WriteLine("Nadjeno na lokaciji: {0}", Path.GetFullPath(f));
                            nadjeno = true;
                        }
                    }
                    else
                    {
                        if ((Path.GetFileName(f).ToLower().Split(".").First()).Equals(nazivDat.ToLower()))
                        {
                            Console.WriteLine("Nadjeno na lokaciji: {0}", Path.GetFullPath(f));
                            nadjeno = true;
                        }
                    }

                }
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
