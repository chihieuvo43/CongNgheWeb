//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CongNgheWeb_3_0.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_Category
    {
        public tbl_Category()
        {
            this.tbl_Sort_Course = new HashSet<tbl_Sort_Course>();
        }
    
        public int ID { get; set; }
        public string CategoryName { get; set; }
        public string Icon { get; set; }
        public Nullable<bool> Deleted { get; set; }
    
        public virtual ICollection<tbl_Sort_Course> tbl_Sort_Course { get; set; }
    }
}
