using CocktailFinder.Validators;
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
            this.Name = "";
            this.isLong = true;
            this.Taste = "";
            this.Occasion = "";
            this.Recipe = "";
            this.Description = "";
            this.Video = "";
        }
        public string Name { get; set; }
        public bool isLong { get; set; }
        public string Taste { get; set; }
        public string Occasion { get; set; }
        public string Recipe { get; set; }
        public string Description { get; set; }
        public string Video { get; set; }
        [ValidateFile(ErrorMessage = "Please select a PNG image smaller than 1MB")]
        public HttpPostedFileBase image { get; set; }
        public List<Models.Extended.Ingredient> Ingredients { get; set; }
    }
}