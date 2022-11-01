using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaNegocio;
using CapaEntidad;
using System.Web.Security;
using Presentacion.permisos;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Web.Helpers;
using System.Globalization;


namespace Presentacion.Controllers
{
    public class UsuarioController : Controller
    {
        // VARIABLE DE LA CAPA NEGOCIO
        ClaseNegocioUsuario Neg = new ClaseNegocioUsuario();
        clase_negocio NegHome = new clase_negocio();

        // NUMERO DE ROL PARA LA AUTENTICACION DEL FILTRO.
        [PermisosRol(2)]

        // AGREGAR UN PRODUCTO A LA BD.

        public ActionResult AgregarProducto1()
        {
            if (Session["Producto"] != null)
            {
                var Pro = (PRODUCTO)Session["Producto"];
                TempData["Foto"] = Pro.imagen;
                TempData["ID"] = Pro.ID_Categoria;

                return View(Pro);
            }
            else
            {

                return View();


            }

        }


// ********************************************************************************************* //
// ********************************************************************************************* //


        // NUMERO DE ROL PARA LA AUTENTICACION DEL FILTRO.
        [PermisosRol(2)]
        [HttpPost]
        public ActionResult AgregarProducto1(PRODUCTO Pro, string Opcion)
        {
            if (Opcion == "NA")
            {
                // CONFIRMACION DE LA ACCION.
                TempData["Confirmacion3"] = "Content";

                // OBTENCION DE ARCHIVO DE LA WEB
                HttpPostedFileBase FileB = Request.Files[0];

                WebImage imagen = new WebImage(FileB.InputStream);

                // OBTENIENDO LOS BYTES DE LA IMAGEN PARA ALMACENARLOS EN LA BD
                Pro.imagen = imagen.GetBytes();

                // ALMACENAR PRODUCTOS DE FORMA TEMPORAL EN UNA SESSION
                Session["Producto"] = Pro;

                // RETORNAR A LA VISTA CORRESPONDIENTE.
                return RedirectToAction("AgregarProducto1", "Usuario");
            }
            else if (Opcion == "Si")
            {

                // DATOS

                var Prod = (PRODUCTO)Session["Producto"];


                // CONFIRMACION DE PETICION

                TempData["ConfirmacionPr"] = "Content";

                // VALORES PREDETERMINADOS

                Prod.ID_USUARIO = (int)Session["id_usuario"];
                Prod.Fecha_Registro = DateTime.Today;
                Prod.Estado = 1;

                // METODO QUE ME INSERTA LOS DATOS EN LA TABLA PRODUCTOS.

                Neg.InsertarProductos(Prod);

                // LIMPIAR SESSION
                Session["Producto"] = "";

                // RETORNAR A LA VISTA CORRESPONDIENTE.
                return RedirectToAction("ListadoProductosUsuario", "Usuario");
            }
            return View();

        }


// ********************************************************************************************* //
// ********************************************************************************************* //


        // NUMERO DE ROL PARA LA AUTENTICACION DEL FILTRO.
        [PermisosRol(2)]

        // MOSTRAR CONTENIDO DE LA TABLA PRODUCTOS

        public ActionResult MostrarProductos1()
        {

            return View(Neg.ListaProductos());
        }


// ********************************************************************************************* //
// ********************************************************************************************* //

        // NUMERO DE ROL PARA LA AUTENTICACION DEL FILTRO.
        [PermisosRol(2)]
        [HttpPost]
        //ActionResult QUE BUSCA POR NOMBRE 
        public ActionResult buscar_N(String NOMBRE)
        {
            // VARIABLE TIPO SESSION QUE ME ALMACENARA LA PALABRA DEL BUSCADOR.
            Session["Busqueda"] = NOMBRE;

            // VARIABLE QUE ME RETORNA SEGUN LA BUSQUEDA.
            var categoria2 = Neg.buscar_N(NOMBRE);

            return View(categoria2);
        }

// ********************************************************************************************* //
// ********************************************************************************************* //


        // NUMERO DE ROL PARA LA AUTENTICACION DEL FILTRO.
        [PermisosRol(2)]
        [HttpPost]
        //ActionResult QUE BUSCA POR CATEGORIA 
        public ActionResult buscar_c(int Categoria)
        {
            // VARIABLE SESSION COVERTIDA A STRING PARA MOSTRAR LA PALABRA 
            // BUSCADA EN EL BUSCADOR.

            string Producto = (string)Session["Busqueda"];

            // VARIABLE SESSION QUE ME ALMACENA LA CATEGORIA
            Session["Categoria"] = Categoria;

            // VARIABLE QUE ALMACENA UN METODO QUE RETORNA SEGUN
            // LOS PARAMETROS Y HACE LA CONSULTA A TRAVES DE LINQ EN LA BD.
            var RCategoria = Neg.buscar_c(Categoria, Producto);


            return View(RCategoria);
        }


// ********************************************************************************************* //
// ********************************************************************************************* //


        // NUMERO DE ROL PARA LA AUTENTICACION DEL FILTRO.
        [PermisosRol(2)]
        [HttpPost]
        //ActionResult QUE BUSCA POR RANGO DE PRECIO
        public ActionResult buscar_P(int PrecioMin, int PrecioMax)
        {
            // VARIABLE QUE TIENE UN METODO QUE RETORNA SEGUN 
            // LAS CONDICIONES CUMPLIDAS A TRAVES DE LOS PARAMETROS

            string Producto = (string)Session["Busqueda"];

            if (PrecioMax < PrecioMin)
            {
                int Categoria = 0;
                var categoria2 = Neg.buscar_P(PrecioMin, PrecioMax, Producto, Categoria);

                ViewBag.ErrorP = "El precio máximo es menor que el mínimo. Favor ingresar " +
                    "los valores nuevamente.";
                return View(categoria2);
            }
            else
            {
                // CONDICIONAL QUE ME REALIZA LAS ACCIONES CORRESPONDIENTES SEGUN LOS RESULTADOS
                // OBTENIDOS

                // SI LA SESSION ES IGUAL A CERO
                if ((int)Session["Categoria"] <= 0)
                {
                    int Categoria = 0;
                    var categoria2 = Neg.buscar_P(PrecioMin, PrecioMax, Producto, Categoria);
                    return View(categoria2);
                }

                // DE LO CONTRARIO
                else
                {
                    int Categoria = (int)Session["Categoria"];
                    var categoria2 = Neg.buscar_P(PrecioMin, PrecioMax, Producto, Categoria);
                    return View(categoria2);
                }


            }



        }


// ********************************************************************************************* //
// ********************************************************************************************* //


        // NUMERO DE ROL PARA LA AUTENTICACION DEL FILTRO.
        [PermisosRol(2)]
        [HttpPost]
        public ActionResult PDetalles(int ID_Producto)
        {
            // USUARIO ESTATICO
            int IDUser = (int)Session["id_usuario"];

            //Session para conocer el id del producto al reportar

            Session["Id_Producto"] = ID_Producto;

            // CONDICIONAL DE LA VARIABLE PRODUCTO, CONDICION QUE ASIGNA EL VALOR DE LA VARIABLE SESSION.
            if (ID_Producto <= 0)
            {
                ID_Producto = (int)Session["IDP"];
            }

            // VALIDACIONES QUE DEVUELVEN UN VALOR DE LA BD.
            var ValidacionProductos = Neg.ValidacionUsuarioProp(IDUser, ID_Producto);
            var ValidacionProductosCarrito = Neg.ValidacionCarritoUsuario(IDUser, ID_Producto);

            // SI EL CONTADOR MARCA QUE ES MAYOR A CERO.
            if (ValidacionProductos.Count() > 0)
            {
                // VIEWBAG QUE ME ALMACENA LOS DATOS DE LAS VARIABLES DE VALIDACION.
                ViewBag.UsuarioPropietario = ValidacionProductos;
            }

            // SI EL CONTADOR MARCA QUE ES MAYOR A CERO.
            else if (ValidacionProductosCarrito.Count() > 0)
            {
                // VIEWBAG QUE ME ALMACENA LOS DATOS DE LAS VARIABLES DE VALIDACION.
                ViewBag.ProductoCarrito = ValidacionProductosCarrito;
            }


            // VARIABLE QUE ME RETORNA EL PRODUCTO SEGUN EL ID.
            var detalles = Neg.detalles_producto(ID_Producto);

            return View(detalles);
        }


// ********************************************************************************************* //
// ********************************************************************************************* //


        // NUMERO DE ROL PARA LA AUTENTICACION DEL FILTRO.
        [PermisosRol(2)]

        // AÑADIR A CARRITO

        public ActionResult carrito(CARRITO CARRITO)
        {
            // METODO QUE ME INSERTA LOS DATOS DEL CARRITO
            Neg.carrito(CARRITO);

            return View();
        }


// ********************************************************************************************* //
// ********************************************************************************************* //


        // NUMERO DE ROL PARA LA AUTENTICACION DEL FILTRO.
        [PermisosRol(2)]

        // MOSTRAR CARRITO DE UN USUARIO.

        public ActionResult ver_carrito()
        {
            // ID USUARIO
            int IDUsuario = (int)Session["id_usuario"];

            // VARIABLE QUE ME RETORNA UNA LISTA SEGUN LOS PRODUCTOS EN CARRITO DEL USUARIO
            var Carrito = Neg.MostrarCarritoU(IDUsuario);

            return View(Carrito);
        }


// ********************************************************************************************* //
// ********************************************************************************************* //


        // NUMERO DE ROL PARA LA AUTENTICACION DEL FILTRO.
        [PermisosRol(2)]
        // MOSTRAR EL CARRITO SEGUN EL ID DEL USUARIO, MAS ADELANTE SE HARA CON UNA VARIABLE SESSION
        [HttpPost]
        public ActionResult ver_carrito(int NConfirmacion, int IDProducto)
        {
            // ID USUARIO
            int IDUsuario = (int)Session["id_usuario"];

            // SI ESTE NUMERO ES IGUAL O MENOR A CERO ME RETORNARA Y ALMACENARA EL ID DEL PRODUCTO SEGUN DONDE PULSE
            // CERO SIGNIFICA QUE NO SE VA A AGREGAR UN PRODUCTO
            if (NConfirmacion <= 0)
            {
                // VARIABLE QUE ME RETORNA UNA LISTA SEGUN LOS PRODUCTOS EN CARRITO DEL USUARIO
                var Carrito = Neg.MostrarCarritoU(IDUsuario);

                // VARIABLE TIPO SESSION QUE ME GUARDA EL IDPRODUCTO DE MANERA DINAMICA.
                Session["IDP"] = IDProducto;
                return View(Carrito);
            }

            // OBJETO TIPO TABLA CARRITO DE LA BD.
            CARRITO c = new CARRITO();

            // GUARDANDO EL ID DEL PRODUCTO SELECCIONADO PARA VALIDARLO DESPUES AL MOMENTO DE PULSAR EL BOTON DE ATRAS
            // DE LA VISTA VERCARRITO
            Session["IDP"] = IDProducto;

            // ALMACENAR ID ESTATICO
            var IDP = (int)Session["IDP"];

            // VALIDACION DE PRODUCTO EXISTENTE O NO EN LA TABLA CARRITO DEL USUARIO.
            var ValidacionProductosCarrito = Neg.ValidacionCarritoUsuario(IDUsuario, IDP);

            // SI EL CONTADOR MARCA QUE MENOR A CERO, PUES ME INSERTARA LOS DATOS.
            if (ValidacionProductosCarrito.Count() <= 0)
            {
                // ASIGNACION DE VALORES
                c.ID_PRODUCTO = IDProducto;
                c.ID_USUARIO = IDUsuario;

                // INSERCION DE DATOS A TRAVES DEL METODO INSETAR CARRITO
                Neg.InsertarCarrito(c);

            }

            // VARIABLE QUE ME RETORNA UNA LISTA SEGUN LOS PRODUCTOS EN CARRITO DEL USUARIO
            var Carrito1 = Neg.MostrarCarritoU(IDUsuario);

            return View(Carrito1);

        }

// ********************************************************************************************* //
// ********************************************************************************************* //

        // NUMERO DE ROL PARA LA AUTENTICACION DEL FILTRO.
        [PermisosRol(2)]
        // ACTIONRESULT QUE ME ELIMINA UN PRODUCTO DE MI CARRITO
        [HttpPost]
        public ActionResult EliminarProductoCarrito(int ID, string Opcion)
        {
            // CREACION DE VARIABLE TIPO CARRITO
            CARRITO C = new CARRITO();

            // ASIGNACION DE DATOS A LA VARIABLA TIPO CARRITO
            C.ID = ID;

            // OPCION PREDETERMINADA PARA LLAMAR LA ALERTA.

            if (Opcion == "NA")
            {
                TempData["Confirmacion"] = "Content";
                return RedirectToAction("ver_carrito", "Usuario");
            }

            // SI EL USUARIO CONFIRMA LA OPERACION PUES SE EJECUTARA
            // Y, LE MOSTRARA UN MENSAJE A TRAVES DEL TEMPDATA DE QUE
            // LA OPERACION FUE EXITOSA.

            else if (Opcion == "Si")
            {
                // METODO QUE ME ELIMINA EL PRODUCTO DEL CARRITO SEGUN EL USUARIO Y PRODUCTO.
                Neg.EliminarProdCarrito(C);
                TempData["Confirmacion2"] = "Content";
                return RedirectToAction("ver_carrito", "Usuario");

            }

            return RedirectToAction("ver_carrito", "Usuario");

        }


// ********************************************************************************************* //
// ********************************************************************************************* //


        // NUMERO DE ROL PARA LA AUTENTICACION DEL FILTRO.
        [PermisosRol(3)]

        // ACTIONRESULT CON LA VISTA MI CUENTA

        public ActionResult MiCuenta()
        {
            int IDUser = (int)Session["id_usuario"];

            var informacion = Neg.DatosMiCuenta(IDUser);


            return View(informacion);
        }



// ********************************************************************************************* //
// ********************************************************************************************* //


        // NUMERO DE ROL PARA LA AUTENTICACION DEL FILTRO.
        [PermisosRol(3)]

        // ACTIONRESULT QUE ME HACE LAS VALIDACIONES PARA CREAR UNA CONTRASENA Y LUEGO REALIZAR LAS ACCIONES 
        // CORRESPONDIENTES.

        [HttpPost]
        public ActionResult MiCuenta(string Password)
        {
            int IDUser = (int)Session["id_usuario"];

            // VALIDACION DE ID DE LA CUENTA
            var informacion = Neg.DatosMiCuenta(IDUser);

            // VALIDACION DE CONTRASENA PARA EVITAR GUARDAR LA MISMA

            var VPassword = Neg.ValidacionContrasena(Password);

            if (VPassword.Count() <= 0)
            {
                // METODO QUE ME RETORNA EL USUARIO SEGUN EL ID

                var Usuario = Neg.ObtenerUsuario(IDUser);

                // ASIGNACION DE VALORES 

                Usuario.Contrasena = Password;

                // INSERCION DE CAMBIOS 

                Neg.CambiarPassword(Usuario);

                // MENSAJE

                ViewBag.Validacion = "Se ha guardado exitosamente la contraseña.";

                // METODO DE CAMBIO DE CONTRASENA

                return View(informacion);
            }

            else
            {
                ViewBag.Error = "Esta contraseña ya ha sido registrada. Por favor, haga el cambio nuevamente.";

                return View(informacion);
            }


        }


 // ********************************************************************************************* //
// ********************************************************************************************* //


        // NUMERO DE ROL PARA LA AUTENTICACION DEL FILTRO.
        [PermisosRol(2)]
        // ACTIONRESULT QUE ME BUSCA EL LISTADO DE UN USUARIO.

        public ActionResult ListadoProductosUsuario()
        {
            // VARIABLE QUE ME ALMACENA EL ID DEL USUARIO A BUSCAR EN LA BD.
            int IDUser = (int)Session["id_usuario"];

            // LISTA DE PRODUCTOS DEL USUARIO.
            var Productos = Neg.ListaProductoUsuario(IDUser);

            return View(Productos);
        }


// ********************************************************************************************* //
// ********************************************************************************************* //


        // NUMERO DE ROL PARA LA AUTENTICACION DEL FILTRO.
        [PermisosRol(2)]
        // ACTIONRESULT QUE ME BUSCA EL LISTADO DE UN USUARIO.

        public ActionResult EditarProductoUsuario(int ID_Producto)
        {
            // SI EL ID ES IGUAL A CERO PUES SE HARAN LAS OPERACIONES CORRESPONDIENTES
            if (ID_Producto != 0)
            {
                // VARIABLE QUE ME ALMACENA LA INFORMACION DEL ID PEDIDO
                var Producto = Neg.ObtenerInformacionProducto(ID_Producto);

                // VARIABLE QUE ME ALMACENA DE FORMA TEMPORAL LA IMAGEN ACTUAL.
                TempData["Imagen"] = Producto.imagen;

                // RETORNAR LOS DATOS OBTENIDOS A LA VISTA.
                return View(Producto);
            }
            else
            {
                // VARIABLE QUE ALMACENA LOS DATOS GUARDADOS EN LA VISTA.
                var Producto = Session["DatosGuardados"];

                // RETORNAR LOS DATOS OBTENIDOS A LA VISTA.
                return View(Producto);
            }

        }


// ********************************************************************************************* //
// ********************************************************************************************* //


        // NUMERO DE ROL PARA LA AUTENTICACION DEL FILTRO.
        [PermisosRol(2)]
        // ACTIONRESULT QUE ACTUALIZA LOS DATOS DE UN PRODUCTO
        [HttpPost]
        public ActionResult ActualizarProductos(PRODUCTO P, string Opcion)
        {
            // OBTENCION DE ARCHIVO DE LA WEB
            HttpPostedFileBase FileB = Request.Files[0];

            // ALERTA DE CONFIRMACION

            if (Opcion == "NA")
            {
                // VARIABLE QUE ME HABILITA LA ALERTA DE CONFIRMACION EN LA VISTA EDITARPRODUCTOUSUARIO
                TempData["ConfirmacionCont"] = "Content";

                // SI NO SE ENVIA NINGUNA IMAGEN ME ALMACENARA LOS DATOS ACTUALES.
                if (FileB.ContentLength <= 0)
                {
                    Session["DatosGuardados"] = P;
                }

                // SI DETECTA UN ARCHIVO, ME ALMACENARA EN BYTES LA IMAGEN ENVIADA
                else
                {
                    // OBTENER LA IMAGEN
                    WebImage imagen = new WebImage(FileB.InputStream);

                    // OBTENIENDO LOS BYTES DE LA IMAGEN PARA ALMACENARLOS EN LA BD
                    P.imagen = imagen.GetBytes();

                    // SESION QUE ME GUARDA LOS DATOS ENVIADOS DE MANERA TEMPORAL
                    Session["DatosGuardados"] = P;
                }

                // RETORNA VISTA Y ENVIANDO UN DATO
                return RedirectToAction("EditarProductoUsuario", "Usuario", new { ID_Producto = 0 });
            }

            else if (Opcion == "Si")
            {

                // CONVERTIR VARIABLE EN PRODUCTO
                var Productos = (PRODUCTO)Session["DatosGuardados"];

                // METODO QUE ME ACTUALIZAN LOS DATOS ENVIADOS
                Neg.ActualizarProductoUsuario(Productos);

                // CONFIRMACION DE OPERACION DE EXITOSA
                TempData["ConfirmacionPr"] = "Content";

                // RETORNAR VISTA
                return RedirectToAction("ListadoProductosUsuario", "Usuario");
            }

            // RETORNAR A LA VISTA CORRESPONDIENTE.
            return RedirectToAction("ListadoProductosUsuario", "Usuario");

        }


// ********************************************************************************************* //
// ********************************************************************************************* //


        // NUMERO DE ROL PARA LA AUTENTICACION DEL FILTRO.
        [PermisosRol(2)]
        // ACTIONRESULT QUE DESHABILITA UN PRODUCTO
        [HttpPost]
        public ActionResult EliminarProducto(int ID_Producto, string Opcion)
        {

            // OPCION PREDETERMINADA PARA LLAMAR LA ALERTA.
            if (Opcion == "NA")
            {
                // CONFIRMACION DE LA ACCION.
                TempData["ConfirmacionP"] = "Content";

                // RETORNAR A LA VISTA CORRESPONDIENTE.
                return RedirectToAction("ListadoProductosUsuario", "Usuario");
            }

            // SI EL USUARIO CONFIRMA LA OPERACION PUES SE EJECUTARA
            // Y, LE MOSTRARA UN MENSAJE A TRAVES DEL TEMPDATA DE QUE
            // LA OPERACION FUE EXITOSA.

            else if (Opcion == "Si")
            {
                // DECLARACION DE VARIABLE QUE ME RETORNA LOS DETALLES DE UN PRODUCTO
                var Producto = Neg.ObtenerInformacionProducto(ID_Producto);

                // METODO QUE ME ELIMINA EL PRODUCTO DEL CARRITO SEGUN EL USUARIO Y PRODUCTO.
                // DESHABILITAR PRODUCTO
                Producto.Estado = 2;

                // METODO QUE ME ACTUALIZAN LOS DATOS ENVIADOS
                Neg.ActualizarProductoUsuario(Producto);
                TempData["ConfirmacionPr"] = "Content";

                // RETORNAR A LA VISTA CORRESPONDIENTE.
                return RedirectToAction("ListadoProductosUsuario", "Usuario");

            }

            // RETORNAR A LA VISTA CORRESPONDIENTE.
            return RedirectToAction("ListadoProductosUsuario", "Usuario");
        }

//********************************************************************************************
//-----------------------------------Vista de Reportes----------------------------------------
//********************************************************************************************
    
        [PermisosRol(2)]
        [HttpGet]
        public ActionResult Reporte()
        {
            int IDUser = (int)Session["id_usuario"];
            int idProduct = (int)Session["Id_Producto"];
            try
            {
                return View(NegHome.get_ReportData(idProduct,IDUser));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [PermisosRol(2)]
        [HttpPost]
        public ActionResult Reporte(reporte_usuario reportData)
        {
            int IDUser = (int)Session["id_usuario"];
            //Sessiones para obtener los ids para crea un reporte (vista Reporte)
            reportData.ID_usuario_reportado = (int)Session["Id_usuario_reportado"];
            reportData.ID_usuario_reporte = (int)Session["Id_usario_reporte"];
            reportData.ID_publicacion = (int)Session["Id_publicacion"];
            var report = reportData;

            if (report != null) 
            {
                NegHome.InsertarReporte(report);
            }

            return RedirectToAction("MostrarProductos1", "Usuario");
        }

        [PermisosRol(2)]
        [HttpGet]
        public ActionResult Contacto()
        {
            int IDUser = (int)Session["id_usuario"];
            int idProduct = (int)Session["Id_Producto"];
            try
            {
                return View(NegHome.get_Contact(idProduct));
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}