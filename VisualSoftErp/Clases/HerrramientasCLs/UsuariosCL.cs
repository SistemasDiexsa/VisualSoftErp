using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml;

namespace VisualSoftErp.Clases
{
    public class UsuariosCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
        public int intUsuariosID { get; set; }
        public string strNombre { get; set; }
        public string strLogin { get; set; }
        public string strClave { get; set; }
        public int intCambiarprecios { get; set; }
        public int intAutorizarrequisiciones { get; set; }
        public int intAutorizarpedidoscxc { get; set; }
        public int intAlertapedidoscxc { get; set; }
        public int intAutorizapedidosxexistencia { get; set; }
        public int intAlertapedidosxexistencia { get; set; }
        public int intAutorizaoc { get; set; }
        public int intAlertaoc { get; set; }
        public int intAutorizabajocosto { get; set; }
        public int intCancelarpedidossurtidos { get; set; }
        public int intVercostos { get; set; }
        public int intAlertapedidosporfacturar { get; set; }
        public int intModificarcantidadalsurtir { get; set; }
        public int intActualizarcostosmanualmente { get; set; }
        public int intAutorizacancelacionesanteriores { get; set; }
        public int intAlmacendefault { get; set; }
        public int intDepuraoc { get; set; }
        public int intUltimosprecios { get; set; }
        public string strSerie { get; set; }  public string strPermiso { get; set; }
        public int intEditarcondicionesdepago { get; set; }
        public string strCapturista { get; set; }
        public int intCancelardepositos { get; set; }
        public int intCancelarpedidos { get; set; }
        public int intCancelarpagoscxp { get; set; }
        public int intCancelarcontrarecibos { get; set; }
        public int intCancelarcompras { get; set; }
        public int intCancelaranticipos { get; set; }
        public int intFlexArtArribosProveedor { get; set; }
        public int intFlexArtMovimientos { get; set; }
        public int intFlexArtVentas { get; set; }
        public int intFlexArtCompras { get; set; }
        public int intActivo { get; set; }
        public int intTienda { get; set; }
        public int iFirmaElectronicaSalidas { get; set; }
        #endregion

        #region Constructor
        public UsuariosCL()
        {
            intUsuariosID = 0;
            strNombre = string.Empty;
            strLogin = string.Empty;
            strClave = string.Empty;
            intCambiarprecios = 0;
            intAutorizarrequisiciones = 0;
            intAutorizarpedidoscxc = 0;
            intAlertapedidoscxc = 0;
            intAutorizapedidosxexistencia = 0;
            intAlertapedidosxexistencia = 0;
            intAutorizaoc = 0;
            intAlertaoc = 0;
            intAutorizabajocosto = 0;
            intCancelarpedidossurtidos = 0;
            intVercostos = 0;
            intAlertapedidosporfacturar = 0;
            intModificarcantidadalsurtir = 0;
            intActualizarcostosmanualmente = 0;
            intAutorizacancelacionesanteriores = 0;
            intAlmacendefault = 0;
            intDepuraoc = 0;
            intUltimosprecios = 0;
            strSerie = string.Empty;
            intEditarcondicionesdepago = 0;
            strCapturista = string.Empty;
            intCancelardepositos = 0;
            intCancelarpedidos = 0;
            intCancelarpagoscxp = 0;
            intCancelarcontrarecibos = 0;
            intCancelarcompras = 0;
            intCancelaranticipos = 0;
            intFlexArtArribosProveedor = 0;
            intFlexArtMovimientos = 0;
            intFlexArtVentas = 0;
            intFlexArtCompras = 0;
            intActivo = 0;
            intTienda = 0;
            iFirmaElectronicaSalidas = 0;
        }
        #endregion

        #region Metodos
        private string loadConnectionString()
        {
            try
            {
                XmlDocument oxml = new XmlDocument();
                oxml.Load(@"C:\VisualSoftErp\xml\conexion.xml");
                XmlNodeList sConfiguracion = oxml.GetElementsByTagName("Configuracion");
                XmlNodeList sGenerales = ((XmlElement)sConfiguracion[0]).GetElementsByTagName("Generales");
                XmlNodeList sStr_Conn = ((XmlElement)sGenerales[0]).GetElementsByTagName("Str_Conn");
                return sStr_Conn[0].InnerText;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public DataTable UsuariosGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "usuariosGrid";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //usuariosGrid

        public string UsuariosCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "UsuariosCRUD";
                cmd.Parameters.AddWithValue("@prmUsuariosID", intUsuariosID);
                cmd.Parameters.AddWithValue("@prmNombre", strNombre);
                cmd.Parameters.AddWithValue("@prmLogin", strLogin);
                cmd.Parameters.AddWithValue("@prmClave", strClave);
                cmd.Parameters.AddWithValue("@prmCambiarprecios", intCambiarprecios);
                cmd.Parameters.AddWithValue("@prmAutorizarrequisiciones", intAutorizarrequisiciones);
                cmd.Parameters.AddWithValue("@prmAutorizarpedidoscxc", intAutorizarpedidoscxc);
                cmd.Parameters.AddWithValue("@prmAlertapedidoscxc", intAlertapedidoscxc);
                cmd.Parameters.AddWithValue("@prmAutorizapedidosxexistencia", intAutorizapedidosxexistencia);
                cmd.Parameters.AddWithValue("@prmAlertapedidosxexistencia", intAlertapedidosxexistencia);
                cmd.Parameters.AddWithValue("@prmAutorizaoc", intAutorizaoc);
                cmd.Parameters.AddWithValue("@prmAlertaoc", intAlertaoc);
                cmd.Parameters.AddWithValue("@prmAutorizabajocosto", intAutorizabajocosto);
                cmd.Parameters.AddWithValue("@prmCancelarpedidossurtidos", intCancelarpedidossurtidos);
                cmd.Parameters.AddWithValue("@prmVercostos", intVercostos);
                cmd.Parameters.AddWithValue("@prmAlertapedidosporfacturar", intAlertapedidosporfacturar);
                cmd.Parameters.AddWithValue("@prmModificarcantidadalsurtir", intModificarcantidadalsurtir);
                cmd.Parameters.AddWithValue("@prmActualizarcostosmanualmente", intActualizarcostosmanualmente);
                cmd.Parameters.AddWithValue("@prmAutorizacancelacionesanteriores", intAutorizacancelacionesanteriores);
                cmd.Parameters.AddWithValue("@prmAlmacendefault", intAlmacendefault);
                cmd.Parameters.AddWithValue("@prmDepuraoc", intDepuraoc);
                cmd.Parameters.AddWithValue("@prmUltimosprecios", intUltimosprecios);
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmEditarcondicionesdepago", 0);
                cmd.Parameters.AddWithValue("@prmCapturista", 0);
                cmd.Parameters.AddWithValue("@prmCancelardepositos", 0);
                cmd.Parameters.AddWithValue("@prmCancelarpedidos", 0);
                cmd.Parameters.AddWithValue("@prmCancelarpagoscxp", 0);
                cmd.Parameters.AddWithValue("@prmCancelarcontrarecibos", 0);
                cmd.Parameters.AddWithValue("@prmCancelarcompras", 0);
                cmd.Parameters.AddWithValue("@prmCancelaranticipos", 0);
                cmd.Parameters.AddWithValue("@prmFlexArtArribosProveedor", intFlexArtArribosProveedor);
                cmd.Parameters.AddWithValue("@prmFlexArtMovimientos", intFlexArtMovimientos);
                cmd.Parameters.AddWithValue("@prmFlexArtVentas", intFlexArtVentas);
                cmd.Parameters.AddWithValue("@prmFlexArtCompras", intFlexArtCompras);
                cmd.Parameters.AddWithValue("@prmActivo", intActivo);
                cmd.Parameters.AddWithValue("@prmTienda", intTienda);
                cmd.Parameters.AddWithValue("@prmiFirmaElectronicaSalidas", iFirmaElectronicaSalidas);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    result = dr["result"].ToString();
                }
                else
                {
                    result = "no read";
                }
                dr.Close();
                cnn.Close();
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        } //UsuariosCrud

        public string UsuariosLlenaCajas()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "UsuariosllenaCajas";
                cmd.Parameters.AddWithValue("@prmUsuariosID", intUsuariosID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    intUsuariosID = Convert.ToInt32(dr["UsuariosID"]);
                    strNombre = dr["Nombre"].ToString();
                    strLogin = dr["Login"].ToString();
                    strClave = dr["Clave"].ToString();
                    intCambiarprecios = Convert.ToInt32(dr["Cambiarprecios"]);
                    intAutorizarrequisiciones = Convert.ToInt32(dr["Autorizarrequisiciones"]);
                    intAutorizarpedidoscxc = Convert.ToInt32(dr["Autorizarpedidoscxc"]);
                    intAlertapedidoscxc = Convert.ToInt32(dr["Alertapedidoscxc"]);
                    intAutorizapedidosxexistencia = Convert.ToInt32(dr["Autorizapedidosxexistencia"]);
                    intAlertapedidosxexistencia = Convert.ToInt32(dr["Alertapedidosxexistencia"]);
                    intAutorizaoc = Convert.ToInt32(dr["Autorizaoc"]);
                    intAlertaoc = Convert.ToInt32(dr["Alertaoc"]);
                    intAutorizabajocosto = Convert.ToInt32(dr["Autorizabajocosto"]);
                    intCancelarpedidossurtidos = Convert.ToInt32(dr["Cancelarpedidossurtidos"]);
                    intVercostos = Convert.ToInt32(dr["Vercostos"]);
                    intAlertapedidosporfacturar = Convert.ToInt32(dr["Alertapedidosporfacturar"]);
                    intModificarcantidadalsurtir = Convert.ToInt32(dr["Modificarcantidadalsurtir"]);
                    intActualizarcostosmanualmente = Convert.ToInt32(dr["Actualizarcostosmanualmente"]);
                    intAutorizacancelacionesanteriores = Convert.ToInt32(dr["Autorizacancelacionesanteriores"]);
                    intAlmacendefault = Convert.ToInt32(dr["Almacendefault"]);
                    intDepuraoc = Convert.ToInt32(dr["Depuraoc"]);
                    intUltimosprecios = Convert.ToInt32(dr["Ultimosprecios"]);
                    strSerie = dr["Serie"].ToString();

                    intFlexArtArribosProveedor = Convert.ToInt32(dr["FlexArtArribosProveedor"]);
                    intFlexArtMovimientos = Convert.ToInt32(dr["FlexArtMovimientos"]);
                    intFlexArtVentas = Convert.ToInt32(dr["FlexArtVentas"]);
                    intFlexArtCompras = Convert.ToInt32(dr["FlexArtCompras"]);
                    intActivo = Convert.ToInt32(dr["Activo"]);
                    intTienda = Convert.ToInt32(dr["Tienda"]);
                    iFirmaElectronicaSalidas = Convert.ToInt32(dr["FirmaElectronicaSalidas"]);

                    result = "OK";
                }
                else
                {
                    result = "no read";
                }

                dr.Close();
                cnn.Close();
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        } // public class LlenaCajas

        public string UsuariosEliminar()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "UsuariosEliminar";
                cmd.Parameters.AddWithValue("@prmUsuariosID", intUsuariosID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    result = dr["result"].ToString();
                }
                else
                {
                    result = "no read";
                }

                dr.Close();
                cnn.Close();
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        } // Public Class Eliminar
        public string UsuariosPermisos()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "UsuariosValidaPermisos";
                cmd.Parameters.AddWithValue("@prmLogin", strLogin);
                cmd.Parameters.AddWithValue("@prmPassword", strClave);
                cmd.Parameters.AddWithValue("@prmPermiso", strPermiso);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    intUsuariosID = Convert.ToInt32(dr["usuariosID"]);
                    result = dr["result"].ToString();
                }
                else
                {
                    result = "no read";
                }

                dr.Close();
                cnn.Close();
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        } // Public Class Eliminar

        #endregion
    }
}