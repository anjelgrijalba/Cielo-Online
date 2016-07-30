namespace CIELO_TM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ORDEN")]
    public partial class ORDEN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ORDEN()
        {
            DETALLES_ORDEN = new HashSet<DETALLES_ORDEN>();
        }

        [Key]
        public int OrderId { get; set; }

        public DateTime? OrderDate { get; set; }

        public decimal? Total { get; set; }

        [Required]
        [StringLength(128)]
        public string cliente_id { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DETALLES_ORDEN> DETALLES_ORDEN { get; set; }
    }
}
