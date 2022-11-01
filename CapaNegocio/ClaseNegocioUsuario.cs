using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class ClaseNegocioUsuario
    {
        // VARIABLE DE LA CLASE DE LA CAPA DATOS
        ClaseDatoUsuario Dt = new ClaseDatoUsuario();

        // METODO QUE INGRESA UN PRODUCTO Y SE LO ENVIA A LA CLASE DATOS USUARIOS.

        public void InsertarProductos(PRODUCTO pro)
        {
            Dt.AñadirProducto(pro);
        }

        // ******************************************************************* //
        // ***********************- TABLA PRODUCTO -*************************** //
        // ******************************************************************* //

        // METODO QUE ME RETORNA LOS DATOS DE LA TABLA USUARIO

        public List<PRODUCTO> ListaProductos()
        {
            return Dt.ListaProductos();
        }
        //METODO QUE FILTRA POR CATEGORIA 
        public List<PRODUCTO> buscar_c(int categoria, string Busqueda)
        {
            return Dt.buscar_c(categoria, Busqueda);
        }
        //METODO QUE FILTRA POR NOMBRE
        public List<PRODUCTO> buscar_N(String NOMBRE)
        {
            return Dt.buscar_N(NOMBRE);
        }
        //METODO QUE FILTRA POR RANGO DE PRECIO
        public List<PRODUCTO> buscar_P(int PRECIO_INICIAL, int PRECIO_MAXIMO, string NombreP, int Categoria)
        {
            return Dt.buscar_P(PRECIO_INICIAL, PRECIO_MAXIMO, NombreP, Categoria);
        }

        // METODO QUE ME RETORN LOS DETALLES DEL PRODUCTO SEGUN EL ID.
        public List<PRODUCTO> detalles_producto(int ID)
        {
            return Dt.detalles_producto(ID);
        }

        // METODO QUE ME RETORNA LOS PRODUCTOS DEL USUARIO SOLICITADO A TRAVES DEL ID.
        public List<PRODUCTO> ListaProductoUsuario(int IDUser)
        {
            return Dt.ListaProductosUsuario(IDUser);
        }

        // METODO QUE ME RETORNA EL PRODUCTO DEL USUARIO SOLICITADO A TRAVES DEL IDPRODUCTO.
        public PRODUCTO ObtenerInformacionProducto(int IDProduct)
        {
            return Dt.ObtenerDatosProducto(IDProduct);
        }

        // METODO QUE ME ACTUALIZA LOS DATOS CAMBIADOS DEL PRODUCTO

        public void ActualizarProductoUsuario(PRODUCTO pro)
        {
            Dt.ActualizarProducto(pro);
        }

        // ******************************************************************* //
        // ***********************- TABLA CARRITO -*************************** //
        // ******************************************************************* //


        // METODO QUE ME INSERTA UN REGISTRO EN LA TABLA CARRITO.
        public void carrito(CARRITO CARRITO)
        {
             Dt.carrito(CARRITO);
        }

        // METODO QUE ME RETORNA UN PRODUCTO SEGUN SU ID.
        public List<CARRITO> optener_producto(int ID)
        {
            return Dt.optener_producto(ID);
        }
  
        // METODO QUE ME RETORNA LOS PRODUCTOS AGREGADOS A CARRITO SEGUN EL ID DEL USUARIO.
        public List<MostrarCarrito> MostrarCarritoU(int ID_Usuario)
        {
            return Dt.MostrarCarrito(ID_Usuario);
        }

        public List<PRODUCTO> ValidacionUsuarioProp(int UserID, int ProductID)
        {
            return Dt.ValidacionUsuarioPropetario(UserID, ProductID);
        }

        public List<CARRITO> ValidacionCarritoUsuario(int UserID, int ProductID)
        {
            return Dt.ValidacionProductosCarrito(UserID, ProductID);
        }

        // METODO QUE ME INSERTA UN PRODUCTO EN LA BD

        public void InsertarCarrito(CARRITO c)
        {
            Dt.AnadirCarrito(c);
        }
        
        // METODO QUE ME ELIMINA UN PRODUCTO DE MI CARRITO
        
        public void EliminarProdCarrito(CARRITO C)
        {
            Dt.EliminarProdCarrito(C);
        }

        // ******************************************************************* //
        // ***********************- TABLA USUARIO -*************************** //
        // ******************************************************************* //

        // METODO TIPO VALIDACION DE CUENTA QUE ME RETORNA LAS INFORMACIONES DEL 
        // USUARIO
        public List<USUARIO> DatosMiCuenta(int UserID)
        {
            return Dt.ValidacionCuenta(UserID);
        }

        // METODO QUE ME VALIDA SI LA CONTRASENA ES EXISTENTE O ES LA MISMA CONTRASENA
        // QUE LA EXISTENTE EN LA BASE DATOS, PARA EVITAR QUE SE GUARDE LA MISMA CONTRASENA
        // VIEJA DEL USUARIO

        public List<USUARIO> ValidacionContrasena(string Password)
        {
            return Dt.ValidacionPassword(Password);
        }

        // OBTENER EL ID DEL USUARIO
        public USUARIO ObtenerUsuario(int IdUser)
        {
            return Dt.get_UserById(IdUser);
        }

        // METODO QUE ME CAMBIA LA CONTRASENA DEL USUARIO.
        public void CambiarPassword(USUARIO user)
        {
            Dt.CambioContrasena(user);
        }
    }
}
