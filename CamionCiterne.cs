using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Projet_A3_LeYhuelic
{
    public class CamionCiterne : Vehicule

    {
        private string cuve;
        private string produit;
        
        public CamionCiterne():base()
        {
            cuve = "";
            produit = "";
        }
        public CamionCiterne(double prixvehicule,string cuve, string produit,string plaque, string marque, string modele, int puissance) : base(prixvehicule, plaque, marque, modele, puissance)
        {
            this.cuve = cuve;
            this.produit = produit;
        }
        public CamionCiterne(string cuve, string produit, string plaque, string marque, string modele, int puissance) : base(plaque, marque, modele, puissance)
        {
            this.cuve = cuve;
            this.produit = produit;
        }

        /// <summary>
        /// Question pour savoir ce dont le client à besoin et donc pour instancier le prix de la commande
        /// </summary>
        public void QuestionCiterne()
        {
            Console.WriteLine("Voulez vous une cuve pour hydrocarbures(1),réfrigéré(2),acier inoxydable(3),gaz(4),goudron(5)");
            switch(Console.ReadLine())
            {
                case "1":
                    cuve = "hydrocarbures";
                    break;
                case "2":
                    cuve = "réfrigéré";
                    break;
                case "3":
                    cuve = "acier inoxydable";
                    break;
                case "4":
                    cuve = "gaz";
                    break;
                default: 
                    cuve = "goudron";
                    break;
            }
        }
        /// <summary>
        /// On calcule le prix final du camion en fonction du type de cuve qu'il possède
        /// </summary>
        public override void PrixFinal()
        {
            if (cuve == "hydrocarbures") PrixVehicule += 0.2;
            if (cuve == "réfrigéré") PrixVehicule += 0.1;
            if (cuve == "acier inoxydable") PrixVehicule += 0.15;
            if (cuve == "gaz") PrixVehicule += 0.25;
            if (cuve == "goudron") PrixVehicule += 0.25;
        }

        public string Cuve { get { return cuve; } set { cuve = value; } }
    }
}
