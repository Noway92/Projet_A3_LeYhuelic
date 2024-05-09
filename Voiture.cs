using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_A3_LeYhuelic
{
    public class Voiture : Vehicule
    {
        //Seul nouveau attribut de voiture
        private int nbplaces;
        /// <summary>
        /// Constructeur 3
        /// </summary>
        /// <param name="prixvehicule"></param>
        /// <param name="nb_places"></param>
        /// <param name="plaque"></param>
        /// <param name="marque"></param>
        /// <param name="modele"></param>
        /// <param name="puissance"></param>
        public Voiture(double prixvehicule,int nb_places,string plaque,string marque, string modele, int puissance) : base(prixvehicule,plaque,marque, modele, puissance)
        {
            this.nbplaces= nb_places;
        }
        /// <summary>
        /// Constructeur 2  
        /// </summary>
        /// <param name="nb_places"></param>
        /// <param name="plaque"></param>
        /// <param name="marque"></param>
        /// <param name="modele"></param>
        /// <param name="puissance"></param>
        public Voiture(int nb_places, string plaque, string marque, string modele, int puissance) : base(plaque, marque, modele, puissance)
        {
            this.nbplaces = nb_places;
        }
        /// <summary>
        /// Constructeur 1
        /// </summary>
        public Voiture():base()
        {
            nbplaces = 0;
        }
        /// <summary>
        /// On vérifie si la voiture à assez de place
        /// </summary>
        /// <param name="nb_passager"></param>
        /// <returns></returns>
        public bool Utilisable(int nb_passager) {return nb_passager <= nbplaces;}

        public int NbPlaces { get { return nbplaces; } set { nbplaces = value; } }
        public override string ToString()
        {
            return$"Nombres de places: {nbplaces}\n"+base.ToString();  
        }

        public override void PrixFinal() { }// Inutile ici car on gare tjrs le même prix au km



    }
}
