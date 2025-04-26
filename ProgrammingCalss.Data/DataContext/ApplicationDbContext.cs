using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PgrogrammingClass.Core.Domain;
using PgrogrammingClass.Data.UserAndPassword;

namespace PgrogrammingClass.Data.DataContext
{
    public class ApplicationDbContext: IdentityDbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            :base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {            
            optionsBuilder.UseSqlServer("Server=.;initial catalog=ProgrammingCalssProject;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;");
           // optionsBuilder.UseSqlServer("Server=185.55.224.234;initial catalog="+DbUserAndPass.DatabaseUser+";persist security info=True;user id="+DbUserAndPass.UserName+";password="+DbUserAndPass.Password+ ";MultipleActiveResultSets=True;App=EntityFramework&quot;TrustServerCertificate=True;");

        }

        public DbSet<ApplicationUser> TblUser { get; set; }
        public DbSet<TblAboutUs> TblAboutUs { get; set; }
        public DbSet<TblBanner> TblBanner { get; set; }
        public DbSet<TblCategory> TblCategory { get; set; }
        public DbSet<TblCity> TblCity { get; set; }
        public DbSet<TblContactUs> TblContactUs { get; set; }
        public DbSet<TblPost> TblPost { get; set; }
        public DbSet<Tblproduct> Tblproduct { get; set; }
        public DbSet<TblProductComment> TblProductComment { get; set; }
        public DbSet<TblProductImage> TblProductImage { get; set; }
        public DbSet<TblProvince> TblProvince { get; set; }
        public DbSet<TblSetting> TblSetting { get; set; } 
        public DbSet<TblSocialMedia> TblSocialMedia { get; set; }
        public DbSet<TblUserAddress> TblUserAddress { get; set; }
        public DbSet<TblShoppingcart> TblShoppingcart { get; set; }
        public DbSet<TblShoppingCartDetails> TblShoppingCartDetails { get; set; }
        public DbSet<TblOffCopon> TblOffCopon { get; set; }

    }
}
