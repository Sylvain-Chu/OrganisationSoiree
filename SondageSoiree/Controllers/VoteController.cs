using SondageSoiree.Models.DAL;
using SondageSoiree.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace SondageSoiree.Controllers
{
    public class VoteController : Controller
    {
                
        public ActionResult Index(int id)
        {
            int idEtudiant = GetIdEtudient();
            Dal d = new Dal();

            if (d.VoteExist(id, idEtudiant))
            {
                return RedirectToAction("Resultat", new { id });
            }

            VoteViewModel vm = new VoteViewModel(d.RenvoieTousLesRestaurants(), id);
            return View(vm);
        }

        [HttpPost]
        public ActionResult Index(VoteViewModel pVote)
        {
            Dal d = new Dal();

            if (ModelState.IsValid)
            {
                int idEtudiant = GetIdEtudient();

                foreach(ChoixRestoViewModel choix in pVote.Choix)
                {
                    if (choix.IsSelected)
                    {
                        d.AjouterVote(pVote.IdSondage, choix.Id, idEtudiant);
                    }
                }

                return RedirectToAction("Resultat", new { id = pVote.IdSondage });
            }

            return View(pVote);
        }

        private int GetIdEtudient()
        {
            return int.Parse(((ClaimsIdentity)User.Identity).Claims.First(s => s.Type == ClaimTypes.NameIdentifier).Value);
        }


        public ActionResult Resultat(int id)
        {
            Dal d = new Dal();
            int idEtudiant = GetIdEtudient();

            if (!d.VoteExist(id, idEtudiant))
                RedirectToAction("Index");

            return View(d.RenvoieResultat(id));
        }

    }
}