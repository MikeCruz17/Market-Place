using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace PruebaUnitaria
{
    [TestClass]
    public class UnitTest1
    {

        CapaNegocio.clase_negocio Neg = new CapaNegocio.clase_negocio();

        // VARIABLE DE LA TABLA USUARIO

        CapaEntidad.USUARIO user = new CapaEntidad.USUARIO();

        // PRUEBA DEL METODO INSERTAR DE LA CAPA NEGOCIO.
        [TestMethod]
        public void TestMethod1()
        {


            // DATOS A ENVIAR A LA BASE DE DATOS

            user.Nombre = "Leandro";
            user.Correo = "Leandro@gmail.com";
            user.Id_Rol = 1;
            user.Id_Estado = 1;
            user.Contrasena = "Leandro17";

            // EJECUCION DEL METODO DE INSERCION DE DATOS.

            Neg.Insertar(user);

        }

        // PRUEBA DE VERIFICACION DE CORREO ELECTRONICO.
        [TestMethod]
        public void TestMethod2()
        {
            // CORREO A VERIFICAR

            user.Correo = "Willis@gmail.com";

            // VARIABLE DEL METODO DE VERIFICACION DE CORREO.

            var Correo = Neg.Validar_correo(user.Correo);

            // ESTE COMANDO VERIDICA DE QUE CORREO NO RETORNE NULO.
            Assert.IsNotNull(Correo);
        }

        [TestMethod]
        public void TestMethod3()
        {
            // CREDENCIALES A VERIFICAR

            user.Correo = "felixcumbe@gmail.com";
            user.Contrasena = "Felix18";

            // VARIABLE DEL METODO DE VERIFICACION DE CORREO.

            var Credenciales = Neg.Login(user);

            // ESTE COMANDO VERIDICA DE QUE LA VARIABLE CREDENCIALES NO RETORNE NULO.
            Assert.IsNotNull(Credenciales);
        }


    }
}
