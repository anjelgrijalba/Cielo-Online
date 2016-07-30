namespace CIELO_TM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CARTS
    {
        [Key]
        public int RecordId { get; set; }

        public string CartId { get; set; }

        public int? ID_producto { get; set; }

        public int? contador { get; set; }

        public DateTime? fecha { get; set; }

        public virtual PRODUCTOS PRODUCTOS { get; set; }
    }
}
