using CocktailFinder.Controllers.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CocktailFinder.Controllers
{
    [Authorize]
    public class AuthenticatedController : Controller
    {
        private readonly IngredientsRepository iRepo;
        private readonly CreatorRepository cRepo;

        public AuthenticatedController()
        {
            iRepo = new IngredientsRepository();
            cRepo = new CreatorRepository();
        }

        public ActionResult Creator()
        {
            var item = cRepo.GetIngredients();
            
            return View(item);
        }
    }
}