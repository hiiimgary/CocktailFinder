//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CocktailFinder.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class contains_alcohol
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public string Unit { get; set; }
        public int CocktailID { get; set; }
        public int AlcoholID { get; set; }
    
        public virtual Alcohol Alcohol { get; set; }
        public virtual Cocktail Cocktail { get; set; }
    }
}
