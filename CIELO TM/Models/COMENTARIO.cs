namespace CIELO_TM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("COMENTARIO")]
    public partial class COMENTARIO
    {
        [Key]
        public int ID_COMENTARIO { get; set; }

        [Column("COMENTARIO")]
        [StringLength(200)]
        public string COMENTARIO1 { get; set; }

        public int ID_PRODUCTO { get; set; }

        [StringLength(128)]
        public string ID_USUARIO { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }

        public virtual PRODUCTOS PRODUCTOS { get; set; }
    }
}
