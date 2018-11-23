using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CongNgheWeb_3_0.Models
{
    public class LoginModel
    {
        [Display(Name ="Email")]
        [Required(ErrorMessage ="Không được bỏ trống email")]
        public string UserName { get; set; }


        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Không được bỏ trống mật khẩu")]
        public string Password { get; set; }

    }
}