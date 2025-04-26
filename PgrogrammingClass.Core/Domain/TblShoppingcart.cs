using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PgrogrammingClass.Core.Domain
{
    public class TblShoppingcart
    {
        [MaxLength(450)]
        [Key]
        public string Id { get; set; }

        [MaxLength(450)]
        public string? Cookie { get; set; }

        public int? AddressId { get; set; }


        [Display(Name = "ارسال شده به کاربر")]
        public bool IsSentToUser { get; set; }


        [Display(Name = "تاریخ پرداخت")]
        public DateTime PayDate { get; set; }


        [Display(Name = "پرداخت شده")]
        public bool IsPaied { get; set; }


        [Display(Name = "ارسال شده به بانک")]
        public bool IsSentToBank { get; set; }


        [Display(Name = "قیمت اجمالی (تومان) ")]
        public int TotalPrice { get; set; }

        [Display(Name = "کوپن تخفیف")]
        [MaxLength(100)]
        public string? OffCopon { get; set; }

        [Display(Name = "قیمت  بدون تخفیف (تومان)")]
        public int PriceWithoutOff { get; set; }

        [Display(Name = "درصد تخفیف")]
        public int OffPercent { get; set; }

        [Display(Name = "شماره تراکنش")]
        [MaxLength(200)]
        public string? TransactionNumber { get; set; }

        [Display(Name = "شماره سفارش")]
        [MaxLength(200)]
        public string? CustomOrderNumber { get; set; }

        [Display(Name = "آی دی کاربر")]
        [MaxLength(450)]
        public string? UserId { get; set; }

        [Display(Name = "وضعیت کد تخفیف")]
        public bool IsCoponSet { get; set; }


        [Display(Name = "تاریخ ثبت")]
        public DateTime CreateDate { get; set; }


        [Display(Name = "شیوه ارسال")]
        public string ShippingWayName { get; set; }

        [Display(Name = "هزینه ارسال")]

        public int ShippingPrice { get; set; }

        public IEnumerable<TblShoppingCartDetails> TblShoppingCartDetails { get; set; }

        [ForeignKey(nameof(AddressId))]
        public TblUserAddress TblUserAddress { get; set; }



    }
}
