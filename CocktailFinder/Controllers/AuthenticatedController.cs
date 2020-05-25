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
        private readonly CocktailsRepository cocktailRepo;
        private readonly UserRepository uRepo;


        public AuthenticatedController()
        {
            iRepo = new IngredientsRepository();
            cRepo = new CreatorRepository();
            uRepo = new UserRepository();
            cocktailRepo = new CocktailsRepository();
        }

        public ActionResult Creator()
        {
            var creatorModel = cRepo.GetIngredients();
            
            return View(creatorModel);
        }
        [HttpPost]
        public ActionResult Creator(Models.ViewModel.Creator model)
        {
            var creatorModel = cRepo.GetIngredients();

            if (!ModelState.IsValid)
            {
                ViewBag.message = "The Given data doesn't meet the requirements!";
                return View(creatorModel);
            }

            var ingredients = new List<Models.Extended.Ingredient>();
            foreach (var item in model.Ingredients)
            {
                if (item.Amount > 0)
                {
                    ingredients.Add(item);
                }
            }
            model.Ingredients = ingredients;


            cRepo.CreateCocktail(model);
            ViewBag.message = "Cocktail created successfully! We are processing your entry!";
            return View(creatorModel);
        }

        //[HttpPost]
        public ActionResult AddIngredientToSession(int id)
        {
            var creatorModel = cRepo.GetIngredients();
            bool success = false;

            //Add ingredient to Session of id equals
            foreach(var item in creatorModel.Ingredients)
            {
                if(item.Id == id)
                {
                    success = true;
                    if(Session["usedIds"] == null)
                    {
                        List<int> idList = new List<int>();                                        
                        idList.Add(id);                        
                        Session["usedIds"] = idList;
                        ViewBag.message = "Ingredient added!";
                    } 
                    else
                    {
                        
                        var idList = Session["usedIds"] as List<int>;
                        foreach (var n in idList)
                        {
                            if(n == item.Id)
                            {
                                ViewBag.message = "Ingredient already Added!";
                                return View("Creator", creatorModel);
                            }
                        }
                        
                        idList.Add(id);
                        
                        Session["usedIds"] = idList;
                        ViewBag.message = "Ingredient added!";
                    }
                    
                    break;                    
                }
            }
            if (!success)
            {
                ViewBag.message = "There is no such Ingredient!";
            }
            
            return View("Creator", creatorModel);
        }

        public ActionResult RemoveIngredientFromSession(int id)
        {
            
            var idList = Session["usedIds"] as List<int>;
            foreach(var item in idList.ToList())
            {
                if(item == id)
                {
                    idList.Remove(item);
                }
            }
            
            Session["usedIds"] = idList;
            var creatorModel = cRepo.GetIngredients();
            return View("Creator", creatorModel);
            
        }

        public bool Like(bool like, int cocktailId)
        {
            var username = User.Identity.Name;
            uRepo.Like(like, username, cocktailId);
            return like;

        }

        public int getUserVote(int cocktailId)
        {
            var username = User.Identity.Name;
            return uRepo.UserVote(username, cocktailId);
        }

        public int setUserVote(int cocktailId, int newRate)
        {
            var username = User.Identity.Name;
            int newvote = uRepo.setUserVote(username, cocktailId, newRate);
            return newvote;
        }

        public ActionResult Favorites()
        {
            var username = User.Identity.Name;
            var cocktails = cocktailRepo.GetFavorites(username);
            return View(cocktails);
        }

        public ActionResult ForYou()
        {
            var username = User.Identity.Name;
            var res = cocktailRepo.GetForYou(username);
            return View(res);
        }
    }
}