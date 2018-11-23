using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CongNgheWeb_3_0.Models
{
    [MetadataTypeAttribute(typeof(PositionMetadata))]
    public partial class tbl_Position
    {
        internal sealed class PositionMetadata
        {
            public int ID { get; set; }
            [Display(Name =("Tên chức vụ"))]
            [Required(ErrorMessage ="Không được bỏ trống chức vụ")]
            
            public string PositionName { get; set; }
        }
    }
}