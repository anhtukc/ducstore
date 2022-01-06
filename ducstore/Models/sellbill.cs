namespace ducstore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("sellbill")]
    public partial class sellbill
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public sellbill()
        {
            sellbilldetails = new HashSet<sellbilldetail>();
        }
        [Display(Name = "Mã hóa đơn")]
        public Guid sellbillid { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Ngày tạo")]
        public DateTime? daycreate { get; set; }

        [Display(Name = "Khách hàng")]
        public Guid customerid { get; set; }

        [Display(Name = "Tổng tiền")]
        public int totalpaid { get; set; }

        public virtual customer customer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sellbilldetail> sellbilldetails { get; set; }
    }
}
