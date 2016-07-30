using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CIELO_TM.Models;
namespace CIELO_TM.Controllers
{
    public class StoreController : Controller
    {

        BaseDatos db = new BaseDatos();
        // GET: Store
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var prs = db.PRODUCTOS.Find(id);



            return View(prs);
        }

        public ActionResult Menu(string v, string q)
        {
            if (v.Equals("Men"))
            {
                var men = from u in db.PRODUCTOS where u.GENERO.Equals("M") select u;
                return View(men);
            }
            var clothing = from u in db.PRODUCTOS where u.CATEGORIA.CATEGORIA1.Equals(v) select u;


            var model = db.PRODUCTOS.Where(r => q == null || r.MARCA.MARCA1.StartsWith(q) &&
            r.CATEGORIA.CATEGORIA1.StartsWith(v))
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
                    });

            if (Request.IsAjaxRequest())
            {
                return PartialView("_Categorias", model);
            }
            return View(clothing);

        }
    





    }
}