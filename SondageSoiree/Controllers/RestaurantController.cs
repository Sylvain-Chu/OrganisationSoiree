using SondageSoiree.Models.DAL;
using SondageSoiree.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SondageSoiree.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly IDal _dal;
        public RestaurantController(IDal dal)
        {
            _dal = dal;
        }


        // GET: Restaurant
        public ActionResult Index()
        {
            return View(_dal.RenvoieTousLesRestaurants());
        }

        public ActionResult CreerRestaurant()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreerRestaurant(Restaurant poResto)
        {
            if (!ModelState.IsValid)
                return View(poResto);

            Dal dal = new Dal();

            if (dal.RestaurantExist(poResto.Nom))
            {
                ModelState.AddModelError("Non", "Un restaurant à déjà le meme nom");
                return View(poResto);
            }

            dal.CreerRestaurant(poResto.Nom, poResto.Adresse, poResto.Telephone, poResto.Email);
            return RedirectToAction("Index");
        }

        public ActionResult AfficherRestaurant(int id)
        {
            Dal dal = new Dal();
            Restaurant r = dal.RenvoieRestaurant(id);
            return View(r);
        }
        public ActionResult ModifierRestaurant(int id)
        {
            Dal dal = new Dal();
            Restaurant r = dal.RenvoieRestaurant(id);
            return View(r);
        }

        [HttpPost]
        public ActionResult ModifierRestaurant(Restaurant restaurant)
        {
            if (!ModelState.IsValid)
                return View(restaurant);

            Dal dal = new Dal();

            dal.ModifierRestaurant(restaurant.Id, restaurant.Nom, restaurant.Adresse, restaurant.Telephone, restaurant.Email);
            return RedirectToAction("AfficherRestaurant", restaurant);
        }
    }
}