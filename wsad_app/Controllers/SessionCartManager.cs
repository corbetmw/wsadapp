using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wsad_app.Models.DataAccess;
using System.Data.Entity;
using System.Web.Mvc;

namespace wsad_app.Controllers
{
    public class SessionCartManager
    {
        internal void AddToCart(string username, int sessionId)
        {
            //Establish Db Context
            using (wsadDbContext context = new wsadDbContext())
            {
                //Capture User Id based on Username
                int? userId = context.Users
                    .Where(row => row.UserName.ToLower() == username.ToLower())
                    .Select(row => row.Id)
                    .FirstOrDefault();

                if (userId.HasValue == false)
                {
                    throw new ArgumentException("Invalid Username");
                }

                /*Check if a session exists for this user's cart*/
                //Does Session Id exist in database?
                if (context.Sessions.Any(row => row.Id == sessionId) == false)
                {
                    throw new ArgumentException("Invalid Session Id");
                }

                SessionCart cartItem = context.SessionCarts
                    .Where(row => row.UserId == userId.Value && row.SessionId == sessionId)
                    .FirstOrDefault();


                //No session exists -- add new session
                cartItem = new SessionCart()
                {
                    UserId = userId.Value,
                    SessionId = sessionId,
                    IsActive = true,
                    DateAdded = DateTime.Now,
                };

               context.SessionCarts.Add(cartItem);
                

                //Update Database
                context.SaveChanges();
            }
        }

        internal IQueryable<SessionCart> GetAllItems(string username, bool asNoTracking = false)
        {
            wsadDbContext context = new wsadDbContext();

            //Get USer Id from Username
            int? userId = context.Users.Where(x => x.UserName.ToLower() == username.ToLower())
                .Select(x => x.Id).FirstOrDefault();

            //Check Username is valid
            if (userId == null) { return null; }

            //Query Items
            IQueryable<SessionCart> results = context.SessionCarts
                .Include(row => row.Session)
                .Where(row => row.UserId == userId.Value && row.IsActive == true);

            //Check for As No Tracking
            if (asNoTracking == false)
            {
                results = results.AsNoTracking();
            }

            //Return Active Session Cart Items for this user
            return results;
        }
    }
}