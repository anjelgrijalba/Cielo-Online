namespace CIELO_TM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PRODUCTOS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRODUCTOS()
        {
            CARTS = new HashSet<CARTS>();
            COMENTARIO = new HashSet<COMENTARIO>();
            DETALLES_ORDEN = new HashSet<DETALLES_ORDEN>();
            INVENTARIO = new HashSet<INVENTARIO>();
        }

        [Key]
        public int ID_PRODUCTO { get; set; }

        [StringLength(200)]
        public string IMAGEN1 { get; set; }

        [StringLength(100)]
        public string IMAGEN2 { get; set; }

        [StringLength(100)]
        public string IMAGEN3 { get; set; }

        [StringLength(50)]
        public string PRODUCTO { get; set; }

        [StringLength(100)]
        public string DESCRIPCION { get; set; }

        [Column(TypeName = "money")]
        public decimal? PRECIO_VENTA { get; set; }

        public int ID_MARCA { get; set; }

        public int ID_CATEGORIA { get; set; }

        public int ID_PROVEDOR { get; set; }

        public int CANTIDAD { get; set; }

        public bool? estatus { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FECHA { get; set; }

        [StringLength(10)]
        public string GENERO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CARTS> CARTS { get; set; }

        public virtual CATEGORIA CATEGORIA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<COMENTARIO> COMENTARIO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DETALLES_ORDEN> DETALLES_ORDEN { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<INVENTARIO> INVENTARIO { get; set; }

        public virtual MARCA MARCA { get; set; }

        public virtual PROVEDORES PROVEDORES { get; set; }
    }
}
