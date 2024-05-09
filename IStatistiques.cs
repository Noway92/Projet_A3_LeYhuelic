using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_A3_LeYhuelic
{
    /// <summary>
    /// Interface pour bien mettre toutes les statistiques
    /// </summary>
    public interface IStatistiques
    {
        public void nb_livraison_chauffeur(Salarie actuel);
        public void moyenne_prix_commande();
        public void nb_commande(Client c);
        public void moyenne_compte_client();
        public void commande_temps(DateTime début, DateTime fin);
    }
}
