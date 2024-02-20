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
    public class AnticiposCxPCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
        public int intAnticiposCxPID { get; set; }
        public int intProveedoresID { get; set; }
        public DateTime fFecha { get; set; }
        public int intChequerasID { get; set; }
        public decimal dImportechequera { get; set; }
        public string strReferencia { get; set; }
        public string strMonedaCehquera { get; set; }
        public decimal dTipodecambio { get; set; }
        public decimal dImporteProveedor { get; set; }
        public string strDescripcion { get; set; }
        public string strStatus { get; set; }
        public string strcFormaPago { get; set; }
        public int intCxPAnticiposID { get; set; }
        public int intUsuCan { get; set; }
        public string strRazon { get; set; }
        public string strMonedaProveedor { get; set; }
        public string intFolioAnt { get; set; }
        public string strSerie { get; set; }
        public int intTipoCargo { get; set; }
        public int intFolioFac { get; set; }

        #endregion

        #region Constructor
        public AnticiposCxPCL()
        {
            intAnticiposCxPID = 0;
            intProveedoresID = 0;
            fFecha = DateTime.Now;
            intChequerasID = 0;
            dImportechequera = 0;
            strReferencia = string.Empty;
            strMonedaCehquera = string.Empty;
            dTipodecambio = 0;
            dImporteProveedor = 0;
            strDescripcion = string.Empty;
            strStatus = string.Empty;
            strcFormaPago = string.Empty;
            intCxPAnticiposID = 0;
            strMonedaProveedor = string.Empty;
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
        public DataTable AnticiposCxPGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "anticiposcxpGrid";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //anticiposcxpGriD

        public DataTable AnticiposCxPPendientes()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "AnticiposCxPPendientes";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //AnticiposCxPGridPorAplicar


        public DataTable AnticiposAplicadosGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                if (globalCL.gv_AnticiposOrigen=="CXP")
                    cmd.CommandText = "AnticiposListaAplicadosCxP";
                else
                    cmd.CommandText = "AnticiposListaAplicadosCxC";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //anticiposcxpGriD

        public DataTable AnticiposCxCGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "AnticiposCxCGrid";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //anticiposcxpGrid

        public string AnticiposCxPCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                if (globalCL.gv_AnticiposOrigen=="CXP")
                {
                    cmd.CommandText = "AnticiposCxPCRUD";
                    cmd.Parameters.AddWithValue("@prmAnticiposCxPID", intAnticiposCxPID);
                    cmd.Parameters.AddWithValue("@prmProveedoresID", intProveedoresID);
                    cmd.Parameters.AddWithValue("@prmFecha", fFecha);
                    cmd.Parameters.AddWithValue("@prmChequerasID", intChequerasID);
                    cmd.Parameters.AddWithValue("@prmImportechequera", dImportechequera);
                    cmd.Parameters.AddWithValue("@prmReferencia", strReferencia);
                    cmd.Parameters.AddWithValue("@prmMonedaCehquera", strMonedaCehquera);
                    cmd.Parameters.AddWithValue("@prmTipodecambio", dTipodecambio);
                    cmd.Parameters.AddWithValue("@prmMonedaProveedor", strMonedaProveedor);
                    cmd.Parameters.AddWithValue("@prmImporteProveedor", dImporteProveedor);
                    cmd.Parameters.AddWithValue("@prmDescripcion", strDescripcion);
                    cmd.Parameters.AddWithValue("@prmStatus", strStatus);
                    cmd.Parameters.AddWithValue("@prmcFormaPago", strcFormaPago);
                    cmd.Parameters.AddWithValue("@Folio", 0);
                    cmd.Parameters.AddWithValue("@Tipodemovimiento", 1);
                    cmd.Parameters.AddWithValue("@SerieCargoAplicado", "");
                    cmd.Parameters.AddWithValue("@TipoCargo", 0);
                    cmd.Parameters.AddWithValue("@FolioCargoAplicado", "");

                }
                else
                {
                    cmd.CommandText = "AnticiposCxCCRUD";
                    cmd.Parameters.AddWithValue("@prmAnticiposCxCID", intAnticiposCxPID);
                    cmd.Parameters.AddWithValue("@prmClientesID", intProveedoresID);
                    cmd.Parameters.AddWithValue("@prmFecha", fFecha);
                    cmd.Parameters.AddWithValue("@prmChequerasID", intChequerasID);
                    cmd.Parameters.AddWithValue("@prmImportechequera", dImportechequera);
                    cmd.Parameters.AddWithValue("@prmReferencia", strReferencia);
                    cmd.Parameters.AddWithValue("@prmMonedaChequera", strMonedaCehquera);
                    cmd.Parameters.AddWithValue("@prmTipodecambio", dTipodecambio);
                    cmd.Parameters.AddWithValue("@prmMonedaCliente", strMonedaProveedor);
                    cmd.Parameters.AddWithValue("@prmImporteCliente", dImporteProveedor);
                    cmd.Parameters.AddWithValue("@prmDescripcion", strDescripcion);
                    cmd.Parameters.AddWithValue("@prmStatus", strStatus);
                    cmd.Parameters.AddWithValue("@prmcFormaPago", strcFormaPago);
                    cmd.Parameters.AddWithValue("@prmFolio", 0);
                    cmd.Parameters.AddWithValue("@prmTipodemovimiento", 1);
                    cmd.Parameters.AddWithValue("@prmSerieCargoAplicado", "");
                    cmd.Parameters.AddWithValue("@prmTipoCargo", 0);
                    cmd.Parameters.AddWithValue("@prmFolioCargoAplicado", "");
                }
               
                
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
        } //AnticiposCxPCrud

        public DataTable Aplicaciones()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();

                if (globalCL.gv_AnticiposOrigen=="CXP")
                {
                    cmd.CommandText = "AnticiposListaAplicadosdeUnFolio";
                    cmd.Parameters.AddWithValue("@prmProv", intProveedoresID);
                }
                else
                {
                    cmd.CommandText = "AnticiposListaAplicadosdeUnFolioCxC";
                    cmd.Parameters.AddWithValue("@prmCte", intProveedoresID);
                }
                
                cmd.Parameters.AddWithValue("@prmFolioAnt", intFolioAnt);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //anticiposcxpGrid

        public string AnticiposCxPLlenaCajas()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "AnticiposCxPllenaCajas";
                cmd.Parameters.AddWithValue("@prmAnticiposCxPID", intAnticiposCxPID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    //intAnticiposCxPID = Convert.ToInt32(dr["AnticiposCxPID"]);
                    intProveedoresID = Convert.ToInt32(dr["ProveedoresID"]);
                    fFecha = Convert.ToDateTime(dr["Fecha"]);
                    intChequerasID = Convert.ToInt32(dr["ChequerasID"]);
                    dImportechequera = Convert.ToDecimal(dr["ImporteChequera"]);
                    strReferencia = dr["Referencia"].ToString();
                    strMonedaCehquera = dr["MonedaChequera"].ToString();
                    dTipodecambio = Convert.ToDecimal(dr["Tipodecambio"]);
                    strMonedaProveedor = dr["Monedaproveedor"].ToString();
                    dImporteProveedor = Convert.ToDecimal(dr["ImporteProveedor"]);
                    strDescripcion = dr["Descripcion"].ToString();
                    strStatus = dr["Status"].ToString();
                    strcFormaPago = dr["cFormaPago"].ToString();
                    intCxPAnticiposID = Convert.ToInt32(dr["CxPAnticiposID"]);
                    intFolioAnt= dr["Folio"].ToString();
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

        public string AnticiposCxCLlenaCajas()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "AnticiposCxCLlenaCajas";
                cmd.Parameters.AddWithValue("@prmAnticiposCxCID", intAnticiposCxPID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    //intAnticiposCxPID = Convert.ToInt32(dr["AnticiposCxPID"]);
                    intProveedoresID = Convert.ToInt32(dr["ClientesID"]);
                    fFecha = Convert.ToDateTime(dr["Fecha"]);
                    intChequerasID = Convert.ToInt32(dr["ChequerasID"]);
                    dImportechequera = Convert.ToDecimal(dr["ImporteChequera"]);
                    strReferencia = dr["Referencia"].ToString();
                    strMonedaCehquera = dr["MonedaChequera"].ToString();
                    dTipodecambio = Convert.ToDecimal(dr["Tipodecambio"]);
                    strMonedaProveedor = dr["Monedacliente"].ToString();
                    dImporteProveedor = Convert.ToDecimal(dr["Importecliente"]);
                    strDescripcion = dr["Descripcion"].ToString();
                    strStatus = dr["Status"].ToString();
                    strcFormaPago = dr["cFormaPago"].ToString();
                    intCxPAnticiposID = Convert.ToInt32(dr["CxCAnticiposID"]);
                    intFolioAnt = dr["Folio"].ToString();
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

        public string AnticiposCxPCancelar()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                if (globalCL.gv_AnticiposOrigen == "CXP")
                {
                    cmd.CommandText = "AnticiposCxPCancelar";
                    cmd.Parameters.AddWithValue("@prmAnticiposCxPID", intAnticiposCxPID);
                }
                else
                {
                    cmd.CommandText = "AnticiposCxCCancelar";
                    cmd.Parameters.AddWithValue("@prmAnticiposCxCID", intAnticiposCxPID);
                }
                cmd.Parameters.AddWithValue("@prmUsu", intUsuCan);
                cmd.Parameters.AddWithValue("@prmRazon", strRazon);
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

        public string AnticiposCxPAplicacionCancelar()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                if (globalCL.gv_AnticiposOrigen=="CXP")
                    cmd.CommandText = "AnticiposAplicacionCancelarCxP";
                else
                    cmd.CommandText = "AnticiposAplicacionCancelarCxC";

                cmd.Parameters.AddWithValue("@prmID", intAnticiposCxPID);
                cmd.Parameters.AddWithValue("@prmUsu", intUsuCan);
                cmd.Parameters.AddWithValue("@prmRazon", strRazon);
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

        public DataTable AnticiposCxPPorAplicar()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                if (globalCL.gv_AnticiposOrigen == "CXP")
                {
                    cmd.CommandText = "AnticiposCxPPorAplicar";
                    cmd.Parameters.AddWithValue("@prmProv", intProveedoresID);
                }
                else
                {
                    cmd.CommandText = "AnticiposCxCPorAplicar";
                    cmd.Parameters.AddWithValue("@prmCte", intProveedoresID);
                }
                    
                
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //anticiposcxpGrid

        // ---------------------------------------- APLICACION DE ANTICIPPOS ----------------------------------
        public string AplicaAnticiposCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                if (globalCL.gv_AnticiposOrigen == "CXP")
                {
                    cmd.CommandText = "AnticiposCxPAplicar";
                    cmd.Parameters.AddWithValue("@prmAnticiposCxPID", intAnticiposCxPID);
                    cmd.Parameters.AddWithValue("@prmProveedoresID", intProveedoresID);
                    cmd.Parameters.AddWithValue("@prmFecha", fFecha);
                    cmd.Parameters.AddWithValue("@prmChequerasID", intChequerasID);
                    cmd.Parameters.AddWithValue("@prmImportechequera", dImportechequera);
                    cmd.Parameters.AddWithValue("@prmReferencia", strReferencia);
                    cmd.Parameters.AddWithValue("@prmMonedaCehquera", strMonedaCehquera);
                    cmd.Parameters.AddWithValue("@prmTipodecambio", dTipodecambio);
                    cmd.Parameters.AddWithValue("@prmMonedaProveedor", strMonedaProveedor);
                    cmd.Parameters.AddWithValue("@prmImporteProveedor", dImporteProveedor);
                    cmd.Parameters.AddWithValue("@prmDescripcion", strDescripcion);
                    cmd.Parameters.AddWithValue("@prmStatus", strStatus);
                    cmd.Parameters.AddWithValue("@prmcFormaPago", strcFormaPago);
                    cmd.Parameters.AddWithValue("@prmFolioAnt", intFolioAnt);
                    cmd.Parameters.AddWithValue("@prmTipodemovimiento", 2);
                    cmd.Parameters.AddWithValue("@prmSerieCargoAplicado", strSerie);
                    cmd.Parameters.AddWithValue("@prmTipoCargo", intTipoCargo);
                    cmd.Parameters.AddWithValue("@prmFolioCargoAplicado", intFolioFac);
                }

                else
                {
                    cmd.CommandText = "AnticiposCxCAplicar";
                    cmd.Parameters.AddWithValue("@prmAnticiposCxCID", intAnticiposCxPID);
                    cmd.Parameters.AddWithValue("@prmClientesID", intProveedoresID);
                    cmd.Parameters.AddWithValue("@prmFecha", fFecha);
                    cmd.Parameters.AddWithValue("@prmChequerasID", intChequerasID);
                    cmd.Parameters.AddWithValue("@prmImportechequera", dImportechequera);
                    cmd.Parameters.AddWithValue("@prmReferencia", strReferencia);
                    cmd.Parameters.AddWithValue("@prmMonedaChequera", strMonedaCehquera);
                    cmd.Parameters.AddWithValue("@prmTipodecambio", dTipodecambio);
                    cmd.Parameters.AddWithValue("@prmMonedaCliente", strMonedaProveedor);
                    cmd.Parameters.AddWithValue("@prmImporteCliente", dImporteProveedor);
                    cmd.Parameters.AddWithValue("@prmDescripcion", strDescripcion);
                    cmd.Parameters.AddWithValue("@prmStatus", strStatus);
                    cmd.Parameters.AddWithValue("@prmcFormaPago", strcFormaPago);
                    cmd.Parameters.AddWithValue("@prmFolioAnt", intFolioAnt);
                    cmd.Parameters.AddWithValue("@prmTipodemovimiento", 2);
                    cmd.Parameters.AddWithValue("@prmSerieCargoAplicado", strSerie);
                    cmd.Parameters.AddWithValue("@prmTipoCargo", intTipoCargo);
                    cmd.Parameters.AddWithValue("@prmFolioCargoAplicado", intFolioFac);
                }
                    
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
        } //AnticiposCxPCrud
        #endregion
    }
}