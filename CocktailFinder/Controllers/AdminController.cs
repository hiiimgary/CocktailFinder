using CocktailFinder.Controllers.DAL;
using CocktailFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CocktailFinder.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly IngredientsRepository iRepo;
        private readonly CocktailsRepository cRepo;
        private readonly UserRepository uRepo;

        public AdminController()
        {
            iRepo = new IngredientsRepository();
            cRepo = new CocktailsRepository();
            uRepo = new UserRepository();
        }
        public ActionResult Index()
        {
            return View(uRepo.List());            
        }

        public ActionResult DeleteUser(string Name)
        {
            uRepo.Delete(Name);
            return RedirectToAction("Index");
        }

        public ActionResult EditUser(string Name)
        {
            var user = uRepo.getUser(Name);
            if (user != null)
            {
                return View(user);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult EditUser(Models.ViewModel.User u)
        {
            bool success = uRepo.EditUser(u);
            if (success)
            {
                ViewBag.message = "User Successfully updated!";
            }
            else
            {
                ViewBag.message = "Something Went Wrong";
            }
            return RedirectToAction("Index");
            
        }

        public ActionResult Cocktails()
        {
            return View(cRepo.List());
        }

        public ActionResult CocktailDetails(int id)
        {
            return View(cRepo.GetCocktail(id));
        }

        public ActionResult CocktailDelete(int id)
        {
            cRepo.Delete(id);
            return RedirectToAction("Cocktails");
        }

        public ActionResult CocktailConfirm(int id)
        {
            if (cRepo.Confirm(id))
            {
                ViewBag.message = "Confirmed";
            } else
            {
                ViewBag.message = "Error";
            }
            return RedirectToAction("CocktailDetails", cRepo.GetCocktail(id));
        }

        public ActionResult Ingredients()
        {
            return View(iRepo.List());
            
        }
        public ActionResult CreateIngredient()
        {
            return View();
        }


        [HttpPost]
        public ActionResult CreateIngredient(Models.ViewModel.Ingredient i)
        {
            bool success = iRepo.Create(i);
            if (success)
            {
                ViewBag.message = "Successfully created!";
            } else
            {
                ViewBag.message = "Something went wrong!";
            }
            return RedirectToAction("Ingredients");
        }

        public ActionResult DeleteIngredient(int id)
        {
            iRepo.Delete(id);
            return RedirectToAction("Ingredients");

        }
        public ActionResult EditIngredient(int id)
        {
            var user = iRepo.GetIngredient(id);
            if(user != null)
            {
                return View(user);
            }
            return RedirectToAction("Ingredients");
        }
        [HttpPost]
        public ActionResult EditIngredient(Models.ViewModel.Ingredient i)
        {
            bool success = iRepo.Edit(i);
            if (success)
            {
                ViewBag.message = "Successful!";
            } else
            {
                ViewBag.message = "Something went wrong!";
            }
            return RedirectToAction("Ingredients");
        }
    }
}