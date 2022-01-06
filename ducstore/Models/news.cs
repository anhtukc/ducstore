namespace ducstore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class news
    {
        public Guid newsid { get; set; }

        [Required]
        [StringLength(300)]
        public string newstittle { get; set; }

        [StringLength(100)]
        public string author { get; set; }

        [StringLength(100)]
        public string picture { get; set; }

        public string content { get; set; }

        [Column(TypeName = "date")]
        public DateTime? daycreate { get; set; }
    }
}
