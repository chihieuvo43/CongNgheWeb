using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CongNgheWeb_3_0.Models
{
    [MetadataTypeAttribute(typeof(CategoryMetadata))]
    public partial class tbl_Category
    {
        internal sealed class CategoryMetadata
        {
            public int ID { get; set; }

            [Display(Name ="Tên danh mục")]
            [Required(ErrorMessage ="Không được bỏ trống tên danh mục")]
            public string CategoryName { get; set; }

            public Nullable<bool> Deleted { get; set; }

            public string Icon { get; set; }
        }
    }
}