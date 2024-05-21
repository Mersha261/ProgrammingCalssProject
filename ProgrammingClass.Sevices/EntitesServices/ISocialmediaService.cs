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
    public interface ISocialmediaService:IRepository<TblSocialMedia>
    {
    }

    public class SocialmediaService : Repository<TblSocialMedia>, ISocialmediaService
    {
        public SocialmediaService(ApplicationDbContext context) : base(context)
        {
        }
    }
}
