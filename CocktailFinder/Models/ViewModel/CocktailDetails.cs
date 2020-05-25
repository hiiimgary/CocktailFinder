using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CocktailFinder.Models.ViewModel
{
    public class CocktailDetails
    {
        public Models.ViewModel.Cocktail Cocktail { get; set; }
        public List<Models.ViewModel.IngredientMin> Ingredients { get; set; }
        public bool Liked { get; set; }
        public double VoteAverage { get; set; }
        public double UserVote { get; set; }

    }
}