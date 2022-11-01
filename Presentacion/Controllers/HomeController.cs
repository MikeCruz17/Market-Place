using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaNegocio;
using CapaEntidad;
using System.Web.Security;
using Presentacion.permisos;

namespace Presentacion.Controllers
{
    public class HomeController : Controller
    {
        clase_negocio Neg = new clase_negocio();
        
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }



// ********************************************************************************************* //
// ********************************************************************************************* //

        [PermisosRol(1)]
        public ActionResult panel_Admin()
        {
            return View();

        }
      
        public ActionResult create_user()
        {
            return View();

        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        // ********************************************************************************************* //
        //                                   TABLA USUARIO 
        // ********************************************************************************************* //
        // METODO QUE ALMACENA EL NUMERO DE ROL PARA VERIFICAR LA AUTENTICACION
        [PermisosRol(1)]
        public ActionResult TablaUsuario()
        {
            ViewBag.Message = "Your Table User page.";

            return View(Neg.get_Usuarios());
        }

        //ActionResults para agregar Usuarios------
        // METODO QUE ALMACENA EL NUMERO DE ROL PARA VERIFICAR LA AUTENTICACION
        [PermisosRol(1)]
        [HttpGet]
        public ActionResult AgregarUsuario()
        {
            ViewBag.Message = "Your Add User page.";

            return View();
        }

        // METODO QUE ALMACENA EL NUMERO DE ROL PARA VERIFICAR LA AUTENTICACION
        [PermisosRol(1)]
        [HttpPost]
        public ActionResult AgregarUsuario(USUARIO user)
        {
            ViewBag.Message = "Your Add User page.";

            //Valida si el modelo es valido
            if (!ModelState.IsValid)
            {
                return View();
            }

            try {
                Neg.Insertar(user);
                return RedirectToAction("TablaUsuario");
            }
            catch (Exception ex) {
                ModelState.AddModelError("Error al agregar el Usuario", ex);
                return View();
            }
        }

        //-------------------------------------------

        //ActionResults para Deshabilitar Usuarios------
        // METODO QUE ALMACENA EL NUMERO DE ROL PARA VERIFICAR LA AUTENTICACION
        [PermisosRol(1)]
        [HttpGet]
        public ActionResult DeshabilitarUsuario(int id)
        {
            ViewBag.Message = "Your Disable User page.";

            try
            {
                return View(Neg.get_UserByID(id));
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        // METODO QUE ALMACENA EL NUMERO DE ROL PARA VERIFICAR LA AUTENTICACION
        [PermisosRol(1)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeshabilitarUsuario(USUARIO user)
        {
            ViewBag.Message = "Your Disable User page.";
            try
            {
                Neg.toDisable(user);

                return RedirectToAction("TablaUsuario");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //ActionResults para editar Usuarios------
        // METODO QUE ALMACENA EL NUMERO DE ROL PARA VERIFICAR LA AUTENTICACION
        [PermisosRol(1)]
        [HttpGet]
        public ActionResult EditarUsuario(int id)
        {
            ViewBag.Message = "Your Edit User page.";

            try
            {
                return View(Neg.get_UserByID(id));
            }
            catch (Exception ex) {
                throw;
            }
        }

        [HttpPost]

        // METODO QUE ALMACENA EL NUMERO DE ROL PARA VERIFICAR LA AUTENTICACION
        [PermisosRol(1)]
        [ValidateAntiForgeryToken]
        public ActionResult EditarUsuario(USUARIO user)
        {
            ViewBag.Message = "Your Edit User page.";

            try {
                Neg.edit_User(user);

                return RedirectToAction("TablaUsuario");
            }
            catch (Exception ex) 
            {
                throw;
            }

        }
        // ********************************************************************************************* //
        //                                   TABLA PRODUCTOS
        // ********************************************************************************************* //
        // METODO QUE ALMACENA EL NUMERO DE ROL PARA VERIFICAR LA AUTENTICACION
        [PermisosRol(1)]
        public ActionResult TablaProductos()
        {
            ViewBag.Message = "Your Table Producto page.";

            return View(Neg.get_Productos());

        }

        //ActionResults para agregar Productos------
        // METODO QUE ALMACENA EL NUMERO DE ROL PARA VERIFICAR LA AUTENTICACION
        [PermisosRol(1)]
        [HttpGet]
        public ActionResult AgregarProducto()
        {
            ViewBag.Message = "Your Add Product page.";

            return View();
        }

        // METODO QUE ALMACENA EL NUMERO DE ROL PARA VERIFICAR LA AUTENTICACION
        [PermisosRol(1)]
        [HttpPost]
        public ActionResult AgregarProducto(PRODUCTO pro)
        {
            ViewBag.Message = "Your Add Product page.";

            //Valida si el modelo es valido
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                pro.Fecha_Registro = DateTime.Now;
                Neg.InsertarProducto(pro);
                return RedirectToAction("TablaProductos");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error al agregar el Producto", ex);
                return View();
            }
        }

        //-------------------------------------------
        //ActionResults para editar Productos------
        // METODO QUE ALMACENA EL NUMERO DE ROL PARA VERIFICAR LA AUTENTICACION
        [PermisosRol(1)]
        [HttpGet]
        public ActionResult EditarProductos(int id)
        {
            ViewBag.Message = "Your Edit Product page.";

            try
            {
                return View(Neg.get_ProductoById(id));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // METODO QUE ALMACENA EL NUMERO DE ROL PARA VERIFICAR LA AUTENTICACION
        [PermisosRol(1)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarProductos(PRODUCTO pro)
        {
            ViewBag.Message = "Your Edit Product page.";

            try
            {
                Neg.edit_Productos(pro);

                return RedirectToAction("TablaProductos");
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        //-------------------------------------------

        //ActionResults para Deshabilitar Productos------
        // METODO QUE ALMACENA EL NUMERO DE ROL PARA VERIFICAR LA AUTENTICACION
        [PermisosRol(1)]
        [HttpGet]
        public ActionResult DeshabilitarProductos(int id)
        {
            ViewBag.Message = "Your Disable Product page.";

            try
            {
                return View(Neg.get_ProductoById(id));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // METODO QUE ALMACENA EL NUMERO DE ROL PARA VERIFICAR LA AUTENTICACION
        [PermisosRol(1)]
        public ActionResult DeshabilitarProductos(PRODUCTO pro)
        {
            ViewBag.Message = "Your Disable Product page.";
            try
            {
                Neg.toDisableProducto(pro);

                return RedirectToAction("TablaProductos");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //-------------------------------------------
        // ********************************************************************************************* //
        //                                   TABLA CATEGORIA
        // ********************************************************************************************* //

        // METODO QUE ALMACENA EL NUMERO DE ROL PARA VERIFICAR LA AUTENTICACION
        [PermisosRol(1)]
        public ActionResult TablaCategoria()
        {
            ViewBag.Message = "Your Table Categoria page.";

            return View(Neg.get_Categoria());

        }

        //ActionResults para agregar Categoria------
        // METODO QUE ALMACENA EL NUMERO DE ROL PARA VERIFICAR LA AUTENTICACION
        [PermisosRol(1)]
        [HttpGet]
        public ActionResult AgregarCategoria()
        {
            ViewBag.Message = "Your Add Categorie page.";

            return View();
        }

        [HttpPost]
        // METODO QUE ALMACENA EL NUMERO DE ROL PARA VERIFICAR LA AUTENTICACION
        [PermisosRol(1)]
        public ActionResult AgregarCategoria(CATEGORIA ctg)
        {
            ViewBag.Message = "Your Add Categorie page.";

            //Valida si el modelo es valido
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            { 
                Neg.InsertarCategoria(ctg);
                return RedirectToAction("TablaCategoria");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error al agregar el Categoria", ex);
                return View();
            }
        }
        //----------------------------------------------
        //ActionResults para editar Productos------

        // METODO QUE ALMACENA EL NUMERO DE ROL PARA VERIFICAR LA AUTENTICACION
        [PermisosRol(1)]
        [HttpGet]
        public ActionResult EditarCategoria(int id)
        {
            ViewBag.Message = "Your Edit Categorie page.";

            try
            {
                return View(Neg.get_CategoriaById(id));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        // METODO QUE ALMACENA EL NUMERO DE ROL PARA VERIFICAR LA AUTENTICACION
        [PermisosRol(1)]
        public ActionResult EditarCategoria(CATEGORIA ctg)
        {
            ViewBag.Message = "Your Edit Catehorie page.";

            try
            {
                Neg.edit_Categoria(ctg);

                return RedirectToAction("TablaCategoria");
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        // METODO QUE ALMACENA EL NUMERO DE ROL PARA VERIFICAR LA AUTENTICACION
        [PermisosRol(1)]
        public ActionResult DashBoard()
        {
            return View();
        }
        //-------------------------------------------

        //********************************************************************************************
        //-----------------------------------Vista de Reportes----------------------------------------
        //********************************************************************************************

        [PermisosRol(1)]
        public ActionResult TablaReporteView()
        {
            int IDUser = (int)Session["id_usuario"];
            try
            {
                var reportes = Neg.get_reportesProd();

                return View(reportes);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [PermisosRol(1)]

        public ActionResult EliminarReportes(int ID)
        {
            // METODO QUE ME ELIMINA LOS REPORES SEGUN EL ID

            Neg.EliminarR(ID);

            return RedirectToAction("TablaReporteView");
        }

    }
}