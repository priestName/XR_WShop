namespace WShop.EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderBillFath")]
    public partial class OrderBillFath
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrderBillFath()
        {
            OrderBillChis = new HashSet<OrderBillChi>();
        }

        [Key]
        [StringLength(50)]
        public string Code { get; set; }

        public int CusId { get; set; }

        public int State { get; set; }

        [Column(TypeName = "numeric")]
        public decimal OrderPrice { get; set; }

        [Column(TypeName = "numeric")]
        public decimal ExpressPrice { get; set; }

        [Column(TypeName = "numeric")]
        public decimal SumPrice { get; set; }

        public int PayId { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Memo { get; set; }

        [Required]
        [StringLength(50)]
        public string CheckUser { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime PayTime { get; set; }

        public DateTime PostTime { get; set; }

        public DateTime ReceTime { get; set; }

        public virtual Customer Customer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderBillChi> OrderBillChis { get; set; }

        public virtual Payment Payment { get; set; }
    }
}
