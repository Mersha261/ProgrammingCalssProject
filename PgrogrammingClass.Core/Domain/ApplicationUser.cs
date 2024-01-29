using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
    public class ApplicationUser : IdentityUser
    {

        [Display(Name = "نام")]
        [MaxLength(200, ErrorMessage = ErrMsgCore.MaxLenghtMsg)]
        public string Name { set; get; }


        [Display(Name = "نام خانوادگی")]
        [MaxLength(200, ErrorMessage = ErrMsgCore.MaxLenghtMsg)]
        public string Family { set; get; }



        [Display(Name = "تاریخ تولد")]
        public DateTime Birthday { set; get; }


        [Display(Name = "جنسیت")]
        [MaxLength(10, ErrorMessage = ErrMsgCore.MaxLenghtMsg)]
        public string Gender { set; get; }


        [Display(Name = "کد تایید")]
        public int MobileConfirmCode { set; get; }

        [Display(Name = "تعداد ارسال کد")]
        public int CodeCounter { get; set; }

        [Display(Name = "آخرین زمان ارسال کد")]
        public DateTime SendDate { get; set; }


        [Display(Name = "تاریخ ثبت")]
        public DateTime CreateDate { get; set; }


        [Display(Name = "تاریخ ویرایش")]
        public DateTime ModifyDate { get; set; }


        [Display(Name = "آواتار")]
        [MaxLength(200, ErrorMessage = ErrMsgCore.MaxLenghtMsg)]
        public string Avatar { set; get; }


        [Display(Name = "آی پی کاربر")]
        [MaxLength(200, ErrorMessage = ErrMsgCore.MaxLenghtMsg)]
        public string LastIpAddress { get; set; }


        [Display(Name = "شماره موبایل")]
        [RegularExpression(@"[0-9]+", ErrorMessage = "لطفا فقط کاراکتر عددی به صورت انگلیسی وارد نمایید")]
        [Remote("DuplicateMobile", "Home", ErrorMessage = "شماره موبایل وارد شده تکراری است، لطفا شماره دیگری وارد نمایید")]
        [MinLength(11, ErrorMessage = ErrMsgCore.MobileCheckLength)]
        [MaxLength(11, ErrorMessage = ErrMsgCore.MobileCheckLength)]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrMsgCore.RequierdMsg)]
        public override string PhoneNumber { get; set; }



        [Display(Name = "کلمه عبور")]
        [MaxLength(455, ErrorMessage = ErrMsgCore.MobileCheckLength)]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrMsgCore.RequierdMsg)]
        public override string PasswordHash { get => base.PasswordHash ; set => base.PasswordHash = value; }
    }
}
