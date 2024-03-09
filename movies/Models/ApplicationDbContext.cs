using Microsoft.EntityFrameworkCore;

namespace movies.Models
{
    public class ApplicationDbContext : DbContext
    {
        ///don't understand it yet  
        
        //ctor to injection 
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
    }
}
