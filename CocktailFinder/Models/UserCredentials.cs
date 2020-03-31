using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CocktailFinder.Models
{
    public class UserCredentials
    {
        public UserLogin UserLogin { get; set; }
        public User UserRegistration { get; set; }
    }
}