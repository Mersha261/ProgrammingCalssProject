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
    public interface ICityService:IRepository<TblCity>
    {
    }
    public class CityService : Repository<TblCity>, ICityService
    {
        public CityService(ApplicationDbContext context) : base(context)
        {
        }
    }
}
