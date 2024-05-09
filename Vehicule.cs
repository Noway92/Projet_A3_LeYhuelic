using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_A3_LeYhuelic
{
    /// <summary>
    /// On crée une classe qui servira de base à tous les autres véhicules
    /// </summary>
    public abstract class Vehicule
    {
        protected string plaque;
        protected string marque;
        protected string modele;
        protected int puissance;
        private double prixvehicule;

        public int Puissance{ get { return puissance; } }
        public double PrixVehicule { get { return prixvehicule; } set { prixvehicule = value; } } // Prix du véhicule au km
        public string Marque { get { return marque; } }
        public string Modele { get { return modele; } }
        public string Plaque { get { return plaque; } }

        public Vehicule()
        {
            this.prixvehicule = 0.1;
            this.plaque = "";
            this.marque = "";
            this.modele = "";
            this.puissance = 0;

        }
        public Vehicule(double prixvehicule,string plaque,string marque, string modele, int puissance)
        {
            this.prixvehicule = prixvehicule;
            this.plaque = plaque;
            this.marque = marque;
            this.modele = modele;
            this.puissance = puissance;
        }
        public Vehicule(string plaque, string marque, string modele, int puissance)
        {
            this.prixvehicule = 0.1;
            this.plaque = plaque;
            this.marque = marque;
            this.modele = modele;
            this.puissance = puissance;
        }
        /// <summary>
        /// Implémentation d'un nouveau ToString()
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Marque: {marque}\nModele: {modele}\nPuissance: {puissance}\nPlaque: {plaque}\nPrix du véhicule au km: {prixvehicule}\n";
     
        }
        
        /// <summary>
        /// Méthode abstraite qui sera dans toutes les classes filles de vehicule
        /// </summary>
        public abstract void PrixFinal();
    }
}
