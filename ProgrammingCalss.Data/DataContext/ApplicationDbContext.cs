using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PgrogrammingClass.Core.Domain;

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
        }

        public DbSet<TblBanner> TblBanner { get; set; }
    }
}
