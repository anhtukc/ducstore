namespace ducstore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("receiptbilldetail")]
    public partial class receiptbilldetail
    {
        [Key]
        [Column(Order = 0)]
        public Guid receiptid { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string productid { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(200)]
        public string productname { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int price { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int quantity { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int grandpaid { get; set; }

        public virtual product product { get; set; }

        public virtual receiptbill receiptbill { get; set; }
    }
}
