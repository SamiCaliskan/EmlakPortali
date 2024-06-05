using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmlakPortali.Models
{
    public class IlanContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public IlanContext(DbContextOptions<IlanContext> options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

         modelBuilder.Entity<Advert>().HasData(new Advert() { AdvertId = 1, AdvertTitle = "Antalya Kültür 2+1", Date = "08.03.2024", IsActive = true });
         modelBuilder.Entity<Advert>().HasData(new Advert() { AdvertId = 2, AdvertTitle = "Butik Otel", Date = "13.01.2023", IsActive = false });
         modelBuilder.Entity<Advert>().HasData(new Advert() { AdvertId = 3, AdvertTitle = "Altınkum 1+1", Date = "07.09.2024", IsActive = true });
         modelBuilder.Entity<Advert>().HasData(new Advert() { AdvertId = 4, AdvertTitle = "Afyon 4+1", Date = "18.11.2023", IsActive = true });
        }

        public DbSet<Advert> Advert { get; set; }
        public DbSet<ForSale> ForSale { get; set; }
        public DbSet<ForRent> ForRent { get; set; }

    }
}
