using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;


namespace CapaDatos
{

    public class ClaseDatoUsuario
    {
        // VARIABLE DE LA BD
        db_a83dd0_markeplaceEntities db = new db_a83dd0_markeplaceEntities();


        // ******************************************************************* //
        // ***********************- TABLA PRODUCTO -*************************** //
        // ******************************************************************* //


        // INGRESAR UN PRODUCTO A LA BASE DE DATOS

        public void AñadirProducto(PRODUCTO Pro)
        {
            db.PRODUCTOes.Add(Pro);
            db.SaveChanges();
        }

        // METODO QUE ME RETORNA TODOS LOS DATOS DE LA TABLA PRODUCTOS
        public List<PRODUCTO> ListaProductos()
        {
            var lista = (from d in db.PRODUCTOes
                         where(d.Estado == 1)
                        orderby d.ID ascending
                        select d).OrderByDescending(d => d.ID).Take(3).ToList();

         
            return lista;
        }
        //METODO QUE BUSCA EN LA BDA EL FILTRO POR CATEGORIA
        public List<PRODUCTO> buscar_c(int categoria, string Busqueda)
        {

            var datos = (from d in db.PRODUCTOes
                         where (d.Nombre.Contains(Busqueda) && d.ID_Categoria == categoria)
                         select d).ToList();

            return datos;



        }
        //METODO QUE BUSCA EN LA BDA EL FILTRO POR NOMBRE
        public List<PRODUCTO> buscar_N(String NOMBRE)
        {

            var datos = (from d in db.PRODUCTOes
                         where (d.Nombre.Contains(NOMBRE) && d.Estado != 2)
                         select d).ToList();

      
                return datos;
        



        }
        //METODO QUE BUSCA EN LA BDA EL FILTRO POR RANGO DE PRECIO 
        public List<PRODUCTO> buscar_P(int PRECIO_INICIAL, int PRECIO_MAXIMO, string Nombre, int categoria)
        {

            // CONDICION QUE SIGNIFICA QUE SI LA VARIABLE CATEGORIA ES MENOR O IGUAL A 0
            // ME HAGA DICHA CONSULTA EN LA BD Y LUEGO ME RETORNE LOS DATOS CORRESPONDIENTES.

            if (categoria <= 0)
            {
                var datos2 = (from d in db.PRODUCTOes
                             where (d.Precio >= PRECIO_INICIAL && d.Precio <= PRECIO_MAXIMO
                             && d.Nombre.Contains(Nombre))
                             select d).ToList();

                return datos2;
            }

            // SI LA PRIMERA CONDICION NO SE CUMPLE PUES SE HARA UNA CONSULTA CON TODOS
            // LOS PARAMETROS

            else
            {
                var datos = (from d in db.PRODUCTOes
                             where (d.Precio >= PRECIO_INICIAL && d.Precio <= PRECIO_MAXIMO
                             && d.Nombre.Contains(Nombre) && d.ID_Categoria == categoria)
                             select d).ToList();

                return datos;
            }

         

        }

        // METODO QUE ME RETORNA UNA LISTA SEGUN EL ID DEL PRODUCTO.
        public List<PRODUCTO> detalles_producto(int ID)
        {


            var datos = (from d in db.PRODUCTOes
                         where (d.ID == ID)
                         select d).ToList();

            return datos;


        }

        // METODO QUE ME RETORNA TODOS LOS DATOS DE LA TABLA PRODUCTOS SEGUN EL ID USUARIO.
        public List<PRODUCTO> ListaProductosUsuario(int IDUser)
        {
            var lista = (from d in db.PRODUCTOes
                         where (d.ID_USUARIO == IDUser && d.Estado != 2)
                         select d).ToList();


            return lista;
        }

        // METODO QUE ME RETORNA TODOS LOS DATOS DE LA TABLA PRODUCTOS SEGUN EL ID USUARIO.
        public PRODUCTO ObtenerDatosProducto(int IDProduct)
        {

            return (from d in db.PRODUCTOes
                         where d.ID == IDProduct
                         select d).FirstOrDefault();
           
        }

        // ACTUALIZAR DATOS DE UN PRODUCTO

        public void ActualizarProducto(PRODUCTO P)
        {
            var d = db.PRODUCTOes.Find(P.ID);
            d.Nombre = P.Nombre;
            d.Precio = P.Precio;
            d.ID_Categoria = P.ID_Categoria;
            d.Descripcion = P.Descripcion;
            d.Estado = P.Estado;
            d.Ubicacion = P.Ubicacion;

            if(P.imagen == null)
            {
                
                db.SaveChanges();
            }
            else
            {
                d.imagen = P.imagen;
                db.SaveChanges();
            }
           
           
        }

        // ******************************************************************* //
        // ***********************- TABLA CARRITO -*************************** //
        // ******************************************************************* //


        // METODO QUE ME INSERTA UN REGISTRO EN LA TABLA CARRITO.
        public void carrito(CARRITO CARRITO)
        {
            db.CARRITOes.Add(CARRITO);
            db.SaveChanges();

        }

        // LISTA QUE ME RETORNA UN PRODUCTO SEGUN EL ID.
        public List<CARRITO> optener_producto(int ID)
        {


            var datos = (from d in db.CARRITOes
                         where (d.ID_USUARIO == ID)
                         select d).ToList();

            return datos;

        }

        // METODO QUE ME RETORNA UNA LISTA DE PRODUCTOS EN EL CARRITO SEGUN EL ID DEL USUARIO.
        public List<MostrarCarrito> MostrarCarrito(int ID_Usuario)
        {

            var Datos = (from d in db.MostrarCarritoes
                          where (d.ID_USUARIO == ID_Usuario)
                          select d).ToList();
            return Datos;
        }

        // INGRESAR UN PRODUCTO EN CARRITO A LA BASE DE DATOS

        public void AnadirCarrito (CARRITO c)
        {

            db.CARRITOes.Add(c);
            db.SaveChanges();
        }

        // ************************************************************************************************ //
        // ***********************- VALIDACIONES DE LA VISTA DETALLES PRODUCTO -*************************** //
        // ************************************************************************************************ //


        // METODO QUE ME RETORNA UNA LISTA TIPO PRODUCTO.
        public List<PRODUCTO> ValidacionUsuarioPropetario(int UserID, int ProductID)
        {
            // ESTA VARIABLE ALMACENA LOS DATOS SEGUN EL CUMPLIMIENTO DE LOS PARAMETROS EN LA TABLA
            // RETORNARA UN VALOR SI SE CUMPLE, SINO RETORNARA EN NULO, VERIFICA SI EL IDUSER Y PRODUCTID
            // COINCIDEN. 

            var Datos = (from d in db.PRODUCTOes
                         where (d.ID_USUARIO == UserID && d.ID == ProductID)
                         select d).ToList();

            return Datos;
        }

        // METODO QUE ME RETORNA UNA LISTA TIPO CARRITO.
        public List<CARRITO> ValidacionProductosCarrito(int UserID, int ProductID)
        {

            // ESTA VARIABLE ALMACENA LOS DATOS SEGUN EL CUMPLIMIENTO DE LOS PARAMETROS EN LA TABLA
            // RETORNARA UN VALOR SI SE CUMPLE, SINO RETORNARA EN NULO, VERIFICA SI EL IDUSER Y PRODUCTID
            // COINCIDEN.

            var Datos = (from d in db.CARRITOes
                         where (d.ID_USUARIO == UserID && d.ID_PRODUCTO == ProductID)
                         select d).ToList();

            return Datos;
        }

        public void EliminarProdCarrito(CARRITO C)
        {

            var Datos = (from d in db.CARRITOes
                         where (d.ID == C.ID)
                         select d).Single();

            db.CARRITOes.Remove(Datos);
            db.SaveChanges();

        }

        // ******************************************************************* //
        // ***********************- TABLA USUARIO -*************************** //
        // ******************************************************************* //

        // METODO QUE ME RETORNA LA INFORMACION DEL IDUSUARIO SOLICITADO.
        public List<USUARIO> ValidacionCuenta(int UserID)
        {

            // ESTA VARIABLE ALMACENA LOS DATOS SEGUN EL CUMPLIMIENTO DE LOS PARAMETROS EN LA TABLA
            // RETORNARA UN VALOR SI SE CUMPLE, SINO RETORNARA EN NULO, VERIFICA SI EL IDUSER COINCIDE
            // CON ALGUNOS DATOS DE LA TABLA USUARIO.

            var Datos = (from d in db.USUARIOs
                         where (d.ID == UserID)
                         select d).ToList();

            return Datos;
        }

        // METODO DE VALIDACION DE CONTRASENA
        public List<USUARIO> ValidacionPassword(string Password)
        {

           // VALIDACION DE CONTRASENA

            var Datos = (from d in db.USUARIOs
                         where (d.Contrasena == Password)
                         select d).ToList();

            return Datos;
        }

        //METODO PARA OBTENER A UN USURIO POR SU ID
        public USUARIO get_UserById(int id)
        {
            USUARIO user = db.USUARIOs.Find(id);
            return user;
        }

        // METODO DE CAMBIO DE CONTRASENA
        public void CambioContrasena (USUARIO user)
        {
            USUARIO userEdit = get_UserById(user.ID);
            userEdit.Contrasena = user.Contrasena;

            db.SaveChanges();

        }
    }
}
