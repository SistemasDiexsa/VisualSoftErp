using System;
using System.IO;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;
using System.Windows.Forms;
using System.Web.ModelBinding;

namespace VisualSoftErp.Clases
{
    public class cfdiCL
    {
        #region Propiedades
        string strcnn = globalCL.gv_strcnn;

        public string sOrigen { get; set; }  // S=Factura de servicios   G=Factura de guias
        public int Timbrar { get; set; }
        public string pSerie { get; set; }
        public int pCliente { get; set; }
        public string pMoneda { get; set; }
        public decimal pTipocambio { get; set; }
        public int pFolio { get; set; }
        public string puuid { get; set; }
        public string pexttotal { get; set; }
        public string pextsubtotal { get; set; }
        public string pextiva { get; set; }
        public string pextretiva { get; set; }
        int pIDCxC { get; set; }
        public int pFolioPedido { get; set; }

        public string pEmisorNombre { get; set; }
        public string pEmisorRegFed { get; set; }
        public string pEmisorCP { get; set; }
        public string pLlaveCFD { get; set; }
        public string pEmisorRegimen { get; set; }
        public string pIvaexcento { get; set; }

        public string pReceptorRegFed { get; set; }
        public string pReceptorNombre { get; set; }
        public string pReceptorCiudad { get; set; }
        public string pReceptorDireccion { get; set; }
        public string pReceptorCP { get; set; }
        public string strReceptorRegimen { get; set; }
        public string pUsoCfdi { get; set; }
        public string pMetodoPago { get; set; }
        public string pTiporelacion { get; set; }
        public string pUuidrelacionado { get; set; }
        public string pFormaPago { get; set; }
        public int pPlazo { get; set; }        
        string pclaveSatUM { get; set; }
        string pclaveSatArt { get; set; }
        public int pServiciosId { get; set; }
        string pUMInterna { get; set; }
        string strresult = string.Empty;
        public string sNomServicio { get; set; }
        public string strArt { get; set; }

        public decimal dSTXML { get; set; }
        public decimal dIvaXML { get; set; }
        public decimal dRetIvaXML { get; set; }
        public decimal dNetoXML { get; set; }

        public string pCalle { get; set; }
        public string pNumExt { get; set; }
        public string pNumInt { get; set; }
        public string pColonia { get; set; }
        public string pLocalidad { get; set; }
        public string pMunicipio { get; set; }
        public string pEstado { get; set; }
        public string pPais { get; set; }
        public string pCP { get; set; }
        public string pEmisorNombre40 { get; set; }
        public string pReceptorCalle { get; set; }
        public string pReceptorNumExt { get; set; }
        public string pReceptorNumInt { get; set; }
        public string pReceptorColonia { get; set; }
        public string pReceptorLocalidad { get; set; }
        public string pReceptorEstado { get; set; }
        public string pReceptorPais { get; set; }
        public string pNumReglDTrib { get; set; }
        public string pReceptorRegimenFiscal { get; set; }
        public string pReceptorNom40 { get; set; }
      

        #endregion

        public string SiguienteFactura()
        {
            string strresult = string.Empty;
                
             SqlConnection cnn = new SqlConnection();
             cnn.ConnectionString = strcnn;
             cnn.Open();

             SqlCommand cmd = new SqlCommand();
             cmd.Connection = cnn;
             cmd.CommandType = System.Data.CommandType.StoredProcedure;
             cmd.CommandText = "FacturasObtenSiguienteId";
             cmd.Parameters.AddWithValue("@prmSerie", pSerie);

             SqlDataReader dr = cmd.ExecuteReader();
             if (dr.HasRows)
             {
                 dr.Read();
                 pFolio = Convert.ToInt32(dr["MF"]);
                 pIDCxC = Convert.ToInt32(dr["MCxC"]);


                strresult = "OK";

             }
             else
             {
                 strresult ="No Read";
             }

             dr.Close();
             cnn.Close();
             return strresult;

        } //SiguienteFactura()

        public string LeeClienteporRfc()
        {
            string strresult = string.Empty;

            SqlConnection cnn = new SqlConnection();
            cnn.ConnectionString = strcnn;
            cnn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "ClientesLeePorRfc";
            cmd.Parameters.AddWithValue("@prmRfc", pReceptorRegFed);

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                pReceptorNombre = dr["Nombre"].ToString();


                strresult = "OK";

            }
            else
            {
                strresult = "No Read";
            }

            dr.Close();
            cnn.Close();
            return strresult;

        } //SiguienteFactura()

        public string DatosCfdiEmisor()
        {
            string strresult = string.Empty;

            SqlConnection cnn = new SqlConnection();
            cnn.ConnectionString = strcnn;
            cnn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "DatosCfdiEmisor_leer";
            cmd.Parameters.AddWithValue("@prmSerie", pSerie);

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                pEmisorNombre = dr["Nombre"].ToString();
                pEmisorRegFed = dr["Rfc"].ToString();
                pEmisorCP = dr["Lugarexpedicion"].ToString();
                pLlaveCFD = dr["llave"].ToString();
                pEmisorRegimen = dr["Regimenfiscal"].ToString();
                pIvaexcento = dr["Ivaexcento"].ToString();

                pCalle = dr["Calle"].ToString();
                pNumExt = dr["NumExt"].ToString();
                pNumInt = dr["NumInt"].ToString();
                pColonia = dr["Colonia"].ToString();
                pLocalidad = dr["Localidad"].ToString();
                pMunicipio = dr["Municipio"].ToString();
                pEstado = dr["Estado"].ToString();
                pPais = dr["Pais"].ToString();
                pCP = dr["CP"].ToString();
                pEmisorNombre40 = dr["Nombre40"].ToString();

                strresult = "OK";

            }
            else
            {
                strresult = "No Read";
            }

            dr.Close();
            cnn.Close();
            return strresult;

        } //DatosCfdiEmisor()

        public string DatosReceptor()
        {
            try
            {

            
                string strresult = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strcnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Clientes_LeeUnCliente";
                cmd.Parameters.AddWithValue("@prmClientesID", pCliente);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    pReceptorRegFed = dr["Rfc"].ToString();
                    pReceptorNombre = dr["Nombre"].ToString();
                    //pUsoCfdi = dr["Usocfdi"].ToString();
                    //pMetodoPago = dr["cMetodopago"].ToString();
                    //pFormaPago = dr["cFormapago"].ToString();
                    pPlazo = Convert.ToInt32(dr["Plazo"]);
                    pReceptorDireccion = dr["Direccion"].ToString();
                    pReceptorCiudad = dr["Ciudad"].ToString();
                    pReceptorCP = dr["CP"].ToString();
                    pReceptorCalle = dr["Calle"].ToString();
                    pReceptorNumExt = dr["NumExt"].ToString();
                    pReceptorNumInt = dr["NumInt"].ToString();
                    pReceptorColonia = dr["Colonia"].ToString();
                    pReceptorLocalidad = dr["Localidad"].ToString();
                    pReceptorEstado = dr["Estado"].ToString();
                    pReceptorPais = dr["Pais"].ToString();
                    pNumReglDTrib = dr["NumReglDTrib"].ToString();
                    pReceptorRegimenFiscal = dr["RegimenFiscal"].ToString();
                    pReceptorNom40 = dr["Nombre40"].ToString();


                    //Revisar con javier el origen 
                    //if (sOrigen!="N")
                    //{
                    //    pUsoCfdi = dr["Usocfdi"].ToString();
                    //    pMetodoPago = dr["cMetodopago"].ToString();
                    //    pFormaPago = dr["cFormapago"].ToString();
                    //    pPlazo = Convert.ToInt32(dr["Plazo"]);
                    //}


                    strresult = "OK";

                }
                else
                {
                    strresult = "No Read";
                }

                dr.Close();
                cnn.Close();
                return strresult;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        } //DatosReceptor()

        public DataTable CCEDetalle()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strcnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PedidosComercioExteriorLeeDetalleParaFacturar";
                cmd.Parameters.AddWithValue("@prmSerie", pSerie);
                cmd.Parameters.AddWithValue("@prmFolio", pFolioPedido);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //fletesGrid

        public string ServiciosLeeUnServicio()
        {
            string strresult = string.Empty;

            SqlConnection cnn = new SqlConnection();
            cnn.ConnectionString = strcnn;
            cnn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;            
            cmd.CommandText = "ArticulosLeeUnArticulo";   
            cmd.Parameters.AddWithValue("@prmArticulosID", pServiciosId);            
            cmd.Parameters.AddWithValue("@prmCte", pCliente);

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();

                try
                {
                    pclaveSatArt = dr["ClaveSat"].ToString();
                }
                catch (Exception)
                {
                    strresult = dr["result"].ToString();
                }

                if (pclaveSatArt.Substring(0,5)== "Error")
                {
                    strresult= dr["ClaveSat"].ToString();
                }
                else {

                    pclaveSatUM = dr["UMClaveSat"].ToString();
                    pUMInterna = dr["UM"].ToString();
                    sNomServicio = dr["Nombre"].ToString();
                    strArt = dr["Articulo"].ToString();

                    strresult = "OK";
                }
               

            }
            else
            {
                strresult = "No Read";
            }

            dr.Close();
            cnn.Close();
            return strresult;

        } //ServiciosLeeUnServicio()

        private string NombreDeMes(int Mes)
        {
            switch (Mes) {
                case 1:
                    return "ENE";
                case 2:
                    return "FEB";
                case 3:
                    return "MAR";
                case 4:
                    return "ABR";
                case 5:
                    return "MAY";
                case 6:
                    return "JUN";
                case 7:
                    return "JUL";
                case 8:
                    return "AGO";
                case 9:
                    return "SEP";
                case 10:
                    return "OCT";
                case 11:
                    return "NOV";
                case 12:
                    return "DIC";
                default:
                    return "";
            }
        }

        public string GeneraCfdi33(DataTable dtfDet,bool bCCE,int intPublicoengeneral,string strCfdiVer)
        {
            try
            {

                string strPedimento = string.Empty;
                string strDesglosaIva = string.Empty;
                string strUUID = string.Empty;
                string strPathCodeBar = string.Empty;
                int intRens = 0;
                decimal decSubTotal = 0;
                decimal decTIva = 0;
                decimal decTIeps = 0;
                decimal pIva = 0;

                string pRutaXSLT;
                string pRutaXML;
                string pRutaCer;
                string pRutaKey;
                string pRutaOpenSSL;



                // Datos del emisor
                if (DatosCfdiEmisor() != "OK")
                {
                    return "No se pudo leer datos CFDI Emisor";
                }


                // Datos del receptor                   
                if (DatosReceptor() != "OK")
                {
                    return "No se pudieron leer los datos rel receptor";
                }


                vsFK.vsFinkok vs = new vsFK.vsFinkok();

                strCfdiVer = "4.0";
                vs.Version33 = strCfdiVer; // "3.3";
                vs.Serie33 = pSerie;
                vs.Folio33 = pFolio.ToString();
                vs.NombreArchivo = pSerie + pFolio.ToString() + "sintimbrar";

                string sYear;
                string sMes;
                string sDia;
                string sHora;
                string sMin;
                string sSeg;

                sYear = DateTime.Now.Year.ToString();
                sMes = DateTime.Now.Month.ToString();
                sDia = DateTime.Now.Day.ToString();
                sHora = DateTime.Now.Hour.ToString();
                sMin = DateTime.Now.Minute.ToString();
                sSeg = DateTime.Now.Second.ToString();

                if (sMes.Length < 2)
                {
                    sMes = "0" + sMes;
                }
                if (sDia.Length < 2)
                {
                    sDia = "0" + sDia;
                }


                if (sHora.Length < 2)
                {
                    sHora = "0" + sHora;
                }
                if (sMin.Length < 2)
                {
                    sMin = "0" + sMin;
                }
                if (sSeg.Length < 2)
                {
                    sSeg = "0" + sSeg;
                }

                //Nota: verificar que ponga en formato de 24 horas la hora
                vs.Fecha33 = sYear + "-" + sMes + "-" + sDia + "T" + sHora + ":" + sMin + ":" + sSeg;

                vs.FormaPago33 = pFormaPago;
                vs.IvaExcento = pIvaexcento;

                pRutaXSLT = System.Configuration.ConfigurationManager.AppSettings["pathxslt40"];

                pRutaXML = System.Configuration.ConfigurationManager.AppSettings["pathxml"];
                pRutaCer = System.Configuration.ConfigurationManager.AppSettings["pathcer"];
                pRutaKey = System.Configuration.ConfigurationManager.AppSettings["pathkey"];
                pRutaOpenSSL = System.Configuration.ConfigurationManager.AppSettings["pathopenssl"];

                // Se verifica que exista el directorio del año  
                string sPath = pRutaXML + sYear + "\\" + NombreDeMes(Convert.ToInt32(sMes));
                if (!System.IO.Directory.Exists(sPath))
                {
                    System.IO.Directory.CreateDirectory(sPath);
                }


                vs.RutaCer33 = pRutaCer;
                vs.RutaKey33 = pRutaKey;
                vs.LlaveCSD33 = pLlaveCFD;
                vs.RutaSSL33 = pRutaOpenSSL;
                vs.RutaXSLT33 = pRutaXSLT;
                vs.RutaXML33 = sPath + "\\";

                vs.CondicionesDePago33 = pPlazo.ToString() + " DIAS";
                vs.Moneda33 = pMoneda;
                vs.TipoCambio33 = pTipocambio.ToString();
                vs.TipoDeComprobante33 = "I";
                vs.MetodoPago33 = pMetodoPago;
                vs.Confirmacion33 = "";

                vs.TipoRelacion33 = pTiporelacion;
                vs.set_UUID33(0, pUuidrelacionado);
                if (pTiporelacion.Length > 0 && pUuidrelacionado.Length > 0)
                {
                    vs.DocumentosRelacionados = 1;
                }

                vs.EmisorNombre33 = pEmisorNombre40;
                vs.EmisorRfc33 = pEmisorRegFed;
                vs.EmisorRegimenFiscal33 = pEmisorRegimen;
                vs.LugarExpedicion33 = pEmisorCP;

                // PARA FACTURAR AL SERVER DE PRUEBAS
                string ambiente = ConfigurationManager.AppSettings["Ambiente"];
                if (ambiente != "Productivo")
                {
                    vs.EmisorNombre33 = "ESCUELA KEMPER URGATE";
                    vs.EmisorRfc33 = "EKU9003173C9";
                    vs.EmisorRegimenFiscal33 = "601";
                    vs.LugarExpedicion33 = "20928";
                    pEmisorRegFed = "EKU9003173C9";
                    vs.LlaveCSD33 = "12345678a";
                }

                
                vs.ReceptorNombre = pReceptorNom40;
                vs.RegimenFiscalReceptor = pReceptorRegimenFiscal;
                vs.DomicilioFiscalReceptor = pReceptorCP.ToString();
                // vs.DomicilioFiscalReceptor = "20928";     // DESCOMENTAR PARA PRUEBAS EN DESARROLLO
                vs.Exportacion40 = "01";
                vs.ReceptorRfc33 = pReceptorRegFed;

                if (intPublicoengeneral == 1)
                {
                    vs.ReceptorNombre = "VENTAS AL PUBLICO EN GENERAL";
                    vs.ReceptorRfc33 = "XAXX010101000";
                    vs.DomicilioFiscalReceptor = ambiente != "Productivo" ? "20928" : pEmisorCP;
                    vs.RegimenFiscalReceptor = "616";
                    pUsoCfdi =  "S01";
                }
                vs.UsoCfdi33 = pUsoCfdi;

                if (pNumReglDTrib.Length > 3)
                {
                    vs.NumRegIdTrib33 = pNumReglDTrib;
                    vs.ResidenciaFiscal33 = pReceptorPais;
                }
                else
                {
                    vs.NumRegIdTrib33 = "";
                    vs.ResidenciaFiscal33 = "";
                }

                if (pReceptorRegFed != "XAXX010101000" & pReceptorRegFed != "XEXX010101000") {
                    strDesglosaIva = "S";
                }
                else {
                    strDesglosaIva = "N";
                }

                bool blnHayRegistros = false;               
                decimal decIva = 0;
                decimal decRetIva = 0;               
                decimal decPrecio = 0;
                decimal decStImp = 0;
                decimal decCant = 0;               
                string sConcepto = string.Empty;
                decimal decTRetIva = 0;
                decimal pIeps = 0;
                decimal decDescuento = 0;
                decimal decTotDescuento = 0;
                

                //aqui se recorren los renglones del datatable que recibimos como parametro
                foreach (DataRow row in dtfDet.Rows)
                {                  

                    blnHayRegistros = true;

                    decCant = Convert.ToDecimal(row["Cantidad"]);
                    decPrecio = Convert.ToDecimal(row["ValorUnitario"]);
                    sConcepto = row["Descripcion"].ToString();
                    pServiciosId = Convert.ToInt32(row["ArticulosID"]);
                    decDescuento = Convert.ToDecimal(row["Descuento"]);
                    strresult = ServiciosLeeUnServicio();
                    if (strresult!="OK")
                    {
                        return "No se pudo leer el articulo: " + pServiciosId.ToString() + " " + strresult;
                    }
                    decStImp = Convert.ToDecimal(row["Importe"]);
                    decIva = Convert.ToDecimal(row["Iva"]);
                    decRetIva = Convert.ToDecimal(row["RetIva"]);

                    vs.set_ClaveProdServ_Conceptos33(intRens, pclaveSatArt);
                    vs.set_NoIdentificacion_Conceptos33(intRens, strArt);
                    vs.set_Cantidad_Conceptos33(intRens, decCant.ToString());
                    vs.set_ClaveUnidad_Conceptos33(intRens, pclaveSatUM);
                    vs.set_Unidad_Conceptos33(intRens, pUMInterna);
                    vs.set_Descripcion_Conceptos33(intRens, sConcepto);
                    vs.set_ValorUnitario_Conceptos33(intRens, decPrecio.ToString("######0.00"));
                    vs.set_Importe_Conceptos33(intRens, decStImp.ToString("######0.00"));
                    vs.set_Excento_Conceptos33(intRens, pIvaexcento);

                    if (decDescuento>0)
                        vs.set_Descuento_Conceptos33(intRens, decDescuento.ToString("######0.00"));
                    else
                        vs.set_Descuento_Conceptos33(intRens, "");
                    if (decIva > 0) {
                        vs.set_TrasladoBase_Conceptos33(intRens, (decStImp-decDescuento).ToString("######0.00"));
                        vs.set_TrasladoImpuesto_Conceptos33(intRens, "002");
                        vs.set_TrasladoTipofactor_Conceptos33(intRens, "Tasa");
                        vs.set_TrasladoTasaOCuota_Conceptos33(intRens, "0.160000");
                        vs.set_TrasladoImporte_Conceptos33(intRens, decIva.ToString("######0.00"));
                            pIva = Convert.ToDecimal(0.16);
                        }
                    else 
                        {
                        vs.set_TrasladoBase_Conceptos33(intRens, decStImp.ToString("######0.00"));
                        vs.set_TrasladoImpuesto_Conceptos33(intRens, "002");
                        vs.set_TrasladoTipofactor_Conceptos33(intRens, "Tasa");
                        vs.set_TrasladoTasaOCuota_Conceptos33(intRens, "0.000000");
                        vs.set_TrasladoImporte_Conceptos33(intRens, "0");
                        pIva = 0;
                        }

                    pIeps = 0;

                    //--- RET IVA ----
                    if (decRetIva > 0) {
                        vs.set_RetencionIvaBase_Conceptos33(intRens, decStImp.ToString("######0.00"));
                        vs.set_RetencionIvaImpuesto_Conceptos33(intRens, "002");
                        vs.set_RetencionIvaTipoFactor_Conceptos33(intRens, "Tasa");
                        vs.set_RetencionIvaTasaOCuota_Conceptos33(intRens, "0.040000");
                        vs.set_RetencionIvaImporte_Conceptos33(intRens, decRetIva.ToString("######0.00"));

                        decTRetIva = decTRetIva + decRetIva;
                    }
                    else
                    {

                        vs.set_RetencionIvaBase_Conceptos33(intRens, decStImp.ToString("######0.00"));
                        vs.set_RetencionIvaImpuesto_Conceptos33(intRens, "002");
                        vs.set_RetencionIvaTipoFactor_Conceptos33(intRens, "Tasa");
                        vs.set_RetencionIvaTasaOCuota_Conceptos33(intRens, "0.000000");
                        vs.set_RetencionIvaImporte_Conceptos33(intRens, "0.00");
                    }

                    vs.set_NumeroPedimento_Conceptos33(intRens, "");

                    decTotDescuento += Math.Round(decDescuento, 6);
                    decSubTotal += Math.Round(decStImp, 6);
                    decTIva += Math.Round(decIva, 6);

                    intRens = intRens + 1;

                } /*foreach*/

                //--------- Complemento de comercio exterior -----------//
                if (bCCE)
                {
                    PedidosComercioExteriorCL clce = new PedidosComercioExteriorCL();
                    clce.strSerie = pSerie;
                    clce.intFolio = pFolioPedido;
                    strresult = clce.PedidosComercioExteriorLlenaCajas();

                    if (strresult == "OK")
                    {
                        vs.Exportacion40 = clce.strTipoExportacion;
                        vs.ComplementoComercioExterior = "S";
                        vs.ECEMotivoTraslado = "";

                        vs.sCEVersion = "2.0";
                        vs.SCEObservaciones = clce.strceObs;
                        vs.ECEIncoterm = clce.strIncoterm;


                        vs.ICECertificadoOrigen = clce.strCertificadororigen;
                        vs.SCENumCertificadoOrigen = clce.strNumcertificadoorigen;
                        vs.ECETipodeCambioUSD = pTipocambio.ToString("#####0.0000");
                        vs.ECEClavedePedimento = clce.strClavedepedimento;

                        // vs.ECETipoOperacion = clce.strTipooperacion;
                        // vs.ICESubDivision = clce.strSubdivision;
                        

                        vs.SCENumeroExportadorConfiable = clce.strNumexportadorconfiable;
                        //--- Emisor ---
                        vs.eCECurp = "";
                        vs.ceDomEmisorsCalle = pCalle;
                        vs.ceDomEmisorsNumeroExterior = pNumExt;
                        vs.ceDomEmisorsNumeroInterior = pNumInt;
                        vs.ceDomEmisorsColonia = pColonia;
                        vs.ceDomEmisorsLocalidad = pLocalidad;
                        vs.ceDomEmisorsReferencia = "";
                        vs.ceDomEmisoreMunicipio = pMunicipio;
                        vs.ceDomEmisoreEstado = pEstado;
                        vs.ceDomEmisorePais = pPais;
                        vs.ceDomEmisorcCP = pEmisorCP;

                        //--- Receptor ---                   
                        //vs.sCENumRegIdTrib = "";
                        vs.sCENumRegIdTrib = pNumReglDTrib;
                        vs.ceDomReceptorsCalle = pReceptorCalle;
                        vs.ceDomReceptorsNumeroExterior = pReceptorNumExt;
                        vs.ceDomReceptorsNumeroInterior = pReceptorNumInt;
                        vs.ceDomReceptorsColonia = pReceptorColonia;
                        vs.ceDomReceptorsLocalidad = pReceptorLocalidad;
                        vs.ceDomReceptorsReferencia = "";
                        vs.ceDomReceptoreMunicipio = pReceptorLocalidad;
                        vs.ceDomReceptoreEstado = pReceptorEstado;
                        vs.ceDomReceptorePais = pReceptorPais;
                        vs.ceDomReceptorcCP= pReceptorCP.ToString();

                        decimal curTotDlsCe = 0;
                        decimal valordolares = 0;

                        DataTable dtc = new DataTable();
                        dtc = CCEDetalle();
                        int i = 0;
                        foreach(DataRow dr in dtc.Rows)
                        {
                            valordolares = Convert.ToDecimal(dr["CantidadAduana"]) * Convert.ToDecimal(dr["ValorUnitarioAduana"]);


                            vs.set_ceMciasNoIdentificacion(i, dr["Articulo"].ToString());
                            vs.set_ceMciaeFraccionArancelaria(i, dr["FraccionArancelaria"].ToString());
                            vs.set_ceMciadCantidadAduana(i, Convert.ToDecimal(dr["CantidadAduana"]).ToString("#####0.000"));
                            vs.set_ceMciaeUnidadAduana(i, dr["UnidadAduana"].ToString());
                            vs.set_ceMciadValorUnitarioAduana(i, Convert.ToDecimal(dr["ValorUnitarioAduana"]).ToString("#####0.000000"));
                            vs.set_ceMciadValorDolares(i, valordolares.ToString("#####0.0000"));

                            curTotDlsCe += valordolares;


                            i++;
                        }

                        vs.ECETotalUSD = curTotDlsCe.ToString("#####0.00");
                        vs.ceRenglones = intRens;
                    }
                    else
                        return "No se pudieron leer los datos de comercio exterior";
                    

                }
                else
                    vs.ComplementoComercioExterior = "N";
                //Eof:CCE


                if (blnHayRegistros == false)
                {
                    return "No se encontraron renglones para timbrar";
                }

                vs.SubTotal33 = decSubTotal.ToString("#####0.00");
                if (decTotDescuento > 0)
                    vs.Descuento33 = decTotDescuento.ToString("#####0.00");
                else
                    vs.Descuento33 = "";
                vs.Total33 = (decSubTotal + decTIva + decTIeps).ToString("#####0.00");

                vs.Renglones33 = intRens;

                vs.set_NumeroPedimento_Conceptos33(1, "");

                vs.CuentaPredial33 = "";// mandar valores del cb

                // if (strCfdiVer=="3.3")
                //   strresult = vs.cfdi33(); //sellado y firmado                    
                //else

                if (ambiente == "Desarrollo" || pEmisorRegFed == "EKU9003173C9")
                {
                    MessageBox.Show("Se encuentra en el servidor de pruebas. Avise a Sistemas.", "Modo Demo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                strresult = vs.cfdi40(); //sellado y firmado   

                if (strresult == "OK")
                {

                    string strrutaxmltimbrado;
                    string strXml;
                    strXml = pRutaXML + sYear + "\\" + NombreDeMes(Convert.ToInt32(sMes)) + "\\" + pSerie + pFolio + "sintimbrar.xml";
                    strrutaxmltimbrado = pRutaXML + sYear + "\\" + NombreDeMes(Convert.ToInt32(sMes)) + "\\" + pSerie + pFolio + "timbrado.xml";

                    if (System.IO.File.Exists(strrutaxmltimbrado))
                    {
                        return "Este folio ya se timbró previamente";
                    }

                    if (pEmisorRegFed == "EKU9003173C9" || ambiente == "Desarrollo")
                    {
                        strresult = vs.DemoTimbrado("jzambrano@live.com.mx", "Jzg1411*", strXml, strrutaxmltimbrado);
                    }                            
                    else
                    {
                        strresult = vs.Timbra("jzambrano@live.com.mx", "Jzg1411*", strXml, strrutaxmltimbrado);
                    }


                    if (strresult == "Ok") {

                        System.IO.File.Delete(strXml);

                        string dato = string.Empty;
                        dato= vs.ExtraeValor(strrutaxmltimbrado, "cfdi:Comprobante", "SubTotal");
                        if (dato!="")
                        {
                            dSTXML = Convert.ToDecimal(dato);
                        }
                        dato = vs.ExtraeValor(strrutaxmltimbrado, "cfdi:Comprobante", "Total");
                        if (dato != "")
                        {
                            dNetoXML = Convert.ToDecimal(dato);
                        }
                        dato = vs.ExtraeValor(strrutaxmltimbrado, "cfdi:Impuestos", "TotalImpuestosRetenidos");
                        if (dato != "")
                        {
                            dRetIvaXML = Convert.ToDecimal(dato);
                        }
                        dato = vs.ExtraeValor(strrutaxmltimbrado, "cfdi:Impuestos", "TotalImpuestosTrasladados");
                        if (dato != "")
                        {
                            dIvaXML = Convert.ToDecimal(dato);
                        }
                        //puuid = vs.ExtraeValor(strrutaxmltimbrado, "tfd:TimbreFiscalDigital", "UUID");
                        //pexttotal = vs.ExtraeValor(strrutaxmltimbrado, "cfdi:Comprobante", "Total");
                        //pextsubtotal = vs.ExtraeValor(strrutaxmltimbrado, "cfdi:Comprobante", "SubTotal");
                        //pextiva = vs.ExtraeValor(strrutaxmltimbrado, "cfdi:Impuestos", "TotalImpuestosTrasladados");
                        //pextretiva = vs.ExtraeValor(strrutaxmltimbrado, "cfdi:Impuestos", "TotalImpuestosRetenidos");

                        //pVersionTimbre = vs.ExtraeValor(strrutaxmltimbrado, "cfdi:Comprobante", "Version")
                        return "OK";
                    }
                    else
                    {
                        return "Error en vs.Timbra: " + strresult;
                    }
                }
                else {
                    return "Error en vs.cfdi40: " + strresult;
                }                                                              
            } // Try
            catch(Exception ex)
            {
                return "Error en GeneraCfdi33: " + ex.Message + "\n línea: " + ex.LineNumber().ToString();
            }
        } //GeneraCfdi33

    } //public class cfdiCL
} //namespace ViajesNet.Clases