using CocktailFinder.Controllers.DAL;
using CocktailFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CocktailFinder.Controllers
{
    public class HomeController : Controller
    {
        private readonly CocktailsRepository cocktailsRepository;
        public HomeController()
        {
            
            cocktailsRepository = new CocktailsRepository();
        }

        // GET: Home
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult Search()
        {
            return View();
        }

        public ActionResult TopRated()
        {
            var cocktails = cocktailsRepository.List();
            return View(cocktails);
        }

        public ActionResult SignIn()
        {
            return View();
        }
    }
}