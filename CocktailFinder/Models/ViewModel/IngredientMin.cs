using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CocktailFinder.Models.ViewModel
{
    public class IngredientMin
    {
        public IngredientMin(int id, string name, double amount, string unit)
        {
            this.Id = id;
            this.Name = name;
            this.Amount = amount;
            this.UsedUnit = unit;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public string UsedUnit { get; set; }
    }
}