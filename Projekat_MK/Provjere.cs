using System;
using System.Collections.Generic;
using System.Text;

namespace Projekat_MK
{
    internal class Provjere
    {
        public static int BrojacRijeci(string[] linija)
        {
            int br = 0;
            foreach (string rijec in linija)
            {
                br++;
            }
            return br;
        }

        public static bool AlreadyLoggedIn(string input, Korisnik user)
        {
            if (input.Equals("login") && user.IsLogged())
                return true;
            else
                return false;
        }
        public static string vratiText(string[] argumenti) 
        {
            int brRijeci;

            string text = null;
            brRijeci = BrojacRijeci(argumenti);
            for (int i = 0; i < brRijeci - 2; i++)
            {
                text = text + " " + argumenti[i + 1];
            }
            return text.Trim();
        }


        public static bool TrebaPrijava(string[] argumenti, Korisnik user)
        {
            bool ima_komanda = false;

            string[] lista_komandi = new string[] { "where", "go", "create", "list",
                "print", "find", "finddat", "logout" };

            foreach (string argument in lista_komandi)
            {
                if (argumenti[0] == argument)
                    ima_komanda = true;
            }
            if (!user.IsLogged() && ima_komanda)
                return true;
            else
            {
                return false;
            }
        }
    }
}
