using CocktailFinder.Models;
using CocktailFinder.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CocktailFinder.Controllers.DAL
{
    public class SearchRepository
    {
        DrinkDBEntities db;
        public SearchRepository()
        {
            db = new DrinkDBEntities();
        }

        public SearchModel InitSearchModel()
        {
            SearchModel model = new SearchModel();

            var spirits = db.Ingredients.Where(x => x.Type == "spirit").Select(y => y.Property).Distinct().ToList();
            var liquors = db.Ingredients.Where(x => x.Type == "liquor").Select(y => y.Property).Distinct().ToList();
            var fruits = db.Ingredients.Where(x => x.Type == "fruit").Select(y => y.Name).Distinct().ToList();
            var others = db.Ingredients.Where(x => x.Type == "other").Select(y => y.Name).Distinct().ToList();
            var softDrinks = db.Ingredients.Where(x => x.Type == "softdrink").Select(y => y.Name).Distinct().ToList();
            var syrups = db.Ingredients.Where(x => x.Type == "syrup").Select(y => y.Name).Distinct().ToList();

            model.Spirits = new List<SearchIngredient>();
            model.Liquors = new List<SearchIngredient>();
            model.Fruits = new List<SearchIngredient>();
            model.Others = new List<SearchIngredient>();
            model.SoftDrinks = new List<SearchIngredient>();
            model.Syrups = new List<SearchIngredient>();

            foreach (var item in spirits)
            {
                model.Spirits.Add(new SearchIngredient { Item = item, Selected = false });
            }
            foreach (var item in liquors)
            {
                model.Liquors.Add(new SearchIngredient { Item = item, Selected = false });
            }
            foreach (var item in fruits)
            {
                model.Fruits.Add(new SearchIngredient { Item = item, Selected = false });
            }
            foreach (var item in others)
            {
                model.Others.Add(new SearchIngredient { Item = item, Selected = false });
            }
            foreach (var item in softDrinks)
            {
                model.SoftDrinks.Add(new SearchIngredient { Item = item, Selected = false });
            }
            foreach (var item in syrups)
            {
                model.Syrups.Add(new SearchIngredient { Item = item, Selected = false });
            }
            model.Name = "";
            model.isLong = true;
            return model;
        }

        public List<Models.ViewModel.Cocktail> getCocktails(SearchModel model)
        {
            
            List<string> ingredientsByName = new List<string>();
            List<string> ingredientsByProperty = new List<string>();
            if(model.Liquors != null)
            {
                foreach (var item in model.Liquors) { if (item.Selected == true) ingredientsByProperty.Add(item.Item); }
            }
            foreach (var item in model.Syrups){ if (item.Selected == true) ingredientsByName.Add(item.Item); }
            foreach(var item in model.Fruits){ if (item.Selected == true) ingredientsByName.Add(item.Item); }
            foreach(var item in model.Others){ if (item.Selected == true) ingredientsByName.Add(item.Item); }
            foreach (var item in model.SoftDrinks){ if (item.Selected == true) ingredientsByName.Add(item.Item); }
            foreach (var item in model.Spirits){ if (item.Selected == true) ingredientsByProperty.Add(item.Item); }

            var results = db.Cocktails.Where(y => y.Type == (model.isLong ? "Long" : "Shot")).Where(y => y.Verified == true);
            if (model.Name != null)
            {
                results = results.Where(x => x.Name.ToLower().Contains(model.Name.ToLower()));
            }

            var cocktails = results.ToList();
            if (results == null) return null;
            foreach(var item in results)
            {
                foreach(var ingred in ingredientsByName)
                {
                    var contains = item.connect_ingredient_cocktail.Where(x => x.Ingredient.Name == ingred).ToList();
                    if (contains.Count == 0)
                    {
                        cocktails.Remove(item);
                        break;
                    } 
                }
                foreach (var ingred in ingredientsByProperty)
                {
                    var contains = item.connect_ingredient_cocktail.Where(x => x.Ingredient.Property == ingred).ToList();
                    if (contains.Count == 0)
                    {
                        cocktails.Remove(item);
                        break;
                    }
                }
            }
            if (cocktails.Count == 0) return null;
            List<Models.ViewModel.Cocktail> final = new List<Models.ViewModel.Cocktail>();
            foreach(var item in cocktails)
            {
                final.Add(ToModel(item));
            }
            return final;

            
        }

        private static Models.ViewModel.Cocktail ToModel(Models.Cocktail v)
        {
            return new Models.ViewModel.Cocktail(v.Id, v.Name, v.Type, v.Taste, v.Occasion, v.Recipe, v.Description, v.img, v.Embed, v.Verified, v.vote_average == null ? 0 : (double)v.vote_average);
        }

    }
}