using SondageSoiree.Models.DAL;
using SondageSoiree.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SondageSoiree.Controllers
{
    [Authorize]
    public class RestaurantController : Controller
    {
        private readonly IDal _dal;
        public RestaurantController(IDal dal)
        {
            _dal = dal;
        }

        public ActionResult Index()
        {
            return View(_dal.RenvoieTousLesRestaurants());
        }
            
        [Authorize(Roles = "Admin")]
        public ActionResult CreerRestaurant()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult CreerRestaurant(Restaurant poResto)
        {
            if (!ModelState.IsValid)
                return View(poResto);

            
            if (_dal.RestaurantExist(poResto.Nom))
            {
                ModelState.AddModelError("Non", "Un restaurant à déjà le meme nom");
                return View(poResto);
            }

            _dal.CreerRestaurant(poResto.Nom, poResto.Adresse, poResto.Telephone, poResto.Email);
            return RedirectToAction("Index");
        }


        public ActionResult AfficherRestaurant(int id)
        {
            Dal dal = new Dal();
            Restaurant r = dal.RenvoieRestaurant(id);
            return View(r);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ModifierRestaurant(int id)
        {
            Restaurant r = _dal.RenvoieRestaurant(id);
            return View(r);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult ModifierRestaurant(Restaurant restaurant)
        {
            if (!ModelState.IsValid)
                return View(restaurant);


            _dal.ModifierRestaurant(restaurant.Id, restaurant.Nom, restaurant.Adresse, restaurant.Telephone, restaurant.Email);
            return RedirectToAction("AfficherRestaurant", restaurant);
        }
    }
}