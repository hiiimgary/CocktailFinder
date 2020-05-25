using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CocktailFinder.Models.ViewModel
{
    public class SearchModel
    {
        public bool isLong { get; set; }
        public List<SearchIngredient> Spirits { get; set; } //prop
        public List<SearchIngredient> Liquors { get; set; } //prop
        public List<SearchIngredient> Fruits { get; set; }
        public List<SearchIngredient> SoftDrinks { get; set; }
        public List<SearchIngredient> Syrups { get; set; }
        public List<SearchIngredient> Others { get; set; }
        public string Name { get; set; }

    }

    public class SearchIngredient
    {
        public bool Selected { get; set; }
        public string Item { get; set; }
    }
}