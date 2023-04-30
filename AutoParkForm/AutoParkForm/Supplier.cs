namespace AutoParkForm
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Supplier")]
    public partial class Supplier
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Supplier()
        {
            Auto = new HashSet<Auto>();
        }

        [Key]
        [StringLength(10)]
        public string IDSupplier { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string FullName { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string Adress { get; set; }

        [Required]
        [StringLength(10)]
        public string Phone { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Auto> Auto { get; set; }
    }
}
