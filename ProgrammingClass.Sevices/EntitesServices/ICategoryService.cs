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
    public interface ICategoryService:IRepository<TblCategory>
    {
    }
    public class CategoryService : Repository<TblCategory>, ICategoryService
    {
        public CategoryService(ApplicationDbContext context) : base(context)
        {
        }
    }
}
