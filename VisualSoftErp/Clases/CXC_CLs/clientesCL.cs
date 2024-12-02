using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml;
using DocumentFormat.OpenXml.Office2013.Excel;

namespace VisualSoftErp.Clases
{
    public class clientesCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
        public int intClientesID { get; set; }
        public string strRfc { get; set; }
        public string strNombre { get; set; }
        public string strDireccion { get; set; }
        public int intZonasID { get; set; }
        public int intSepomexEstadosID { get; set; }
        public int intSepomexCiudadesID { get; set; }
        public int IntSepomexColoniasID { get; set; }
        public int intCiudadesId { get; set; }
        public string strCP { get; set; }
        public string strTelefono { get; set; }
        public string strEMail { get; set; }
        public string strEncabezado { get; set; }
        public string strPie { get; set; }
        public DateTime fFechaingreso { get; set; }
        public int intPlazo { get; set; }
        public decimal intCreditoautorizado { get; set; }
        public string strUsocfdi { get; set; }
        public string strcMetodopago { get; set; }
        public string strcFormapago { get; set; }
        public string strTipocop { get; set; }
        public string strActivo { get; set; }
        public int intAgentesID { get; set; }
        public string strAgente { get; set; }
        public int intListadeprecios { get; set; }
        public int intExportar { get; set; }
        public string strMoneda { get; set; }
        public decimal intPIva { get; set; }
        public decimal intPIeps { get; set; }
        public decimal intPRetiva { get; set; }
        public decimal intPRetisr { get; set; }
        public int intCanalesdeventa { get; set; }
        public int intBancoordenanteID { get; set; }
        public string strCuentaordenante { get; set; }
        public string strCFormapagoDepositos { get; set; }

        public int intPedirmonedaenventas { get; set; }
        public string strResponsabledepagos { get; set; }
        public string strResponsabledecompras { get; set; }
        public int intUEN { get; set; }
        public int intCadena { get; set; }
        public string strCuentapesos { get; set; }
        public string strCuentadolares { get; set; }
        public int intTransportesID { get; set; }
        public string strAtencionA { get; set; }
        public int intTipodeenvio { get; set; }
        public int intEnvioCargo { get; set; }
        public string strProveedorSAP { get; set; }
        public string strRI { get; set; }
        public string strNumeroproveedor { get; set; }
        public string strAddenda { get; set; }
        public string strSerieEle { get; set; }
        public string strBuyerGLN { get; set; }
        public string strTradingPartner { get; set; }
        public string strCalidadProveedor { get; set; }
        public string strCalificadorEDI { get; set; }
        public int intMercado { get; set; }
        public string strSellerGLN { get; set; }
        public decimal dDesctoPP { get; set; }
        public decimal dDescuentoBase { get; set; }
        public int intDiasPlazoBloquear { get; set; }
        public int intDiadepago { get; set; }
        public int intDiaderevision { get; set; }
        public string strHoraPago { get; set; }
        public string strHoraRev { get; set; }
        public decimal dIncremental { get; set; }
        public int intBloqueadoporcartera { get; set; }
        public int intDesglosardescuentoalfacturar { get; set; }
        public string strNumReglDTrib { get; set; }
        public string strResidenciafiscal { get; set; }
        public decimal decDisponible { get; set; }
        public decimal decCreditoAutorizado { get; set; }
        public decimal decPedidosSurtidosSinFacturar { get; set; }
        public decimal decCxC { get; set; }
        public DataTable dtm { get; set; }
        public int intFacturasvencidas { get; set; }
        public decimal intPiva { get; set; }
        public decimal intPieps { get; set; }
        public decimal intPretiva { get; set; }
        public decimal intPretisr { get; set; }
        public int intTesk { get; set; }
        public string strceCalle { get; set; }
        public string strceNumext { get; set; }
        public string strceNumint { get; set; }
        public string strceColonia { get; set; }
        public string strceLocalidad { get; set; }
        public string strceReferencia { get; set; }
        public string strceMunicipio { get; set; }
        public string strceEstado { get; set; }
        public string strcePais { get; set; }
        public string strceCP { get; set; }
        public int intClasificacionesportipoID { get; set; }
        public int intGirosID { get; set; }
        public string strEmailTienda { get; set; }
        public string strRegimenFiscal { get; set; }
        public string strNom40 { get; set; }
        public string strCfdiVer { get; set; }
        public string strRecordatorioCumpleañosDestinatario { get; set; }
        public DateTime dateRecordatorioCumpleañosFecha { get; set; }

        #endregion

        #region Constructor
        public clientesCL()
        {
            strCfdiVer = string.Empty;
            intClientesID = 0;
            strRfc = string.Empty;
            strNombre = string.Empty;
            strDireccion = string.Empty;
            intZonasID = 0;
            intSepomexEstadosID = 0;
            intSepomexCiudadesID = 0;
            IntSepomexColoniasID = 0;
            intCiudadesId = 0;
            strCP = string.Empty;
            strTelefono = string.Empty;
            strEMail = string.Empty;
            DateTime fFechaingreso = DateTime.Now;
            intPlazo = 0;
            intCreditoautorizado = 0;
            strUsocfdi = string.Empty;
            strcMetodopago = string.Empty;
            strcFormapago = string.Empty;
            strTipocop = string.Empty;
            strActivo = string.Empty;
            intAgentesID = 0;
            strAgente = string.Empty;
            intListadeprecios = 0;
            intExportar = 0;
            strMoneda = string.Empty;
            
            intCanalesdeventa = 0;
            intBancoordenanteID = 0;
            string strCuentaordenante = string.Empty;
            intPiva = 0;
            intPieps = 0;
            intPretiva = 0;
            intPretisr = 0;
            intCanalesdeventa = 0;
            intFacturasvencidas = 0;

            strCFormapagoDepositos = string.Empty;
            intPedirmonedaenventas = 0;
            strResponsabledepagos = string.Empty;
            strResponsabledecompras = string.Empty;
            intUEN = 0;
            intCadena = 0;
            strCuentapesos = string.Empty;
            strCuentadolares = string.Empty;
            intTransportesID = 0;
            strAtencionA = string.Empty;
            intTipodeenvio = 0;
            intEnvioCargo = 0;
            strProveedorSAP = string.Empty;
            strRI = string.Empty;
            strNumeroproveedor = string.Empty;
            strAddenda = string.Empty;
            strSerieEle = string.Empty;
            strBuyerGLN = string.Empty;
            strTradingPartner = string.Empty;
            strCalidadProveedor = string.Empty;
            strCalificadorEDI = string.Empty;
            intMercado = 0;
            strSellerGLN = string.Empty;
            dDesctoPP = 0;
            dDescuentoBase = 0;
            intDiasPlazoBloquear = 0;
            intDiadepago = 0;
            intDiaderevision = 0;
            strHoraPago = string.Empty;
            strHoraRev = string.Empty;
            dIncremental = 0;
            intBloqueadoporcartera = 0;
            strNumReglDTrib = string.Empty;
            strResidenciafiscal = string.Empty;
            intDesglosardescuentoalfacturar = 0;
            intTesk = 0;
            strceCalle = string.Empty;
            strceNumext = string.Empty;
            strceNumint = string.Empty;
            strceColonia = string.Empty;
            strceLocalidad = string.Empty;
            strceReferencia = string.Empty;
            strceMunicipio = string.Empty;
            strceEstado = string.Empty;
            strcePais = string.Empty;
            strceCP = string.Empty;
            intClasificacionesportipoID = 0;
            intGirosID = 0;
            strRecordatorioCumpleañosDestinatario = string.Empty;
            dateRecordatorioCumpleañosFecha = DateTime.Now;
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
        public DataTable ClientesGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ClientesGRID";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //clientesGrid


        public string ClientesCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ClientesCRUD";
                cmd.Parameters.AddWithValue("@prmClientesID", intClientesID);
                cmd.Parameters.AddWithValue("@prmRfc", strRfc);
                cmd.Parameters.AddWithValue("@prmNombre", strNombre);
                cmd.Parameters.AddWithValue("@prmDireccion", strDireccion);
                cmd.Parameters.AddWithValue("@prmCiudadesID", intCiudadesId);
                cmd.Parameters.AddWithValue("@prmCP", strCP);
                cmd.Parameters.AddWithValue("@prmTelefono", strTelefono);
                cmd.Parameters.AddWithValue("@prmEMail", strEMail);
                cmd.Parameters.AddWithValue("@prmFechaingreso", fFechaingreso);
                cmd.Parameters.AddWithValue("@prmPlazo", intPlazo);
                cmd.Parameters.AddWithValue("@prmCreditoautorizado", intCreditoautorizado);
                cmd.Parameters.AddWithValue("@prmUsocfdi", strUsocfdi);
                cmd.Parameters.AddWithValue("@prmcMetodopago", strcMetodopago);
                cmd.Parameters.AddWithValue("@prmcFormapago", strcFormapago);
                cmd.Parameters.AddWithValue("@prmTipocop", strTipocop);
                cmd.Parameters.AddWithValue("@prmActivo", strActivo);
                cmd.Parameters.AddWithValue("@prmAgentesID", intAgentesID);
                cmd.Parameters.AddWithValue("@prmListadeprecios", intListadeprecios);
                cmd.Parameters.AddWithValue("@prmExportar", intExportar);
                cmd.Parameters.AddWithValue("@prmMoneda", strMoneda);
                cmd.Parameters.AddWithValue("@prmPIva", intPIva);
                cmd.Parameters.AddWithValue("@prmPIeps", intPIeps);
                cmd.Parameters.AddWithValue("@prmPRetiva", intPRetiva);
                cmd.Parameters.AddWithValue("@prmPRetisr", intPRetisr);         
                cmd.Parameters.AddWithValue("@prmCanalesdeventaID", intCanalesdeventa);
                cmd.Parameters.AddWithValue("@prmBancoordenanteID", intBancoordenanteID);
                cmd.Parameters.AddWithValue("@prmCuentaordenante", strCuentaordenante);
                cmd.Parameters.AddWithValue("@prmcFormapagoDepositos", strCFormapagoDepositos);
                cmd.Parameters.AddWithValue("@prmPedirmonedaenventas", intPedirmonedaenventas);
                cmd.Parameters.AddWithValue("@prmResponsabledepagos", strResponsabledepagos);
                cmd.Parameters.AddWithValue("@prmResponsabledecompras", strResponsabledecompras);
                cmd.Parameters.AddWithValue("@prmUEN", intUEN);
                cmd.Parameters.AddWithValue("@prmCadena", intCadena);
                cmd.Parameters.AddWithValue("@prmCuentapesos", strCuentapesos);
                cmd.Parameters.AddWithValue("@prmCuentadolares", strCuentadolares);
                cmd.Parameters.AddWithValue("@prmTransportesID", intTransportesID);
                cmd.Parameters.AddWithValue("@prmAtencionA", strAtencionA);
                cmd.Parameters.AddWithValue("@prmTipodeenvio", intTipodeenvio);
                cmd.Parameters.AddWithValue("@prmEnvioCargo", intEnvioCargo);
                cmd.Parameters.AddWithValue("@prmProveedorSAP", strProveedorSAP);
                cmd.Parameters.AddWithValue("@prmRI", strRI);
                cmd.Parameters.AddWithValue("@prmNumeroproveedor", strNumeroproveedor);
                cmd.Parameters.AddWithValue("@prmAddenda", strAddenda);
                cmd.Parameters.AddWithValue("@prmSerieEle", strSerieEle);
                cmd.Parameters.AddWithValue("@prmBuyerGLN", strBuyerGLN);
                cmd.Parameters.AddWithValue("@prmTradingPartner", strTradingPartner);
                cmd.Parameters.AddWithValue("@prmCalidadProveedor", strCalidadProveedor);
                cmd.Parameters.AddWithValue("@prmCalificadorEDI", strCalificadorEDI);
                cmd.Parameters.AddWithValue("@prmMercado", intMercado);
                cmd.Parameters.AddWithValue("@prmSellerGLN", strSellerGLN);
                cmd.Parameters.AddWithValue("@prmDesctoPP", dDesctoPP);
                cmd.Parameters.AddWithValue("@prmDescuentoBase", dDescuentoBase);
                cmd.Parameters.AddWithValue("@prmDiasPlazoBloquear", intDiasPlazoBloquear);
                cmd.Parameters.AddWithValue("@prmDiadepago", intDiadepago);
                cmd.Parameters.AddWithValue("@prmDiaderevision", intDiaderevision);
                cmd.Parameters.AddWithValue("@prmHoraPago", strHoraPago);
                cmd.Parameters.AddWithValue("@prmHoraRev", strHoraRev);
                cmd.Parameters.AddWithValue("@prmIncremental", dIncremental);
                cmd.Parameters.AddWithValue("@prmBloqueadoporcartera", intBloqueadoporcartera);
                cmd.Parameters.AddWithValue("@prmNumReglDTrib", strNumReglDTrib);
                cmd.Parameters.AddWithValue("@prmResidenciafiscal", strResidenciafiscal);
                cmd.Parameters.AddWithValue("@prmDesglosardescuentoalfacturar", intDesglosardescuentoalfacturar);
                cmd.Parameters.AddWithValue("@prmTesk", intTesk);
                cmd.Parameters.AddWithValue("@prmCalle", strceCalle);
                cmd.Parameters.AddWithValue("@prmNumExt", strceNumext);
                cmd.Parameters.AddWithValue("@prmNumInt", strceNumint);
                cmd.Parameters.AddWithValue("@prmColonia", strceColonia);
                cmd.Parameters.AddWithValue("@prmLocalidad", strceLocalidad);
                cmd.Parameters.AddWithValue("@prmReferencia", strceReferencia);
                cmd.Parameters.AddWithValue("@prmMunicipio", strceMunicipio);
                cmd.Parameters.AddWithValue("@prmEstado", strceEstado);
                cmd.Parameters.AddWithValue("@prmPais", strcePais);
                cmd.Parameters.AddWithValue("@prmceCP", strceCP);
                cmd.Parameters.AddWithValue("@prmZonasID", intZonasID);
                cmd.Parameters.AddWithValue("@prmSepomexEstadosID", intSepomexEstadosID);
                cmd.Parameters.AddWithValue("@prmSepomexCiudadesID", intSepomexCiudadesID);
                cmd.Parameters.AddWithValue("@prmSepomexColoniasID", IntSepomexColoniasID);
                cmd.Parameters.AddWithValue("@prmClasificacionesportipoID", intClasificacionesportipoID);
                cmd.Parameters.AddWithValue("@prmGirosID", intGirosID);
                cmd.Parameters.AddWithValue("@prmEmailTienda", strEmailTienda);
                cmd.Parameters.AddWithValue("@prmNombre40", strNom40);
                cmd.Parameters.AddWithValue("@prmRegimenFiscal", strRegimenFiscal);
                cmd.Parameters.AddWithValue("@prmCfdiVer", strCfdiVer);
                cmd.Parameters.AddWithValue("@prmRecordatorioCumpleañosDestinatario", strRecordatorioCumpleañosDestinatario);
                cmd.Parameters.AddWithValue("@prmRecordatorioCumpleañosFecha", dateRecordatorioCumpleañosFecha);

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
        } //ClientesCrud

        public string ClientesLlenaCajas()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ClientesllenaCajas";
                cmd.Parameters.AddWithValue("@prmClientesID", intClientesID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    intClientesID = Convert.ToInt32(dr["ClientesID"]);
                    strRfc = dr["Rfc"].ToString();
                    strNombre = dr["Nombre"].ToString();
                    strDireccion = dr["Direccion"].ToString();                    
                    strCP = dr["CP"].ToString();
                    strTelefono = dr["Telefono"].ToString();
                    strEMail = dr["EMail"].ToString();
                    fFechaingreso = Convert.ToDateTime(dr["Fechaingreso"]);
                    intPlazo = Convert.ToInt32(dr["Plazo"]);
                    intCreditoautorizado = Convert.ToInt32(dr["CreditoAutorizado"]);
                    strUsocfdi = dr["Usocfdi"].ToString();
                    strcMetodopago = dr["cMetodopago"].ToString();
                    strcFormapago = dr["cFormapago"].ToString();
                    strTipocop = dr["Tipocop"].ToString();
                    strActivo = dr["Activo"].ToString();
                    intAgentesID = Convert.ToInt32(dr["AgentesID"]);
                    strAgente = dr["AgentesID"].ToString();
                    intListadeprecios = Convert.ToInt32(dr["Listadeprecios"]);
                    intExportar = Convert.ToInt32(dr["Exportar"]);
                    strMoneda = dr["Moneda"].ToString();
                    intPIva = Convert.ToInt32(dr["PIva"]);
                    intPIeps = Convert.ToInt32(dr["PIeps"]);
                    intPRetiva = Convert.ToInt32(dr["PRetiva"]);
                    intPRetisr = Convert.ToInt32(dr["PRetisr"]);
                    intCanalesdeventa = Convert.ToInt32(dr["CanalesdeventaID"]);
                    intBancoordenanteID = Convert.ToInt32(dr["BancoordenanteID"]);
                    strCuentaordenante = dr["Cuentaordenante"].ToString();
                    strCFormapagoDepositos = dr["cFormapagoDepositos"].ToString();
                    intPedirmonedaenventas = Convert.ToInt32(dr["Pedirmonedaenventas"]);
                    strResponsabledepagos = dr["Responsabledepagos"].ToString();
                    strResponsabledecompras = dr["Responsabledecompras"].ToString();
                    intUEN = Convert.ToInt32(dr["UEN"]);
                    intCadena = Convert.ToInt32(dr["Cadena"]);
                    strCuentapesos = dr["TeskCuenta"].ToString();
                    strCuentadolares = dr["Cuentadolares"].ToString();
                    intTransportesID = Convert.ToInt32(dr["TransportesID"]);
                    strAtencionA = dr["AtencionA"].ToString();
                    intTipodeenvio = Convert.ToInt32(dr["Tipodeenvio"]);
                    intEnvioCargo = Convert.ToInt32(dr["EnvioCargo"]);
                    strProveedorSAP = dr["ProveedorSAP"].ToString();
                    strRI = dr["RI"].ToString();
                    strNumeroproveedor = dr["Numeroproveedor"].ToString();
                    strAddenda = dr["Addenda"].ToString();
                    strSerieEle = dr["SerieEle"].ToString();
                    strBuyerGLN = dr["BuyerGLN"].ToString();
                    strTradingPartner = dr["TradingPartner"].ToString();
                    strCalidadProveedor = dr["CalidadProveedor"].ToString();
                    strCalificadorEDI = dr["CalificadorEDI"].ToString();
                    intMercado = Convert.ToInt32(dr["Mercado"]);
                    strSellerGLN = dr["SellerGLN"].ToString();
                    dDesctoPP = Convert.ToDecimal(dr["DesctoPP"]);
                    dDescuentoBase = Convert.ToDecimal(dr["DescuentoBase"]);
                    intDiasPlazoBloquear = Convert.ToInt32(dr["DiasPlazoBloquear"]);
                    intDiadepago = Convert.ToInt32(dr["Diadepago"]);
                    intDiaderevision = Convert.ToInt32(dr["Diaderevision"]);
                    strHoraPago = dr["HoraPago"].ToString();
                    strHoraRev = dr["HoraRev"].ToString();
                    dIncremental = Convert.ToDecimal(dr["Incremental"]);
                    intBloqueadoporcartera = Convert.ToInt32(dr["Bloqueadoporcartera"]);
                    strNumReglDTrib = dr["NumReglDTrib"].ToString();
                    strResidenciafiscal = dr["Residenciafiscal"].ToString();
                    intDesglosardescuentoalfacturar = Convert.ToInt32(dr["Desglosardescuentoalfacturar"]);
                    strceCalle = dr["Calle"].ToString();
                    strceNumext = dr["NumExt"].ToString();
                    strceNumint = dr["NumInt"].ToString();
                    strceColonia = dr["Colonia"].ToString();
                    strceLocalidad = dr["Localidad"].ToString();
                    strceReferencia = dr["Referencia"].ToString();
                    strceMunicipio = dr["Municipio"].ToString();
                    strceEstado = dr["Estado"].ToString();
                    strcePais = dr["Pais"].ToString();
                    strceCP = dr["ceCP"].ToString();
                    intZonasID = Convert.ToInt32(dr["ZonasID"]);
                    intSepomexEstadosID = Convert.ToInt32(dr["SepomexestadosID"]);
                    intSepomexCiudadesID = Convert.ToInt32(dr["SepomexciudadesID"]);
                    IntSepomexColoniasID = Convert.ToInt32(dr["SepomexcoloniasID"]);
                    intClasificacionesportipoID = Convert.ToInt32(dr["ClasificacionesportipoID"]);
                    intGirosID = Convert.ToInt32(dr["GirosID"]);
                    strEmailTienda = dr["EmailTienda"].ToString();
                    intTesk = Convert.ToInt32(dr["Tesk"]);
                    strNom40 = dr["Nombre40"].ToString();
                    strRegimenFiscal = dr["RegimenFiscal"].ToString();
                    strCfdiVer = dr["CfdiVer"].ToString();
                    strRecordatorioCumpleañosDestinatario = dr["RecordatorioCumpleañosDestinatario"].ToString();
                    dateRecordatorioCumpleañosFecha = Convert.ToDateTime(dr["RecordatorioCumpleañosFecha"]);

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

        public string ClientesEliminar()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ClientesEliminar";
                cmd.Parameters.AddWithValue("@prmClientesID", intClientesID);
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

        

        public string clientesVerificaCreditoDisponible()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ClientesCreditoDisponible";
                cmd.Parameters.AddWithValue("@prmCte", intClientesID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    decCreditoAutorizado = Convert.ToDecimal(dr["CreAut"]);
                    decPedidosSurtidosSinFacturar = Convert.ToDecimal(dr["PedidosSurtidosSinFacturar"]);
                    decCxC = Convert.ToDecimal(dr["CxC"]);
                    decDisponible = Convert.ToDecimal(dr["Disponible"]);
                    intFacturasvencidas = Convert.ToInt32(dr["Facturasvencidas"]);
                    intPlazo = Convert.ToInt32(dr["Plazo"]);
                    intBloqueadoporcartera = Convert.ToInt32(dr["Bloqueadoporcartera"]);
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

        public DataTable ClientesAntiguedaddesaldos()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CxCAntiguedaddeSaldosRep";
                cmd.Parameters.AddWithValue("@prmEmp", 0);
                cmd.Parameters.AddWithValue("@prmSuc", 0);
                cmd.Parameters.AddWithValue("@prmFechacorte", DateTime.Now);
                cmd.Parameters.AddWithValue("@prmClienteIn", intClientesID);
                cmd.Parameters.AddWithValue("@prmClienteFin", intClientesID);
                cmd.Parameters.AddWithValue("@prmAgenteIn", 0);
                cmd.Parameters.AddWithValue("@prmAgenteFin", 99999);
                cmd.Parameters.AddWithValue("@prmNivelinfo", 0);
                cmd.Parameters.AddWithValue("@prmDummy", "");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //clientesGrid

        //-------------------------------------------------- InfoCliente-----------------------------//
        public DataTable infoCliente()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "infoCliente";
                cmd.Parameters.AddWithValue("@prmDes", strNombre);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //infoCliemte

        public DataTable infoClienteVentas()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "infoClienteVentas";
                cmd.Parameters.AddWithValue("@prmCte", intClientesID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //clientesGrid
        #endregion
    }
}