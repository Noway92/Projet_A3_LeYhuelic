using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Projet_A3_LeYhuelic
{
    public abstract class Personne
    {
        // Modèle de base aux clients et aux salariés
        protected string noSS;
        protected string nom;
        protected string prenom;
        protected DateTime date_de_naissance;
        protected string telephone;
        protected Adresse adresse_postale;
        protected string mail;

        //Il y a 3 constructeurs

        public Personne(string noSS, string nom, string prenom, DateTime date_de_naissance, string telephone, Adresse adresse_postale, string mail)
        {
            this.noSS = noSS;
            this.nom = nom;
            this.prenom = prenom;
            this.date_de_naissance = date_de_naissance;
            this.telephone = telephone;
            this.adresse_postale = adresse_postale;
            this.mail = mail;
        }

        public Personne()
        {
            this.noSS ="";
            this.nom = "";
            this.prenom = "";
            this.date_de_naissance = DateTime.Now;
            this.telephone = "";
            this.adresse_postale = new Adresse();
            this.mail = "";
        }
        public Personne(string mail,string nom,string prenom)
        {
            this.noSS = "";
            this.nom = nom;
            this.prenom = prenom;
            this.date_de_naissance = DateTime.Now;
            this.telephone = "";
            this.adresse_postale = new Adresse();
            this.mail = mail;
        }

        public string Nom { get { return nom; } set { nom = value; } }
        public string Prenom { get { return prenom; } set { prenom = value; } }
        public Adresse Adresse_Postale { get { return adresse_postale; } set { adresse_postale = value; } }
        public string Mail{ get { return mail; } set { mail = value; } }
        public string Telephone { get { return telephone; } set { telephone = value; } }
        /// <summary>
        /// Pas be soin du set car jamais on ne le change
        /// </summary>
        public DateTime Date_De_Naissance { get { return date_de_naissance;} }
        /// <summary>
        /// On ne met pas le Set à NoSS car il ne bougera jamais
        /// </summary>
        public string NoSS { get { return noSS; } }

        /// <summary>
        /// Implémentation de la méthode ToString()
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Nom: {nom}\nPrenom: {prenom}\nAdresse: {adresse_postale}\nMail: {mail}\nTelephone: {telephone}\nDate_de_Naissance: {date_de_naissance.ToShortDateString()}\nNuméro_De_Sécu : {noSS}\n";
        }



    }
}
