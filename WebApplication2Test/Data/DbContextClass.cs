using Microsoft.EntityFrameworkCore;
using WebApplication2Test.Models;

namespace WebApplication2Test.Data
{
    public class DbContextClass : DbContext
    {
        protected readonly IConfiguration configuration;
        public DbContextClass(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        //still should be changed for better implementation
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Product> Products { get; set; }
    }
}
