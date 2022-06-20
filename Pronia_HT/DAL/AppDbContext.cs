using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Pronia_HT.Areas.ProniaAdmin.Controllers;
using Pronia_HT.Models;

namespace Pronia_HT.DAL
{
    public class AppDbContext:DbContext
    {
       

        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AnotherSetting>()
                .HasIndex(u => u.Key)
                .IsUnique();
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Card> Cards { get; set; } 
        public DbSet<Color> Colors { get; set; } 
        public DbSet<Plant> Plants { get; set; } 
        public DbSet<Size> Sizes { get; set; } 
        public DbSet<Setting> Settings { get; set; } 
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<AnotherSetting> AnotherSettings { get; set; }
    }
}
