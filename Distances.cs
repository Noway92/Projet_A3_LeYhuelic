using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Projet_A3_LeYhuelic
{
    /// <summary>
    /// Création de la classe Distance qui va implémenter Djikstra et qui permet de convertir chaque ligne du CSV en une instance de cette classe
    /// </summary>
    public class Distances
    {
        private string depart;
        private string arrivee;
        private int distance;
        // On met un TimeSpan et pas un DateTime car on a une durée dans le fichier csv
        private TimeSpan duree;

        public Distances()
        {
            depart = "";
            arrivee = "";
            distance = 0;
            duree = new TimeSpan(0, 0, 0);
        }
        public Distances(string depart, string arrivee)
        {
            this.depart = depart;
            this.arrivee = arrivee;
            distance = 0;
            duree = new TimeSpan(0, 0, 0);
        }

        public Distances(string depart, string arrivee, int distance, TimeSpan duree)
        {
            this.depart = depart;
            this.arrivee = arrivee;
            this.distance = distance;
            this.duree = duree;
        }



        public string Depart { get { return depart; } set { depart = value; } }
        public string Arrivee { get { return arrivee; } set { arrivee = value; } }
        public int Distance { get { return distance; } set { distance = value; } }
        public TimeSpan Duree { get { return duree; } set { duree = value; } }

        /// <summary>
        /// Algorithme de Djikstra pour trouver la distance minimale en partant du principe que ce n'est pas un graphe orienté
        /// Si on a Paris => Rouen qui fait 10 km alors Rouen => Paris fait aussi 10 km
        /// </summary>
        public int Djikstra()
        {
            // On récupère toutes les infos du csv ici
            string[] fichier = File.ReadAllLines("Distances.csv");
            //On convertit notre chier CSV en une liste de Distance
            List<Distances> distances = new List<Distances>();
            foreach (string f in fichier)
            {
                string[] inter = f.Split(';');
                // On part du principe que inter[3] est toujours dê la même forme __h__min
                // On fait les différents cas pour bien récuperer le TimeSpan
                TimeSpan duree = new TimeSpan();
                if (inter[3][0]=='0' && inter[3][4]=='0') duree = new TimeSpan(Convert.ToInt32(inter[3][5]), Convert.ToInt32(inter[3][1]), 0);
                if(inter[3][0] != '0' && inter[3][4] == '0') duree = new TimeSpan(Convert.ToInt32(inter[3][4]+inter[3][5]), Convert.ToInt32(inter[3][1]), 0);
                if (inter[3][0] == '0' && inter[3][4] != '0') duree = new TimeSpan(Convert.ToInt32(inter[3][5]), Convert.ToInt32(inter[3][0]+inter[3][1]), 0);
                if (inter[3][0] == '0' && inter[3][4] != '0') duree = new TimeSpan(Convert.ToInt32(inter[3][5]), Convert.ToInt32(inter[3][0] + inter[3][1]), 0);
                if (inter[3][0] != '0' && inter[3][4] != '0') duree = new TimeSpan(Convert.ToInt32(inter[3][4] + inter[3][5]), Convert.ToInt32(inter[3][0] + inter[3][1]), 0);
                distances.Add(new Distances(inter[0], inter[1], Convert.ToInt32(inter[2]), duree));
            }

            // List avec toutes les villes différentes, on utilisera l'indice à la fin pour retrouver la ville
            List<string> Toutes_les_villes = new List<string>();
            foreach(Distances D in distances)
            {
                if(Toutes_les_villes.Contains(D.Depart)==false) Toutes_les_villes.Add(D.Depart);
                if (Toutes_les_villes.Contains(D.Arrivee) == false) Toutes_les_villes.Add(D.Arrivee);
            }

            // Utiliser DJikstra
            // On crée une matrice de ma structure pour représenter Réellement le tableau de Djikstra
            // On a aussi intanciée Toutes_les_villes.Count en nombre de ligne car on sait que l'on ne dépassera jamais cela
            MaStructure[,] mat= new MaStructure[Toutes_les_villes.Count,Toutes_les_villes.Count];
            // Le min signifie que celui qui à la valeur la plus faible
            MaStructure min = new MaStructure(depart,depart, 0);//Initialiser à Paris
            mat[0,0]=min;
            for(int i =1;i<Toutes_les_villes.Count;i++)
            {
                //On initialise à 100000 car on ne peux pas mettre infini mais on considère que vu que l'on reste en France il n'y aura jamais un plus gros trajet
                if (Toutes_les_villes[i]!="Paris") mat[0,i] = new MaStructure(Toutes_les_villes[i], Toutes_les_villes[i], 10000); 
            }

            // Creer le tableau de Djikstra
            int compteur = 1;
            int compte_ancien_min = 0; //Utile pour stopper le while

            // On crée chaque ligne jusqu'à obtenir le résultat cherché
            // Quand la ville du min est = à l'arrivée cela veut dire que l'on a fini Djiikstra
            // Les autres conditions sont au cas où on a fait tout le tableau
            while (min.VilleInitiale != arrivee && compte_ancien_min !=Toutes_les_villes.Count && compteur<15)
            {
                //On crée une liste avec toutes les distances direct entre la ville min actuelle et les autres
                List<Distances> distancesmin = distances.FindAll(x => x.Arrivee == min.VilleInitiale || x.Depart == min.VilleInitiale);
                //On remplit la ligne de Djikstra, sachant que chaque colonne est une ville 
                for (int j =0;j<Toutes_les_villes.Count;j++)
                {                     
                    //Si il y a une distance direct entre les 2 villes
                    Distances direct = distancesmin.Find(x => x.Arrivee == Toutes_les_villes[j] || x.Depart == Toutes_les_villes[j]);
                    // On teste si c'est le min pour l'enlever de l'algorithme
                    // On met -1 car valeur inutile et pareil pour STOP
                    // Au final on remontera le tableau du bas pour retrouver les informations
                    if (min.VilleInitiale == Toutes_les_villes[j]) mat[compteur, j] = new MaStructure("STOP","STOP",-1);
                    else
                    {
                        // Ici on a les 3 cas qui nous font écrire une nouvelle distance avec une nouvelle ville précédente
                        if (direct != null && mat[compteur - 1, j].Valeur > min.Valeur + direct.Distance && mat[compteur - 1, j].VilleInitiale != "STOP")
                        {
                            mat[compteur, j] = new MaStructure(mat[compteur-1,j].VilleInitiale,min.VilleInitiale, min.Valeur + direct.Distance);
                        }
                        // 3 cas : si c'est null, si c'était déjà un minimum et si c'était déjà plus rapide
                        else mat[compteur, j] = mat[compteur - 1, j];
                    }                                                 
                }
                min = new MaStructure("STOP","STOP", 200000); // On met un nombre jamais atteint pour minimum 
                // Compteur pour tester si tout n'a pas déjà été min (Utiliser comme condition dans le while)
                // On cherche le nouveau min 
                compte_ancien_min = 0; 
                for (int i =0; i<Toutes_les_villes.Count; i++)
                {
                    if (mat[compteur, i].IndiceVille != "STOP")
                    {
                        if (mat[compteur, i].Valeur < min.Valeur)
                        {
                            min = mat[compteur, i];
                        }
                    }
                    else compte_ancien_min++;
                }
                compteur++;
            }
            // On sait que notre valeur finale sera dans la colonne de l'arrivée
            int indice = Toutes_les_villes.IndexOf(arrivee);
            //Trouvons notre distance finale
            int newcompteur = 0;
            for (int i=0;i<mat.GetLength(0)-1 && newcompteur==0;i++)
            {
                // La matrice de Struct initialise à null les strings
                if (mat[i + 1, indice].VilleInitiale == null)
                {
                    // Sivi+1 est null,alors i est la valeur final
                    distance = mat[i, indice].Valeur;
                    newcompteur = i;
                }
                //Cas pour la distance la plus éloignée(ici Monaco)
                else if (i == mat.GetLength(0) - 2)
                {
                    distance = mat[i + 1, indice].Valeur;
                    newcompteur = i + 1;
                }
            }
            List<string> AfficherVille = new List<string>() { arrivee };
            //Affichons Toutes les villes que le chauffeur doit parcourir
            int indice2 = Toutes_les_villes.IndexOf(arrivee);
            for (int i = newcompteur; i >= 1; i--)
            {
                string villeinter = mat[i, indice2].IndiceVille;
                if (villeinter != "STOP")
                {
                    AfficherVille.Add(villeinter);
                    indice2 = Toutes_les_villes.IndexOf(villeinter);
                }
            }
            Console.WriteLine("Le chauffeur devra parcourir les villes dans cette ordre la : ");
            for (int i = AfficherVille.Count - 1; i >= 0; i--)
            {
                Console.WriteLine(AfficherVille[i]);
            }
            //Utile pour afficher tout
            //for (int i = 0; i < mat.GetLength(0); i++)
            //{
            //    for (int j = 0; j < mat.GetLength(1); j++)
            //    {
            //        Console.Write($"{mat[i, j].VilleInitiale}, {mat[i, j].IndiceVille} : {mat[i, j].Valeur} //");
            //    }
            //    Console.WriteLine("\n");
            //}
            return distance; 
        }

        /// <summary>
        /// Création d'une structure qui va représenter chaque case du tableau de Djikstra
        /// </summary>
        public struct MaStructure
        {
            // C'est la ville dans laquelle on était juste avant pour faire le chemin le plus cours
            public string IndiceVille;
            // C'est Le nom de notre ville, de notre colonne
            public string VilleInitiale;
            // C'est la distance parcourue
            public int Valeur;

            public MaStructure(string villeinitiale,string indiceville, int valeur)
            {
                IndiceVille = indiceville;
                Valeur = valeur;
                VilleInitiale = villeinitiale;
            }
        }
    }
}
