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

        public IQueryable<SessionCart> GetAllSessionsByUser(string username, int? id, bool asNoTracking = false)
        {
            wsadDbContext context = new wsadDbContext();

            int? userId;

            //If username is not null, get USer Id from Username
            if (username != null)
            {
                userId = context.Users.Where(x => x.UserName.ToLower() == username.ToLower())
                    .Select(x => x.Id).FirstOrDefault();
            }
            else
            {
                userId = id;
            }

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

        public IQueryable<SessionCart> GetAllUsersBySession(int id, bool asNoTracking = false)
        {
            wsadDbContext context = new wsadDbContext();

            //Get USer Id from Username
            int? sessionId = id;

            //Check Username is valid
            if (sessionId == null) { return null; }

            //Query Items
            IQueryable<SessionCart> results = context.SessionCarts
                .Include(row => row.Session)
                .Where(row => row.SessionId == sessionId.Value && row.IsActive == true);

            //Check for As No Tracking
            if (asNoTracking == false)
            {
                results = results.AsNoTracking();
            }

            //Return Active Session Cart Items for this user
            return results;
        }

        public void DeleteRegistration(int sessionCartId)
        {
            using (wsadDbContext context = new wsadDbContext())
            {
                SessionCart sessionCartDTO = context.SessionCarts.Find(sessionCartId);

                context.SessionCarts.Remove(sessionCartDTO);

                context.SaveChanges();
            }
        }
    }
}