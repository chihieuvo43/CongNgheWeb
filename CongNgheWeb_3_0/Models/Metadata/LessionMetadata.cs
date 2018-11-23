using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CongNgheWeb_3_0.Models
{
    [MetadataTypeAttribute(typeof(LessionMetada))]
    public partial class tbl_Lession
    {
        internal sealed class LessionMetada
        {
            public int ID { get; set; }

            [Display(Name ="Tên bài học")]
            [Required(ErrorMessage ="Không được bỏ trống tên bài học")]
            public string LessionName { get; set; }

            [Display(Name = "Đường dẫn")]
            [Required(ErrorMessage = "Không được bỏ trống đường dẫn")]
            public string URLLession { get; set; }

            [Display(Name = "Tổng thời gian")]
            public Nullable<int> TotalTime { get; set; }

            [Display(Name = "Khóa học")]
            //[Required(ErrorMessage = "Không được bỏ trống khóa học")]
            public Nullable<int> CourseID { get; set; }

            public Nullable<bool> Deleted { get; set; }

        }
    }
  
}