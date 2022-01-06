namespace ducstore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("receiptbill")]
    public partial class receiptbill
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public receiptbill()
        {
            receiptbilldetails = new HashSet<receiptbilldetail>();
        }

        [Key]
        [Display(Name = "Mã hóa đơn")]
        public Guid receiptid { get; set; }

        [Display(Name = "Nhân viên")]
        public Guid employeeid { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Ngày tạo")]
        public DateTime? daycreate { get; set; }

        [Display(Name = "Nhà cung cấp")]
        public Guid providerid { get; set; }

        [Display(Name = "Tổng tiền")]
        public int totalpaid { get; set; }

        public virtual employee employee { get; set; }

        public virtual provider provider { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<receiptbilldetail> receiptbilldetails { get; set; }
    }
}
