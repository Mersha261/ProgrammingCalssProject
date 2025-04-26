using Microsoft.AspNetCore.Mvc;
using PgrogrammingClass.Core.Utilitty;
using System.ComponentModel.DataAnnotations;

namespace ProgramingCalssProject.Models.ViewModel
{
    public class RegisterViewModel
    {
        [Key]
        public int MyProperty { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ErrMsgCore.RequierdMsg)]
        [Display(Name = "نام")]
        [MaxLength(200, ErrorMessage = ErrMsgCore.MaxLenghtMsg)]
        public string Name { set; get; }


        [Required(AllowEmptyStrings = false, ErrorMessage = ErrMsgCore.RequierdMsg)]
        [Display(Name = "نام خانوادگی")]
        [MaxLength(200, ErrorMessage = ErrMsgCore.MaxLenghtMsg)]
        public string Family { set; get; }


        [Display(Name = "روز")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrMsgCore.RequierdMsg)]
        public int Day { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ErrMsgCore.RequierdMsg)]
        [Display(Name = "ماه")]
        public int Month { get; set; }


        [Display(Name = "سال")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrMsgCore.RequierdMsg)]
        public int Year { get; set; }


        [Display(Name = "جنسیت")]
        [MaxLength(10, ErrorMessage = ErrMsgCore.MaxLenghtMsg)]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrMsgCore.RequierdMsg)]
        public string Gender { set; get; }



        [Display(Name = "شماره موبایل")]
        [RegularExpression(@"[0-9]+", ErrorMessage = "لطفا فقط کاراکتر عددی به صورت انگلیسی وارد نمایید")]
        [Remote("DuplicateMobile", "Account", ErrorMessage = "شماره موبایل وارد شده تکراری است، لطفا شماره دیگری وارد نمایید", HttpMethod = "Post")]
        [MinLength(11, ErrorMessage = ErrMsgCore.MobileCheckLength)]
        [MaxLength(11, ErrorMessage = ErrMsgCore.MobileCheckLength)]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrMsgCore.RequierdMsg)]
        public string PhoneNumber { get; set; }


        [Display(Name = "ایمیل")]
        [Remote("DuplicateEmail", "Account", ErrorMessage = "ایمیل  وارد شده تکراری است، لطفا ایمیل دیگری وارد نمایید", HttpMethod = "Post")]
        [MaxLength(255, ErrorMessage = ErrMsgCore.MobileCheckLength)]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrMsgCore.RequierdMsg)]
        public string Email { get; set; }


        [Display(Name = "کلمه عبور")]
        [MaxLength(455, ErrorMessage = ErrMsgCore.MobileCheckLength)]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrMsgCore.RequierdMsg)]
        [DataType(DataType.Password)]
        public string PasswordHash { get; set; }

        [Display(Name = "کلمه عبور")]
        [MaxLength(455, ErrorMessage = ErrMsgCore.MobileCheckLength)]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrMsgCore.RequierdMsg)]
        [DataType(DataType.Password)]
        [Compare(nameof(PasswordHash),ErrorMessage ="کلمه عبور با تکرار آن مطابقت ندارد")]
        public string ConfirmedPasswordHash { get; set; }

    }
}
