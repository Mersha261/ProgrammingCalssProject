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
    public interface IProductImageService:IRepository<TblProductImage>
    {
    }
    public class ProductImageService : Repository<TblProductImage>, IProductImageService
    {
        public ProductImageService(ApplicationDbContext context) : base(context)
        {
        }
    }
}
