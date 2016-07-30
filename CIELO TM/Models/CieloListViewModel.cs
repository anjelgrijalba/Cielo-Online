using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CIELO_TM.Models
{
    public class CieloListViewModel
    {
        public int ID_PRODUCTO { get; set; }

        public string IMAGEN1 { get; set; }

        public string IMAGEN2 { get; set; }

        public string IMAGEN3 { get; set; }

        public string PRODUCTO { get; set; }

        public string DESCRIPCION { get; set; }

        public decimal? PRECIO_VENTA { get; set; }

        public int ID_MARCA { get; set; }

        public int ID_CATEGORIA { get; set; }

        public int NumeroDeVentas { get; set; }

    }
}