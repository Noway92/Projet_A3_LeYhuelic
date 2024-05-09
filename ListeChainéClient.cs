using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
namespace Projet_A3_LeYhuelic
{

    // Classe qui réécris toutes les fonctions du Type List<> mais pour une liste chainée (AU FINAL PAS UTILISE)
    //Region avec beaucoup de fonctions de List qui ont été réécrites pour une liste chainée
    #region 
    public class ListeChainéClient
    {
        private Client premierclient;

        public ListeChainéClient(Client c)
        {
            this.premierclient= c;
        }
        public ListeChainéClient()
        {
            this.premierclient = null;
        }
        public Client PremierClient{set { this.premierclient = value; }get { return this.premierclient; }}

        /// <summary>
        /// Ajoute un client dans la liste chainée
        /// </summary>
        /// <param name="c"></param>
        public void Add(Client c)
        {
            if (premierclient != null)
            {
                Client Dernier = DernierClient();
                Dernier.ClientSuivant = c;
            }
            else premierclient = c;
        }
        public Client DernierClient()
        {
            Client c = this.premierclient; //tête de la liste
            if (this.premierclient != null)
            {
                while (c.ClientSuivant != null)
                {//on passe au maillon suivant
                    c = c.ClientSuivant; //avancer dans la liste
                }
            }
            return c;
        }
        /// <summary>
        /// On peut maintenant supprimer un client de la liste chainée
        /// </summary>
        /// <param name="c"></param>
        public void Remove(Client c)
        {
            //Cas ou tout est null
            if (premierclient == null) return;
            //Cas où c'est la tête de la liste chainée que l'on enlève
            if(premierclient==c)
            {
                premierclient = c.ClientSuivant;
                return;
            }
            Client actuel = premierclient;
            while (actuel.ClientSuivant != null)
            {
                if (actuel.ClientSuivant == c)
                {
                    // On a trouvé le nœud précédant c
                    actuel.ClientSuivant = c.ClientSuivant; // Suppression de c de la liste
                    return; // On a supprimé l'élément, donc on peut quitter la méthode
                }
                actuel = actuel.ClientSuivant;
            }
        }

        /// <summary>
        /// Contains en récursif
        /// </summary>
        /// <param name="actuel"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public bool Contains(Client actuel,Client c)
        {
            if (actuel == null) return false;
            if (actuel == c) return true;
            return Contains(actuel.ClientSuivant, c);

        }
        /// <summary>
        /// FindAll en itératif
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public List<Client> FindAll(Predicate<Client> match)
        {
            List<Client> resultList = new List<Client>();
            Client actuel = premierclient;
            while (actuel != null)
            {
                if (match(actuel))
                {
                    resultList.Add(actuel);
                }
                actuel = actuel.ClientSuivant;
            }
            return resultList;
        }

        public Client Find(Predicate<Client> match)
        {
            Client actuel = premierclient;
            while (actuel != null)
            {
                if (match(actuel))
                {
                    return actuel;
                }
                actuel = actuel.ClientSuivant;
            }
            return null;
        }

        public void ForEach(Action<Client> action)
        {
            Client actuel = premierclient;
            while (actuel != null)
            {
                action(actuel);
                actuel = actuel.ClientSuivant;
            }
        }
        public void Sort()
        {
            Client actuel = premierclient;
            Client index = null;
            Client min = null;
            Client minPrecedent = null;

            while (actuel != null)
            {
                index = actuel.ClientSuivant;
                min = actuel;
                minPrecedent = null;

                while (index != null)
                {
                    if (min.CompareTo(index) > 0)
                    {
                        min = index;
                        minPrecedent = actuel;
                    }
                    index = index.ClientSuivant;
                }

                if (min != actuel)
                {
                    // Échange les valeurs des nœuds min et actuel
                    Client temp = min.ClientSuivant;
                    if (minPrecedent != null)
                    {
                        minPrecedent.ClientSuivant = min.ClientSuivant;
                    }
                    else
                    {
                        premierclient = min.ClientSuivant;
                    }
                    min.ClientSuivant = actuel.ClientSuivant;
                    actuel.ClientSuivant = temp;
                }

                actuel = actuel.ClientSuivant;
            }
        }
    }
    #endregion
}
*/