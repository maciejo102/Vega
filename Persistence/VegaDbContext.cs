using Microsoft.EntityFrameworkCore;
using Vega.Contract.Models;

namespace Vega.Persistance
{
    public class VegaDbContext : DbContext
    {
        public DbSet<Make> Makes { get; set; } 
        public DbSet<Feature> Features { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Model> Models { get; set; }
        

        public VegaDbContext(DbContextOptions<VegaDbContext> options)
            : base (options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // primary key for this entity
            modelBuilder.Entity<VehicleFeature>().HasKey(vf => 
                new { vf.VehicleId, vf.FeatureId });
        }
        
        ///////////// stare podejście
        // public VegaDbContext(string connectionString)
        //     : base(connectionString)
        // {
        //     // connection string jest brany z konfiguracji i przekazywany do klasy bazowej
        // }
    }
}