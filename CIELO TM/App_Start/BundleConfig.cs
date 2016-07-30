using System.Web;
using System.Web.Optimization;

namespace CIELO_TM
{
    public class BundleConfig
    {
        // Para obtener más información sobre Bundles, visite http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/slider").Include("~/Scripts/jquery.sldr.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryvalunobtrusive").Include(
                    "~/Scripts/jquery.validate.unobtrusive.js"));



            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                     "~/Scripts/jquery-ui-{version}.js"));


            bundles.Add(new ScriptBundle("~/bundles/cielo").Include(
                      "~/Scripts/cielo.js"));

            bundles.Add(new ScriptBundle("~/bundles/ajax").Include(
                       "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/arriba").Include(
                       "~/Scripts/arriba.js"));

            //bundles.Add(new ScriptBundle("~/bundles/header").Include(
            //          "~/Scripts/header.js"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // preparado para la producción y podrá utilizar la herramienta de compilación disponible en http://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/fonts.css",
                      "~/Content/style.css",
                      "~/Content/PagedList.css"
                      ));
        }
    }
}
