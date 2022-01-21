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
        // GET: Restaurant
        public ActionResult Index()
        {
            return View();
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

                dal.CreerRestaurant(poResto.Nom, poResto.Adresse, poResto.Telephone, poResto.Email);
            }
            return View();
        }
    }
}