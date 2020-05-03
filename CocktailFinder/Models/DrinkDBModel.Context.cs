﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DrinkDBEntities : DbContext
    {
        public DrinkDBEntities()
            : base("name=DrinkDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Alcohol> Alcohols { get; set; }
        public virtual DbSet<Cocktail> Cocktails { get; set; }
        public virtual DbSet<connect_user_cocktail> connect_user_cocktail { get; set; }
        public virtual DbSet<Fruit> Fruits { get; set; }
        public virtual DbSet<Liquor> Liquors { get; set; }
        public virtual DbSet<Other> Other { get; set; }
        public virtual DbSet<SoftDrink> SoftDrinks { get; set; }
        public virtual DbSet<Spirit> Spirits { get; set; }
        public virtual DbSet<Syrup> Syrups { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<contains_alcohol> contains_alcohol { get; set; }
        public virtual DbSet<contains_fruit> contains_fruit { get; set; }
        public virtual DbSet<contains_other> contains_other { get; set; }
        public virtual DbSet<contains_softdrink> contains_softdrink { get; set; }
        public virtual DbSet<contains_syrup> contains_syrup { get; set; }
        public virtual DbSet<connect_ingredient_cocktail> connect_ingredient_cocktail { get; set; }
        public virtual DbSet<Ingredient> Ingredients { get; set; }
    }
}
