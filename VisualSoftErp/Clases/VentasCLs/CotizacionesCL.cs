using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSoftErp.Clases.VentasCLs
{
    class CotizacionesCL
    {
        #region propiedades
        string strCnn = globalCL.gv_strcnn;
        public string strSerie { get; set; }
        public int intFolio { get; set; }
        public DataTable dtm { get; set; }
        public DataTable dtd { get; set; }
        public int intID { get; set; }
        public string strDoc { get; set; }
        public int intUsuarioID { get; set; }
        public string strMaquina { get; set; }
        
        public string strRazon { get; set; }

        public DateTime fFecha { get; set; }
        public string strTipocop { get; set; }
        public int intClientesID { get; set; }
        public int intAgentesID { get; set; }
        public string strTiempodeentrega { get; set; }
        public string strCondicionesdepago { get; set; }
        public int intVigencia { get; set; }
        public string strLibreabordo { get; set; }
        public string strAtenciona { get; set; }
        public string strConcopiapara { get; set; }
        public string strEmail { get; set; }
        public string strMonedasID { get; set; }
        public string strEncabezado { get; set; }
        public string strPiedepagina { get; set; }
        public int intStatus { get; set; }
        public decimal intSubtotal { get; set; }
        public decimal intIva { get; set; }
        public decimal intNeto { get; set; }
        public string strDocumentoasignado { get; set; }
        public string strDocumentoserie { get; set; }
        public int intDocumentofolio { get; set; }
        public int intMotivosnoaprobacioncotizacionesID { get; set; }
        public string strNoaprobacioncomentarios { get; set; }
        public decimal decPDescto { get; set; }
        public string strZonaInstalacion { get; set; }

        public string strStatus { get; set; }

       
        public int intSeq { get; set; }
        public string strFecha { get; set; }
        public string strHora { get; set; }
        public int intMediosdecontactoID { get; set; }
        public string strComentarios { get; set; }
        public string strMonedasid { get; set; }

        public int intAño { get; set; }
        public int intMes { get; set; }
        #endregion

        #region constructor


        public CotizacionesCL()
        {
            strSerie = string.Empty;
            intFolio = 0;
            dtm = null;
            dtd = null;
            intID = 0;
            intUsuarioID = 0;
            strMaquina = string.Empty;
         
            strRazon = string.Empty;

            DateTime fFecha = DateTime.Now;
            strTipocop = string.Empty;            
            strTiempodeentrega = string.Empty;
            strCondicionesdepago = string.Empty;            
            strLibreabordo = string.Empty;
            strAtenciona = string.Empty;
            strConcopiapara = string.Empty;
            strEmail = string.Empty;
            strMonedasid = string.Empty;
            strEncabezado = string.Empty;
            strPiedepagina = string.Empty;
            //int intStatus = 0;
            //decimal intSubtotal = 0;
            //decimal intIva = 0;
            //decimal intNeto = 0;
            strDocumentoasignado = string.Empty;
            strDocumentoserie = string.Empty;
            //int intDocumentofolio = 0;
            //int intMotivosnoaprobacioncotizacionesid = 0;
            string strNoaprobacioncomentarios = string.Empty;
            decPDescto = 0;
            strZonaInstalacion = string.Empty;
            intAño = 0;
            intMes = 0;

        string strStatus = string.Empty;

        
            //int intSeq = 0;
    
            string strHora = string.Empty;
            //int intMediosdecontactoid = 0;
            string strComentarios = string.Empty;

        }
        #endregion
        #region metodos

        public DataTable CotizacionesGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "cotizacionesGrid";
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
        } //cotizacionesGrid

        public string CotizacionesCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CotizacionesCRUD";
                cmd.Parameters.AddWithValue("@prmCot", dtm);
                cmd.Parameters.AddWithValue("@prmCotDet", dtd);
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.Parameters.AddWithValue("@prmUsuario", intUsuarioID);
                cmd.Parameters.AddWithValue("@prmMaquina", strMaquina);

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
        }

        public string CotizaciionesCambiarStatus()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                switch (strStatus)
                {
                    case "Aceptar":
                        cmd.CommandText = "CotizacionesAceptar";
                        break;
                    case "Rechazar":
                        cmd.CommandText = "CotizacionesRechazar";
                        break;
                    case "Expirada":
                        cmd.CommandText = "Cotizaciones???";
                        break;
                    case "Cancelar":
                        cmd.CommandText = "CotizacionesCancelar";
                        break;
                    
                        
                }
                     

                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.Parameters.AddWithValue("@prmUsuario", intUsuarioID);              
                cmd.Parameters.AddWithValue("@prmMaquina", strMaquina);
                cmd.Parameters.AddWithValue("@prmNoprobacioncoment", strRazon);

                if (strStatus == "Rechazar")
                {
                    cmd.Parameters.AddWithValue("@prmMotivosnoaprobacion", intMotivosnoaprobacioncotizacionesID);
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
        }

        public string CotizacionesLlenaCajas()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CotizacionesLlenaCajas";
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
                    strTipocop = dr["Tipocop"].ToString();
                    intClientesID = Convert.ToInt32(dr["ClientesID"]);
                    intAgentesID = Convert.ToInt32(dr["AgentesID"]);
                    strTiempodeentrega = dr["Tiempodeentrega"].ToString();
                    strCondicionesdepago = dr["Condicionesdepago"].ToString();
                    intVigencia = Convert.ToInt32(dr["Vigencia"]);
                    strLibreabordo = dr["Libreabordo"].ToString();
                    strAtenciona = dr["Atenciona"].ToString();
                    strConcopiapara = dr["Concopiapara"].ToString();
                    strEmail = dr["Email"].ToString();
                    strMonedasID = dr["MonedasID"].ToString();
                    strEncabezado = dr["Encabezado"].ToString();
                    strPiedepagina = dr["Piedepagina"].ToString();
                    intStatus = Convert.ToInt32(dr["Status"]);
                    intSubtotal = Convert.ToInt32(dr["Status"]);
                    intIva = Convert.ToInt32(dr["Status"]);
                    intNeto = Convert.ToInt32(dr["Status"]);
                    strDocumentoasignado = dr["Documentoasignado"].ToString();
                    strDocumentoserie = dr["Documentoserie"].ToString();
                    intDocumentofolio = Convert.ToInt32(dr["Documentofolio"]);
                    intMotivosnoaprobacioncotizacionesID = Convert.ToInt32(dr["MotivosnoaprobacioncotizacionesID"]);
                    strNoaprobacioncomentarios = dr["Noaprobacioncomentarios"].ToString();
                    decPDescto = Convert.ToDecimal(dr["PDescto"]);
                    strZonaInstalacion = dr["Zonainstalacion"].ToString();
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
    
        public DataTable CotizacionesDetalleGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CotizacionesdetalleLlenaCajas";
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception)
            {
                return dt;
            }
        } //cotizacionesGrid

        public DataTable CotizacioneshistorialGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "cotizacioneshistorialGrid";
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception)
            {
                return dt;
            }
        } //cotizacioneshistorialGrid

        public string CotizacioneshistorialCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CotizacioneshistorialCRUD";
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.Parameters.AddWithValue("@prmSeq", intSeq);
                cmd.Parameters.AddWithValue("@prmFecha", Convert.ToDateTime(strFecha));
                cmd.Parameters.AddWithValue("@prmHora", strHora);
                cmd.Parameters.AddWithValue("@prmMediosdecontactoID", intMediosdecontactoID);
                cmd.Parameters.AddWithValue("@prmComentarios", strComentarios);
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
                return ("CotizacioneshistorialCrud: " + ex.Message);
            }
        } //CotizacioneshistorialCrud

        public string CotizacioneshistorialLlenaCajas()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CotizacioneshistorialllenaCajas";
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.Parameters.AddWithValue("@prmSeq", intSeq);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    strSerie = dr["Serie"].ToString();
                    intFolio = Convert.ToInt32(dr["Folio"]);
                    intSeq = Convert.ToInt32(dr["Seq"]);
                    fFecha = Convert.ToDateTime(dr["Fecha"]);
                    strHora = dr["Hora"].ToString();
                    intMediosdecontactoID = Convert.ToInt32(dr["MediosdecontactoID"]);
                    strComentarios = dr["Comentarios"].ToString();
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


        #endregion
    }
}
