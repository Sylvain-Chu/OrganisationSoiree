using SondageSoiree.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

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
            Eleve eleve = new Eleve()
            {
                Nom = nom,
                Prenom = prenom,
                Password = Crypto.HashPassword(password)
            };
            
            this.soiree.Eleves.Add(eleve);
            return this.soiree.SaveChanges();
        }

        public int AjouterVote(int idSondage, int idResto, int idEtudiant)
        {
            Vote v = new Vote()
            {
                IdEtudiant = idEtudiant,
                IdResto = idResto,
                IdSondage = idSondage
            };
            this.soiree.Votes.Add(v);
            return this.soiree.SaveChanges();
        }

        public Eleve Authentifier(string nom, string password)
        {

            Eleve el = soiree.Eleves.FirstOrDefault(e => e.Nom == nom);
            
            if (el != null && Crypto.VerifyHashedPassword(el.Password, password))
            {
                return el;
            }
            else
            {
                return null;
            }
        }

        public int CreerRestaurant(string nom, string adresse, string telephone, string email)
        {
            Restaurant restaurant = new Restaurant(nom, adresse, telephone, email);
            this.soiree.Restaurants.Add(restaurant);
            return this.soiree.SaveChanges();
        }

        public int CreerSondage(DateTime date)
        {
            Sondage sondage = new Sondage(date);
            this.soiree.Sondages.Add(sondage);
            return this.soiree.SaveChanges();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool EtudiantExist(string nom)
        {
            return this.soiree.Eleves.Any(e => e.Nom == nom);
        }

        public void ModifierRestaurant(int idResto, string nom, string adresse, string telephone, string email)
        {
            Restaurant r = RenvoieRestaurant(idResto);
            r.Nom = nom;
            r.Adresse = adresse;
            r.Telephone= telephone;
            r.Email = email;
            soiree.SaveChanges();
        }

        public Eleve RenvoieEtudiant(int idEtudiant)
        {
            return this.soiree.Eleves.Find(idEtudiant);
        }

        public Restaurant RenvoieRestaurant(int idRestaurant)
        {
            return this.soiree.Restaurants.Find(idRestaurant);
        }

        public IList<Resultat> RenvoieResultat(int idSondage)
        {
            return this.soiree.Restaurants
                .GroupJoin(
                this.soiree.Votes.Where(v => v.IdSondage == idSondage),
                r => r.Id,
                v => v.IdResto,
                (r, v) => new Resultat() { Nom = r.Nom, NbVote = v.Count() }
                )
                .OrderByDescending(r => r.NbVote)
                .ToList();
        }

        public Sondage RenvoieSondage(int idSondage)
        {
            return this.soiree.Sondages.Find(idSondage);
        }

        public IList<Restaurant> RenvoieTousLesRestaurants()
        {
            return this.soiree.Restaurants.ToList();
        }

        public IList<Sondage> RenvoieTousLesSondages()
        {
            return this.soiree.Sondages.ToList();
        }

        public bool RestaurantExist(string nom)
        {
            return this.soiree.Restaurants.Any(r => r.Nom == nom);
        }

        public bool VoteExist(int idSondage, int idEtudiant)
        {
            return this.soiree.Votes.Any(v => v.IdSondage == idSondage && v.IdEtudiant == idEtudiant);
        }

    }
}