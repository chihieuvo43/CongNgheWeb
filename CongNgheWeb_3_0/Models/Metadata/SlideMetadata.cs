using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CongNgheWeb_3_0.Models
{
    [MetadataTypeAttribute(typeof(SildeMetadata))]
    public partial class tbl_Slide
    {
        internal sealed class SildeMetadata
        {

            public int ID { get; set; }

            [Display(Name = "Hình ảnh")]
            public string SildeImage { get; set; }

            [Display(Name = "Khóa")]
            public Nullable<bool> Clock { get; set; }

            [Display(Name = "Ngày tạo")]
            [DataType(DataType.Date)]
            public Nullable<System.DateTime> CreateDate { get; set; }
        }
    }
}