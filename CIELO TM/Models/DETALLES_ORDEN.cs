namespace CIELO_TM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DETALLES_ORDEN
    {
        [Key]
        public int OrderDetailId { get; set; }

        public int? OrderId { get; set; }

        public int? ProductoId { get; set; }

        public int? Quantity { get; set; }

        public decimal? UnitPrice { get; set; }

        public virtual PRODUCTOS PRODUCTOS { get; set; }

        public virtual ORDEN ORDEN { get; set; }
    }
}
