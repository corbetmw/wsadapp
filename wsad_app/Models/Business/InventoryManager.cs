using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using wsad_app.Models.DataAccess;

namespace wsad_app.Models.Business
{
    public class InventoryManager
    {
        internal static IQueryable<Product> GetAllProducts(bool asNoTracking = false)
        {
            wsadDbContext context = new wsadDbContext();

            IQueryable<Product> results = context.Products;

            if (asNoTracking == false)
            {
                results = results.AsNoTracking();
            }

            return results;
        }
    }
}