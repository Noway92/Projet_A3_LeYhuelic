using MySql.Data.MySqlClient;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Projet_A3_LeYhuelic
{
    /// <summary>
    /// Implémente IComparable pour pouvoir Utiliser la méthode List.Sort avce la méthode CompareTo
    /// </summary>
    public class Client : Personne,IComparable<Client>
    {
        private string mdp;
        private string statut;
        // La création client et les achats cummulés ne seront pas dans le ToString()
        private double achat_cummulé;
        private DateTime creation_client;
        private int typecomparaison;
        //2 constructeurs différents
        public Client(DateTime creation_client,double achat, string statut,string mdp, string noSS, string nom, string prenom, DateTime date_de_naissance, string telephone, Adresse adresse_postale, string mail) : base(noSS, nom, prenom, date_de_naissance, telephone, adresse_postale, mail)
        {
            this.achat_cummulé = achat;
            this.mdp=mdp;
            this.statut=statut;
            this.creation_client = creation_client;
            typecomparaison = 0;
        }

        public Client():base()
        {
            this.mdp = "";
            statut = "";
            achat_cummulé = 0;
            creation_client = DateTime.Now;
            typecomparaison = 0;
        }
        public string Mdp { get { return mdp; } set { mdp = value; } }
        public string Statut { get { return statut; } set { statut = value; } }
        public double Achat_Cumulé { get { return achat_cummulé; } set { achat_cummulé = value; } }
        public DateTime Creation_Client { get { return creation_client; } set { creation_client = value; } }

        public int TypeComparaison {get { return typecomparaison; } set { typecomparaison = value; }}

        /// <summary>
        /// Fonction utile pour afficher les clients dans une statistique
        /// </summary>
        /// <returns></returns>
        public string ShortToString()
        {
            return $"Nom: {nom},Prenom: {prenom},Mail: {mail},Numéro_De_Sécu : {noSS}";
        }

        /// <summary>
        /// ToString() principale
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if(statut ==""|| statut==null) return base.ToString() + "Montant achat cumulé : " + achat_cummulé + "\nStatut du client : CLASSIQUE";
            else return base.ToString() + "Montant achat cumulé : " + achat_cummulé + "\nStatut du client :" + statut;

        }
        /// <summary>
        /// Création du client ou connexion du client
        /// </summary>
        /// <param name="connexion"></param>
        public void Creer_Client(MySqlConnection connexion)
        {
            int rep = 0;
            do
            {
                Console.WriteLine("Voulez vous creer un compte (tapez 1)\nVoulez vous vous connecter (tapez 2)");
                try
                {
                    rep = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            } while (rep != 1 && rep != 2);
            // rep = 1 creer un compte
            //rep = 2 connecter un compte
            Console.Clear();
            switch (rep)
            {

                case 1:
                    //Toutes les demandes pour creer le client
                    Console.WriteLine("Donnez votre adresse email : ");
                    Mail = Console.ReadLine();
                    Console.WriteLine("Donnez votre mot de passe : ");
                    mdp = Console.ReadLine();
                    Console.WriteLine("Donnez votre prénom : ");
                    Prenom = Console.ReadLine();
                    Console.WriteLine("Donnez votre nom : ");
                    Nom = Console.ReadLine();
                    Console.WriteLine("Donnez votre numéro de téléphone : ");
                    Telephone = Console.ReadLine();
                    Console.WriteLine("Donnez votre rue, ville, codePostal, pays : ");
                    string[] tab = Console.ReadLine().Split(',');
                    Adresse_Postale.Rue = tab[0];
                    Adresse_Postale.Ville = tab[1];
                    Adresse_Postale.CodePostal = tab[2];
                    Adresse_Postale.Pays = tab[3];
                    Console.WriteLine("Donnez votre date de naissance DD/MM/AAAA");
                    string[] tab2 = Console.ReadLine().Split('/');
                    date_de_naissance = new DateTime(Convert.ToInt32(tab2[2]), Convert.ToInt32(tab2[1]), Convert.ToInt32(tab2[0]));
                    Console.WriteLine("Donnez votre numéro de Sécu");
                    noSS = Console.ReadLine();

                    MySqlCommand command1 = connexion.CreateCommand();


                    bool verif = false;

                    while (verif == false)
                    {
                        //Commande SQL pour prendre les mail de tous les clients
                        //Peut être remplacé par une délégation c# si on a la liste des clients en paramètres
                        command1.CommandText = " SELECT mail FROM CLIENT ;";
                        MySqlDataReader reader1 = command1.ExecuteReader();
                        verif = true;
                        while (reader1.Read() && verif == true)// parcourt ligne par ligne
                        {
                            //il ne faut pas les "" si on fait une égalité
                            if (Mail == reader1.GetString(0))
                            {
                                verif = false;
                            }

                        }
                        //Au cas où la personne s'est trompée
                        reader1.Close();

                        // On vérifie que le client ne donne pas un mail déjà utilisé dans la base de donnée et donc dans notre futur liste de client
                        if (verif == false)
                        {
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.Clear();
                            // On attend 1 seconde pour pouvoir laisser l'écran rouge
                            DateTime attendre = DateTime.Now;
                            while ((DateTime.Now - attendre).TotalSeconds < 1)
                            {

                            }
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.Clear();
                            Console.WriteLine("L'adresse est déjà utilisée");

                            Console.WriteLine("Donnez une autre adresse email : ");
                            Mail = Console.ReadLine();
                            Console.WriteLine("Donnez un autre mot de passe : ");
                            mdp = Console.ReadLine();
                        }
                        creation_client = DateTime.Now;

                        if (verif == true)
                        {
                            // On rentre toutes les données du clients dans la BDD sur SQL
                            MySqlCommand command3 = connexion.CreateCommand();
                            command3.CommandText = " INSERT INTO CLIENT VALUES(@mdp,@noSS,@nom,@prenom,@date_de_naissance,@telephone,@adresse_postale,@mail,@statut,@achat,@creation_client);";
                            command3.Parameters.AddWithValue("@mail", Mail);
                            command3.Parameters.AddWithValue("@nom", Nom);
                            command3.Parameters.AddWithValue("@prenom", Prenom);
                            command3.Parameters.AddWithValue("@telephone", Telephone);
                            command3.Parameters.AddWithValue("@mdp", mdp);
                            command3.Parameters.AddWithValue("@noSS", noSS);
                            command3.Parameters.AddWithValue("@date_de_naissance", date_de_naissance);
                            command3.Parameters.AddWithValue("@statut", statut);
                            command3.Parameters.AddWithValue("@achat", achat_cummulé);
                            command3.Parameters.AddWithValue("@creation_client", creation_client);
                            //On met ToString() car on veut un string
                            command3.Parameters.AddWithValue("@adresse_postale", adresse_postale.ToString());
                            command3.ExecuteNonQuery();
                        }

                    }

                    break;
                case 2:
                    // Tester si le client existe
                    bool test = false;
                    MySqlCommand command2 = connexion.CreateCommand();
                    do
                    {
                        // On demande les 2 infos nécessaires pour se connecter
                        Console.WriteLine("Donnez votre adresse email : ");
                        Mail = Console.ReadLine();
                        Console.WriteLine("Donnez votre mot de passe : ");
                        mdp = Console.ReadLine();
                        // On SELECT tout pour pouvoir instancier le client si il se connecte
                        command2.CommandText = " SELECT mdp, noSS, nom, prenom, date_de_naissance, telephone, adresse_postale, mail,statut,achat,creation_client FROM CLIENT WHERE mail='" + Mail + "'AND mdp='" + mdp + "';";
                        MySqlDataReader reader2 = command2.ExecuteReader();
                        // Si le mdp et le mail son bien dans la BDD test = true
                        while (reader2.Read())
                        {
                            test = true;
                        }
                        if (test == false)
                        {
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.Clear();
                            // On attend 1 secondes pour pouvoir laisser l'écran rouge
                            DateTime attendre = DateTime.Now;
                            while ((DateTime.Now - attendre).TotalSeconds < 1)
                            {


                            }
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.Clear();
                        }
                        if (test == true)
                        {
                            // Il faut instancier le client lorsqu'il se connecte
                            mdp = reader2.GetString(0);
                            noSS = reader2.GetString(1);
                            nom = reader2.GetString(2);
                            prenom = reader2.GetString(3);
                            date_de_naissance = reader2.GetDateTime(4);
                            telephone = reader2.GetString(5);
                            string[] adresse = reader2.GetString(6).Split(",");
                            adresse_postale = new Adresse(adresse[0], adresse[1], adresse[2], adresse[3]);
                            mail = reader2.GetString(7);
                            statut = reader2.GetString(8);
                            achat_cummulé = reader2.GetDouble(9);
                            creation_client = reader2.GetDateTime(10);

                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.Clear();
                            // On attend 1 secondes pour pouvoir laisser l'écran vert
                            DateTime attendre = DateTime.Now;
                            while ((DateTime.Now - attendre).TotalSeconds < 1)
                            {


                            }
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.Clear();

                        }
                        reader2.Close();


                    } while (test == false);
                    break;

            }
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
        }

        /// <summary>
        /// Implémentation de la méthode CompareTo de l'interface Icomparable
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public int CompareTo(Client c)
        {
            
            if (c == null) return 1;
            // Compare en fonction du Nom 
            if (typecomparaison==1) return Nom.CompareTo(c.Nom);
            // Compare en fonction de la ville
            if (typecomparaison==2) return Adresse_Postale.Ville.CompareTo(c.Adresse_Postale.Ville);
            // Compare en fonction du total des achats du client
            if (typecomparaison == 3) return achat_cummulé.CompareTo(c.Achat_Cumulé);
            // Compare en fonction du Nom Puis de la ville si ils ont le même nom Puis du montant total dépensé si ils ont la même villr
            if (Nom!=c.Nom) return Nom.CompareTo(c.Nom);
            if(Adresse_Postale.Ville!=c.Adresse_Postale.Ville) return Adresse_Postale.Ville.CompareTo(c.Adresse_Postale.Ville);
            return achat_cummulé.CompareTo(c.Achat_Cumulé);
        }

        /// <summary>
        /// Création des opérateurs pour pouvoir comparer les clietns directement en utilisant == ou !=
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(Client a, Client b)
        {
            if (a is null && b is null) return true;
            if (a is null || b is null)
                return false;
            //Clé primaire
            return a.Mail == b.Mail;
        }
        public static bool operator !=(Client a, Client b)
        {
            return !(a == b);
        }



    }
}
