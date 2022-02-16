using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using SondageSoiree.Models.DAL;
using SondageSoiree.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace SondageSoiree.Controllers
{
    [AllowAnonymous]
    public class CompteController : Controller
    {
        
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Eleve eleve)
        {
            Dal dal = new Dal();

            Eleve e = dal.Authentifier(eleve.Nom, eleve.Password);
            IdentitySignin(e);
           
            if(e == null)
            {
                ModelState.AddModelError("Password", "Erreur de mot de passe");
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Restaurant");
            }
        }


        public ActionResult CreerCompte()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreerCompte(Eleve eleve)
        {
            if (!ModelState.IsValid)
                return View(eleve);

            Dal dal = new Dal();


            if (dal.EtudiantExist(eleve.Nom))
            {
                ModelState.AddModelError("Non", "Un Compte a déjà le même nom");
                return View(eleve);
            }

            dal.AjouterEtudiant(eleve.Nom, eleve.Prenom, eleve.Password);

            return RedirectToAction("Index", "Restaurant");

        }

        public ActionResult Logout()
        {
            IdentitySignout();
            return RedirectToAction("Login");
        }

        private void IdentitySignin(Eleve eleve)
        {
            var claims = new List<Claim>
            {
            new Claim(ClaimTypes.NameIdentifier, eleve.Id.ToString()),
            new Claim(ClaimTypes.Name, eleve.Nom)
            };

            if (eleve.Role != null)
                claims.Add(new Claim(ClaimTypes.Role, eleve.Role));

            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

            HttpContext.GetOwinContext().Authentication.SignIn(new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = true,
                ExpiresUtc = DateTime.UtcNow.AddDays(7)
            }, identity);

            
        }

        private void IdentitySignout()
        {
            HttpContext.GetOwinContext().Authentication.SignOut(
            DefaultAuthenticationTypes.ApplicationCookie,
            DefaultAuthenticationTypes.ExternalCookie
            );
        }


    }
}