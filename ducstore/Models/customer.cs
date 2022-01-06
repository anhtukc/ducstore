namespace ducstore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("customer")]
    public partial class customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public customer()
        {
            sellbills = new HashSet<sellbill>();
        }

        public Guid customerid { get; set; }

        [Required]
        [StringLength(15)]
        public string phonenumber { get; set; }

        [Required]
        [StringLength(100)]
        public string customername { get; set; }

        [Required]
        [StringLength(100)]
        public string address { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sellbill> sellbills { get; set; }
    }
}
