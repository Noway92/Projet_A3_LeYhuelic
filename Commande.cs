using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_A3_LeYhuelic
{
    public class Commande 
    {
        //Numéro_commande est notre clé primaire de commande
        private Client client;
        private double prix;
        private Salarie chauffeur;
        private DateTime date;
        private Vehicule transport;
        private string depart;
        private string arrivee;
        private int numero_commande;

        public Commande(int numero_commande, Vehicule tansport,Client client, double prix, Salarie chauffeur, DateTime date,string depart, string arrivee)
        {
            this.transport=tansport;
            this.client = client;
            this.prix = prix;
            this.chauffeur = chauffeur;
            this.date = date;
            this.depart = depart;
            this.arrivee = arrivee;
            this.numero_commande = numero_commande;
        }

        public Commande(Client client)
        {
            // Il faudra écrire plus tard le type en fonction du besoin
            this.transport = null;
            this.client = client;
            this.prix = 0;
            this.chauffeur = new Salarie();
            this.date = DateTime.Now;
            this.depart = "";
            this.arrivee = client.Adresse_Postale.Ville;
            this.numero_commande = -1;
        }

        public Commande()
        {
            // Il faudra écrire plus tard le type en fonction du besoin
            this.transport = null ;
            this.client = new Client();
            this.prix = 0;
            this.chauffeur = new Salarie() ;
            this.date = DateTime.Now;
            this.depart = "";
            this.arrivee = "";
            this.numero_commande = -1;
        }

        public string Depart { get { return depart; } set { depart = value; } }
        public string Arrivee { get { return arrivee; } set { arrivee = value; } }
        public int Numero_commande { get { return numero_commande; } set { numero_commande = value; } }
        public Salarie Chauffeur { get { return chauffeur; } set { chauffeur = value; } }
        public Vehicule Transport { get { return transport; } set { transport = value; } }
        public Client Client_Commande { get { return client; } set { client = value; } }
        public double Prix_Commande { get { return prix; } set { prix = value; } }
        public DateTime Date_Commande { get { return date; } set { date = value; } }

        /// <summary>
        /// Implémentation de la fonction ToString()
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Livraison de {depart} à {arrivee} le {date.ToShortDateString()} ayant couter {prix} euros";
        }

        /// <summary>
        /// Fonctions récursvies qui permet de retourner la liste des dates que le client pourra choisir si sa date n'était pas disponible
        /// </summary>
        /// <param name="actuel"></param>
        /// <param name="commandes"></param>
        /// <returns></returns>
        public List<DateTime> ToutesDispos(Salarie actuel, List<Commande> commandes)
        {
            List<DateTime> recherche_dispo = new List<DateTime>();
            if (actuel != null)
            {
                if (actuel.Poste == "Chauffeur")
                {
                    List<DateTime> inter = new List<DateTime>();
                    inter = actuel.NouvelleDispo(date, commandes);
                    foreach (var y in inter)
                    {
                        if (recherche_dispo.Contains(y) == false) recherche_dispo.Add(y);
                    }
                }
                if (actuel?.Frère != null) recherche_dispo.AddRange(ToutesDispos(actuel.Frère, commandes).Except(recherche_dispo));
                if (actuel?.Sucesseur != null) recherche_dispo.AddRange(ToutesDispos(actuel.Sucesseur, commandes).Except(recherche_dispo));

            }
            return recherche_dispo;
        }
        /// <summary>
        /// Fonction qui va creer une commande et donc instancier tous les attributs de celle-ci
        /// </summary>
        /// <param name="connexion"></param>
        /// <param name="vehicules"></param>
        /// <param name="salaries"></param>
        /// <param name="commandes"></param>
        public void Creer_Commande(MySqlConnection connexion,List<Vehicule> vehicules,ArbreNaireSalarie salaries,List<Commande> commandes)
        {
            //Gérer le numéro de commande
            MySqlCommand command = connexion.CreateCommand();
            command.CommandText = "SELECT MAX(numero_commande) FROM COMMANDE";
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                if(reader.IsDBNull(0))
                {
                    numero_commande = 1;
                }
                else numero_commande = reader.GetInt32(0) + 1;
            }
            reader.Close();
            // Demander la date souhaité de la commande
            do
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Pouvez vous entrer la date de livraison désirée dans le format suivant : '31/01/2023'");
                    Console.ResetColor();
                    Console.WriteLine("Cette date doit logiquement être après la date actuelle, nous vous préviendrons si la date n'est pas possible");
                    string reponse = Console.ReadLine();
                    date = new DateTime(Convert.ToInt32(reponse.Substring(6, 4)), Convert.ToInt32(reponse.Substring(3, 2)), Convert.ToInt32(reponse.Substring(0, 2)));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            } while (date<DateTime.Now);
            Console.Clear();

            // Il faut faire regarder tous les chauffeurs disponibles 
            List<Salarie> chauffeur_dispo = salaries.FindAll(salaries.Racine, x => x.ChauffeurDisponible(date, commandes));
            if (chauffeur_dispo != null && chauffeur_dispo.Count != 0)
            {
                Random rand = new Random();
                int nombreAleatoire = rand.Next(0, chauffeur_dispo.Count()); // générer un nombre aléatoire entre 1 le nombre de possibilités
                chauffeur = chauffeur_dispo[nombreAleatoire];
            }
            else
            {
                // C'est le cas où aucun chauffeur n'est disponible à la date demandée
                List<DateTime> recherche_dispo = new List<DateTime>();
                Console.WriteLine("La date que vous avez demandé n'est pas possible, voici l'ensemble des dates possibles proche de celle la\nChoisissez celle que vous voulez en indiquant le numéro");
                recherche_dispo = ToutesDispos(salaries.Racine, commandes);

                //On tri les dates dans l'ordre
                recherche_dispo.Sort();
                for (int i = 0; i < recherche_dispo.Count(); i++)
                {
                    Console.WriteLine($"{recherche_dispo[i].ToShortDateString()} ({i + 1})");
                }
                int choix = Convert.ToInt32(Console.ReadLine());
                date = recherche_dispo[choix - 1];

                //On prend un des chauffeurs qui est disponible pour cette date au hasard
                chauffeur_dispo = salaries.FindAll(salaries.Racine, x => x.Poste == "Chauffeur" && x.ChauffeurDisponible(date, commandes));
                Random rand = new Random();
                int nombreAleatoire = rand.Next(0, chauffeur_dispo.Count()); // générer un nombre aléatoire entre 1 le nombre de possibilités
                chauffeur = chauffeur_dispo[nombreAleatoire];
            }

            Console.Clear();
            //Ensemble des questions pour savoir quels véhicules le client à besoin
            int type = -1;
            do
            {
                Console.WriteLine("Avez vous besoin d'une voiture (1) ou d'un véhicule de transport (2) ?");
                type = Convert.ToInt32(Console.ReadLine());
            } while (type != 1 && type != 2);
            if(type==1)
            {                
                int nbpersonne = -1;
                do
                {
                    Console.WriteLine("Nous avons des voitures de maximum 7 places, combien êtes vous ?");
                    try
                    {
                        nbpersonne = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }                    
                } while (nbpersonne<1 || nbpersonne>7);
                List<Vehicule> Vs = vehicules.FindAll(x => x is Voiture);
                bool sortir = false;
                int compteur = 0;
                Voiture voitureFinal = new Voiture();
                //On cherche la voiture avec le bon nombre de places
                while (sortir==false)
                {
                    //On prend toutes les voitures
                    voitureFinal= (Voiture)Vs[compteur];
                    if (voitureFinal.Utilisable(nbpersonne))
                    {
                        sortir = true;
                    }
                    compteur++;
                }
                transport = voitureFinal;
            }
            else
            {
                Console.WriteLine("Votre commande sera elle supérieur à 5m^3 ? (Y/N)");
                string volume = Console.ReadLine();
                //C'est donc pour une camionette
                if (volume=="N")
                {
                    Camionnette camionette = (Camionnette) vehicules.Find(x => x is Camionnette);
                    // On prend le premier véhicule qui correspond                   
                    
                    Console.WriteLine("Pour quel usage avez vous besoin de cela ?\nMarchandise légère(1),verrerie(2),déplacement(3)");
                    switch(Console.ReadLine())
                    {
                        case "1":
                            camionette.Usage = "marchandise légère";
                            break;
                        case "2":
                            camionette.Usage = "verrerie";
                            break;
                        default:
                            camionette.Usage = "déplacement";
                            break;
                    }
                    transport = camionette;
                }
                else
                {
                    int type2 = -1;
                    do
                    {
                        Console.WriteLine("Avez vous besoin d'un camion Benne (1), d'un camion Citerne (2) ou d'un camion Frigorifique (3)");
                        try
                        {
                            type2 = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    } while (type2 < 1 || type2 > 3);
                    switch(type2)
                    {
                        case 1:
                            CamionBenne camion1= (CamionBenne)vehicules.Find(x => x is CamionBenne);
                            camion1.QuestionBenne();
                            transport = camion1;
                            break;

                        case 2:
                            CamionCiterne camion2 = (CamionCiterne)vehicules.Find(x => x is CamionCiterne);
                            camion2.QuestionCiterne();
                            transport = camion2;
                            break;

                        case 3:
                            Console.WriteLine("Donner le volume en m^3 de votre commande (min 5 et max 50)");
                            int V = Convert.ToInt32(Console.ReadLine());
                            CamionFrigorifique camion3 = (CamionFrigorifique)vehicules.Find(x => x is CamionFrigorifique);
                            camion3.Volume = V;
                            transport = camion3;
                            break;
                    }                    
                }
            }           

            // Le départ est toujours de Paris car on considère que l'on a un seul entrepot pour l'instant
            depart = "Paris";
            arrivee = client.Adresse_Postale.Ville;
            Console.WriteLine("Voulez vous être livrés à votre adresse client(1) ou bien autre part(2)?");
            if(Console.ReadLine()=="2")
            {
                //Donner la liste des possibilités
                Console.WriteLine("Dans quelle ville voulez vous être livré ?");
                arrivee = Console.ReadLine();
                
            }
            Console.Clear();
            Distances D = new Distances(depart,arrivee);
            // On prend la bonne valeur a modifié
            transport.PrixFinal();
            // On calcule le prix de la commande
            int distanceprix = D.Djikstra();
            Console.WriteLine($"Il devra donc parcourir {distanceprix} km");
            prix = distanceprix * transport.PrixVehicule;
            
            // On cacul le bon prix en focntion du statut du client
            if (client.Statut == "OR")
            {
                prix = prix * 0.85;
                Console.WriteLine("Grace à votre fidélité OR, vous possédez 15% de réduction sur toutes vos livraisons");
            }
            else if(client.Statut == "BRONZE")
            {
                prix = prix * 0.95;
                Console.WriteLine("Grace à votre fidélité BRONZE, vous possédez 5% de réduction sur toutes vos livraisons");               
            }

            long carte_crédit = -1;
            do
            {
                try
                {
                    Console.WriteLine($"Le prix à payer est {prix} euros.\nDonnez le numéro de votre carte de crédit : ");
                    carte_crédit = long.Parse(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            } while (carte_crédit < 0 || carte_crédit > 10000000000000000);

            //La commande à bien été passé
            client.Achat_Cumulé = client.Achat_Cumulé + prix;
            MySqlCommand achatcummulé = connexion.CreateCommand();
            //Utile pour convertir la ',' en '.' car sur SQL on utilise les '.' pour les doubles
            string achatCumuleStr = client.Achat_Cumulé.ToString(System.Globalization.CultureInfo.InvariantCulture);

            achatcummulé.CommandText = " UPDATE CLIENT SET achat="+achatCumuleStr+" WHERE mail ='" + client.Mail + "';";
            achatcummulé.ExecuteNonQuery();

            //Changement du statut du client en fct du prix qu'il à payé
            int temps = (DateTime.Now.Month - client.Creation_Client.Month) + (DateTime.Now.Year - client.Creation_Client.Year) * 12;
            if (temps == 0) temps = 1;
            //Ici le temps sera donc par mois
            MySqlCommand command2 = connexion.CreateCommand();
            if (client.Achat_Cumulé/temps>300)
            {
                command2.CommandText = " UPDATE CLIENT SET statut='OR' WHERE mail ='" + client.Mail + "';";
                command2.ExecuteNonQuery();
                client.Statut = "OR";
            }
            else
            {
                if (client.Achat_Cumulé / temps > 100)
                {
                    command2.CommandText = " UPDATE CLIENT SET statut='BRONZE' WHERE mail ='" + client.Mail + "';";
                    client.Statut = "BRONZE";
                    command2.ExecuteNonQuery();
                }
                else
                {
                    command2.CommandText = " UPDATE CLIENT SET statut='' WHERE mail ='" + client.Mail + "';";
                    command2.ExecuteNonQuery();
                    client.Statut = "";
                }
            }

            // On rentre notre commande dans notre BDD
            MySqlCommand command5 = connexion.CreateCommand();
            command5.CommandText = " INSERT INTO COMMANDE VALUES(@numero_commande,@transport,@Mail_Client,@prix,@chauffeur,@Date_Livraison,@depart,@arrivee);";
            command5.Parameters.AddWithValue("@numero_commande", numero_commande);
            command5.Parameters.AddWithValue("@transport", transport.Plaque);
            command5.Parameters.AddWithValue("@Mail_Client", client.Mail);
            command5.Parameters.AddWithValue("@prix", prix);
            command5.Parameters.AddWithValue("@chauffeur", chauffeur.Mail);
            command5.Parameters.AddWithValue("@Date_Livraison", date);
            command5.Parameters.AddWithValue("@depart", depart);
            command5.Parameters.AddWithValue("@arrivee", arrivee);
            command5.ExecuteNonQuery();

            Console.WriteLine("Merci d'avoir commandé chez nous !");

        }


    }
}
