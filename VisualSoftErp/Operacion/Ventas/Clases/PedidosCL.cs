using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml;

namespace VisualSoftErp.Clases.VentasCLs
{
    public class PedidosCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
        public string strSerie { get; set; }
        public int intFolio { get; set; }
        public int intPublicoengeneral { get; set; }
        public string strOrdenTienda { get; set; }
        public string Factura { get; set; }
        public string strSerieCot { get; set; }
        public int intFolioCot { get; set; }
        public DateTime fFecha { get; set; }
        public int intClientesID { get; set; }
        public int intAgentesID { get; set; }
        public decimal intSubtotal { get; set; }
        public decimal intIva { get; set; }
        public decimal intRetIva { get; set; }
        public decimal intIeps { get; set; }
        public decimal intNeto { get; set; }
        public decimal intDescuento { get; set; }
        public string strObservaciones { get; set; }
        public string strCondicionesdepago { get; set; }
        public int intPlazo { get; set; }
        public int intStatus { get; set; }
        public DateTime fFechacancelacion { get; set; }
        public string strRazoncancelacion { get; set; }
        public int intUsuariosID { get; set; }
        public int intTransportesID { get; set; }
        public string strMonedasID { get; set; }
        public int intAlmacenesID { get; set; }
        public string strOc { get; set; }
        public DateTime fFechaestimadadeentrega { get; set; }
        public decimal fTipodecambio { get; set; }
        public int intExportacion { get; set; }
        public int intStatuscxc { get; set; }
        public int intStatusexistencia { get; set; }
        public int intStatusexistenciaparcial { get; set; }
        public int intStatusbajocosto { get; set; }
        public int intDepurado { get; set; }
        public string strRazondepurado { get; set; }
        public DateTime fFechadepurado { get; set; }
        public int intUsuariodepurado { get; set; }
        public int intID { get; set; }
        public string strDoc { get; set; }
        public string strFechadepurado { get; set; }

        public DataTable dtm { get; set; }
        public DataTable dtd { get; set; }

        public int intUsuarioID { get; set; }
        public string strMaquina { get; set; }
        public string strPrograma { get; set; }

        public string strIVA { get; set; }
        public string strRetIva { get; set; }
        public string strRetIsr { get; set; }
        public decimal dSubTotal { get; set; }
        public decimal dDescuento { get; set; }

        public string strStatus { get; set; }
        public string strRazon { get; set; }
        public string strOrigen { get; set; }
        public int intClientesid { get; set; }
        public int intAgentesid { get; set; }
        public decimal decRetiva { get; set; }


        public int intUsuariosid { get; set; }
        public int intTransportesid { get; set; }
        public string strMonedasid { get; set; }
        public int intAlmacenesid { get; set; }

        public decimal dSubtotal { get; set; }

        public int intAño { get; set; }
        public int intMes { get; set; }

        public string strAddenda { get; set; }

        public string strRazonCambioPrecio { get; set; }
        public int intCanalesdeventaID { get; set; }

        #endregion

        #region Constructor
        public PedidosCL()
        {
            intPublicoengeneral = 0;
            strOrdenTienda = string.Empty;
            strRazonCambioPrecio = string.Empty;
            strSerie = string.Empty;
            intFolio = 0;
            strSerieCot = string.Empty;
            intFolioCot = 0;
            fFecha = DateTime.Now;
            intClientesid = 0;
            intAgentesid = 0;
            intSubtotal = 0;
            intIva = 0;
            decRetiva = 0;
            intIeps = 0;
            intNeto = 0;
            intDescuento = 0;
            strObservaciones = string.Empty;
            strCondicionesdepago = string.Empty;
            intPlazo = 0;
            intStatus = 0;
            fFechacancelacion = DateTime.Now;
            strRazoncancelacion = string.Empty;
            intUsuariosid = 0;
            intTransportesid = 0;
            strMonedasid = string.Empty;
            intAlmacenesid = 0;
            strOc = string.Empty;
            fFechaestimadadeentrega = DateTime.Now;
            fTipodecambio = 0;
            intExportacion = 0;
            intStatuscxc = 0;
            intStatusexistencia = 0;
            intStatusexistenciaparcial = 0;
            intStatusbajocosto = 0;
            intDepurado = 0;
            strRazondepurado = string.Empty;
            fFechadepurado = DateTime.Now;
            intUsuariodepurado = 0;
            strIVA = string.Empty;
            strRetIva = string.Empty;
            strRetIsr = string.Empty;
            dSubtotal = 0;
            dDescuento = 0;

            dtm = null;
            dtd = null;
            intUsuarioID = 0;
            strMaquina = string.Empty;
            strPrograma = string.Empty; string strFechadepurado = string.Empty;
            strStatus = string.Empty;
            strRazon = string.Empty;
            strOrigen = string.Empty;
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

        public DataTable PedidosGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "pedidosGrid";
                cmd.Parameters.AddWithValue("@prmAño", intAño);
                cmd.Parameters.AddWithValue("@prmMes", intMes);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //pedidosGrid
        public DataTable PedidosGridAutorizaPrecios()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PedidosGridparaAutorizarCambiosdePrecio";
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //pedidosGrid

        public string PedidosCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PedidosCRUD";
                cmd.Parameters.AddWithValue("@prmPedidos", dtm);
                cmd.Parameters.AddWithValue("@prmPedidosDetalle", dtd);
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.Parameters.AddWithValue("@prmUsuario", intUsuarioID);
                cmd.Parameters.AddWithValue("@prmMaquina", strMaquina);
                cmd.Parameters.AddWithValue("@prmSerieCot", strSerieCot);
                cmd.Parameters.AddWithValue("@prmFolioCot", intFolioCot);
                cmd.Parameters.AddWithValue("@prmRazonCambioPrecio", strRazonCambioPrecio);

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
        } //PedidosCrud

        public string PedidosAutorizaCambiodePrecio()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PedidosAutorizarCambiosdePrecio";
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.Parameters.AddWithValue("@prmUsuario", globalCL.gv_UsuarioID);

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
        } //PedidosCrud

        public string PedidosLlenaCajas()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PedidosllenaCajas";
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    strSerie = dr["Serie"].ToString();
                    intFolio = Convert.ToInt32(dr["Folio"]);
                    fFecha = Convert.ToDateTime(dr["Fecha"]);
                    intClientesID = Convert.ToInt32(dr["ClientesID"]);
                    intAgentesID = Convert.ToInt32(dr["AgentesID"]);
                    intSubtotal = Convert.ToInt32(dr["AgentesID"]);
                    intIva = Convert.ToInt32(dr["AgentesID"]);
                    intRetIva = Convert.ToInt32(dr["AgentesID"]);
                    intIeps = Convert.ToInt32(dr["AgentesID"]);
                    intNeto = Convert.ToInt32(dr["AgentesID"]);
                    intDescuento = Convert.ToInt32(dr["AgentesID"]);
                    strObservaciones = dr["Observaciones"].ToString();
                    strCondicionesdepago = dr["Condicionesdepago"].ToString();
                    intPlazo = Convert.ToInt32(dr["Plazo"]);
                    intStatus = Convert.ToInt32(dr["Status"]);
                    fFechacancelacion = Convert.ToDateTime(dr["Fechacancelacion"]);
                    strRazoncancelacion = dr["Razoncancelacion"].ToString();
                    intUsuariosID = Convert.ToInt32(dr["UsuariosID"]);
                    intTransportesID = Convert.ToInt32(dr["TransportesID"]);
                    strMonedasID = dr["MonedasID"].ToString();
                    intAlmacenesID = Convert.ToInt32(dr["AlmacenesID"]);
                    strOc = dr["Oc"].ToString();
                    fFechaestimadadeentrega = Convert.ToDateTime(dr["Fechaestimadadeentrega"]);
                    fTipodecambio = Convert.ToDecimal(dr["Tipodecambio"]);
                    intExportacion = Convert.ToInt32(dr["Exportacion"]);
                    intStatuscxc = Convert.ToInt32(dr["Statuscxc"]);
                    intStatusexistencia = Convert.ToInt32(dr["Statusexistencia"]);
                    intStatusexistenciaparcial = Convert.ToInt32(dr["Statusexistenciaparcial"]);
                    intStatusbajocosto = Convert.ToInt32(dr["Statusbajocosto"]);
                    intDepurado = Convert.ToInt32(dr["Depurado"]);
                    strRazondepurado = dr["Razondepurado"].ToString();
                    strFechadepurado = dr["Razondepurado"].ToString();
                    intUsuariodepurado = Convert.ToInt32(dr["Usuariodepurado"]);

                    dSubTotal = Convert.ToDecimal(dr["SubTotal"]);
                    dDescuento = Convert.ToDecimal(dr["Descuento"]);
                    strIVA = dr["Iva"].ToString();
                    strRetIva = dr["RetIva"].ToString();
                    strSerieCot = dr["SerieCot"].ToString();
                    intFolioCot = Convert.ToInt32(dr["FolioCot"]);
                    strOrdenTienda = dr["OrdenTienda"].ToString();
                    intPublicoengeneral = Convert.ToInt32(dr["Publicoengeneral"]);
                    Factura = dr["Factura"].ToString();
                    intCanalesdeventaID = Convert.ToInt32(dr["CanalesdeventaID"]);

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

        public DataTable PedidosDetalleGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PedidosDetalleLlenaCajas";
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //PedidosDetalleGrid
        public DataTable CotizacionesDetalleGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CotizacionesLlenaPedido";
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //remisionesGrid

        public string BuscarOrdendecompra()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ClientesExisteOC";
                cmd.Parameters.AddWithValue("@prmCte", intClientesID);
                cmd.Parameters.AddWithValue("@prmOC", strOc);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();

                    strSerie = dr["Serie"].ToString();
                    intFolio = Convert.ToInt32(dr["Folio"]);
                    strOrigen = dr["Origen"].ToString();

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

        public string DocumentosSiguienteID()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "DocumentosSiguienteID";
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmDoc", strDoc);


                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    intID = Convert.ToInt32(dr["SigID"]);
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
            #endregion
        }

        public string PedidosCambiarStatus()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();                
                cmd.CommandText = "PedidosCancelar";                
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.Parameters.AddWithValue("@prmUsuario", intUsuarioID);
                cmd.Parameters.AddWithValue("@prmMaquina", strMaquina);
                cmd.Parameters.AddWithValue("@prmRazondecancelacion", strRazon);
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
        }

        public string PedidosLiberaSurtido()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PedidosLiberaSurtido";
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.Parameters.AddWithValue("@prmUsuarioID", intUsuarioID);
                cmd.Parameters.AddWithValue("@prmMaquina", strMaquina);
                cmd.Parameters.AddWithValue("@prmRazondecancelacion", strRazon);
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
        }

        public DataTable PedidosBitacoraGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PedidosBitacoraGrid";
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //Bitacora

        public DataTable PedidosCambiodeprecio()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PedidosCambiodeprecioListar";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //remisionesGrid

        public DataTable CotizacionesPedidosGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CotizacionesGRIDPedidos";
                cmd.Parameters.AddWithValue("@prmCte", intClientesID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception) { return dt; }
        } //CotizacionesPedidosGrid

        public string PedidosExisteAddenda()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PedidosAddendaValidar";
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.Parameters.AddWithValue("@prmAddenda", strAddenda);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
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
    }
}
