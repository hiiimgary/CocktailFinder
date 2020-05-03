﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CocktailFinder.Models.ViewModel
{
    public class Ingredient
    {
        public Ingredient() { }
        public Ingredient(int id, string name, string type, string prop, double alcohol)
        {
            this.Id = id;
            this.Name = name;
            this.Type = type;
            this.Property = prop;
            this.Alcohol = alcohol;
        }

        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        [Required]
        public string Type { get; set; }
        [MaxLength(50)]
        [Required]
        public string Property { get; set; }
        public double Alcohol { get; set; }
    }
}