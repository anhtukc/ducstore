namespace ducstore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("employee")]
    public partial class employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public employee()
        {
            accounts = new HashSet<account>();
            receiptbills = new HashSet<receiptbill>();
        }
        [Display(Name = "Mã nhân viên")]
        public Guid employeeid { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Tên nhân viên")]
        public string employeename { get; set; }

        [Required]
        [StringLength(15)]
        [Display(Name = "Số điện thoại")]
        public string phonenumber { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Ngày sinh")]
        public DateTime birthday { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Địa chỉ")]
        public string address { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "email")]
        public string email { get; set; }

        [StringLength(20)]
        [Display(Name = "cmnd")]
        public string identitycardnumber { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<account> accounts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<receiptbill> receiptbills { get; set; }
    }
}
