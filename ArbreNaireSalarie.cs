using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Projet_A3_LeYhuelic
{
    /// <summary>
    /// C'est notre tête de notre arbre N aire
    /// </summary>
    public class ArbreNaireSalarie
    {
        Salarie racine;

        public ArbreNaireSalarie()
        {
            this.racine = null;
        }

        ArbreNaireSalarie(Salarie racine)
        {
            this.racine = racine;
        }

        public Salarie Racine
        {
            set { this.racine = value; }
            get { return this.racine; }
        }

        public bool EstArbreVide()
        {
            return racine == null;
        }

        /// <summary>
        /// Permet d'afficher notre arbre d'un manière lisible où l'on voit bien les relations hiérarchiques
        /// </summary>
        /// <param name="actuel"></param>
        /// <param name="espace"></param>
        public void Afficher_Organigramme(Salarie actuel,int espace=0)
        {
            if(actuel!=null)
            {
                if(espace==0) Console.ForegroundColor = ConsoleColor.Yellow;
                if (espace == 6) Console.ForegroundColor = ConsoleColor.DarkBlue;
                if (espace == 12) Console.ForegroundColor = ConsoleColor.DarkMagenta;
                if (espace == 18) Console.ForegroundColor = ConsoleColor.Green;
                if (espace == 24) Console.ForegroundColor = ConsoleColor.DarkGray;
                if (espace == 30) Console.ForegroundColor = ConsoleColor.Cyan;
                string chaine = $"{actuel.Nom}/{actuel.Poste}"; 
                Console.WriteLine(chaine.PadLeft(chaine.Length + espace));
                Afficher_Organigramme(actuel.Sucesseur,espace+6);
                Afficher_Organigramme(actuel.Frère, espace);
                Console.ResetColor();
            }
        }
        /// <summary>
        /// Méthode pour ajouter un salarié
        /// </summary>
        /// <param name="actuel"></param>
        /// <param name="chef"></param>
        /// <param name="s"></param>
        public void Ajouter_Salarie(Salarie actuel,Salarie chef,Salarie s)
        {
            if (Contains(racine,s)) Console.WriteLine("Ce salarié est déjà dans l'entreprise");
            else if (Contains(racine, chef) == false) Console.WriteLine("Ce chef n'existe pas");
            else Ajouter(actuel, chef, s);
            Afficher_Organigramme(racine);
        }
        /// <summary>
        /// Méthode récursive qui ajoute notre salarié
        /// </summary>
        /// <param name="actuel"></param>
        /// <param name="chef"></param>
        /// <param name="s"></param>
        public void Ajouter(Salarie actuel,Salarie chef,Salarie s)
        {
            if (actuel == null || chef == null) return;
            if(actuel==chef)
            {
                if (chef.Sucesseur == null) chef.Sucesseur = s;
                else
                {
                    actuel = chef.Sucesseur;
                    while(actuel.Frère!=null)
                    {
                        actuel = actuel.Frère;
                    }
                    actuel.Frère = s;
                }
            }
            else
            {
                Ajouter(actuel.Frère, chef, s);
                Ajouter(actuel.Sucesseur, chef, s);
            }
        }
        public void Supprimer_Salarie(Salarie actuel,Salarie s)
        {
            if (Contains(racine, s)==false) Console.WriteLine("Ce salarié n'est pas dans l'entreprise");
            else Supprimer(racine, s);
            Afficher_Organigramme(racine);
        }
        /// <summary>
        /// Méthode récursive qui supprimer notre salarié
        /// </summary>
        /// <param name="actuel"></param>
        /// <param name="s"></param>
        public void Supprimer(Salarie actuel,Salarie s)
        {
            // Trop chiant
        }    
        /// <summary>
        /// Réécriture da la fonction List.Contains pour un arbre N aire
        /// </summary>
        /// <param name="actuel"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public bool Contains(Salarie actuel, Salarie s)
        {
            if (actuel == null) return false;
            if (actuel == s) return true;
            return Contains(actuel.Sucesseur, s) || Contains(actuel.Frère, s);
        }

        /// <summary>
        /// Réécriture de la fonction List.Find pour un arbre N aire
        /// </summary>
        /// <param name="actuel"></param>
        /// <param name="match"></param>
        /// <returns></returns>
        public Salarie Find(Salarie actuel,Predicate<Salarie> match)
        {
            if (actuel == null) return null;
            if (match(actuel)) return actuel;
            if (Find(actuel.Sucesseur, match) == null && Find(actuel.Frère, match) == null) return null;
            if (Find(actuel.Sucesseur, match) != null) return Find(actuel.Sucesseur, match);
            return Find(actuel.Frère, match);
        }
        /// <summary>
        /// Réécriture de la fonction List.FindAll pour un arbre N aire
        /// </summary>
        /// <param name="actuel"></param>
        /// <param name="match"></param>
        /// <returns></returns>
        public List<Salarie> FindAll(Salarie actuel, Predicate<Salarie> match)
        {
            List<Salarie> salariéstrouvés = new List<Salarie>();
            // Vérifier si le salarié actuel correspond au prédicat
            if (actuel != null && match(actuel)) salariéstrouvés.Add(actuel);
            // Rechercher dans le successeur
            //?. permet de renvoyer null sans faire d'erreur si actuel est null
            if (actuel?.Sucesseur != null) salariéstrouvés.AddRange(FindAll(actuel.Sucesseur, match));
            // Rechercher dans les frères
            if (actuel?.Frère != null) salariéstrouvés.AddRange(FindAll(actuel.Frère, match));
            return salariéstrouvés;
        }
    }
}
