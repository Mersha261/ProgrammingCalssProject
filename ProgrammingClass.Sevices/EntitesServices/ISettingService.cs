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
    public interface ISettingService:IRepository<TblSetting>
    {
    }

    public class SettingService : Repository<TblSetting>, ISettingService
    {
        public SettingService(ApplicationDbContext context) : base(context)
        {
        }
    }
}
