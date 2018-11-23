using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CongNgheWeb_3_0.Models
{
    [MetadataTypeAttribute(typeof(CourseMetadata))]
    public partial class tbl_Course
    {
        internal sealed class CourseMetadata
        {
            public int ID { get; set; }

            [Display(Name ="Tên khóa học")]
            [Required(ErrorMessage ="Không được bỏ trống tên khóa học")]
            public string CourseName { get; set; }

            [Display(Name = "Ảnh bìa")]
            public string CourseImage { get; set; }

            [Display(Name = "Giá")]
            public Nullable<decimal> Pirce { get; set; }

            [Display(Name = "Giảm giá")]
            public Nullable<decimal> Discount { get; set; }

            [Display(Name = "Tổng lượt xem")]
            public Nullable<int> TotalView { get; set; }

            [Display(Name = "Tổng lượt học")]
            public Nullable<int> TotalStudents { get; set; }

            [Display(Name = "Mô tả")]
            public string CourseDescription { get; set; }

            [Display(Name = "Danh mục")]
            public Nullable<int> CategoryID { get; set; }

            [Display(Name = "Người tạo")]
            public Nullable<int> UserID { get; set; }

            
            [Display(Name = "Đã duyệt")]
            public Nullable<bool> censored { get; set; }

            [Display(Name = "Ngày đăng")]
            [DataType(DataType.Date)]
            public Nullable<System.DateTime> CreateDate { get; set; }

            public Nullable<bool> Deleted { get; set; }

        }
    }
}