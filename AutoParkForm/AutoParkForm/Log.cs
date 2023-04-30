namespace AutoParkForm
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Log")]
    public partial class Log
    {
        [StringLength(30)]
        public string Name { get; set; }

        [Key]
        [StringLength(30)]
        public string RegNum { get; set; }

        [StringLength(30)]
        public string Notes { get; set; }
    }
}
