using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Projet_A3_LeYhuelic
{
    /// <summary>
    /// Classe qui va récupérer tout les clients, voitures,  salariés et commandes
    /// </summary>
    public class Entreprise : IStatistiques
    {

        private ArbreNaireSalarie salaries;
        private List<Vehicule> vehicules;
        private List<Client> clients;
        private List<Commande> commandes;

        public Entreprise(MySqlConnection connexion)
        {
            clients = new List<Client>();
            vehicules = new List<Vehicule>();
            salaries = new ArbreNaireSalarie();
            commandes = new List<Commande>();
            // On crée nos adresses pour les salariés

            //Création des salariés avec les adresses correspondantes
            // Création des adresses pour chaque salarié
            Adresse adresse1 = new Adresse("123 rue de la Liberté", "Villebourg", "12345", "France");
            Adresse adresse2 = new Adresse("456 avenue de la République", "Villefort", "54321", "France");
            Adresse adresse3 = new Adresse("789 boulevard des Fleurs", "Villeneuve", "98765", "France");
            Adresse adresse4 = new Adresse("321 chemin du Soleil", "Villesoleil", "54321", "France");
            Adresse adresse5 = new Adresse("654 rue de la Paix", "Villepaix", "12345", "France");
            Adresse adresse6 = new Adresse("987 avenue des Champs", "Villechamps", "98765", "France");
            Adresse adresse7 = new Adresse("741 chemin de la Montagne", "Villehaute", "54321", "France");
            Adresse adresse8 = new Adresse("852 rue du Lac", "Villelac", "12345", "France");
            Adresse adresse9 = new Adresse("963 avenue de la Plage", "Villeplage", "98765", "France");
            Adresse adresse10 = new Adresse("147 boulevard des Vents", "Villevent", "54321", "France");
            Adresse adresse11 = new Adresse("258 rue du Soleil", "Villesoleil", "12345", "France");
            Adresse adresse12 = new Adresse("369 avenue de la Lune", "Villelune", "98765", "France");
            Adresse adresse13 = new Adresse("159 boulevard de la Mer", "Villemer", "54321", "France");
            Adresse adresse14 = new Adresse("753 rue de la Montagne", "Villemontagne", "12345", "France");
            Adresse adresse15 = new Adresse("852 avenue de la Rivière", "Villeriviere", "98765", "France");
            Adresse adresse16 = new Adresse("963 boulevard des Étoiles", "Villeetoile", "54321", "France");
            Adresse adresse17 = new Adresse("147 rue de la Forêt", "Villeforet", "12345", "France");
            Adresse adresse18 = new Adresse("258 avenue des Rochers", "Villerocher", "98765", "France");
            Adresse adresse19 = new Adresse("369 boulevard de la Cascade", "Villecascade", "54321", "France");
            Adresse adresse20 = new Adresse("456 avenue de la Fortune", "Villefortune", "54321", "France");

            //Création des salariés avec des adresses et des dates d'entrée et de naissance différentes
            Salarie salarie1 = new Salarie(new DateTime(2010, 10, 15), "Directeur Général", 0, "1234567890123", "Dupond", "Mr", new DateTime(1960, 1, 1), "0123456789", adresse1, "mr.dupond@example.com");
            Salarie salarie2 = new Salarie(new DateTime(2012, 3, 20), "Directrice Commerciale", 0, "1234567890123", "Fiesta", "Mme", new DateTime(1972, 2, 2), "0123456789", adresse2, "mme.fiesta@example.com");
            Salarie salarie3 = new Salarie(new DateTime(2015, 7, 8), "Commercial", 0, "1234567890123", "Forge", "Mr", new DateTime(1980, 3, 3), "0123456789", adresse3, "mr.forge@example.com");
            Salarie salarie4 = new Salarie(new DateTime(2018, 12, 12), "Commerciale", 0, "1234567890123", "Fermi", "Mme", new DateTime(1985, 4, 4), "0123456789", adresse4, "mme.fermi@example.com");
            Salarie salarie5 = new Salarie(new DateTime(2019, 5, 1), "Directeur des opérations", 0, "1234567890123", "Fetard", "Mr", new DateTime(1990, 5, 5), "0123456789", adresse5, "mr.fetard@example.com");
            Salarie salarie6 = new Salarie(new DateTime(2020, 8, 15), "Chef Equipe", 0, "1234567890123", "Royal", "Mr", new DateTime(1995, 6, 6), "0123456789", adresse6, "mr.royal@example.com");
            Salarie salarie7 = new Salarie(new DateTime(2017, 4, 2), "Chauffeur", 0, "1234567890123", "Romu", "Mr", new DateTime(2000, 7, 7), "0123456789", adresse7, "mr.romu@example.com");
            Salarie salarie8 = new Salarie(new DateTime(2022, 9, 30), "Chauffeur", 0, "1234567890123", "Romi", "Mme", new DateTime(2005, 8, 8), "0123456789", adresse8, "mme.romi@example.com");
            Salarie salarie9 = new Salarie(new DateTime(2023, 11, 10), "Chauffeur", 0, "1234567890123", "Roma", "Mr", new DateTime(2010, 9, 9), "0123456789", adresse9, "mr.roma@example.com");
            Salarie salarie10 = new Salarie(new DateTime(2024, 6, 5), "Chef d'Equipe", 0, "1234567890123", "Prince", "Mme", new DateTime(2015, 10, 10), "0123456789", adresse10, "mme.prince@example.com");
            Salarie salarie11 = new Salarie(new DateTime(2023, 2, 20), "Chauffeur", 0, "1234567890123", "Rome", "Mme", new DateTime(2020, 11, 11), "0123456789", adresse11, "mme.rome@example.com");
            Salarie salarie12 = new Salarie(new DateTime(2022, 1, 5), "Chauffeur", 0, "1234567890123", "Rimou", "Mme", new DateTime(2025, 12, 12), "0123456789", adresse12, "mme.rimou@example.com");
            Salarie salarie13 = new Salarie(new DateTime(2021, 3, 18), "Directrice des RH", 0, "1234567890123", "Joyeuse", "Mme", new DateTime(1960, 1, 1), "0123456789", adresse13, "mme.joyeuse@example.com");
            Salarie salarie14 = new Salarie(new DateTime(2020, 7, 1), "Formation", 0, "1234567890123", "Couleur", "Mme", new DateTime(1970, 2, 2), "0123456789", adresse14, "mme.couleur@example.com");
            Salarie salarie15 = new Salarie(new DateTime(2019, 10, 25), "Contrats", 0, "1234567890123", "ToutleMonde", "Mme", new DateTime(1965, 3, 3), "0123456789", adresse15, "mme.toutlemonde@example.com");
            Salarie salarie16 = new Salarie(new DateTime(2018, 5, 10), "Directeur Financier", 0, "1234567890123", "GripSous", "Mr", new DateTime(1975, 4, 4), "0123456789", adresse16, "mr.gripsous@example.com");
            Salarie salarie17 = new Salarie(new DateTime(2017, 8, 22), "Direction comptable", 0, "1234567890123", "Picsou", "Mr", new DateTime(1980, 5, 5), "0123456789", adresse17, "mr.picsou@example.com");
            Salarie salarie18 = new Salarie(new DateTime(2016, 9, 14), "Comptable", 0, "1234567890123", "Fournier", "Mme", new DateTime(1985, 6, 6), "0123456789", adresse18, "mme.fournier@example.com");
            Salarie salarie19 = new Salarie(new DateTime(2015, 4, 30), "Comptable", 0, "1234567890123", "Gautier", "Mme", new DateTime(1990, 7, 7), "0123456789", adresse19, "mme.gautier@example.com");
            Salarie salarie20 = new Salarie(new DateTime(2023, 11, 10), "Controleur de Gestion", 0, "1234567890123", "GrosSous", "Mr", new DateTime(1980, 9, 9), "0123456789", adresse20, "mr.grossous@example.com");

            //Création de l'arbre N aire donnée dans l'énoncé
            salaries.Racine = salarie1;
            salarie1.AssocierNoeudFils(salarie2);
            salarie2.AssocierNoeudFils(salarie3);
            salarie3.AssocierNoeudFrère(salarie4);
            salarie2.AssocierNoeudFrère(salarie5);
            salarie5.AssocierNoeudFils(salarie6);
            salarie6.AssocierNoeudFils(salarie7);
            salarie7.AssocierNoeudFrère(salarie8);
            salarie8.AssocierNoeudFrère(salarie9);
            salarie6.AssocierNoeudFrère(salarie10);
            salarie10.AssocierNoeudFils(salarie11);
            salarie11.AssocierNoeudFrère(salarie12);
            salarie5.AssocierNoeudFrère(salarie13);
            salarie13.AssocierNoeudFils(salarie14);
            salarie14.AssocierNoeudFrère(salarie15);
            salarie13.AssocierNoeudFrère(salarie16);
            salarie16.AssocierNoeudFils(salarie17);
            salarie17.AssocierNoeudFils(salarie18);
            salarie18.AssocierNoeudFrère(salarie19);
            salarie17.AssocierNoeudFrère(salarie20);

            //Création des différents véhicules
            Voiture voiture1 = new Voiture(7, "1ZE45Z", "Audit", "A7", 300);
            Voiture voiture2 = new Voiture(5, "123ABC", "Toyota", "Yaris", 100);
            Camionnette camionnette1 = new Camionnette("456DEF", "Renault", "Kangoo", 150) ;
            CamionBenne camionbenne1 = new CamionBenne(0, 1, false, "DEF456", "MAN", "TGS", 480);
            CamionFrigorifique camionfrigorifique1 = new CamionFrigorifique(0, "ABC123", "Volvo", "FH16", 600);
            CamionCiterne camionciterne1 = new CamionCiterne("goudron", "goudron", "539TYZ", "Renault", "Kangoo", 150);
            vehicules.Add(voiture2);
            vehicules.Add(voiture1);
            vehicules.Add(camionnette1);
            vehicules.Add(camionbenne1);
            vehicules.Add(camionfrigorifique1 );
            vehicules.Add(camionciterne1);

            // On instancie les clients
            MySqlCommand command = connexion.CreateCommand();
            command.CommandText = "SELECT * FROM Client";
            MySqlDataReader reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                string mdp = reader.GetString(0);
                string noSS = reader.GetString(1);
                string nom = reader.GetString(2);
                string prenom = reader.GetString(3);
                DateTime dateNaissance = reader.GetDateTime(4);
                string telephone = reader.GetString(5);
                string[] adresse = reader.GetString(6).Split(",");
                string mail = reader.GetString(7);
                string statut = reader.GetString(8);
                double achat = reader.GetDouble(9);
                DateTime creation_client = reader.GetDateTime(10);
                Client c = new Client(creation_client,achat,statut, mdp, noSS, nom, prenom, dateNaissance, telephone, new Adresse(adresse[0], adresse[1], adresse[2], adresse[3]), mail);
                Ajouter_Client(c,connexion);

            }
            reader.Close();

            //On instancie les commandes
            MySqlCommand command1 = connexion.CreateCommand();
            command1.CommandText = "SELECT * FROM COMMANDE";
            MySqlDataReader reader1 = command1.ExecuteReader();
            while (reader1.Read())
            {
                int numero_commande = reader1.GetInt32(0);
                string transport = reader1.GetString(1);
                string mail_client = reader1.GetString(2);
                double prix = reader1.GetDouble(3);
                string mail_chauffeur = reader1.GetString(4);
                DateTime date_livraison = reader1.GetDateTime(5);
                string depart = reader1.GetString(6);
                string arrivee = reader1.GetString(7);

                Salarie chauffeur = salaries.Find(salaries.Racine,x => x.Mail == mail_chauffeur);
                Vehicule V = vehicules.Find(x => x.Plaque == transport);
                Client clientici = clients.Find(x => x.Mail == mail_client);
                Commande c = new Commande(numero_commande, V, clientici, prix, chauffeur, date_livraison, depart, arrivee);
                Ajouter_Commande(c);

               
            }
            reader1.Close();
            
        }


        public ArbreNaireSalarie Salaries { get { return salaries; } set { salaries = value; } }
        public List<Commande> Commandes { get { return commandes; } set { commandes = value; } }
        public List<Vehicule> Vehicule { get { return vehicules; } set { vehicules = value; } }
        public List<Client> Clients { get { return clients; } set { clients = value; } }

        /// <summary>
        /// Fonction pour modifier un client
        /// </summary>
        /// <param name="c"></param>
        /// <param name="connexion"></param>
        public void Modifier_Client(Client c, MySqlConnection connexion)
        {
            Console.WriteLine("Que voulez vous modifier ?\nVotre mdp(1), votre numéro de téléphone(2),votre nom(3), votre prenom(4)");
            int rep = Convert.ToInt32(Console.ReadLine());
            Console.Clear();           
            MySqlCommand command = connexion.CreateCommand();
            string rep2;
            switch(rep)
            {
                case 1:
                    Console.WriteLine("Ecrivez votre nouveau mdp : ");
                    rep2 = Console.ReadLine();
                    command.CommandText = " UPDATE CLIENT SET mdp='" + rep2 + "' WHERE mail ='" + c.Mail + "';";
                    break;
                case 2:
                    Console.WriteLine("Ecrivez votre nouveau numéro de téléphone: ");
                    rep2 = Console.ReadLine();
                    command.CommandText = " UPDATE CLIENT SET telephone='" + rep2 + "' WHERE mail ='" + c.Mail + "';";
                    break;
                case 3:
                    Console.WriteLine("Ecrivez votre nouveau nom: ");
                    rep2 = Console.ReadLine();
                    command.CommandText = " UPDATE CLIENT SET nom ='"+rep2+"' WHERE mail ='" + c.Mail + "';";
                    break;

                case 4:
                    Console.WriteLine("Ecrivez votre nouveau prénom: ");
                    rep2 = Console.ReadLine();
                    command.CommandText = " UPDATE CLIENT SET prenom='" + rep2 + "' WHERE mail ='" + c.Mail + "';";
                    break;

            }
            command.ExecuteNonQuery();
        }
        /// <summary>
        /// Fonction pour supprimer un client
        /// </summary>
        /// <param name="c"></param>
        /// <param name="connexion"></param>
        public void Supprimer_Client(Client c, MySqlConnection connexion)
        {
            clients.Remove(c);
            // La supprimer de la base de donnée
            MySqlCommand command = connexion.CreateCommand();
            command.CommandText = "DELETE FROM Client WHERE mail = '" + c.Mail + "'";
            MySqlDataReader reader = command.ExecuteReader();
            reader.Close();
        }
        /// <summary>
        /// Fonction qui va afficher les clients en fonction du Compare To que l'on a crée
        /// </summary>
        public void Afficher_Clients()
        {
            Console.WriteLine("Voulez vous comparer les clients par Nom(1), par Ville(2), par Montant d'achats cumulé(3), par successivement chacun(4)");
            int rep = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            clients.ForEach(x => x.TypeComparaison = rep);
            clients.Sort();
            clients.ForEach(x => Console.WriteLine(x.ToString() + "\n"));
        }
        /// <summary>
        /// Jamais utilsé dans notre code mais au cas où
        /// </summary>
        /// <param name="c"></param>
        /// <param name="connexion"></param>
        public void Supprimer_Commande(Commande c, MySqlConnection connexion)
        {
            if (commandes.Contains(c)) // Le mail est une clé primaire
            {
                commandes.Remove(c);

                // La supprimer de la base de donnée
                MySqlCommand command = connexion.CreateCommand();
                command.CommandText = "DELETE FROM Commande WHERE numero_commande ='" + c.Numero_commande + "'";
                MySqlDataReader reader = command.ExecuteReader();
                reader.Close();
            }

        }    
        /// <summary>
        /// Jamais utilisé dans notre code mais au cas où
        /// </summary>
        /// <param name="c"></param>
        public void Ajouter_Commande(Commande c)
        {
            if (commandes.Contains(c) == false) // le numéro de commande est une clé primaire
            {
                commandes.Add(c);
            }
            //Pas besoin d'ajouter dans la BDD, c'est juste pour ajouter à la liste des commandes
        }
        /// <summary>
        /// Fonction qui permet d'ajouter un client dans notre liste de client
        /// </summary>
        /// <param name="c"></param>
        /// <param name="connexion"></param>
        public void Ajouter_Client(Client c, MySqlConnection connexion)
        {
            if (clients == null || clients.Contains(c) == false) // Le mail est une clé primaire
            {
                clients.Add(c);
            }
            //Pas besoin d'ajouter dans la BDD, c'est juste pour ajouter à la liste de client
        }
        /// <summary>
        /// Fonction qui pose différentes questions au patorn pour savoir quelles statistiques il veut voir
        /// </summary>
        public void statistiques()
        {
            int choix = -1;
            {
                try
                {
                    Console.WriteLine("Quels statistiques vous interessent ?\n\nLe nombre de livraison par chauffeur ?(1)\nLes commandes selon une période de temps ?(2)\nLa moyenne du prix des commandes ?(3)\nLa moyenne des comptes clients ?(4)\nLa liste des commandes pour un client ?(5)");
                    choix = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);

                }
            } while (choix < 1 || choix > 5) ;
            Console.Clear();
            switch (choix)
            {
                case 1:
                    nb_livraison_chauffeur(salaries.Racine);
                    break;
                case 2:
                    //Il faudra demander 2 dates
                    DateTime date1 = new DateTime();
                    DateTime date2 = new DateTime();

                    bool datesSaisiesCorrectement = false;

                    while (!datesSaisiesCorrectement)
                    {
                        try
                        {
                            Console.WriteLine("Entrez la première date, la plus ancienne(au format AAAA-MM-JJ) :");
                            date1 = DateTime.Parse(Console.ReadLine());

                            Console.WriteLine("Entrez la deuxième date soit après le date 1(au format AAAA-MM-JJ) :");
                            date2 = DateTime.Parse(Console.ReadLine());
                            Console.Clear()
;                            if (date1 >= date2) Console.WriteLine("la 2nd date que tu as donné n'est pas plus récente");
                            else datesSaisiesCorrectement = true;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                    Console.Clear();
                    commande_temps(date1,date2);
                    break;
                case 3:
                    moyenne_prix_commande();
                    break;
                case 4:
                    moyenne_compte_client();
                    break;
                case 5:
                    Console.WriteLine("Choisissez le client en écrivant son numéro\n");
                    for (int i = 0; i < clients.Count(); i++)
                    {
                        Console.WriteLine($"{clients[i].ShortToString()} ({i + 1})");
                    }
                    int choix2 = Convert.ToInt32(Console.ReadLine());
                    Console.Clear();
                    nb_commande(clients[choix2-1]);
                    break;

                 
            }
            
        }
        // On retrouve toutes les fonctions statistiques de IStatistiques demandées par l'énoncé
        public void nb_livraison_chauffeur(Salarie actuel)
        {
            if (actuel != null)
            {
                if (actuel.Poste == "Chauffeur")
                {
                    int nb = commandes.FindAll(x => x.Chauffeur == actuel).Count();
                    Console.WriteLine($"Le chauffeur : {actuel.Prenom} {actuel.Nom} a réalisé {nb} commandes");
                }
                nb_livraison_chauffeur(actuel.Sucesseur);
                nb_livraison_chauffeur(actuel.Frère);
            }
        }
        public void moyenne_prix_commande()
        {
            double result = 0;
            foreach(Commande c in commandes)
            {
                result += c.Prix_Commande;
            }
            Console.WriteLine($"La moyenne des prix des commandes est de {result/commandes.Count} euros");
        }

        public void nb_commande(Client c)
        {
            Console.WriteLine($"Voici les commandes de notre client {c.Nom} {c.Prenom}: ");
            commandes.FindAll(x => x.Client_Commande == c).ForEach(x=>Console.WriteLine(x.ToString()));
        }

        public void moyenne_compte_client()
        {
            foreach(Client c1 in clients)
            {
                double result = 0;
                int compteur = 0;
                foreach (Commande c in commandes)
                {
                    if (c.Client_Commande == c1)
                    {
                        result += c.Prix_Commande;
                        compteur++;
                    }                      
                }
                if(result!=0) Console.WriteLine($"La moyenne des prix du client : {c1.Nom} {c1.Prenom} ({c1.Mail}) est en moyenne de {result / compteur} euros");
                else Console.WriteLine($"La moyenne des prix du client: {c1.Nom} {c1.Prenom} ({c1.Mail}) est de 0 euro");

            }
            
            
        }

        public void commande_temps(DateTime début,DateTime fin)
        {
            Console.WriteLine($"L'ensemble des commandes réalisées entre le {début.ToShortDateString()} et le {fin.ToShortDateString()} sont: ");
            commandes.FindAll(x=>x.Date_Commande<=fin && x.Date_Commande>=début).ForEach(x=> Console.WriteLine((x.ToString())));
        }
    }
}
