using CocktailFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CocktailFinder.Controllers.DAL
{
    public class IngredientsRepository
    {
        private readonly DrinkDBEntities db;
        public IngredientsRepository()
        {
            this.db = new DrinkDBEntities();
        }

        public IEnumerable<Models.ViewModel.Ingredient> List()
        {
            return db.Ingredients.Select(ToModel).ToList();
        }
        public bool Create(Models.ViewModel.Ingredient i)
        {
            var q = db.Ingredients.Where(x => x.Name == i.Name).FirstOrDefault();
            if (q != null)
            {
                return false;
            }

            db.Ingredients.Add(new Models.Ingredient { Name = i.Name, Type = i.Type, Property = i.Property, Alcohol = i.Alcohol, Units = i.Units});
            db.SaveChanges();
            return true;
        }

        public void Delete(int id)
        {
            var query = db.Ingredients.Where(x => x.Id == id).FirstOrDefault();
            if(query != null)
            {
                db.Ingredients.Remove(query);
                db.SaveChanges();
            }
        }

        public bool Edit(Models.ViewModel.Ingredient i)
        {
            var query = db.Ingredients.FirstOrDefault(x => x.Id == i.Id);
            if(query != null)
            {
                query.Name = i.Name;
                query.Property = i.Property;
                query.Type = i.Type;
                query.Alcohol = i.Alcohol;
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public Models.ViewModel.Ingredient GetIngredient(int id)
        {
            var query = db.Ingredients.FirstOrDefault(x => x.Id == id);
            return ToModel(query);
        }

        private static Models.ViewModel.Ingredient ToModel(Ingredient i)
        {
            return new Models.ViewModel.Ingredient(i.Id, i.Name, i.Type, i.Property, (double) i.Alcohol, i.Units);
        }
    }
}