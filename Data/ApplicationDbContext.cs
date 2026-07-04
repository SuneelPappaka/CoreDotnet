using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoreDotnet.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public  ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<CoreDotnet.Models.Product> Products
        {
            get; set;
        }
        public DbSet<CoreDotnet.Models.AdminProducts> AdminProducts
        {
            get; set;
        }   
    }
}
