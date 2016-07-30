using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CIELO_TM.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CIELO_TM.Controllers
{
    [Authorize(Roles ="Cliente")]
    public class CheckoutController : Controller
    {
        BaseDatos db = new BaseDatos();
        public ActionResult Payment()
        {
            var cart = CarritoDeCompras.GetCart(this.HttpContext);
            ViewBag.total = cart.GetTotal();

            return View();
            
        }


        // GET: Checkout
        [HttpPost]
        public ActionResult Payment(ORDEN order)
        {


            TryUpdateModel(order);

                    var cart = CarritoDeCompras.GetCart(this.HttpContext);

                    order.cliente_id = User.Identity.GetUserId();
                    order.OrderDate = DateTime.Now;
                    order.Total = cart.GetTotal();
                    db.ORDEN.Add(order);
                    db.SaveChanges();

            cart.CrearOrden(order);
            return RedirectToAction("Completado", new { id = order.OrderId });
          

        }

        public ActionResult Completado(int id)
        {
            bool isValid = db.ORDEN.Any(o => o.OrderId == id &&  o.AspNetUsers.UserName == User.Identity.Name);
            if (isValid)
            {
                return View(id);
            }
            else
            {
    
                return View("Error");
            }
        }



        public ActionResult infoUsuario()
        {
            var currentUserId = User.Identity.GetUserId();
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var currentUser = manager.FindById(User.Identity.GetUserId());

            var AspUser = from u in db.AspNetUsers where u.Id == currentUser.Id.ToString() select u;
            return PartialView(AspUser);
        }
    }
}