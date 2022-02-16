using SondageSoiree.Models.DAL;
using SondageSoiree.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SondageSoiree.Controllers
{
    public class SondageController : Controller
    {
        private readonly IDal _dal;
        public SondageController(IDal dal)
        {
            _dal = dal;
        }

        public ActionResult Index()
        {
            return View(_dal.RenvoieTousLesSondages());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult CreerSondage()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult CreerSondage(Sondage sondage)
        {
            if (!ModelState.IsValid)
                return View(sondage);


            _dal.CreerSondage(sondage.Date);
            return RedirectToAction("Index");
        }

        public ActionResult AfficherSondage(int id)
        {
            Dal dal = new Dal();
            Sondage r = dal.RenvoieSondage(id);
            return View(r);
        }
    }
}