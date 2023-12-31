using DataModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RazorPages.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Anion> Anions { get; set; }
        public DbSet<Cation> Cations { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Pallet> Pallets { get; set; }
        public DbSet<SaleEntry> SaleEntries { get; set; }
    }
}