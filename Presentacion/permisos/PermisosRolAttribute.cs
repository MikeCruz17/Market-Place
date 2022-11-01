using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidad;
using Presentacion.Controllers;

namespace Presentacion.permisos
{
    public class PermisosRolAttribute : ActionFilterAttribute
    {
        // VARIABLE TIPO ROL QUE ALMACENARA LOS DATOS INGRESADOS A TRAVES DEL METODO.
        private int ROL;

        // VARIABLE TIPO USUARIP
        private USUARIO user;

        // CONSTRUCTOR
        public PermisosRolAttribute()
        {
        }

        // METODO QUE ME TRAE POR PARAMETRO EL IDROL
        public PermisosRolAttribute(int _idrol)
        {
            ROL = _idrol;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // LA VARIABLE TIPO USUARIO QUE ALMACENA LOS QUE SE ENCUENTRA EN LA SESSION DEL CONTROLADOR LOGIN
            user = (USUARIO)HttpContext.Current.Session["usuario"];
            USUARIO usuario = HttpContext.Current.Session["usuario"] as USUARIO;

            // SI EL USUARIO ES NULO
            if (user == null)
            {

                if ((filterContext.Controller is LoginController) == false)
                {
                    // RETORNO A LA VISTA LOGIN
                    filterContext.Result = new RedirectResult("/Login/Login");

                }

                base.OnActionExecuting(filterContext);

            }

            // SI EL ROL QUE RECIBE EL METODO ES IGUAL A 3 PUES PUEDE ACCEDER TANTO UN
            // USUARIO ADMIN COMO UN VENDEDOR/COMPRADOR
            else if (ROL == 3)
            {
                // CEDER EL PASO A LA VISTA
                if ((filterContext.Controller is LoginController) == true)
                {

                }

            }


            // SI EL USUARIO NO ES NULO PERO TIENE UN ROL DIFERENTE EN LA VISTA QUE QUIERE ACCEDER
            // ESTO PREVIENE LA ENTRADA A UNA VISTA QUE NO LE CORRESPONDE SEGUN SU ROL DE USUARIO.
            else if (usuario.Id_Rol != ROL)
            {

                if ((filterContext.Controller is LoginController) == false)
                {
                    // LIMPIAR LA SESSSION
                    HttpContext.Current.Session["usuario"] = null;

                    // RETORNO A LA VISTA LOGIN.
                    filterContext.Result = new RedirectResult("/Login/Login");

                }

            }

        }
    }
}