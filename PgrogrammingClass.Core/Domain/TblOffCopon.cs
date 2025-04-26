using PgrogrammingClass.Core.Utilitty;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PgrogrammingClass.Core.Domain
{
    public class TblOffCopon:BaseEntity
    {
        [Display(Name = "نام کوپن")]
        [MaxLength(100, ErrorMessage = ErrMsgCore.MaxLenghtMsg)]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrMsgCore.RequierdMsg)]
        public string CoponName { get; set; }


        [Display(Name = "درصد تخفیف")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrMsgCore.RequierdMsg)]
        public int Percent { get; set; }
    }
}
