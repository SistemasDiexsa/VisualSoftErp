using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml;
using System.IO;
using VisualSoftErp.Catalogos;

namespace VisualSoftErp.Clases
{
    public class NotasdecreditoCL
    {
        #region Propiedades

        public int intClienteFac { get; set; }
        public string TipoRelacionAlCancelar { get; set; }
        public string UUIDASustituirAlCancelar { get; set; }

        string strCnn = globalCL.gv_strcnn;
        public string strSerie { get; set; }
        public int intFolio { get; set; }
        public string strSerieCfdi { get; set; }
        public int intFolioCfdi { get; set; }
        public decimal dTotalimpuestosret { get; set; }
        public decimal dTotalimpuestostras { get; set; }
        public DateTime fFecha { get; set; }
        public DateTime fFechaFac { get; set; }
        public int intClientesID { get; set; }
        public int intObservacionesID { get; set; }
        public int intAgentesID { get; set; }
        public decimal intSubtotal { get; set; }
        public decimal intIva { get; set; }
        public decimal intRetIva { get; set; }
        public decimal intRetIsr { get; set; }
        public decimal intPIva { get; set; }
        public decimal intPRetIva { get; set; }
        public decimal intPRetIsr { get; set; }
        public decimal intNeto { get; set; }
        public int intStatus { get; set; }
        public DateTime fFechacancelacion { get; set; }
        public string strRazoncancelacion { get; set; }
        public int intUsuariosID { get; set; }
        public string strMonedasID { get; set; }
        public decimal strTipodecambio { get; set; }
        public int intEsdevolucion { get; set; }
        public int intEntradaTipodemovimientoinvID { get; set; }
        public string strEntradaserie { get; set; }
        public int intEntradafolio { get; set; }
        public int intPolizasID { get; set; }
        public DataTable dtm { get; set; }
        public DataTable dtd { get; set; }
        public int intUsuarioID { get; set; }
        public int intID { get; set; }
        public string strMaquina { get; set; }
        public string strPrograma { get; set; }
        public string strDoc { get; set; }
        public string strRazon { get; set; }
        public string strc_FP { get; set; }
        #endregion

        #region Constructor
        public NotasdecreditoCL()
        {
            strSerie = string.Empty;
            intFolio = 0;
            strSerieCfdi = string.Empty;
            intFolioCfdi = 0;
            dTotalimpuestosret = 0;
            dTotalimpuestostras = 0;
            fFecha = DateTime.Now;
            intClientesID = 0;
            intObservacionesID = 0;
            intAgentesID = 0;
            intSubtotal = 0;
            intIva = 0;
            intRetIva = 0;
            intRetIsr = 0;
            intPIva = 0;
            intPRetIva = 0;
            intPRetIsr = 0;
            intNeto = 0;
            intStatus = 0;
            fFechacancelacion = DateTime.Now;
            strRazoncancelacion = string.Empty;
            intUsuariosID = 0;
            strMonedasID = string.Empty;
            strTipodecambio = 0;
            intEsdevolucion = 0;
            intEntradaTipodemovimientoinvID = 0;
            strEntradaserie = string.Empty;
            strDoc = string.Empty; strRazon = string.Empty;
            intEntradafolio = 0;
            intPolizasID = 0;
            intID = 0;
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
        public DataTable NotasdecreditoGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "NotasdecreditoGRID";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //notasdecreditoGrid

        public string NotasdecreditoCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "NotasdecreditoCRUD";
                cmd.Parameters.AddWithValue("@prmNotasdecredito", dtm);
                cmd.Parameters.AddWithValue("@prmNotasdecreditodetalle", dtd);
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
        } //NotasdecreditoCrud

        public string NotasdecreditoLlenaCajas()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "NotasdecreditollenaCajas";
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
                    intObservacionesID = Convert.ToInt32(dr["ObservacionesID"]);
                    intAgentesID = Convert.ToInt32(dr["AgentesID"]);
                    intSubtotal = Convert.ToInt32(dr["AgentesID"]);
                    intIva = Convert.ToInt32(dr["AgentesID"]);
                    intRetIva = Convert.ToInt32(dr["AgentesID"]);
                    intRetIsr = Convert.ToInt32(dr["AgentesID"]);
                    intPIva = Convert.ToInt32(dr["AgentesID"]);
                    intPRetIva = Convert.ToInt32(dr["AgentesID"]);
                    intPRetIsr = Convert.ToInt32(dr["AgentesID"]);
                    intNeto = Convert.ToInt32(dr["AgentesID"]);
                    intStatus = Convert.ToInt32(dr["Status"]);
                    fFechacancelacion = Convert.ToDateTime(dr["Fechacancelacion"]);
                    strRazoncancelacion = dr["Razoncancelacion"].ToString();
                    intUsuariosID = Convert.ToInt32(dr["UsuariosID"]);
                    strMonedasID = dr["MonedasID"].ToString();
                    strTipodecambio = Convert.ToDecimal(dr["Tipodecambio"]);
                    intEsdevolucion = Convert.ToInt32(dr["Esdevolucion"]);
                    intEntradaTipodemovimientoinvID = Convert.ToInt32(dr["EntradaTipodemovimientoinvID"]);
                    strEntradaserie = dr["Entradaserie"].ToString();
                    intEntradafolio = Convert.ToInt32(dr["Entradafolio"]);
                    intPolizasID = Convert.ToInt32(dr["PolizasID"]);
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

        public DataTable NotasdecreditodetalleLlenaCajas()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "NotasdecreditodetalleLlenaCajas";
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
        } //notasdecreditoGrid

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

        public string BuscarImpuestosporSerieyFolioCfdi()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FacturasLeeIvayRet";
                cmd.Parameters.AddWithValue("@prmSerie", strSerieCfdi);
                cmd.Parameters.AddWithValue("@prmFolio", intFolioCfdi);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    dTotalimpuestosret = Convert.ToDecimal(dr["TotalmpuestosRetenidos"]);
                    dTotalimpuestostras = Convert.ToDecimal(dr["TotalImpuestosTrasladados"]);
                    strMonedasID = dr["Moneda"].ToString();

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

        public string NotasdecreditoCancelar()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "NotasdecreditoCancelar";
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

        public string LeeUnaFactura()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FacturasLeeUnafactura";
                cmd.Parameters.AddWithValue("@prmSerie", strSerieCfdi);
                cmd.Parameters.AddWithValue("@prmFolio", intFolioCfdi);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();

                    fFechaFac = Convert.ToDateTime(dr["Fecha"]);
                    intPIva = Convert.ToInt32(dr["Piva"]);
                    intClienteFac = Convert.ToInt32(dr["ClientesID"]);

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

        //Timbrar 

        public string TimbraCfdi33(DataTable dtDetNC, DataTable dtNCFac)
        {        
            
            string strResult = string.Empty;
            string RutaCertificado = string.Empty;
            string RutaKey = string.Empty;
            string PasswordKey = string.Empty;
            string strCondicionesdePago = string.Empty;
            Decimal curDescuento=0;
            string strEmisorNombre = string.Empty;
            string strEmisorRfc = string.Empty;
            string strEmisorRegimenFiscal = string.Empty;
            string strReceptorNombre = string.Empty;
            string strReceptorRfc33 = string.Empty;
            string strResidenciaFiscal33 = string.Empty;
            string strNumRegIdTrib33 = string.Empty;
            string strLugarExp = string.Empty;
            string Sql = string.Empty;       
            string strRutaXml = string.Empty;
            string strRutaXmlTimbrado = string.Empty;

            string strcFormaPago = string.Empty;
            string strcMetodoPago = string.Empty;
            string strcUsoCfdi = string.Empty;
            DateTime Time;
        
            string strResult1 = string.Empty;
            string strResult2 = string.Empty;
            string strResult3 = string.Empty;
            string strResult4 = string.Empty;
            string decMonedaSat = string.Empty;
            string strDirDatos = "C:\\Avc\\cv\\DEMO33";
            int intClaveSAT = 0;
            string strClaveSatMetPago = string.Empty;
            string strClaveSatUsoCfdi = string.Empty;
            int intMes = 0;
            string strMes = string.Empty;
            string strFechaFactura = string.Empty;
            string strSSL33NCR = string.Empty;
            string strXML33NCR = string.Empty;
            string strSerie = string.Empty;
            //Dim clb As New DepositosCL
            string strFacturas = string.Empty;


            cfdiCL cl1 = new cfdiCL();
            globalCL clG = new globalCL();
            //vsFK.vsFinkok vs = new vsFK.vsFinkok();

            
           

            try
            {
                vsFK.vsFinkok vs = new vsFK.vsFinkok();

                strSerie = System.Configuration.ConfigurationManager.AppSettings["Serie"].ToString();

                cl1.pSerie = strSerie;
                strResult2 = cl1.DatosCfdiEmisor();
                if (strResult2 == "OK")
                {
                    strEmisorNombre = cl1.pEmisorNombre;
                    strEmisorRfc = cl1.pEmisorRegFed;
                    strEmisorRegimenFiscal = cl1.pEmisorRegimen;
                    strLugarExp = cl1.pEmisorCP;
                    PasswordKey = cl1.pLlaveCFD;
                }
                else
                {
                    return "No se pudieron leer los datos del Emisor";
                }


               

                vs.Version33 = "4.0";
                vs.Serie33 = "NCR";
                vs.Folio33 = intFolio.ToString();

                int intDia = fFecha.Day;

                string sFechaTimbre = string.Empty;
                sFechaTimbre = fFecha.Year.ToString();
                sFechaTimbre = sFechaTimbre + "-";
                if (fFecha.Month < 10)
                    sFechaTimbre = sFechaTimbre + "0" + fFecha.Month.ToString();
                else
                    sFechaTimbre = sFechaTimbre + fFecha.Month.ToString();

                if (fFecha.Day < 10)
                    sFechaTimbre = sFechaTimbre + "-0" + fFecha.Day.ToString();
                else
                    sFechaTimbre = sFechaTimbre + "-" + fFecha.Day.ToString();

                sFechaTimbre = sFechaTimbre + "T";

                if (DateTime.Now.Hour < 10)
                    sFechaTimbre = sFechaTimbre + "0" + DateTime.Now.Hour.ToString() + ":";
                else
                    sFechaTimbre = sFechaTimbre + DateTime.Now.Hour.ToString() + ":";
                if (DateTime.Now.Minute < 10)
                    sFechaTimbre = sFechaTimbre + "0" + DateTime.Now.Minute.ToString() + ":";
                else
                    sFechaTimbre = sFechaTimbre + DateTime.Now.Minute.ToString() + ":";
                if (DateTime.Now.Second < 10)
                    sFechaTimbre = sFechaTimbre + "0" + DateTime.Now.Second.ToString();
                else
                    sFechaTimbre = sFechaTimbre + DateTime.Now.Second.ToString();

                vs.Fecha33 = sFechaTimbre;


                
                vs.RutaCer33 = System.Configuration.ConfigurationManager.AppSettings["pathcer"].ToString();
                vs.RutaKey33 = System.Configuration.ConfigurationManager.AppSettings["pathkey"].ToString();
                vs.LlaveCSD33 = PasswordKey;

                strRutaXml = System.Configuration.ConfigurationManager.AppSettings["pathxml"].ToString();
                strRutaXml = strRutaXml + fFecha.Year.ToString() + "\\" + clG.NombreDeMes(fFecha.Month,3) + "\\";
                vs.IvaExcento = "N";
                vs.RutaSSL33 = System.Configuration.ConfigurationManager.AppSettings["pathopenssl"].ToString();

                intMes = fFecha.Month;
                strMes = clG.NombreDeMes(fFecha.Month,3);
         

                vs.RutaXML33 = strRutaXml;
                vs.RutaXSLT33 = System.Configuration.ConfigurationManager.AppSettings["pathxslt"].ToString();
                vs.NombreArchivo = "NCRSINTIMBRAR" + intFolio.ToString();

                string strRutaFac = strRutaXml + "NCR" + intFolio.ToString() + ".xml";
                strRutaXmlTimbrado = strRutaFac.Substring(0, strRutaFac.Length - 4) + "timbrado.xml";

                string strRutaXMLTimbradoNCR;
                string strRutaXMLNCR;

                strRutaXMLNCR = vs.RutaXML33 + "NCRSINTIMBRAR" + intFolio.ToString() + ".XML";
                strRutaXMLTimbradoNCR = vs.RutaXML33 + "NCR" + intFolio.ToString() + ".xml";

                vs.MetodoPago33 = "PUE";

                //RECEPTOR
                cl1.pCliente = intClientesID;
                strResult2 = cl1.DatosReceptor();
                if (strResult2 != "OK")
                {
                    return "Error al leer datos de receptor: ID=" + intClientesID.ToString();
                }

                strReceptorNombre = cl1.pReceptorNom40;
                strReceptorRfc33 = cl1.pReceptorRegFed;
                strResidenciaFiscal33 = string.Empty;
                strNumRegIdTrib33 = string.Empty;
                string strReceptorRegFis = cl1.pReceptorRegimenFiscal;
                string strReceptorCP = cl1.pReceptorCP;

                if (File.Exists(strRutaXMLTimbradoNCR))
                {
                    return "Este folio ya se timbro previamente";
                }

                vs.CondicionesDePago33 = "Credito";

                string strMonedaNC = string.Empty;

                decMonedaSat = "MXN";
                strMonedaNC = "MXN";
                vs.TipoCambio33 = "1";

                vs.TipoDeComprobante33 = "E";
                vs.MetodoPago33 = "PUE";
                vs.LugarExpedicion33 = strLugarExp;
                vs.Confirmacion33 = ""; //txtClaveConfirmacion
                vs.Moneda33 = "MXN";

                vs.TipoRelacion33 = "01";

                if (TipoRelacionAlCancelar != "")
                {
                    if (TipoRelacionAlCancelar != "04")
                        return "El tipo de relación debe ser 04";

                    if (UUIDASustituirAlCancelar.Length == 0)
                        return "Debe leer el UUID de la nota de crédito que se va a cancelar para sustituirlo";

                    vs.TipoRelacionParaCancelar = TipoRelacionAlCancelar;
                    vs.DocumentosRelacionadosParaCancelar = 1;
                    vs.set_UUID33ParaCancelar(0, UUIDASustituirAlCancelar);
                }

                //Dim clnc As New NotasCreditoCL
                string strRutaXmlfac = System.Configuration.ConfigurationManager.AppSettings["pathxml"].ToString();
                int intYear = 0;
                string strSerieFactura = string.Empty;
                DateTime dFechaFac;
                decimal decPIva = 0;
                string strPIva = string.Empty;
                string strUUID = string.Empty;
                string sMonedaFac = string.Empty;
               
                DepositosCL cld = new DepositosCL();
                
                int i = 0;
                foreach (DataRow dr in dtDetNC.Rows)
                {
                    strSerieCfdi = dr["Seriecfdi"].ToString();
                    intFolioCfdi = Convert.ToInt32(dr["Foliocfdi"]);

                    strResult1 = LeeUnaFactura();

                    strFacturas = strFacturas + strSerieCfdi + intFolioCfdi.ToString() + " ";

                    if (strResult1 == "OK")
                        dFechaFac = fFechaFac;
                    else
                        return "No se pudo leer la factura: " + strSerieCfdi + intFolioCfdi.ToString();

                    if (intClientesID != intClienteFac)
                        return "La factura " + strSerieCfdi + intFolioCfdi.ToString() + " no es del mismo cliente";

                    intYear = dFechaFac.Year;
                    intMes = dFechaFac.Month;
                    strMes = clG.NombreDeMes(intMes,3);
                    strRutaFac = strRutaXmlfac + intYear.ToString() + "\\" + strMes + "\\" + strSerieCfdi + intFolioCfdi.ToString() + "Timbrado.xml";
                    strRutaXmlTimbrado = strRutaFac;  //.Substring(0, strRutaFac.Length - 4) + "timbrado.xml";

                    strUUID = vs.ExtraeValor(strRutaXmlTimbrado, "tfd:TimbreFiscalDigital", "UUID");
                    vs.set_UUID33(i, strUUID);

                    
                    if (strUUID.Length == 0)
                    {
                        return "No se pudo leer el XML de la factura " + strSerieCfdi + intFolioCfdi.ToString();
                    }

                    sMonedaFac = vs.ExtraeValor(strRutaXmlTimbrado, "cfdi:Comprobante", "Moneda");

                    if (sMonedaFac!=strMonedasID)
                    {
                        return "La moneda de la factura: " + sMonedaFac + " es diferente a la de la nota de crédito";
                    }

                    strPIva = vs.ExtraeValor(strRutaXmlTimbrado, "cfdi:Traslado", "TasaOCuota");

                    if (i == 0)
                        vs.FormaPago33 = vs.ExtraeValor(strRutaXmlTimbrado, "cfdi:Comprobante", "FormaPago");
                    i = i + 1;
                } //For

                if (strc_FP=="0")
                {
                    if (vs.FormaPago33 == "")
                        vs.FormaPago33 = "03";
                }
                else
                {
                    vs.FormaPago33 = strc_FP;
                }
                

                decPIva = Convert.ToDecimal(strPIva);

                vs.DocumentosRelacionados = i;

                vs.EmisorNombre33 = strEmisorNombre;
                vs.EmisorRfc33 = strEmisorRfc;
                vs.EmisorRegimenFiscal33 = strEmisorRegimenFiscal;

                vs.ReceptorNombre = strReceptorNombre;
                vs.ReceptorRfc33 = strReceptorRfc33;
                vs.ResidenciaFiscal33 = "";
                vs.NumRegIdTrib33 = "";
                vs.UsoCfdi33 = "G02";
                vs.RegimenFiscalReceptor = strReceptorRegFis;
                vs.DomicilioFiscalReceptor = strReceptorCP;
                vs.Exportacion40 = "01";


                curDescuento = 0;

                decimal curST = 0;
                decimal curIva = 0;
                decimal decIvaImp = 0;
                string strUm = string.Empty;
                string strClaveSatUM = string.Empty;
                string strClaveProdServ = string.Empty;
                decimal decCant = 0;
                decimal decPrecio = 0;
                decimal decImp = 0;
                decimal decPtjeIva = 0;
               

                i = 0;

                foreach (DataRow dr in dtDetNC.Rows) {

                    //if (dr["ARTICULO"].ToString().Length > 0)
                    //    vs.set_NoIdentificacion_Conceptos33(i, dr["ARTICULO"].ToString());
                    //else
                        vs.set_NoIdentificacion_Conceptos33(i, "SINART");

                    //strUm = vs.ExtraeValor(strRutaXmlTimbrado, "cfdi:Concepto", "Unidad");
                    //if (strUm.Length == 0)
                        strUm = "SERVICIO";

                    strClaveSatUM = "ACT";
                    strClaveProdServ = "84111506";

                    decCant = Convert.ToDecimal(dr["Cantidad"]);
                    decPrecio = Convert.ToDecimal(dr["Precio"]);
                    decImp = Math.Round(decCant * decPrecio, 2);

                    vs.set_ClaveProdServ_Conceptos33(i, strClaveProdServ);
                    vs.set_Cantidad_Conceptos33(i, decCant.ToString("######0.00"));
                    vs.set_ClaveUnidad_Conceptos33(i, strClaveSatUM);
                    vs.set_Unidad_Conceptos33(i, strUm);
                    vs.set_Descripcion_Conceptos33(i, dr["Descripcion"].ToString());
                    vs.set_ValorUnitario_Conceptos33(i, decPrecio.ToString("######0.00"));
                    vs.set_Importe_Conceptos33(i, decImp.ToString("######0.00"));
                    vs.set_Descuento_Conceptos33(i, "");
                    vs.set_TrasladoBase_Conceptos33(i, decImp.ToString("######0.00"));
                    vs.set_TrasladoImpuesto_Conceptos33(i, "002");
                    vs.set_TrasladoTipofactor_Conceptos33(i, "Tasa");
                    vs.set_Excento_Conceptos33(i, "N");


                    if (decPIva > 0)
                    {
                        vs.set_TrasladoTasaOCuota_Conceptos33(i, "0.160000");
                        decPtjeIva = Convert.ToDecimal(0.16);
                    }
                    else
                    {
                        vs.set_TrasladoTasaOCuota_Conceptos33(i, "0.000000");
                        decPtjeIva = 0;
                    }


                    decIvaImp = Math.Round(decImp * decPtjeIva, 2);
                    vs.set_TrasladoImporte_Conceptos33(i, decIvaImp.ToString("########0.00"));

                    vs.set_NumeroPedimento_Conceptos33(i, "");

                    curDescuento = curDescuento + 0;
                    curST = curST + decImp;
                    curIva = curIva + decIvaImp;

                    i = i + 1;

                } // For

                vs.Renglones33 = i;

                vs.Descuento33 = "";
                vs.SubTotal33 = Math.Round(curST - curDescuento, 2).ToString("########0.00"); 
                vs.Total33 = Math.Round(curST + curIva, 2).ToString("########0.00");

                vs.CuentaPredial33 = "";

                strResult2 = vs.cfdi40();



                if (strResult2 == "OK")
                {
                    if (strEmisorRfc == "EKU9003173C9")
                    {
                        strResult2 = vs.DemoTimbrado("jzambrano@live.com.mx", "Jzg1411*", strRutaXMLNCR, strRutaXMLTimbradoNCR);
                    }
                    else
                    {
                        strResult2 = vs.Timbra("jzambrano@live.com.mx", "Jzg1411*", strRutaXMLNCR, strRutaXMLTimbradoNCR);
                    }

                    //strResult2 = vs.Timbra("jzambrano@live.com.mx", "Jzg1411*", strRutaXMLNCR, strRutaXMLTimbradoNCR);
                    if (strResult2 != "Ok")
                        strResult2 = "Al timbrar:" + strResult2;
                }
                else
                {
                    strResult2 = "Al firmar:" + strResult2;
                }

                return strResult2;
        }
        catch(Exception ex)
        {
            return "Al timbrar:" + ex.Message;
        }                   
    }

        //Eof:Timbrar

        #endregion
    }
}