using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace wsad_app.Models.DataAccess
{
    public class wsadDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Correspondence> Correspondences { get; set; }
    }
}