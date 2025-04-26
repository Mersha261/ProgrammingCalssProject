using PgrogrammingClass.Core.Utilitty;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PgrogrammingClass.Core.Domain
{
    public class TblUserAddress : BaseEntity
    {
        [Display(Name = "نام کاربر")]
        [MaxLength(450, ErrorMessage = ErrMsgCore.MaxLenghtMsg)]
        public string? UserId { get; set; }

        [Display(Name = "شماره موبایل")]
        [RegularExpression(@"[0-9]+", ErrorMessage = "لطفا فقط کاراکتر عددی به صورت انگلیسی وارد نمایید")]
        [MinLength(11, ErrorMessage = ErrMsgCore.MobileCheckLength)]
        [MaxLength(11, ErrorMessage = ErrMsgCore.MobileCheckLength)]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrMsgCore.RequierdMsg)]
        public string PhoneNumber { get; set; }


        [Display(Name = "نام")]
        [MaxLength(200, ErrorMessage = ErrMsgCore.MaxLenghtMsg)]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrMsgCore.RequierdMsg)]
        public string Name { set; get; }


        [Display(Name = "نام خانوادگی")]
        [MaxLength(200, ErrorMessage = ErrMsgCore.MaxLenghtMsg)]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrMsgCore.RequierdMsg)]
        public string Family { set; get; }


        [Display(Name = "آدرس")]
        [MaxLength(1000, ErrorMessage = ErrMsgCore.MaxLenghtMsg)]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrMsgCore.RequierdMsg)]
        public string Address { get; set; }

        [Display(Name = "شهر")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrMsgCore.RequierdMsg)]
        public int CityId { get; set; }

        [Display(Name = "کد پستی")]
        [MaxLength(10, ErrorMessage = ErrMsgCore.MaxLenghtMsg)]
        [MinLength(10, ErrorMessage = ErrMsgCore.MinLenghtMsg)]
        [RegularExpression(@"[0-9]+", ErrorMessage = "لطفا فقط کاراکتر عددی به صورت انگلیسی وارد نمایید")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrMsgCore.RequierdMsg)]
        public string PostalCode { get; set; }

        [Display(Name = "ایمیل")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [ForeignKey(nameof(CityId))]
        public TblCity TblCity { get; set; }

        public IEnumerable<TblShoppingcart> TblShoppingcart { get; set; }

    }
}
