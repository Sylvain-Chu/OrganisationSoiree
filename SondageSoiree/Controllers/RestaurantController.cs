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
            if (ModelState.IsValid)
            {
                Dal dal = new Dal();

                if (dal.RestaurantExist(poResto.Nom))
                {
                    dal.CreerRestaurant(poResto.Nom, poResto.Adresse, poResto.Telephone, poResto.Email);
                }

            }
            return View();
        }
    }
}