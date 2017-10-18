namespace WShop.EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderBillChi")]
    public partial class OrderBillChi
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string Code { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string ProCode { get; set; }

        [Column(TypeName = "numeric")]
        public decimal UnitPrice { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Qty { get; set; }

        [Column(TypeName = "numeric")]
        public decimal SumPrice { get; set; }

        public bool IdReview { get; set; }

        public virtual OrderBillFath OrderBillFath { get; set; }

        public virtual Product Product { get; set; }
    }
}
