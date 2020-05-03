using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CocktailFinder.Models.ViewModel
{
    public class Cocktail
    {
        public Cocktail(int id, string name, string type, string taste, string occasion, string recipe, string description, byte[] img)
        {
            this.Id = id;
            this.Name = name;
            this.Type = type;
            this.Taste = taste;
            this.Occasion = occasion;
            this.Recipe = recipe;
            this.Description = description;
            this.img = img;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Taste { get; set; }
        public string Occasion { get; set; }
        public string Recipe { get; set; }
        public string Description { get; set; }
        public byte[] img { get; set; }
    }
}