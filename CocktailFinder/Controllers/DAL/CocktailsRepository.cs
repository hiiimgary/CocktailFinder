using CocktailFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CocktailFinder.Controllers.DAL
{
    public class CocktailsRepository
    {
        private readonly DrinkDBEntities db;

        public CocktailsRepository()
        {
            this.db = new DrinkDBEntities();
        }

        public IEnumerable<Models.ViewModel.Cocktail> List()
        {
            return db.Cocktails.Select(ToModel).ToList();
        }

        public void Delete(int id)
        {
            var item = db.Cocktails.Find(id);
            if(item != null)
            {
                var connectIngred = db.connect_ingredient_cocktail.Where(x => x.CocktailID == id);
                if(connectIngred != null)
                {
                    foreach(var row in connectIngred)
                    {
                        db.connect_ingredient_cocktail.Remove(row);
                    }
                }
                var connectUser = db.connect_user_cocktail.Where(x => x.CocktailID == id);
                if (connectUser != null)
                {
                    foreach (var row in connectUser)
                    {
                        db.connect_user_cocktail.Remove(row);
                    }
                }
                db.Cocktails.Remove(item);
                db.SaveChanges();
            }
            return;
        }

        public Models.ViewModel.Cocktail GetCocktail(int id)
        {
            return ToModel(db.Cocktails.Find(id));
        }

        public Models.ViewModel.CocktailDetails GetCocktailDetails(int id)
        {
            var cocktail = db.Cocktails.Find(id);
            var ingredients = cocktail.connect_ingredient_cocktail;
            List<Models.ViewModel.IngredientMin>ingredientViewModel = new List<Models.ViewModel.IngredientMin>();
            foreach(var item in ingredients.ToList())
            {
                ingredientViewModel.Add(new Models.ViewModel.IngredientMin(item.Ingredient.Id, item.Ingredient.Name, item.Amount, item.Unit));
            }
            var users = cocktail.connect_user_cocktail;
            double average = 0;
            foreach(var user in users.ToList())
            {
                average += user.Rate;
            }
            average = average / users.Count();

            Models.ViewModel.CocktailDetails res = new Models.ViewModel.CocktailDetails();
            res.Cocktail = ToModel(cocktail);
            res.Ingredients = ingredientViewModel;
            res.VoteAverage = average;
            res.Liked = false;
            return res;
        }

        public bool Confirm(int id)
        {
            var item = db.Cocktails.Find(id);
            if(item != null)
            {
                item.Verified = true;
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Models.ViewModel.Cocktail> GetFavorites(string username)
        {
            var connect = db.Users.FirstOrDefault(x => x.Username == username).connect_user_cocktail.Where(y => y.IsLiked == true);
            List<Models.ViewModel.Cocktail> cocktails = new List<Models.ViewModel.Cocktail>();
            foreach(var item in connect)
            {
                cocktails.Add(ToModel(item.Cocktail));
            }
            return cocktails;
        }

        private static Models.ViewModel.Cocktail ToModel(Cocktail v)
        {
            return new Models.ViewModel.Cocktail(v.Id, v.Name, v.Type, v.Taste, v.Occasion, v.Recipe, v.Description, v.img, v.Embed, v.Verified, v.vote_average == null ? 0 : (double)v.vote_average);
        }

    }
}