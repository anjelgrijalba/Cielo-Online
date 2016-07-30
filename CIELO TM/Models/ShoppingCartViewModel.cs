using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CIELO_TM.Models
{
    public class ShoppingCartViewModel
    {
        public List<CARTS> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}