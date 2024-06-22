using PgrogrammingClass.Core.Utilitty;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PgrogrammingClass.Core.Domain
{
    public class TblAboutUs:BaseEntity
    {
        [Display(Name ="درباره ما")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrMsgCore.RequierdMsg)]
        public string AboutUs { get; set; }

        [Display(Name = "تماس با ما")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrMsgCore.RequierdMsg)]
        public string ContactUs { get; set; }

        [Display(Name = "درباره ما فوتر")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrMsgCore.RequierdMsg)]
        public string AboutUsFooter { get; set; }


        [Display(Name = "شماره تماس")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrMsgCore.RequierdMsg)]
        [MaxLength(300, ErrorMessage = ErrMsgCore.MaxLenghtMsg)]
        public string PhoneNumbers { get; set; }


        [Display(Name = "ایمیل")]
        [MaxLength(300, ErrorMessage = ErrMsgCore.MaxLenghtMsg)]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrMsgCore.RequierdMsg)]
        public string Email { get; set; }


        [Display(Name = "آدرس")]
        [MaxLength(300, ErrorMessage = ErrMsgCore.MaxLenghtMsg)]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrMsgCore.RequierdMsg)]
        public string Address { get; set; }


    }
}
