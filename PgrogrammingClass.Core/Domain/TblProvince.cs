using PgrogrammingClass.Core.Utilitty;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PgrogrammingClass.Core.Domain
{
    public class TblProvince:BaseEntity
    {
        [Display(Name = "استان")]
        [MaxLength(100, ErrorMessage = ErrMsgCore.MaxLenghtMsg)]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrMsgCore.RequierdMsg)]
        public string ProvinceName { get; set; }

        public ICollection <TblCity> TblCities { get; set; }
    }
}
