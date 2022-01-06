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
        [Display(Name = "Mã sản phẩm")]
        public string productid { get; set; }

        [StringLength(50)]
        [Display(Name = "Mã loại sản phẩm")]
        public string typeproductid { get; set; }

        [Display(Name = "Mã nhà cung cấp")]
        public Guid? providerid { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Tên sản phẩm")]
        public string productname { get; set; }

        [Display(Name = "Giá khuyến mãi")]
        public int promotion { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Ảnh")]
        public string picture { get; set; }

        [Display(Name = "Giá bán")]
        public int price { get; set; }

        [Display(Name = "Số lượng")]
        public int quantity { get; set; }

        [Display(Name = "Miêu tả")]
        public string description { get; set; }

        public virtual provider provider { get; set; }

        public virtual typeproduct typeproduct { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<receiptbilldetail> receiptbilldetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sellbilldetail> sellbilldetails { get; set; }
    }
}
