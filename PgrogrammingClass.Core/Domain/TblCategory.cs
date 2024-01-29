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
    public class TblCategory:BaseEntity
    {

        [Display(Name = "عنوان دسته")]
        [MaxLength(200, ErrorMessage = ErrMsgCore.MaxLenghtMsg)]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrMsgCore.RequierdMsg)]
        public string Name { get; set; }


        [Display(Name = "توضیحات")]
        [MaxLength(1000, ErrorMessage = ErrMsgCore.MaxLenghtMsg)]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrMsgCore.RequierdMsg)]
        public string Description { get; set; }


        [Display(Name = "کلمات کلیدی برای موتور های جستجو")]
        [MaxLength(200, ErrorMessage = ErrMsgCore.MaxLenghtMsg)]
        public string MetaKeywords { get; set; }


        [Display(Name = "توضیحات برای موتور های جستجو")]
        [MaxLength(200, ErrorMessage = ErrMsgCore.MaxLenghtMsg)]
        public string MetaDescription { get; set; }


        [Display(Name = "عنوان برای موتورهای جستجو")]
        [MaxLength(100, ErrorMessage = ErrMsgCore.MaxLenghtMsg)]
        public string MetaTitle { get; set; }


        [Display(Name = "والد")]
        public int ParentCategoryId { get; set; }


        [Display(Name = "آیا منوی اصلی است؟")]
        public bool IsIncludeInTopMenu { get; set; }

        public ICollection<Tblproduct> Tblproducts { get; set; }

    }
}
