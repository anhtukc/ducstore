namespace ducstore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("account")]
    public partial class account
    {
        [Display(Name = "Mã nhân viên")]
        public Guid? employeeid { get; set; }

        [Key]
        [StringLength(100)]
        [Display(Name = "Tài khoản")]
        public string username { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Mật khẩu")]
        public string password { get; set; }

        public virtual employee employee { get; set; }
    }
}
