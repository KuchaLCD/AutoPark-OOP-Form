namespace AutoParkForm
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Auto")]
    public partial class Auto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Auto()
        {
            Order = new HashSet<Order>();
        }

        [Key]
        [StringLength(10)]
        public string IDAuto { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Type { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Column(TypeName = "money")]
        public decimal? Insurance_cost { get; set; }

        [Required]
        [StringLength(10)]
        public string State_numb { get; set; }

        [Required]
        [StringLength(10)]
        public string IDSupplier { get; set; }

        [Column(TypeName = "money")]
        public decimal Cost { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string VolumeOfEngine { get; set; }

        [Column(TypeName = "text")]
        public string Additions { get; set; }

        public virtual Supplier Supplier { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Order { get; set; }
    }
}
