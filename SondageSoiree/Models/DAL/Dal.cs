using SondageSoiree.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SondageSoiree.Models.DAL
{
    public class Dal : IDal
    {
        private SoireeContext soiree;

        public Dal()
        {
            soiree = new SoireeContext();
        }

        public int AjouterEtudiant(string nom, string prenom, string password)
        {
            throw new NotImplementedException();
        }

        public int AjouterVote(int idSondage, int idResto, int idEtudiant)
        {
            throw new NotImplementedException();
        }

        public Eleve Authentifier(string nom, string password)
        {
            throw new NotImplementedException();
        }

        public int CreerRestaurant(string nom, string adresse, string telephone, string email)
        {
            Restaurant restaurant = new Restaurant(nom, adresse, telephone, email);
            this.soiree.Restaurants.Add(restaurant);
            return this.soiree.SaveChanges();             
            
        }

        public int CreerSondage(DateTime date)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void ModifierRestaurant(int idResto, string nom, string adresse, string telephone, string email)
        {
            throw new NotImplementedException();
        }

        public Eleve RenvoieEtudiant(int idEtudiant)
        {
            throw new NotImplementedException();
        }

        public Restaurant RenvoieRestaurant(int idRestaurant)
        {
            throw new NotImplementedException();
        }

        public IList<Resultat> RenvoieResultat(int idSondage)
        {
            throw new NotImplementedException();
        }

        public IList<Restaurant> RenvoieTousLesRestaurants()
        {
            throw new NotImplementedException();
        }

        public bool RestaurantExist(string nom)
        {
            throw new NotImplementedException();
        }

        public bool VoteExist(int idSondage, int idEtudiant)
        {
            throw new NotImplementedException();
        }
    }
}