namespace AutoParkForm
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Reporting")]
    public partial class Reporting
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string IDReport { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string IDOrder { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "date")]
        public DateTime DateReport { get; set; }
    }
}
