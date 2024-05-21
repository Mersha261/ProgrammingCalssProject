using Microsoft.EntityFrameworkCore;
using PgrogrammingClass.Core.Domain;
using PgrogrammingClass.Data.DataContext;
using PgrogrammingClass.Sevices.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PgrogrammingClass.Sevices.EntitesServices
{
    public interface IApplicationUserService:IRepository<ApplicationUser>
    {
        Task<string> GetNameAndFamily(string id);
    }

    public class ApplicationUserService : Repository<ApplicationUser>, IApplicationUserService
    {
        public ApplicationUserService(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<string> GetNameAndFamily(string id)
        {
            var user= await _context.TblUser.Where(a => a.Id == id).SingleOrDefaultAsync();

            return user.Name + " " + user.Family;
        }
    }
}
