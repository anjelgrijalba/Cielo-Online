using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CIELO_TM.Models;
using PagedList;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CIELO_TM.Controllers
{
    public class HomeController : Controller
    {
        BaseDatos db = new BaseDatos();

        public ActionResult AutoComple(string term)
        {
            var model = db.PRODUCTOS.Where(r => r.PRODUCTO.StartsWith(term)).Take(5).Select(r => new
            {
                label = r.PRODUCTO
            });

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index(string buscar =null, int pagina=1)
        {

            //try
            //{
            //    var currentuserid = user.identity.getuserid();
            //    var manager = new usermanager<applicationuser>(new userstore<applicationuser>(new applicationdbcontext()));
            //    var currentuser = manager.findbyid(user.identity.getuserid());
            //    viewbag.apell = currentuser.apellido.tostring();
            //}
            //catch
            //{

            //}

            var model = db.PRODUCTOS.OrderByDescending(a => a.DETALLES_ORDEN.Average(B => B.PRODUCTOS.DETALLES_ORDEN.Count()))
                .Where(r => buscar == null || r.PRODUCTO.StartsWith(buscar))
                .Select
                (
                    u => new CieloListViewModel
                    {
                        ID_PRODUCTO = u.ID_PRODUCTO,
                        IMAGEN1 = u.IMAGEN1,
                        IMAGEN2 = u.IMAGEN2,
                        IMAGEN3 = u.IMAGEN3,
                        ID_MARCA = u.ID_MARCA,
                        ID_CATEGORIA = u.ID_CATEGORIA,
                        PRODUCTO = u.PRODUCTO,
                        PRECIO_VENTA = u.PRECIO_VENTA,
                        DESCRIPCION = u.DESCRIPCION,
                        NumeroDeVentas = u.DETALLES_ORDEN.Count()
                    }).ToPagedList(pagina,8);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_Productos", model);
            }
            return View(model);
        }


        public ActionResult AgregarComentario(int id)
        {
            PRODUCTOS state = db.PRODUCTOS.Find(id);
            ViewBag.id = id;
            return View();
        }

        [HttpPost]
        public ActionResult PublicarComentario([Bind(Include ="ID_COMENTARIO,COMENTARIO1,ID_PRODUCTO,ID_USUARIO")]COMENTARIO comment)
        {

            if (ModelState.IsValid)
            {
                comment.ID_USUARIO = User.Identity.GetUserId();
            
    
                db.COMENTARIO.Add(comment);
                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("AgregarComentario");
            }
        }




    }
}