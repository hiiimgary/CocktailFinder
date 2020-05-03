using CocktailFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CocktailFinder.Controllers.DAL
{
    
    public class UserRepository
    {
        private readonly DrinkDBEntities db;
        public UserRepository()
        {
            this.db = new DrinkDBEntities();
        }

        public IEnumerable<Models.ViewModel.User> List()
        {
            return db.Users.Select(ToModel).ToList();
        }

        public void Delete(string name)
        {
            var query = db.Users.FirstOrDefault(x => x.Username == name);
            if(query != null)
            {
                db.Users.Remove(query);
                db.SaveChanges();
            }
        }

        public Models.ViewModel.User getUser(string username)
        {
            var q = db.Users.FirstOrDefault(x => x.Username == username);
            return ToModel(q);
        }

        public bool EditUser(Models.ViewModel.User u)
        {
            var user = db.Users.FirstOrDefault(x => x.Username == u.Username);
            if(user != null)
            {
                if(u.Role != null)
                {
                    user.Role = u.Role;
                }
                if(!user.isVerified && u.isVerified)
                {
                    user.isVerified = true;
                }
                db.SaveChanges(); //TODO: Validation broken
                return true;
            }
            return false;
        }


        private Models.ViewModel.User ToModel(Models.User u)
        {
            return new Models.ViewModel.User(u.Username, u.Email, u.Password, u.isVerified, u.Role);
        }
    }
}