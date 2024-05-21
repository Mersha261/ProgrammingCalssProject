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
    public class AboutUsDto:BaseEntity
    {
        [Display(Name = "درباره ما")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrMsgCore.RequierdMsg)]
        public string MyProperty { get; set; }
    }
}
