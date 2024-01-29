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
    public class TblProductComment:BaseEntity
    {
        [Display(Name = "نام و نام خانوادگی")]
        [MaxLength(100, ErrorMessage = ErrMsgCore.MaxLenghtMsg)]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrMsgCore.RequierdMsg)]
        public string NameAndFamily { get; set; }


        [Display(Name = "شماره موبایل")]
        [MaxLength(100, ErrorMessage = ErrMsgCore.MaxLenghtMsg)]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrMsgCore.RequierdMsg)]
        public string PhoneNumber { get; set; }

        [Display(Name = "ایمیل")]
        [MaxLength(100, ErrorMessage = ErrMsgCore.MaxLenghtMsg)]
        public string? Email { get; set; }

        [Display(Name = "آی پی کاربر")]
        [MaxLength(100, ErrorMessage = ErrMsgCore.MaxLenghtMsg)]
        public string? UserIp { get; set; }


        [Display(Name = "وضعیت")]
        public bool IsRead { get; set; }


        [Display(Name = "متن نظر")]
        [MaxLength(1000, ErrorMessage = ErrMsgCore.MaxLenghtMsg)]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrMsgCore.RequierdMsg)]
        public string Comment { get; set; }

        [Display(Name = "نام محصول")]
        public int ProductId { get; set; }


        [ForeignKey(nameof(ProductId))]
        public Tblproduct Tblproduct { get; set; }
    }
}
