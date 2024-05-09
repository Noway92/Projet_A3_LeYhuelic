using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_A3_LeYhuelic
{
    public class Adresse
    {
        // Propriétés de l'adresse
        private string rue;
        private string ville;
        private string codePostal;
        private string pays;

        // Constructeur par défaut
        public Adresse()
        {
            rue = "";
            ville = "";
            codePostal = "";
            pays = "";
        }

        /// <summary>
        /// Constructeur avec paramètres pour initialiser les propriétés de l'adresse
        /// </summary>
        /// <param name="rue"></param>
        /// <param name="ville"></param>
        /// <param name="codePostal"></param>
        /// <param name="pays"></param>
        public Adresse(string rue,string ville,string codePostal, string pays)
        {
            this.rue = rue;
            this.ville = ville;
            this.codePostal = codePostal;
            this.pays = pays;
        }

        // Méthode pour afficher l'adresse sous forme de chaîne de caractères
        public override string ToString()
        {
            return $"{rue},{ville},{codePostal},{pays}";
        }

        public string Rue{get { return rue;}set { rue = value;}}
        public string Ville { get { return ville; } set { ville = value; } }
        public string CodePostal { get { return codePostal; } set { codePostal = value; } }
        public string Pays { get { return pays; } set { pays = value; } }

        public static bool operator ==(Adresse a, Adresse b)
        {

            if (a is null || b is null)
                return false;

            return a.Rue == b.Rue &&
                   a.Ville == b.Ville &&
                   a.CodePostal == b.CodePostal &&
                   a.Pays == b.Pays;
        }

        // Surcharge de l'opérateur !=
        public static bool operator !=(Adresse a, Adresse b)
        {
            return !(a == b);
        }

    }

}
