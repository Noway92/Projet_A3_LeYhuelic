using Org.BouncyCastle.Asn1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_A3_LeYhuelic
{
    /// <summary>
    /// Elle correspond à la classe noeud de mon arbre N aire
    /// </summary>
    public class Salarie : Personne
    {
        private DateTime date_entree;
        private string poste;
        private int salaire;
        private Salarie frère;
        private Salarie sucesseur;

        //Il y a 4 constructeurs
        public Salarie(DateTime date_entree, string poste, int salaire, string noSS, string nom, string prenom, DateTime date_de_naissance, string telephone, Adresse adresse_postale, string mail,Salarie frère,Salarie successeur) : base(noSS, nom, prenom, date_de_naissance, telephone, adresse_postale, mail) 
        {
            this.date_entree = date_entree;
            this.poste = poste;
            this.salaire = salaire;
            this.frère = frère;
            this.sucesseur = sucesseur;
        }

        public Salarie(DateTime date_entree, string poste, int salaire, string noSS, string nom, string prenom, DateTime date_de_naissance, string telephone, Adresse adresse_postale, string mail) : base(noSS, nom, prenom, date_de_naissance, telephone, adresse_postale, mail)
        {
            this.date_entree = date_entree;
            this.poste = poste;
            this.salaire = salaire;
            this.frère = null;
            this.sucesseur = null;
        }

        public Salarie():base()
        {
            this.date_entree = DateTime.Now;
            this.poste = "";
            this.salaire = 0;
            this.frère = null;
            this.sucesseur = null;
        }
        public Salarie(string mail,string poste,string nom,string prenom) : base(mail,nom,prenom)
        {
            this.date_entree = DateTime.Now;
            this.poste = poste;
            this.salaire = 0;
            this.frère = null;
            this.sucesseur = null;
        }

        public int Salaire { get { return salaire; } set { salaire = value; } }
        public string Poste { get { return poste; } set { poste = value; } }
        public DateTime Date_Entree { get { return date_entree; } }
        public Salarie Frère { get { return this.frère; } set { frère = value; } }
        public Salarie Sucesseur { get { return this.sucesseur; } set { sucesseur= value; } }

        public override string ToString()
        {
            return $"Date d'entrée: {date_entree.ToShortDateString()}\nPoste: {poste}\nSalaire: {salaire} euros\n"+base.ToString();
        }

        /// <summary>
        /// Fonction qui vient regarder si un chauffeur est disponible à une date
        /// Il est disponible si il ne travaille déjà pas ce jour la et si c'est la cas,
        /// si il ne travaille pas 4 jours au tour de ce jour la pour ne jamais taffer 5 jours de suite
        /// </summary>
        /// <param name="date"></param>
        /// <param name="C"></param>
        /// <returns></returns>
        public bool ChauffeurDisponible(DateTime date, List<Commande>C)
        {
            if (poste != "Chauffeur") return false;
            // On regarde si le chauffeur est disponible ce jour la
            bool rep = true;
            foreach (Commande commande in C)
            {
                if (commande.Date_Commande == date && commande.Chauffeur==this)
                {
                    return false;
                }
            }
            if(rep==true)
            {
                int jour = 1;
                bool moins = true;
                bool plus = true;
                int compteurmoins = 0;
                int compteurplus = 0;
                while(jour<=5)
                {
                    //On Optimise en ne donnant jamais 5 commandes d'affilée à un chauffeur
                    // A chaque fois la date moins sera un jour de 1 moins 
                    DateTime datemoins = date.AddDays(-jour);
                    DateTime dateplus= date.AddDays(jour);
                    // Si moins a été false une fois, il le reste tout le temps. On vérifie si ce chauffeur est déjà pris ce jour la pour compter les 5 jours d'affilée ou pas
                    if (moins == true && C.Find(x => x.Chauffeur == this && datemoins == x.Date_Commande) != null) compteurmoins++;
                    else moins = false;
                    if (plus == true && C.Find(x => x.Chauffeur == this && dateplus == x.Date_Commande) != null) compteurplus++;
                    else plus = false;
                    // C'est le cas bilatéral 
                    if (moins == false && plus == false && jour <= 3) return true;
                    //Cela gère le cas Unilatéral et Bilatéral avec plus de 3 jours
                    if (compteurmoins+compteurplus>=5)return false;
                    jour++;
                }                
            }
            return rep;
        }

        /// <summary>
        /// Fonction qui si tous les chauffeurs ne sont pas disponibles un certains jour va retourner toutes les dates disponibles les plus proches de celle-ci
        /// </summary>
        /// <param name="date"></param>
        /// <param name="C"></param>
        /// <returns></returns>
        public List<DateTime> NouvelleDispo(DateTime date, List<Commande> C)
        {
            List<DateTime> newList = new List<DateTime>();
            int maxdate = 0;
            int jour = 1;
            // Nombre max de jour que l'on retourne
            while (maxdate<3)
            {
                DateTime datemoins = date.AddDays(-jour);
                DateTime dateplus = date.AddDays(jour);
                if (ChauffeurDisponible(dateplus, C) )
                {
                    newList.Add(dateplus);
                    maxdate++;
                }
                if (ChauffeurDisponible(datemoins, C) && datemoins>DateTime.Now)
                {
                    newList.Add(datemoins);
                    maxdate++;
                }
                jour++;
            }
            return newList;
        }


        public bool AssocierNoeudFils(Salarie fils)
        {
            bool ok = false;
            if (this != null)
            {
                if (sucesseur is null && fils != null)
                {
                    sucesseur = fils;
                    ok = true;
                }
            }
            return ok;
        }

        public bool AssocierNoeudFrère(Salarie embryon)
        {
            bool ok = false;
            if (this != null)
            {
                if (frère is null && embryon != null)
                {
                    frère = embryon;
                    ok = true;
                }
            }
            return ok;
        }
        /// <summary>
        /// Utile pour comparer directment des salariés en utilisant == ou !=
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(Salarie a, Salarie b)
        {
            if (a is null && b is null) return true;
            if (a is null || b is null)
                return false;
            //Clé primaire
            return a.Mail == b.Mail;
        }

        public static bool operator !=(Salarie a, Salarie b)
        {
            return !(a == b);
        }
    }

}
