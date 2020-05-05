using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CocktailFinder.Models.ViewModel
{
    public class Creator
    {
        public Creator()
        {
            this.Name = "Cocktail";
            this.isLong = true;
            this.Taste = "taste...";
            this.Recipe = "recipe";
            this.Description = "description...";
            this.Video = "youtube...";
        }
        public string Name { get; set; }
        public bool isLong { get; set; }
        public string Taste { get; set; }
        public string Recipe { get; set; }
        public string Description { get; set; }
        public string Video { get; set; }
        public IEnumerable<Models.Extended.Ingredient> Ingredients { get; set; }
    }
}