namespace WShop.EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FeedBack")]
    public partial class FeedBack
    {
        [Key]
        public int FID { get; set; }

        [Required]
        [StringLength(11)]
        public string FTel { get; set; }

        [Required]
        [StringLength(1000)]
        public string FText { get; set; }
    }
}
