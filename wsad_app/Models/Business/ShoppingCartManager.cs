using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wsad_app.Models.DataAccess;

namespace wsad_app.Models.Business
{
    public class ShoppingCartManager
    {
        internal void AddToCart(string username, int productId)
        {
            //estandlish dbcontext
            using (wsadDbContext context = new wsadDbContext())
            {
                //captureu userid based on username
                int? userId = context.Users
                    .Where(row => row.UserName.ToLower() == username.ToLower())
                    .Select(row => row.Id)
                    .FirstOrDefault();

                if (userId.HasValue == false)
                {
                    throw new ArgumentException("invalid username");
                }
                //check if a porcut ecxists for this user's cart
                //does productid ec=stit is databse
                if(context.Products.Any(row => row.Id == productId) == false)
                {
                    throw new ArgumentException("oinvalid product ID");
                }

                ShoppingCart cartItem = context.ShoppingCarts
                    .Where(row => row.UserId == userId.Value && row.ProductId == productId)
                    .FirstOrDefault();
                //if procuct ecists add to wuinttyt
                if (cartItem != null)
                {
                    if (cartItem.IsActive)
                    {
                        cartItem.IsActive = true;
                        cartItem.Quantity = 1;
                    }
                    else
                    {
                        //existing item
                        cartItem.Quantity++; //add one to the quantity
                    }
                }
                else
                {

                }
   
                //no product, add new proicut
            }

        }
    }
}