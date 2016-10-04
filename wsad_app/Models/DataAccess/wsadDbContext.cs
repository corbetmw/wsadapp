using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace wsad_app.Models.DataAccess
{
    public class wsadDbContext : DbContext
    {
        public wsadDbContext()
        {
            Database.SetInitializer<wsadDbContext>(null);
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Correspondence> Correspondences { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    }
}