namespace ducstore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("provider")]
    public partial class provider
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public provider()
        {
            products = new HashSet<product>();
            receiptbills = new HashSet<receiptbill>();
        }

        [Display(Name = "Mã nhà cung cấp")]
        public Guid providerid { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Tên nhà cung cấp")]
        public string providername { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Địa chỉ")]
        public string address { get; set; }

        [Required]
        [StringLength(15)]
        [Display(Name = "Số điện thoại")]
        public string phonenumber { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<product> products { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<receiptbill> receiptbills { get; set; }
    }
}
