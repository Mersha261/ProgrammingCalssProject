using PgrogrammingClass.Core.Utilitty;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PgrogrammingClass.Core.Domain
{
    public class TblCity:BaseEntity
    {
        [Display(Name = "شهر")]
        [MaxLength(100, ErrorMessage = ErrMsgCore.MaxLenghtMsg)]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrMsgCore.RequierdMsg)]
        public string CityName { get; set; }


        [Display(Name = "استان")]
        public int ProvinceId { get; set; }

        [Display(Name = "استان")]
        [ForeignKey(nameof(ProvinceId))]
        public TblProvince TblProvince { get; set; }

        public IEnumerable<TblUserAddress> TblUserAddresses { get; set; }

    }
}
