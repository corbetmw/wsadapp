using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using wsad_app.Models.DataAccess;

namespace wsad_app.Controllers
{
    public class ShoppingCartManager
    {
        internal void AddToCart(string username, int productId)
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

                /*Check if a product exists for this user's cart*/
                //Does Product Id exist in database?
                if (context.Products.Any(row => row.Id == productId) == false)
                {
                    throw new ArgumentException("Invalid Product Id");
                }

                ShoppingCart cartItem = context.ShoppingCarts
                    .Where(row => row.UserId == userId.Value && row.ProductId == productId)
                    .FirstOrDefault();

                //If product exists -- add one to the quantity
                if (cartItem != null)
                {
                    //Is it Active?
                    if (cartItem.IsActive == false)
                    {
                        cartItem.IsActive = true;
                        cartItem.Quantity = 1;
                    }
                    else
                    {
                        //Existing Active Item
                        cartItem.Quantity++;    //Add one to the quantity
                    }
                }
                else
                {
                    //No product exists -- add new product
                    cartItem = new ShoppingCart()
                    {
                        UserId = userId.Value,
                        ProductId = productId,
                        Quantity = 1,
                        IsActive = true,
                        DateAdded = DateTime.Now,
                    };

                    context.ShoppingCarts.Add(cartItem);
                }

                //Update Database
                context.SaveChanges();
            }
        }

        internal IQueryable<ShoppingCart> GetAllItems(string username, bool asNoTracking = false)
        {
            wsadDbContext context = new wsadDbContext();

            //Get USer Id from Username
            int? userId = context.Users.Where(x => x.UserName.ToLower() == username.ToLower())
                .Select(x => x.Id).FirstOrDefault();

            //Check Username is valid
            if (userId == null) { return null; }

            //Query Items
            IQueryable<ShoppingCart> results = context.ShoppingCarts
                .Include(row => row.Product)
                .Where(row => row.UserId == userId.Value && row.IsActive == true);

            //Check for As No Tracking
            if (asNoTracking == false)
            {
                results = results.AsNoTracking();
            }

            //Return Active Shopping Cart Items for this user
            return results;
        }
    }
}