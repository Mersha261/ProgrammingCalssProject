﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PgrogrammingClass.Core.Utilitty;
using System.ComponentModel.DataAnnotations;

namespace ProgramingCalssProject.Models.ViewModel
{
    public class LogInViewModel
    {
        [Display(Name = "شماره موبایل")]
        [RegularExpression(@"[0-9]+", ErrorMessage = "لطفا فقط کاراکتر عددی به صورت انگلیسی وارد نمایید")]
        [MinLength(11, ErrorMessage = ErrMsgCore.MobileCheckLength)]
        [MaxLength(11, ErrorMessage = ErrMsgCore.MobileCheckLength)]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrMsgCore.RequierdMsg)]
        public string PhoneNumber { get; set; }


        [Display(Name = "کلمه عبور")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrMsgCore.RequierdMsg)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
