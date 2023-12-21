using Microsoft.EntityFrameworkCore;

namespace COOKING_RECIPE_PORTAL.Model
{
    public class LoginContext : DbContext
    {
        public LoginContext(DbContextOptions<LoginContext> options) : base(options) 
        {
            
        }

        public DbSet<Login> Login { get; set; }         // The DbSet enables the user to perform various operations like add, remove, update, etc. on the entity set.
        public DbSet<Veg_Recipe> Veg_Recipe { get; set; }
        public DbSet<NonVeg_Recipe> NonVeg_Recipe { get; set; }
    }

}

