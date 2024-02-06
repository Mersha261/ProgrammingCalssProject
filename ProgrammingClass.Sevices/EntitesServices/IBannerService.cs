using Microsoft.EntityFrameworkCore.Update;
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
    public interface IBannerService:IRepository<TblBanner>
    {
    }
    public class BannerService : Repository<TblBanner>, IBannerService
    {
        public BannerService(ApplicationDbContext context) : base(context)
        {
        }
    }
}
