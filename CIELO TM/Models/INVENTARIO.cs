namespace CIELO_TM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("INVENTARIO")]
    public partial class INVENTARIO
    {
        [Key]
        public int ID_VENTARIO { get; set; }

        public int? ID_PRODUCTO { get; set; }

        public int? DISPONIBLES { get; set; }

        public int? ID_PROVEDOR { get; set; }

        public virtual PRODUCTOS PRODUCTOS { get; set; }

        public virtual PROVEDORES PROVEDORES { get; set; }
    }
}
