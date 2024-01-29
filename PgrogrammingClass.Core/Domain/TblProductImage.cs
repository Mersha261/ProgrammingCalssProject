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
    public class TblProductImage : BaseEntity
    {
        [Display(Name = "نام محصول")]
        public int ProductId { get; set; }

        [Display(Name = "عنوان")]
        [MaxLength(200, ErrorMessage = ErrMsgCore.MaxLenghtMsg)]
        public string? Picture { get; set; }

        [Display(Name = "عنوان")]
        [MaxLength(200, ErrorMessage = ErrMsgCore.MaxLenghtMsg)]
        public string? Thumbnaile { get; set; }

        [Display(Name = "عنوان")]
        [MaxLength(100, ErrorMessage = ErrMsgCore.MaxLenghtMsg)]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrMsgCore.RequierdMsg)]
        public string Title { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Tblproduct Tblproduct { get; set; }
    }
}
