using Microsoft.EntityFrameworkCore;
using Vega.Models;

namespace Vega.Persistance
{
    public class VegaDbContext : DbContext
    {
        public VegaDbContext(DbContextOptions<VegaDbContext> options)
            : base (options)
        {

        }

        public DbSet<Make> Makes { get; set; } 
        
        ///////////// stare podejście
        // public VegaDbContext(string connectionString)
        //     : base(connectionString)
        // {
        //     // connection string jest brany z konfiguracji i przekazywany do klasy bazowej
        // }
    }
}