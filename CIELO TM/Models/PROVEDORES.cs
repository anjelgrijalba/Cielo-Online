namespace CIELO_TM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PROVEDORES
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PROVEDORES()
        {
            INVENTARIO = new HashSet<INVENTARIO>();
            PRODUCTOS = new HashSet<PRODUCTOS>();
        }

        [Key]
        public int ID_PROVEDORES { get; set; }

        [StringLength(30)]
        public string NOMBRE { get; set; }

        [StringLength(30)]
        public string APELLIDO { get; set; }

        [StringLength(100)]
        public string DIRECCION { get; set; }

        [StringLength(15)]
        public string TELEFONO { get; set; }

        [StringLength(100)]
        public string CORREO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<INVENTARIO> INVENTARIO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRODUCTOS> PRODUCTOS { get; set; }
    }
}
