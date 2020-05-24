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

    }
}