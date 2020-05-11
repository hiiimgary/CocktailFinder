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

        public void CreateCocktail(Models.ViewModel.Creator model)
        {
            Cocktail c = new Cocktail
            {
                Name = model.Name,
                Type = model.isLong ? "Long" : "Shot",
                Taste = model.Taste,
                Occasion = model.Occasion,
                Recipe = model.Recipe,
                Description = model.Description,
                Embed = model.Video,
                Verified = false
            };
            c.img = new byte[model.image.ContentLength];
            model.image.InputStream.Read(c.img, 0, model.image.ContentLength);

            db.Cocktails.Add(c);
            db.SaveChanges();
            int id = c.Id;

            foreach(var ingredient in model.Ingredients)
            {
                db.connect_ingredient_cocktail.Add(new connect_ingredient_cocktail
                {
                    CocktailID = id,
                    IngredientID = ingredient.Id,
                    Amount = ingredient.Amount,
                    Unit = ingredient.UsedUnit
                });
                db.SaveChanges();
            }
            
            

        }

        private static Models.Extended.Ingredient ToIngredientsModel(Ingredient i)
        {
            return new Models.Extended.Ingredient(i.Id, i.Name, i.Type, i.Property, (double)i.Alcohol, i.Units, 0, "", false);
        }

    }
}