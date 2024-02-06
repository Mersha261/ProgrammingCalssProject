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
    public interface IPostService:IRepository<TblPost>
    {
    }
    public class PostService : Repository<TblPost>, IPostService
    {
        public PostService(ApplicationDbContext context) : base(context)
        {
        }
    }
}
