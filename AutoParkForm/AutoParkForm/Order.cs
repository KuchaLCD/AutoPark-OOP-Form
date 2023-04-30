namespace AutoParkForm
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        [Key]
        [StringLength(10)]
        public string IDOrder { get; set; }

        [Required]
        [StringLength(10)]
        public string IDCust { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date_of_SR { get; set; }

        [Required]
        [StringLength(30)]
        public string Count_of_DR { get; set; }

        [Required]
        [StringLength(10)]
        public string IDAuto { get; set; }

        [Column(TypeName = "money")]
        public decimal Billing { get; set; }

        public virtual Auto Auto { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
