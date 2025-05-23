using Microsoft.EntityFrameworkCore;
using MottuNET.model;
using MottuNET.Models;

namespace MottuNET.data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Ala> Alas { get; set; }
        public DbSet<Moto> Motos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)


        {

            modelBuilder.Entity<Moto>().ToTable("tb_moto");
            modelBuilder.Entity<Ala>().ToTable("tb_ala");
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<Ala>()
                .HasMany(a => a.Motos)
                .WithOne(m => m.Ala)
                .HasForeignKey(m => m.AlaId)
                .OnDelete(DeleteBehavior.Cascade);

          
            modelBuilder.Entity<Moto>()
                .Property(m => m.Status)
                .HasConversion<string>();
        }
    }
}
