using PgrogrammingClass.Core.Utilitty;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PgrogrammingClass.Core.Domain
{
    public class TblBanner : BaseEntity
    {

        [Display(Name = "تصویر")]
        [MaxLength(200, ErrorMessage = ErrMsgCore.MaxLenghtMsg)]
        public string Picture { get; set; }


        [Display(Name = "عنوان")]
        [MaxLength(100, ErrorMessage = ErrMsgCore.MaxLenghtMsg)]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrMsgCore.RequierdMsg)]
        public string Title { get; set; }

        [Display(Name = "متن جایگذین")]
        [MaxLength(100, ErrorMessage = ErrMsgCore.MaxLenghtMsg)]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrMsgCore.RequierdMsg)]
        public string Alt { get; set; }

    }
}
