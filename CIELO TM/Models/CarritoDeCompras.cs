using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CIELO_TM.Models
{
    public partial class CarritoDeCompras
    {
        BaseDatos db = new BaseDatos();
        string carritosdecomprasID { get; set; }
        public const string CartSessionKey = "CartId";

        public static CarritoDeCompras GetCart(HttpContextBase context)
        {
            var cart = new CarritoDeCompras();
            cart.carritosdecomprasID = cart.GetCartId(context);
            return cart;
        }

        public static CarritoDeCompras GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }

        public void AgregarCarrito(PRODUCTOS prod)
        {
            var cartItem = db.CARTS.SingleOrDefault(c => c.CartId == carritosdecomprasID && c.ID_producto == prod.ID_PRODUCTO);

            if (cartItem == null)
            {
                cartItem = new CARTS
                {
                    ID_producto = prod.ID_PRODUCTO,
                    CartId = carritosdecomprasID,
                    contador = 1,
                    fecha = DateTime.Now
                };

                db.CARTS.Add(cartItem);
            }
            else
            {
                cartItem.contador++;
            }

            db.SaveChanges();
        }

        public void eliminarCarrito(int id)
        {
            var cartItems = db.CARTS.Single(cart => cart.CartId.Equals(carritosdecomprasID) && cart.RecordId.Equals(id));

            if (cartItems != null)
            {
              db.CARTS.Remove(cartItems);
              db.SaveChanges();
            }
            else
            {
                List<PRODUCTOS> a = new List<PRODUCTOS>();
                a.RemoveAt(id);
            }


        }
        public void VaciarCarrito()
        {
            var cartItems = db.CARTS.Where(cart => cart.CartId == carritosdecomprasID);

            foreach (var cartItem in cartItems)
            {
                db.CARTS.Remove(cartItem);
            }

            db.SaveChanges();
        }
        public List<CARTS> GetCartItems()
        {
            return db.CARTS.Where(cart => cart.CartId == carritosdecomprasID).ToList();
        }

        public int GetCount()
        {
            var eb = from u in db.CARTS select u.PRODUCTOS.PRODUCTO;
            if (eb == GetCartItems())
            {

            }


            else
            {
                int? count = (from cartItems in db.CARTS
                              where cartItems.CartId == carritosdecomprasID
                              select (int?)cartItems.contador).Sum();
                return count ?? 0;

            }

            return 0;
        }

        public decimal GetTotal()
        {
 
            decimal? total = (from cartItems in db.CARTS
                              where cartItems.CartId == carritosdecomprasID
                              select (int?)cartItems.contador * cartItems.PRODUCTOS.PRECIO_VENTA).Sum();
            return total ?? decimal.Zero;
        }

        public int CrearOrden(ORDEN order)
        {
            decimal? orderTotal = 0;

            var cartItems = GetCartItems();

            foreach (var item in cartItems)
            {
                var orderDetail = new DETALLES_ORDEN
                {
                    ProductoId = item.ID_producto,
                    OrderId = order.OrderId,
                    UnitPrice = item.PRODUCTOS.PRECIO_VENTA,
                    Quantity = item.contador
                };

                orderTotal += (item.contador * item.PRODUCTOS.PRECIO_VENTA);

                db.DETALLES_ORDEN.Add(orderDetail);
                orderDetail.PRODUCTOS.CANTIDAD = (int)(orderDetail.PRODUCTOS.CANTIDAD - orderDetail.Quantity);
            }

            

            order.Total = orderTotal;

            db.SaveChanges();

            VaciarCarrito();

            return order.OrderId;
        }

        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] = context.User.Identity.Name;
                }
                else
                {
                    Guid tempCartId = Guid.NewGuid();

                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }

            return context.Session[CartSessionKey].ToString();
        }

        //public void MigrateCart(string userName)
        //{
        //    var shoppingCart = db.CARTS.Where(c => c.CartId == carritosdecomprasID);

        //    foreach (CARTS item in shoppingCart)
        //    {
        //        item.CartId = userName;
        //    }
        //    db.SaveChanges();
        //}

    }
}