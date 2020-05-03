using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CocktailFinder.Models.ViewModel
{
    public class User
    {
        public User() { }
        public User(string username, string email, string password, bool isverified, string role)
        {
            this.Username = username;
            this.Email = email;
            this.Password = password;
            this.isVerified = isverified;
            this.Role = role;
        }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool isVerified { get; set; }
        [MaxLength(50)]
        public string Role { get; set; }
    }
}