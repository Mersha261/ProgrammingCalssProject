using PgrogrammingClass.Core.Utilitty;
using System.ComponentModel.DataAnnotations;

namespace ProgramingCalssProject.Models.ViewModel
{
    public class ChangePass
    {

        [Display(Name = "کلمه عبور فعلی")]
        [MaxLength(455, ErrorMessage = ErrMsgCore.MobileCheckLength)]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrMsgCore.RequierdMsg)]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Display(Name = "کلمه عبور جدید")]
        [MaxLength(455, ErrorMessage = ErrMsgCore.MobileCheckLength)]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrMsgCore.RequierdMsg)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Display(Name = "تکرار کلمه عبور جدید")]
        [MaxLength(455, ErrorMessage = ErrMsgCore.MobileCheckLength)]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrMsgCore.RequierdMsg)]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = "کلمه عبور با تکرار آن مطابقت ندارد")]
        public string ConfirmedPassword { get; set; }

    }
}
