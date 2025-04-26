using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PgrogrammingClass.Core.Domain
{
    public class TblShoppingCartDetails
    {
        [MaxLength(450)]
        [Key]
        public string Id { get; set; }

        [MaxLength(450)]
        public string ShoppingCartId { get; set; }

        [Display(Name = "آی دی محصول")]
        public int ProductId { get; set; }

        [Display(Name = "نام محصول")]
        [MaxLength(450)]
        public string ProductName { get; set; }

        [Display(Name = "وزن")]
        public int? Weight { get; set; }


        [Display(Name = "قیمت (تومان) ")]
        public int Price { get; set; }

        [Display(Name = "قیمت (تومان) با کد تخفیف ")]
        public int PriceWithOffCopon { get; set; }

        [Display(Name = "تعداد")]
        public int Count { get; set; }

        [ForeignKey(nameof(ShoppingCartId))]
        public TblShoppingcart TblShoppingcart { get; set; }
    }
}
