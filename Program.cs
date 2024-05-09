//Ajouter la connexion avec SQL ajouter dans les références 
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Net.Sockets;
using System.Numerics;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;



namespace Projet_A3_LeYhuelic
{
    public class Program
    {
        static void Main(string[] args)
        {
            

            Console.ForegroundColor = ConsoleColor.Red;
            string[] bienvenue = {
            "BBBB   III  EEEEE  NN   NN  V   V  EEEEE  NN   NN  U   U  EEEEE         CCCCC  HH  HH  EEEEE  ZZZZZZ  ",
            "B   B   I   E      N N  NN  V   V  E      N N  NN  U   U  E            C       HH  HH  E         ZZ   ",
            "BBBB    I   EEEE   N  N NN  V   V  EEEE   N  N NN  U   U  EEEE         C       HHHHHH  EEEE     ZZ    ",
            "B   B   I   E      N   NNN   V V   E      N   NNN  U   U  E            C       HH  HH  E       ZZ     ",
            "BBBB   III  EEEEE  N    NN    V    EEEEE  N    NN   UUU   EEEEE         CCCCC  HH  HH  EEEEE  ZZZZZZ   "
            };

            string[] transconnect = {
            "TTTTTTTT  RRRRRR      AAA    NN   NN  SSSSSS   CCCC  OOOOOO  NN   NN  NN   NN  EEEEE   CCCC  TTTTTTTT ",
            "   TT     R     R    A   A   N N  NN  SS      C      O    O  N N  NN  N N  NN  E      C          TT    ",
            "   TT     RRRRRR     AAAAA   N  N NN  SSSSS   C      O    O  N  N NN  N  N NN  EEEE   C          TT    ",
            "   TT     R     R   A     A  N   NNN      SS  C      O    O  N   NNN  N   NNN  E      C          TT    ",
            "   TT     R      R  A     A  N    NN  SSSSSS   CCCC  OOOOOO  N    NN  N    NN  EEEEE   CCCC      TT    "
            };

            // Affichage de chaque mot
            foreach (string line in bienvenue)
            {
                Console.WriteLine(line);
            }

            Console.WriteLine(); // Ligne vide entre les mots

            Console.WriteLine(); // Ligne vide entre les mots

            foreach (string line in transconnect)
            {
                Console.WriteLine(line);
            }
            Console.ResetColor();
            Console.WriteLine("\n\n\n\n\n\n");
            MySqlConnection maConnexion = null;
            try
            {
                string connectionString = "SERVER=localhost;PORT=3306;DATABASE=Projet_SQL;UID=root;PASSWORD=#TON MDP";
                maConnexion = new MySqlConnection(connectionString);
                maConnexion.Open();
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Erreur de connexion : " + e.Message);
                return;
            }
            string type = "";
            do
            {
                Console.WriteLine("Etes vous un client ou le patron ?");
                type = Console.ReadLine();
                if (type == "patron")
                {
                    Console.WriteLine("Donnez votre mot de passe de patron : ");
                    string mdp_patron = Console.ReadLine();
                    if (mdp_patron != "patron1234")
                    {
                        Console.WriteLine("Vous vous êtes trompé, nous vous redirigeons au début");
                        type = "";
                    }
                    Console.Clear();
                }

            } while (type != "client" && type != "patron");
            Console.Clear();
            Entreprise TransConnect = new Entreprise(maConnexion);
            switch (type)
            {
                // PARTIE CLIENT 
                case "client":
                    Client a = new Client();
                    a.Creer_Client(maConnexion);
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("Voulez vous passer une commande(1)\nModifier votre compte client(2)\nSupprimer votre compte client(3)");
                    int rep3 = Convert.ToInt32(Console.ReadLine());
                    Console.Clear();
                    switch(rep3)
                    {
                        case 1:
                            Commande b = new Commande(a);
                            b.Creer_Commande(maConnexion, TransConnect.Vehicule, TransConnect.Salaries, TransConnect.Commandes);
                            break;
                        case 2:
                            Console.Clear();
                            TransConnect.Modifier_Client(a, maConnexion);
                            break;

                        case 3:
                            Console.WriteLine("Etes vous sure de vous, cela serait définitif.");
                            if(Console.ReadLine().ToLower()=="oui") TransConnect.Supprimer_Client(a, maConnexion);
                            break;
                    }
                    Console.Clear();
                    break;
                //PARTIE patron
                case "patron":
                    string rep = "";
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("Voulez vous regarder les statistiques de transconnect(1)\nAfficher l'organigramme des salarié(2)\nSupprimer un salarié(3)\nAjouter un salarié(4)\nAfficher les Clients(5)");
                        int rep2 = Convert.ToInt32(Console.ReadLine());
                        Console.Clear();
                        switch(rep2)
                        {
                            case 1:
                                TransConnect.statistiques();
                                break;
                            case 2:
                                TransConnect.Salaries.Afficher_Organigramme(TransConnect.Salaries.Racine);
                                Console.WriteLine("\n");
                                break;
                            case 3:
                                Salarie supp = TransConnect.Salaries.Find(TransConnect.Salaries.Racine, x => x.Mail == "mr.picsou@example.com");
                                TransConnect.Salaries.Supprimer_Salarie(TransConnect.Salaries.Racine, supp);
                                break;
                            case 4:
                                Console.WriteLine("Donner le Nom,Prenom,Mail,Poste du nouveau Salarié");
                                string[]inter = Console.ReadLine().Split(",");
                                Salarie newsalarie = new Salarie(inter[2], inter[3], inter[0], inter[1]);
                                Console.WriteLine("Donner le mail du chef de ce salarie");
                                string mailchef = Console.ReadLine();
                                Salarie chef = TransConnect.Salaries.Find(TransConnect.Salaries.Racine, x => x.Mail == mailchef);
                                TransConnect.Salaries.Ajouter_Salarie(TransConnect.Salaries.Racine,chef, newsalarie);           
                                break;
                            case 5:
                                TransConnect.Afficher_Clients();
                                break;

                        }
                        
                        Console.WriteLine("Voulez vous continuer ? (oui/non)");
                        rep = Console.ReadLine().ToLower();                        
                    } while (rep != "non");

                    break;
            }
            Console.WriteLine("SEE YOU SOON !!");
            maConnexion.Close();                  
            Console.ReadKey();
        }


       

        


    }
}
