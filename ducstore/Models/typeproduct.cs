namespace ducstore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("typeproduct")]
    public partial class typeproduct
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public typeproduct()
        {
            products = new HashSet<product>();
        }

        [StringLength(50)]
        [Display(Name = "Mã lo?i s?n ph?m")]
        public string typeproductid { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Tên lo?i s?n ph?m")]
        public string typeproductname { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<product> products { get; set; }
    }
}
