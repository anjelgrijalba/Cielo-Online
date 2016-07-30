using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CIELO_TM.Models
{
    public class OrdenesViewModel
    {
        public int OrderId { get; set; }

        public DateTime? OrderDate { get; set; }

        public decimal? Total { get; set; }

        public string cliente_id { get; set; }
        public int OrderDetailId { get; set; }

        public int? ProductoId { get; set; }

        public int? Quantity { get; set; }

        public decimal? UnitPrice { get; set; }

        public virtual PRODUCTOS productos { get; set; }


    }
}