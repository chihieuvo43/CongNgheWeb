using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CongNgheWeb_3_0.Models
{
    [MetadataTypeAttribute(typeof(UserMetadata))]
    public partial class tbl_User
    {
        internal sealed class UserMetadata
        {
            public int ID { get; set; }


            [Display(Name ="Tên người dùng")]
            [Required(ErrorMessage ="Không được bỏ trống tên người dùng")]
            public string UserName { get; set; }

            [Display(Name = "Hình ảnh")]
            public string UserImage { get; set; }

            [Display(Name = "Tên đăng nhập")]
            public string LoginName { get; set; }

            [Display(Name = "Mật khẩu")]
            [Required(ErrorMessage = "Không được bỏ trống mật khẩu")]
            public string PasswordUser { get; set; }

            [Display(Name = "Tiểu sử")]
            public string DescriptionUser { get; set; }

            [Display(Name = "Ngày tạo")]
            [DataType(DataType.Date)]
            public Nullable<System.DateTime> CreateDate { get; set; }

            [Display(Name = "Số điện thoại")]
            [Required(ErrorMessage = "Không được bỏ trống số điện thoại")]
            public string NumberPhone { get; set; }

            [Display(Name = "Email")]
            [Required(ErrorMessage = "Không được bỏ trống Email")]
            public string Email { get; set; }

            [Display(Name = "Đã khóa")]
            public Nullable<bool> Clock { get; set; }

            [Display(Name = "Quyền hạn")]
            public Nullable<int> Position { get; set; }

            [Display(Name = "Đã gởi mail")]
            public Nullable<bool> SendMail { get; set; }


            [Display(Name = "Thông báo")]
            public Nullable<bool> Notificationed { get; set; }

            public Nullable<bool> Deleted { get; set; }
        }
    }
}