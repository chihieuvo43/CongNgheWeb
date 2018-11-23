using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CongNgheWeb_3_0.Models
{

    [MetadataTypeAttribute(typeof(PosterMetadata))]
    public partial class tbl_Poster
    {
        internal sealed class PosterMetadata
        {
            public int ID { get; set; }

            [Display(Name = "Hình ảnh")]
            public string ImagePoster { get; set; }

            [Display(Name = "Ngày tạo")]
            [DataType(DataType.Date)]
            public Nullable<System.DateTime> CreateDate { get; set; }

            [Display(Name = "Khóa")]
           
            public Nullable<bool> Clock { get; set; }
        }
    }
}