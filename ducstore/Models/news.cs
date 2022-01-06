namespace ducstore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class news
    {
        [Display(Name = "Mã tin")]
        public Guid newsid { get; set; }

        [Required]
        [StringLength(300)]
        [Display(Name = "Tiêu đề")]
        public string newstittle { get; set; }

        [StringLength(100)]
        [Display(Name = "Tác giả")]
        public string author { get; set; }

        [StringLength(100)]
        [Display(Name = "Ảnh")]
        public string picture { get; set; }

        [Display(Name = "Bài viết")]
        public string content { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Ngày tạo")]
        public DateTime? daycreate { get; set; }
    }
}
