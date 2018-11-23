using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CongNgheWeb_3_0.Models
{
    [MetadataTypeAttribute(typeof(SortCourseMetadata))]
    public partial class tbl_Sort_Course
    {
        internal sealed class SortCourseMetadata
        {
            public int ID { get; set; }

            [Display(Name = "Tên danh mục")]
            public Nullable<int> CategoryID { get; set; }

            [Display(Name = "Tên danh mục khóa học")]
            [Required(ErrorMessage = "Không được bỏ trống tên danh mục khóa học")]
            public string SortCourseName { get; set; }

            public Nullable<bool> Deleted { get; set; }
        }
    }
}