namespace ducstore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("product")]
    public partial class product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public product()
        {
            receiptbilldetails = new HashSet<receiptbilldetail>();
            sellbilldetails = new HashSet<sellbilldetail>();
        }

        [StringLength(50)]
        public string productid { get; set; }

        [StringLength(50)]
        public string typeproductid { get; set; }

        public Guid? providerid { get; set; }

        [Required]
        [StringLength(200)]
        public string productname { get; set; }

        public int promotion { get; set; }

        [Required]
        [StringLength(100)]
        public string picture { get; set; }

        public int price { get; set; }

        public int quantity { get; set; }

        public string description { get; set; }

        public virtual provider provider { get; set; }

        public virtual typeproduct typeproduct { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<receiptbilldetail> receiptbilldetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sellbilldetail> sellbilldetails { get; set; }
    }
}
