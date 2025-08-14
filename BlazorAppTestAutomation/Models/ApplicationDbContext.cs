using Microsoft.EntityFrameworkCore;

namespace BlazorAppTestAutomation.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<Country> Countries { get; set; }
    }
}
