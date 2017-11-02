namespace WShop.EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Stock")]
    public partial class Stock
    {
        [Key]
        [StringLength(50)]
        public string ProCode { get; set; }

        public int QTY { get; set; }

        public int sales { get; set; }

        public DateTime CreateTime { get; set; }

        public virtual Product Product { get; set; }
    }
}
