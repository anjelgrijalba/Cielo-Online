using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CIELO_TM.Models;
using System.Data.Entity;
using System.Web.Helpers;
using System.Net;

namespace CIELO_TM.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class StoreManagerController : Controller
    {
        INVENTARIO inve = new INVENTARIO();
        BaseDatos db = new BaseDatos();
        ApplicationUser appuser = new ApplicationUser();

        // GET: StoreManager
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult AgregarMarca()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GuardarMarca(MARCA marca)
        {
            if (ModelState.IsValid)
            {
                db.MARCA.Add(marca);
                db.SaveChanges();
                return RedirectToAction("AgregarMarca");
            }
            else
            {
                return RedirectToAction("index");
            }

        }


        public ActionResult AgregarCategoria()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GuardarCategoria(CATEGORIA categoria)
        {
            if (ModelState.IsValid)
            {
                db.CATEGORIA.Add(categoria);
                db.SaveChanges();
                return RedirectToAction("AgregarCategoria");
            }
            else
            {
                return RedirectToAction("index");
            }

        }



        public ActionResult AgregarProducto()
        {
            ViewBag.ID_CATEGORIA = new SelectList(db.CATEGORIA, "ID_CATEGORIA", "CATEGORIA1");
            ViewBag.ID_MARCA = new SelectList(db.MARCA, "ID_MARCA", "MARCA1");
            ViewBag.ID_PROVEDORES = new SelectList(db.PROVEDORES, "ID_PROVEDORES", "NOMBRE");
            return View();
        }
        [HttpPost]
        public ActionResult GuardarProducto(PRODUCTOS productos, HttpPostedFileBase foto1, HttpPostedFileBase foto2, HttpPostedFileBase foto3)
        {
            if (ModelState.IsValid)
            {
              
                var PhotoUrl = Server.MapPath("/images" + foto1.FileName);

                if (foto1 != null && foto1.ContentLength > 0)
                    foto1.SaveAs(PhotoUrl);
                    productos.IMAGEN1= "/images" + foto1.FileName;

                if (foto2 != null && foto2.ContentLength > 0)
                    foto2.SaveAs(PhotoUrl);
                productos.IMAGEN2 = "/images" + foto2.FileName;


                if (foto3 != null && foto3.ContentLength > 0)
                    foto3.SaveAs(PhotoUrl);
                productos.IMAGEN3 = "/images" + foto3.FileName;


                inve.DISPONIBLES = productos.CANTIDAD;
                    inve.ID_PRODUCTO = productos.ID_PRODUCTO;
                    inve.ID_PROVEDOR = productos.ID_PROVEDOR;

                    productos.estatus = false;
                    productos.FECHA = DateTime.Now;
                    db.PRODUCTOS.Add(productos);
                    db.INVENTARIO.Add(inve);

                    db.SaveChanges();


                return RedirectToAction("AgregarProducto");
            }




            return View(productos);


        }

        public ActionResult inventario()
        {
            var inv = from u in db.INVENTARIO select u;
            return View(inv.ToList());
        }

        public ActionResult CuentasPorPagar()
        {
            ViewBag.TotalperProvedor = from prod in db.PRODUCTOS select (int)(prod.CANTIDAD * prod.PRECIO_VENTA);
            ViewBag.Total= (from prod in db.PRODUCTOS where prod.estatus == false select prod.CANTIDAD * prod.PRECIO_VENTA).Sum();
            var sql = from u in db.PRODUCTOS where u.estatus == false select u;

            return View(sql);
        }

        public ActionResult PagarCuentas(int id, PRODUCTOS product)
        {

            PRODUCTOS pRODUCTOS = db.PRODUCTOS.Find(id);
            if (pRODUCTOS == null)
            {
                return HttpNotFound();
            }
            return View(pRODUCTOS);
        }
        [HttpPost]
        [ActionName("PagarCuentas")]
        public ActionResult PagarCuentas([Bind(Include  = "ID_PRODUCTO,IMAGEN1,IMAGEN2,IMAGEN3,PRODUCTO,DESCRIPCION,PRECIO_VENTA,ID_MARCA,ID_CATEGORIA,ID_PROVEDOR,CANTIDAD,estatus")] PRODUCTOS product)
        {

            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");

            }
            return View(product);
        }

        public ActionResult Cuentaspagadas()
        {
            var sql = from u in db.PRODUCTOS where u.estatus == true select u;
            return View(sql);
        }

        //el real invento que salio positivo :p
        public ActionResult informarVentas()
        {
            var producctos = db.PRODUCTOS.OrderByDescending(a => a.DETALLES_ORDEN.Average(B => B.PRODUCTOS.DETALLES_ORDEN.Count()))
                .Select
                (

                    u => new
                    {
                        PRODUCTO = u.PRODUCTO,
                        NumeroDeVentas = u.DETALLES_ORDEN.Count()
                    });


            var myChart = new Chart(width: 800, height: 600)
                 .AddTitle("Informe de ventas")
                 .AddSeries(
                     name: "Ventas",
                     xValue: producctos, xField: "PRODUCTO",
                    yValues: producctos, yFields: "NumeroDeVentas")
                .Write();

            return View(myChart);
        }

        public ActionResult Provedores()
        {
            var prove = from i in db.PROVEDORES select i;
            try
            {
                return View(prove);
            }
            catch
            {
                return View();
            }


        }

        // GET: PROVEDORES/Details/5
        public ActionResult DetallesProvedores(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROVEDORES pROVEDORES = db.PROVEDORES.Find(id);
            if (pROVEDORES == null)
            {
                return HttpNotFound();
            }
            return View(pROVEDORES);
        }

        public ActionResult AgregarProvedor()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarProvedor([Bind(Include = "ID_PROVEDORES,NOMBRE,APELLIDO,DIRECCION,TELEFONO,CORREO")] PROVEDORES pROVEDORES)
        {
            if (ModelState.IsValid)
            {
                db.PROVEDORES.Add(pROVEDORES);
                db.SaveChanges();
                return RedirectToAction("Provedores");
            }

            return View(pROVEDORES);
        }

        // GET: PROVEDORES/Edit/5
        public ActionResult EditarProvedor(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROVEDORES pROVEDORES = db.PROVEDORES.Find(id);
            if (pROVEDORES == null)
            {
                return HttpNotFound();
            }
            return View(pROVEDORES);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarProvedor([Bind(Include = "ID_PROVEDORES,NOMBRE,APELLIDO,DIRECCION,TELEFONO,CORREO")] PROVEDORES pROVEDORES)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pROVEDORES).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("provedores");
            }
            return View(pROVEDORES);
        }

        // GET: PROVEDORES/Delete/5
        public ActionResult BorrarProvedor(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROVEDORES pROVEDORES = db.PROVEDORES.Find(id);
            if (pROVEDORES == null)
            {
                return HttpNotFound();
            }
            return View(pROVEDORES);
        }

        [HttpPost, ActionName("BorrarProvedor")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PROVEDORES pROVEDORES = db.PROVEDORES.Find(id);
            db.PROVEDORES.Remove(pROVEDORES);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult EliminarProducto(int? id, int v)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            INVENTARIO inve = db.INVENTARIO.Find(id);
            PRODUCTOS prod = db.PRODUCTOS.Find(v);


            db.INVENTARIO.Remove(inve);
            db.PRODUCTOS.Remove(prod);
            db.SaveChanges();
            return RedirectToAction("inventario");
        }






    }
}
