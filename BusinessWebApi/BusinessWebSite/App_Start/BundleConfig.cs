using System.Web;
using System.Web.Optimization;

namespace BusinessWebSite
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-3.4.1.min.js",
            //            "~/Scripts/jquery-3.4.1.js"
            //            ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Content/datatables/datatables.min.js",
                        "~/Content/datatables/Buttons-1.6.1/js/dataTables.buttons.min.js",
                        "~/Content/datatables/JSZip-2.5.0/jszip.min.js",
                        "~/Content/datatables/pdfmake-0.1.36/pdfmake.min.js",
                        "~/Content/datatables/pdfmake-0.1.36/vfs_fonts.js",
                        "~/Content/datatables/Buttons-1.6.1/js/buttons.html5.min.js",
                        "~/Content/js/global.js"
                        ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/bootstrap-theme.css",
                      "~/Content/css/global.css",
                      "~/Content/css/master.css",
                      "~/Content/site.css",
                      "~/Content/datatables/datatables.min.css",
                      "~/Content/datatables/DataTables-1.10.20/css/dataTables.bootstrap4.min.css"
                      ));
        }
    }
}
