using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
namespace CapaDatos
{
    public class ClaseDato
    {
        db_a83dd0_markeplaceEntities db = new db_a83dd0_markeplaceEntities();

        // METODO QUE INSERTA O AGREGA NUEVOS USUARIOS
        public void añadir_USUARIOS(USUARIO user)
        {
            db.USUARIOs.Add(user);
            db.SaveChanges();
        }

        // METODO TIPO USUARIO QUE ME RETORNA EL CORREO ELECTRONICO SI ES EXISTENTE.
        public USUARIO validar_correo(String correo)
        {
            return (from user in db.USUARIOs
                    where user.Correo == correo
                    select user).FirstOrDefault();
        }
        public USUARIO login(USUARIO ususario)
        {
            var oUser = (from d in db.USUARIOs
                         where d.Correo == ususario.Correo && d.Contrasena == ususario.Contrasena
                         select d).FirstOrDefault();



            return (oUser);

        }

        //METODO PARA OBTENER A TODOS LOS USUARIOS HABILITADOS DE LA BASE DE DATOS

        public List<USUARIO> get_Usuarios()
        {
            List<USUARIO> lista;

            var userList = (from d in db.USUARIOs
                            where d.Id_Estado == 1
                            select d).ToList();

            //lista = db.USUARIOS.ToList();
            lista = userList;

            return lista;
        }

        //METODO PARA OBTENER A UN USURIO POR SU ID
        public USUARIO get_UserById(int id)
        {
            USUARIO user = db.USUARIOs.Find(id);
            return user;
        }

        //METODO PARA EDITAR USUARIO
        public void edit_User(USUARIO user)
        {
            USUARIO userEdit = get_UserById(user.ID);
            userEdit.Nombre = user.Nombre;
            userEdit.Correo = user.Correo;
            userEdit.Id_Rol = user.Id_Rol;
            userEdit.Id_Estado = user.Id_Estado;
            userEdit.Contrasena = user.Contrasena;
            userEdit.TELEFONO = user.TELEFONO;

            db.SaveChanges();
        }

        //METODO PARA DESHABILITAR USUARIOS
        public void toDisable(USUARIO user)
        {
            USUARIO userDisable = get_UserById(user.ID);
            userDisable.Id_Estado = user.Id_Estado;

            db.SaveChanges();
        }
        //---------------------------------------------------
        //******************************************************************************
        //                    TABLA PRODUCTO
        //*******************************************************************************

        //METODO PARA OBTENER A TODOS LOS PRODUCTOS HABILITADOS DE LA BASE DE DATOS

        public List<PRODUCTO> get_Productos()
        {
            List<PRODUCTO> lista;

            var productList = (from d in db.PRODUCTOes
                               where d.Estado == 1
                               select d).ToList();

            //lista = db.PRODUCTOS.ToList();
            lista = productList;

            return lista;
        }

        //METODO PARA OBTENER A UN PRODUCTO POR SU ID
        public PRODUCTO get_ProductoById(int id)
        {
            PRODUCTO product = db.PRODUCTOes.Find(id);
            return product;
        }

        //METODO PARA EDITAR PRODUCTOS
        public void edit_Productos(PRODUCTO pro)
        {
            PRODUCTO proEdit = get_ProductoById(pro.ID);
            proEdit.Nombre = pro.Nombre;
            proEdit.ID_USUARIO = pro.ID_USUARIO;
            proEdit.Precio = pro.Precio;
            proEdit.Descripcion = pro.Descripcion;
            proEdit.Estado = pro.Estado;
            proEdit.Ubicacion = pro.Ubicacion;

            db.SaveChanges();
        }

        //METODO PARA DESHABILITAR USUARIOS
        public void toDisableProducto(PRODUCTO pro)
        {
            PRODUCTO proDisable = get_ProductoById(pro.ID);
            proDisable.Estado = pro.Estado;

            db.SaveChanges();
        }
        //---------------------------------------------------

        // METODO QUE INSERTA O AGREGA NUEVOS USUARIOS
        public void añadir_PRODUCTOS(PRODUCTO pro)
        {
            db.PRODUCTOes.Add(pro);
            db.SaveChanges();
        }

        //---------------------------------------------------
        //******************************************************************************
        //                    TABLA CATEGORIA
        //*******************************************************************************

        //METODO PARA OBTENER A TODOS LAS CATEGORIAS

        public List<CATEGORIA> get_Categoria()
        {
            List<CATEGORIA> lista;

            lista = db.CATEGORIAs.ToList();

            return lista;
        }

        //METODO PARA OBTENER A UNA CATEGORIA POR SU ID
        public CATEGORIA get_CategoriaById(int id)
        {
            CATEGORIA categoria = db.CATEGORIAs.Find(id);
            return categoria;
        }

        //METODO PARA EDITAR CATEGORIA
        public void edit_Categorias(CATEGORIA ctg)
        {
            CATEGORIA categoria = get_CategoriaById(ctg.ID);
            categoria.Descripcion = ctg.Descripcion;

            db.SaveChanges();
        }

        // METODO QUE INSERTA O AGREGA NUEVOS CATEGORIA
        public void añadir_CATEGORIA(CATEGORIA ctg)
        {
            db.CATEGORIAs.Add(ctg);
            db.SaveChanges();
        }

        //---------------------------------------------------

        //---------------------------------------------------------
        //------------------------Tabla Reporte--------------------
        //---------------------------------------------------------

        //METODO Para obetener los datos de un reporte menos la descripcion

        public reporte_usuario get_ReportData(int idProduct, int idUser) 
        {
            reporte_usuario report = new reporte_usuario();
            var product = get_ProductoById(idProduct);

            report.ID_usuario_reporte = idUser;
            report.ID_usuario_reportado = product.ID_USUARIO;
            report.ID_publicacion = product.ID;

            return report;
        }

        // METODO QUE INSERTA O AGREGAR un reporte
        public void añadir_Reporte(reporte_usuario report)
        {
            db.reporte_usuario.Add(report);
            db.SaveChanges();
        }

        // Metodo para obtener todos productos reportados

        public List<PRODUCTO> get_ProductosReportados() 
        {
            List<PRODUCTO> lista;

            var query =
                (from p in db.PRODUCTOes
                 join r in db.reporte_usuario on p.ID equals r.ID_publicacion
                 where p.Estado != 2
                 select p).ToList();

            lista = query;

            return lista;
        }

        //METODO PARA OBTENER LA CANTIDAD DE REPORTES DE UN PRODUCTO

        public List<int> get_CantidadReportes(List<PRODUCTO> product) 
        {
            List<int> lista = new List<int>();

            foreach (var item in product)
            {
                var query =
                    (from re in db.reporte_usuario
                     where re.ID_publicacion == item.ID
                     select re).Count();

                lista.Add(query);
            }

            return lista;
        }

     
        //METODO PARA OBTENER LOS NOMBRES DE LOS REPORTES

        public List<String> get_NombresReportes(List<PRODUCTO> product)
        {

            List<PRODUCTO> lista = new List<PRODUCTO>();
            List<String> listString = new List<String>();

            foreach (var item in product) {

                PRODUCTO pro = db.PRODUCTOes.Find(item.ID);

                lista.Add(pro);
            }
            foreach (var item in lista) 
            {
                listString.Add(item.Nombre);
            }
            return listString;

        }

        //METODO PARA OBTENER A TODOS LOS Reportes

        public List<reporte_usuario> get_Reportes()
        {
            List<reporte_usuario> lista;

            lista = db.reporte_usuario.ToList();

            var query = (from r in db.reporte_usuario
                         orderby r.ID_publicacion
                         select r).ToList();

            return lista;
        }

        //METODO PARA DESHABILITAR Productos Reportados
        public void toDisable_ProductoReportado(PRODUCTO pro)
        {
            PRODUCTO productDisable = get_ProductoById(pro.ID);
            productDisable.Estado = pro.Estado;

            db.SaveChanges();
        }

        //METODO PARA OBTENER EL USUARIO QUE VENDE EL PRODUCTO

        public USUARIO get_Contact(int idProduct)
        {
            PRODUCTO product = get_ProductoById(idProduct);
            USUARIO user = get_UserById(product.ID_USUARIO);

            return user;
        }

        //Datas de productos reportados

        public List<TABLAREPORTE> get_reportesProd()
        {
            var result = db.TABLAREPORTEs.ToList();

            return result;
        }

        // ELIMINAR REPORTES

        public void ELIMINARREPORTES(int ID)
        {
            var query = (from r in db.reporte_usuario
                         where r.ID_publicacion == ID
                         select r).ToList();


            db.reporte_usuario.RemoveRange(query);
            db.SaveChanges();
        }
    }
}
