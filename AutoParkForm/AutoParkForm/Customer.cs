namespace AutoParkForm
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Customer")]
    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            Order = new HashSet<Order>();
        }

        [Key]
        [StringLength(10)]
        public string IDCust { get; set; }

        [Required]
        [StringLength(20)]
        public string Firs_name { get; set; }

        [Required]
        [StringLength(20)]
        public string Sure_name { get; set; }

        [Required]
        [StringLength(20)]
        public string Last_name { get; set; }

        [Required]
        [StringLength(10)]
        public string IDPos { get; set; }

        public virtual Positions Positions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Order { get; set; }
    }
}
