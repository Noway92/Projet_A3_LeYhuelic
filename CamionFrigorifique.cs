using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_A3_LeYhuelic
{
    public class CamionFrigorifique : Vehicule
    {
        private int volume;

        public CamionFrigorifique() : base()
        {
            volume = 0; //50 m^3 max
        }
        public CamionFrigorifique(double prixvehicule,int volume,string plaque, string marque, string modele, int puissance) : base(prixvehicule,plaque, marque, modele, puissance)
        {
            this.volume = volume;
        }
        public CamionFrigorifique(int volume, string plaque, string marque, string modele, int puissance) : base(plaque, marque, modele, puissance)
        {
            this.volume = volume;
        }

        public int Volume { get { return volume; } set { volume = value; } }

        /// <summary>
        /// Calcule le prix Maximal au km du camion en fonction de son volume
        /// </summary>
        public override void PrixFinal()
        {
            PrixVehicule += volume * 0.005; //Le prix max en plus est de 0.25 euros /km
        }
    }
}