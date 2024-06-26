﻿using PgrogrammingClass.Core.Domain;
using PgrogrammingClass.Data.DataContext;
using PgrogrammingClass.Sevices.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PgrogrammingClass.Sevices.EntitesServices
{
    public interface IProvinceService:IRepository<TblProvince>
    {
    }

    public class ProvinceService : Repository<TblProvince>, IProvinceService
    {
        public ProvinceService(ApplicationDbContext context) : base(context)
        {
        }
    }
}
