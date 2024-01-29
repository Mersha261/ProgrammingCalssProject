using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PgrogrammingClass.Core
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="تاریخ ثبت")]
        public DateTime CreateDate { get; set; }

        [Display(Name ="تاریخ ویرایش")]
        public DateTime ModifyDate { get; set; }

    }
}
