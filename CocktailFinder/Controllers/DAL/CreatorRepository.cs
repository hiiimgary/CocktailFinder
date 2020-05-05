using CocktailFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CocktailFinder.Controllers.DAL
{
    public class CreatorRepository
    {
        DrinkDBEntities db;
        public CreatorRepository()
        {
            this.db = new DrinkDBEntities();
        }

        public Models.ViewModel.Creator GetIngredients()
        {
            Models.ViewModel.Creator creator = new Models.ViewModel.Creator();
            var query = db.Ingredients.Select(ToIngredientsModel).ToList();
            creator.Ingredients = query;
            return creator;
        }

        private static Models.Extended.Ingredient ToIngredientsModel(Ingredient i)
        {
            return new Models.Extended.Ingredient(i.Id, i.Name, i.Type, i.Property, (double)i.Alcohol, i.Units, 0, "cl", false);
        }

    }
}