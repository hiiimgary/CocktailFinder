using CocktailFinder.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name Required!")]
        [MaxLength(50)]
        public string Name { get; set; }

        public bool isLong { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Taste Required!")]
        [MaxLength(50)]
        public string Taste { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Occasion Required!")]
        public string Occasion { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Recipe Required!")]
        [DataType(DataType.MultilineText)]
        public string Recipe { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Description Required!")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Video Required!")]
        public string Video { get; set; }

        [ValidateFile(ErrorMessage = "Please select a PNG image smaller than 1MB")]
        public HttpPostedFileBase image { get; set; }
        public List<Models.Extended.Ingredient> Ingredients { get; set; }
    }
}