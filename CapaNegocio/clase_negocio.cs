using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
   
   
    public class clase_negocio
    {
        // VARIABLE DE LA CLASE DE LA CAPA DATOS
        ClaseDato Dt = new ClaseDato();


        // METODO QUE ME INSERTA UN USUARIO A LA BD.
        public void Insertar(USUARIO user)
        {
            Dt.añadir_USUARIOS(user);
        }

        // METODO QUE ME VALIDA EL CORREO ELECTRONICO EN LA PAGINA DE REGISTRO.
        public USUARIO Validar_correo(String correo)
        {
            return Dt.validar_correo(correo);
        }
        public USUARIO Login(USUARIO usuario)
        {
           return Dt.login(usuario);
                
        }

        //METODO PARA OBTENER TODOS LOS USURIOS HABILITADOS
        public List<USUARIO> get_Usuarios() {
            return Dt.get_Usuarios();
        }

        //METODO PARA OBTENER A UN USURIO POR SU ID
        public USUARIO get_UserByID(int id) {
            return Dt.get_UserById(id);
        }

        //METODO PARA EDITAR UN USUARIO
        public void edit_User(USUARIO user) {
            Dt.edit_User(user);
        }

        //METODO PARA DESHABILITAR USUARIOS
        public void toDisable(USUARIO user) {
            Dt.toDisable(user);
        }
        //------------------------------------------------------------
        //*************************************************************
        //                    TABLA PRODUCTOS
        //*************************************************************

        //METODO PARA OBTENER TODOS LOS PRODUCTOS HABILITADOS
        public List<PRODUCTO> get_Productos()
        {
            return Dt.get_Productos();
        }

        //METODO PARA OBTENER A UN PRODUCTO POR SU ID
        public PRODUCTO get_ProductoById(int id)
        {
            return Dt.get_ProductoById(id);
        }

        //METODO PARA EDITAR UN USUARIO
        public void edit_Productos(PRODUCTO pro)
        {
            Dt.edit_Productos(pro);
        }

        //METODO PARA DESHABILITAR USUARIOS
        public void toDisableProducto(PRODUCTO pro)
        {
            Dt.toDisableProducto(pro);
        }

        // METODO QUE ME INSERTA UN USUARIO A LA BD.
        public void InsertarProducto(PRODUCTO pro)
        {
            Dt.añadir_PRODUCTOS(pro);
        }

        //------------------------------------------------------------
        //*************************************************************
        //                    TABLA CATEGORIAS
        //*************************************************************

        //METODO PARA OBTENER TODOS LOS PRODUCTOS HABILITADOS
        public List<CATEGORIA> get_Categoria()
        {
            return Dt.get_Categoria();
        }

        //METODO PARA OBTENER A UN PRODUCTO POR SU ID
        public CATEGORIA get_CategoriaById(int id)
        {
            return Dt.get_CategoriaById(id);
        }

        //METODO PARA EDITAR UN USUARIO
        public void edit_Categoria(CATEGORIA ctg)
        {
            Dt.edit_Categorias(ctg);
        }

        // METODO QUE ME INSERTA UN USUARIO A LA BD.
        public void InsertarCategoria(CATEGORIA ctg)
        {
            Dt.añadir_CATEGORIA(ctg);
        }

        //---------------------------------------------------------
        //------------------------Tabla Reporte--------------------
        //---------------------------------------------------------

        //METODO Para obetener los datos de un reporte menos la descripcion

        public reporte_usuario get_ReportData(int idProduct, int idUser) 
        {
           var result = Dt.get_ReportData(idProduct, idUser);
            return result;
        }

        // METODO QUE INSERTA O AGREGAR un reporte
        public void InsertarReporte(reporte_usuario report)
        {
            Dt.añadir_Reporte(report);
        }

        // Metodo para obtener todos productos reportados

        public List<PRODUCTO> get_ProductosReportados()
        {
            var product = Dt.get_ProductosReportados();

            return product;
        }

        //METODO PARA OBTENER LA CANTIDAD DE REPORTES DE UN PRODUCTO

        public List<int> get_CantidadReportes(List<PRODUCTO> product)
        {

            var result = Dt.get_CantidadReportes(product);

            return result;
        }

        //METODO PARA OBTENER LOS NOMBRES DE LOS REPORTES

        public List<String> get_NombresReportes(List<PRODUCTO> product)
        {
            var result = Dt.get_NombresReportes(product);

            return result;
        }

        //METODO PARA OBTENER A TODOS LOS Reportes

        public List<reporte_usuario> get_Reportes()
        {
            List<reporte_usuario> lista;

            lista = Dt.get_Reportes();

            return lista;
        }

        //METODO PARA DESHABILITAR Productos Reportados
        public void toDisable_ProductoReportado(PRODUCTO pro)
        {
            Dt.toDisable_ProductoReportado(pro);
        }

        //METODO PARA OBTENER EL USUARIO QUE VENDE EL PRODUCTO

        public USUARIO get_Contact(int idProduct)
        {
            var user = Dt.get_Contact(idProduct);

            return user;
        }

        //Datas de productos reportados

        public List<TABLAREPORTE> get_reportesProd()
        {
            var result = Dt.get_reportesProd();

            return result;
        }

        // ELIMINAR REPORTES

        public void EliminarR(int ID)
        {
            Dt.ELIMINARREPORTES(ID);
        }
    }
}
