using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_A3_LeYhuelic
{
    public class CamionBenne : Vehicule
    {
        private int benne; //Compris entre 1 et 3
        private bool grue;
        private int volume;

        public CamionBenne(double prixvehicule, int volume,int benne, bool grue, string plaque, string marque, string modele, int puissance) : base(prixvehicule, plaque, marque, modele, puissance)
        {
            this.volume = volume;
            this.benne = benne;
            this.grue = grue;
        }
        public CamionBenne(int volume, int benne, bool grue, string plaque, string marque, string modele, int puissance) : base(plaque, marque, modele, puissance)
        {
            this.volume = volume;
            this.benne = benne;
            this.grue = grue;            
        }

        public CamionBenne():base()
        {
            volume = 0;
            benne = 1;
            grue = false;
        }
        /// <summary>
        /// Question pour savoir ce qu'il faut pour le client
        /// </summary>
        public void QuestionBenne()
        {
            Console.WriteLine("Avez vous besoin de la grue (Y/N)");
            if (Console.ReadLine() == "Y") grue = true;
            Console.WriteLine("Combien de benne avez vous besoin ? (Entre 1 et 3)");
            benne=Convert.ToInt32(Console.ReadLine());
        }

        /// <summary>
        /// Calcul le prix au km de notre véhicule
        /// </summary>
        public override void PrixFinal()
        {
            if (grue == true) PrixVehicule += 0.05; // 0.05 euros si il y a la grue
            PrixVehicule += (benne - 1) * 0.02; // 0.02 euros par benne en plus au km
        }

        public int Benne { get { return benne; } set { benne = value; } }
        public bool Grue { get { return grue; } set { grue = value; } }
        public int Volume { get { return volume; } }

    }
}
