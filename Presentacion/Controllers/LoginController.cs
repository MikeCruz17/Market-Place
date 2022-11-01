using CapaEntidad;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace Presentacion.Controllers
{
    public class LoginController : Controller
    {

        clase_negocio Neg = new clase_negocio();


        // ACTION RESULT QUE ME PERMITE INICIAR SESION.

        public ActionResult login()
        {
            return View();
        }

        // METODO QUE ME REALIZA UN SERIE DE VERFIFICACIONES ANTES DE DAR PASO AL INICIO DE SESION.
        // TAMBIEN ME ENVIA LOS DATOS DE LA VISTA.

        [HttpPost]
        public ActionResult Login(USUARIO usuario)
        {
            // VARIABLE USUARIO QUE ME RETORNA EL USUARIO.
            var user = Neg.Login(usuario);

            // CONDICIONAL QUE VERIFICA SI EL USUARIO ES NULO.
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.Correo, false);

                // VARIABLE QUE ME GUARDA LOS DATOS DEL USUARIO.
                Session["usuario"] = user;

                //session para optener el id del usuario
                Session["id_usuario"] = user.ID;

                // SESSION QUE ME GUARDA EL NOMBRE DEL USUARIO

                Session["NombreU"] = user.Nombre;

                // SESION QUE ME GUARDA EL ROL DEL USUARIO

                Session["ID_ROL"] = user.Id_Rol;


                // CONDICIONAL ANIDADA QUE ME VERIFICA EL TIPO DE USUARIO Y RETORNA A LA VISTA QUE LE CORRESPONDE.
                if (user.Id_Rol == 2)
                {
                    return RedirectToAction("MostrarProductos1", "Usuario");

                }
                else if (user.Id_Rol == 1)
                {

                    return RedirectToAction("DashBoard", "Home");
                }

            }

            // CONDICIONAL QUE SIGINIFICA QUE SI LA BD DEVUELVE UN USUARIO NULO O LAS CREDENCIALES NO CONCUERDAN
            // ENTRE SI, PUES ME LANZARÁ UN MENSAJE DE ERROR EN LA PAGINA.

            else if (user == null)
            {
                // ESTE COMANDO ME PERMITE LIMPIAR LOS INPUTS DE LA PÁGINA
                ModelState.Clear();

                // MENSAJE DE ERROR
                ViewBag.Error = "Contraseña o Correo invalido";

            }

            return View();


        }

        // ACTION RESULT QUE ME PERMITE CREAR UNA CUENTA DESDE LA PAGINA.

        public ActionResult WebPage1()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        [HttpPost]
        public ActionResult WebPage1(USUARIO user)
        {
            //validacion para no repeticion de correo

            var correo = Neg.Validar_correo(user.Correo);

            //condicion para validar la no repeticion de correo
            if (correo == null)
            {
                // VALORES POR PREDETERMINADO AL INSERTAR LOS DATOS EN LA PAGINA DE REGISTRO.

                user.Id_Estado = 1;
                user.Id_Rol = 2;


                // METODO DE LA CAPA NEGOCIO QUE ME INSERTA LOS DATOS A LA BASE DE DATOS.

                Neg.Insertar(user);


                // ESTE COMANDO ME PERMITE LIMPIAR LOS INPUTS DE LA PÁGINA
                ModelState.Clear();


            }
            else
            {
                // ESTE COMANDO ME PERMITE LIMPIAR LOS INPUTS DE LA PÁGINA
                ModelState.Clear();

                // MENSAJE DE ERROR
                ViewBag.Error = "Este correo ya esta en uso";

            }


            return View();
        }

        public ActionResult CerrarSesion()
        {
            FormsAuthentication.SignOut();
            Session["usuario"] = null;
            return RedirectToAction("login", "login");

        }




        // GET: Login/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Login/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Login/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Login/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Login/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
