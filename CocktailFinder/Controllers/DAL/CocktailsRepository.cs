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

        private static Models.ViewModel.Cocktail ToModel(Cocktail v)
        {
            return new Models.ViewModel.Cocktail(v.Id, v.Name, v.Type, v.Taste, v.Occasion, v.Recipe, v.Description, v.img, v.Embed, v.Verified);
        }

    }
}