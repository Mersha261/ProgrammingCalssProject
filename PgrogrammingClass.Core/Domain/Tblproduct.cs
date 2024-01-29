using PgrogrammingClass.Core.Utilitty;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PgrogrammingClass.Core.Domain
{
    public class Tblproduct : BaseEntity
    {
        [Display(Name = "عنوان")]
        [MaxLength(100, ErrorMessage = ErrMsgCore.MaxLenghtMsg)]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrMsgCore.RequierdMsg)]
        public string Title { get; set; }

        [Display(Name = "توضیحات کوتاه")]
        [MaxLength(1000, ErrorMessage = ErrMsgCore.MaxLenghtMsg)]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrMsgCore.RequierdMsg)]
        public string ShortDescription { get; set; }

        [Display(Name = "توضیحات")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrMsgCore.RequierdMsg)]
        public string FullDescription { get; set; }


        [Display(Name = "توضیحات موتورهای جستجو")]
        [MaxLength(200, ErrorMessage = ErrMsgCore.MaxLenghtMsg)]
        [MinLength(50, ErrorMessage = ErrMsgCore.MinLenghtMsg)]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrMsgCore.RequierdMsg)]
        public string MetaDescription { get; set; }

        [Display(Name = "کامنت")]
        [MaxLength(500, ErrorMessage = ErrMsgCore.MaxLenghtMsg)]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrMsgCore.RequierdMsg)]
        public string Comment { get; set; }

        [Display(Name = "نمایش در صفحه اصلی")]
        public bool ShowOnHomepage { get; set; }

        [Display(Name = "کلمات کلیدی")]
        [MaxLength(100, ErrorMessage = ErrMsgCore.MaxLenghtMsg)]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrMsgCore.RequierdMsg)]
        public string KeyWord { get; set; }


        [Display(Name = "تعداد بازدید")]
        public int ViewCount { get; set; }

        [Display(Name = "تعداد فروش")]
        public int SoldCount { get; set; }


        [Display(Name = "موجودی انبار")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrMsgCore.RequierdMsg)]
        public int StockQuantity { get; set; }


        [Display(Name = "قیمت")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrMsgCore.RequierdMsg)]
        public int Price { get; set; }



        public int OldPrice { get; set; }


        [Display(Name = "وزن محصول")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrMsgCore.RequierdMsg)]
        public int Weight { get; set; }


        [Display(Name = "عنوان سئو")]
        [MaxLength(100, ErrorMessage = ErrMsgCore.MaxLenghtMsg)]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrMsgCore.RequierdMsg)]
        public string MetaTitle { get; set; }

        [Display(Name = "نام دسته")]
        public int CategoryId { get; set; }


        [ForeignKey(nameof(CategoryId))]
        public TblCategory TblCategory { get; set; }
        public ICollection<TblProductImage> ProductImages { get; set; }
        public ICollection<TblProductComment> TblProductComments { get; set; }

    }
}
