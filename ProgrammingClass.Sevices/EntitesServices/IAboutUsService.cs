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
    public interface IAboutUsService:IRepository<TblAboutUs>
    {
    }

    public class AboutUsService : Repository<TblAboutUs>, IAboutUsService
    {
        public AboutUsService(ApplicationDbContext context) : base(context)
        {
        }
    }
}
