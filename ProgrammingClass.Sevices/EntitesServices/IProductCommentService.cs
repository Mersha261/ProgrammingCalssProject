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
    public interface IProductCommentService:IRepository<TblProductComment>
    {
    }

    public class ProductCommentService : Repository<TblProductComment>, IProductCommentService
    {
        public ProductCommentService(ApplicationDbContext context) : base(context)
        {
        }
    }
}
