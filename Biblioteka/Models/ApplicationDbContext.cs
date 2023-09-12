using System.Data.Entity;

namespace Biblioteka.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(): base("DefaultConnection")
        {

        }
        public DbSet<Ksiazka> Ksiazki { get; set; }
    }
}