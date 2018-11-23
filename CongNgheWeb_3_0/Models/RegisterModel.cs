using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CongNgheWeb_3_0.Models
{
    public class RegisterModel
    {

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Không được bỏ trống email")]
        public string Email { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Không được bỏ trống mật khẩu")]
        public string Password { get; set; }

        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage ="Xác nhận mật khẩu không chính xác")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Tên người dùng")]
        [Required(ErrorMessage = "Không được bỏ trống tên người dùng")]
        public string UserName { get; set; }

        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Không được bỏ trống số điện thoại")]
        public string NumberPhone { get; set; }

    }
}