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
    
    public partial class connect_user_cocktail
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public int CocktailID { get; set; }
        public bool IsLiked { get; set; }
        public int Rate { get; set; }
        public bool IsCreator { get; set; }
    
        public virtual Cocktail Cocktail { get; set; }
        public virtual User User { get; set; }
    }
}
