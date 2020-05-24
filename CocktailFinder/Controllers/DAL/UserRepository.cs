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
                user.ConfirmPassword = user.Password;
                db.SaveChanges(); //TODO: Validation broken
                return true;
            }
            return false;
        }

        public bool Liked(string username, int id)
        {
            var user = db.Users.FirstOrDefault(x => x.Username == username);
            if(user != null)
            {
                var cocktail = user.connect_user_cocktail.FirstOrDefault(x => x.CocktailID == id);
                if(cocktail != null)
                {
                    return cocktail.IsLiked;
                }
            }
            return false;
        }
        
        public int UserVote(string username, int id)
        {
            var userVote = db.Users.FirstOrDefault(x => x.Username == username).connect_user_cocktail.FirstOrDefault(y => y.CocktailID == id);
            if (userVote == null) return 0;
            if (userVote.Rate == -1) return 0;
            return userVote.Rate;
        }

        public int setUserVote(string username, int cocktailId, int newVote)
        {
            var userCocktail = db.Users.FirstOrDefault(x => x.Username == username).connect_user_cocktail.FirstOrDefault(x => x.CocktailID == cocktailId);
            if(userCocktail == null)
            {
                db.connect_user_cocktail.Add(new connect_user_cocktail { CocktailID = cocktailId, IsCreator = false, IsLiked = false, Rate = newVote, Username = username });
                var cocktail = db.Cocktails.Find(cocktailId);
                if (cocktail == null) return 0;
                cocktail.Total_Votes += newVote;
                cocktail.Number_of_Votes++;
                cocktail.vote_average = cocktail.Total_Votes / cocktail.Number_of_Votes;
                db.SaveChanges();
            } else
            {
                var cocktail = db.Cocktails.Find(cocktailId);
                if (cocktail == null) return 0;
                if (userCocktail.Rate == -1)
                {
                    userCocktail.Rate = newVote;
                    cocktail.Total_Votes += newVote;
                    cocktail.Number_of_Votes++;
                    cocktail.vote_average = cocktail.Total_Votes / cocktail.Number_of_Votes;
                } else
                {
                    cocktail.Total_Votes = cocktail.Total_Votes - userCocktail.Rate + newVote;
                    cocktail.vote_average = cocktail.Total_Votes / cocktail.Number_of_Votes;
                    userCocktail.Rate = newVote;
                }
                db.SaveChanges();
            }
            return newVote;
        }

        public bool Like(bool like, string username, int cocktailId)
        {
            var userCocktail = db.Users.FirstOrDefault(x => x.Username == username);
            if(userCocktail != null)
            {
                var cocktail = userCocktail.connect_user_cocktail.FirstOrDefault(y => y.CocktailID == cocktailId);
                if (cocktail != null)
                {
                    cocktail.IsLiked = like;
                    db.SaveChanges();
                    return true;
                } else
                {
                    db.connect_user_cocktail.Add(new connect_user_cocktail { Username = username, CocktailID = cocktailId, IsCreator = false, IsLiked = like, Rate = -1 });
                    db.SaveChanges();
                    return true;
                }
            }

            return false;
        }


        private Models.ViewModel.User ToModel(Models.User u)
        {
            return new Models.ViewModel.User(u.Username, u.Email, u.Password, u.isVerified, u.Role);
        }
    }
}