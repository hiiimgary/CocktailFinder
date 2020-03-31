using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CocktailFinder.Models
{
    [MetadataType(typeof(UserMetadata))]
    public partial class User
    {
        public string ConfirmPassword { get; set; }
    }

    public class UserMetadata
    {
        [Display(Name ="Username")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Username Required!")]
        public string Username { get; set; }

        [Display(Name ="Email")]
        [Required(AllowEmptyStrings =false, ErrorMessage = "Email adress required!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [MinLength(6, ErrorMessage = "Password must be at least 6 characters!")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password required!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and Confirm Password must be the same!")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}