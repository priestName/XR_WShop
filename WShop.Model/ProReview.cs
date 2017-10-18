namespace WShop.EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProReview")]
    public partial class ProReview
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string ProCode { get; set; }

        public int CusId { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Body { get; set; }

        public byte Staty { get; set; }

        [Required]
        [StringLength(50)]
        public string Rate { get; set; }

        public DateTime CreateTime { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Product Product { get; set; }
    }
}
