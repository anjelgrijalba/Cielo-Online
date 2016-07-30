using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CIELO_TM.Models;

namespace CIELO_TM.Controllers
{
    public class ShoppingCartController : Controller
    {
        BaseDatos db = new BaseDatos();
        // GET: ShoppingCart
        public ActionResult Index()
        {
            var cart = CarritoDeCompras.GetCart(this.HttpContext);

            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };

            return View(viewModel);
        }

        public ActionResult AddToCart(int id)
        {

            var agregarProduc = db.PRODUCTOS.Single(prod => prod.ID_PRODUCTO == id);

            var cart = CarritoDeCompras.GetCart(this.HttpContext);

            cart.AgregarCarrito(agregarProduc);

            return RedirectToAction("Index","Home");
        }

        public ActionResult RemoveFromCart(int id)
        {
            var cart = CarritoDeCompras.GetCart(this.HttpContext);

            cart.eliminarCarrito(id);

           return RedirectToAction("Index");
        }

        public ActionResult emptycart()
        {
            var cart = CarritoDeCompras.GetCart(this.HttpContext);

            cart.VaciarCarrito();

            return RedirectToAction("Index");
        }

        public ActionResult numeritos()
        {
            var cart = CarritoDeCompras.GetCart(this.HttpContext);
          
                ViewData["conteo"] = cart.GetCount();
                return PartialView("_numeritos");
                
         
        }
    }
}