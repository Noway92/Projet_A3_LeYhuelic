using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_A3_LeYhuelic
{
    /// <summary>
    /// Classe fille de véchicule pour creer les camionettes
    /// </summary>
    public class Camionnette : Vehicule
    {
        private string usage;
        public Camionnette(double prixvehicule,string plaque, string marque, string modele, int puissance) : base(prixvehicule, plaque,marque, modele, puissance)
        {
            usage = "verrerie";
        }

        public string Usage { get { return usage; } set { usage = value; } }

        public Camionnette(string plaque, string marque, string modele, int puissance) : base(plaque, marque, modele, puissance)
        {
            usage = "verrerie";
        }


        public override string ToString()
        {
           return base.ToString();
        }

        /// <summary>
        /// Calcul le prix de la camionette au km en fonction de ce qu'elle transporte
        /// </summary>
        public override void PrixFinal()
        {
            if (usage == "marchandise légère") PrixVehicule += 0.05;
            if (usage == "verrerie") PrixVehicule += 0.1;
            if (usage == "déplacement") PrixVehicule += 0;                      
        }


    }
}
