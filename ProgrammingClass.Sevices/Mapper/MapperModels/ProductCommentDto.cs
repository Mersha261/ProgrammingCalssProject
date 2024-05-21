using PgrogrammingClass.Core;
using PgrogrammingClass.Core.Utilitty;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PgrogrammingClass.Sevices.Mapper.MapperModels
{
    public class ProductCommentDto:BaseEntity
    {
        [Display(Name = "نام و نام خانوادگی")]
        [MaxLength(100, ErrorMessage = ErrMsgCore.MaxLenghtMsg)]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrMsgCore.RequierdMsg)]
        public string NameAndFamily1 { get; set; }


        [Display(Name = "شماره موبایل")]
        [MaxLength(100, ErrorMessage = ErrMsgCore.MaxLenghtMsg)]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrMsgCore.RequierdMsg)]
        public string PhoneNumber1 { get; set; }

        [Display(Name = "ایمیل")]
        [MaxLength(100, ErrorMessage = ErrMsgCore.MaxLenghtMsg)]
        public string? Email1 { get; set; }

        [Display(Name = "آی پی کاربر")]
        [MaxLength(100, ErrorMessage = ErrMsgCore.MaxLenghtMsg)]
        public string? UserIp1 { get; set; }


        [Display(Name = "وضعیت")]
        public bool IsRead1 { get; set; }


        [Display(Name = "متن نظر")]
        [MaxLength(1000, ErrorMessage = ErrMsgCore.MaxLenghtMsg)]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrMsgCore.RequierdMsg)]
        public string Comment1 { get; set; }

        [Display(Name = "نام محصول")]
        public int ProductId1 { get; set; }
    }
}
