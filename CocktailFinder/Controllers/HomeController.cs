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
        private readonly UserRepository userRepository;
        private readonly SearchRepository sRepository;

        public HomeController()
        {
            userRepository = new UserRepository();
            cocktailsRepository = new CocktailsRepository();
            sRepository = new SearchRepository();
        }

        // GET: Home
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult Search()
        {
            var model = sRepository.InitSearchModel();
            return View(model);
        }

        public ActionResult SearchCocktails()
        {

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

        public ActionResult ShowDetails(int cocktailId)
        {
            var cocktail = cocktailsRepository.GetCocktailDetails(cocktailId);
            var username = User.Identity.Name;
            if(username != "")
            {
                cocktail.Liked = userRepository.Liked(username, cocktailId);
                cocktail.UserVote = userRepository.UserVote(username, cocktailId);
            }
            return PartialView("_details", cocktail);
        }
    }
}