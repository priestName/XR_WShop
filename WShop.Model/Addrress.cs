namespace WShop.EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Addrress")]
    public partial class Addrress
    {
        public int ID { get; set; }

        [Required]
        [StringLength(500)]
        public string Address { get; set; }

        [Required]
        [StringLength(500)]
        public string AddressMain { get; set; }

        [Required]
        [StringLength(11)]
        public string Tel { get; set; }

        [Required]
        [StringLength(50)]
        public string Aname { get; set; }

        public int Acode { get; set; }

        public int? CusID { get; set; }

        public int State { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
