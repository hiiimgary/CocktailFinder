using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CocktailFinder.Controllers
{
    public class HomeController : Controller
    {
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
            return View();
        }

        public ActionResult SignIn()
        {
            return View();
        }
    }
}