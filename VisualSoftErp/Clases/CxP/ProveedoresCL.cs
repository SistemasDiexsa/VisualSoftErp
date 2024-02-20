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
    public class ProveedoresCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
        public int intProveedoresID { get; set; }
        public DateTime fFechaInfo { get; set; }
        public DateTime fFechaInfoI { get; set; }
        public string strNombre { get; set; }
        public string strDireccion { get; set; }
        public int intCiudadesID { get; set; }
        public int intPais { get; set; }
        public string strRfc { get; set; }
        public string strContacto { get; set; }
        public int intTipo { get; set; }
        public int intPLazo { get; set; }
        public decimal intCreditoautorizado { get; set; }
        public string strTelefono { get; set; }
        public string strEmail { get; set; }
        public string strMonedasID { get; set; }
        public DateTime fFechaderegistro { get; set; }
        //public int intTiempodeentrega { get; set; }
        public int intDiasTraslado { get; set; }
        public int intPiva { get; set; }
        public int intIvaEstricto { get; set; }
        public int intRetIvaEstricto { get; set; }
        public string strObservaciones { get; set; }
        public string strClasificacion { get; set; }
        public decimal decRetiva { get; set; }
        public decimal decRetisr { get; set; }
        public int intEsdeservicio { get; set; }
        public int intActivo { get; set; }
        public string strCorrespondenciaA { get; set; }
        public string strLab { get; set; }
        public int intVia { get; set; }
        public int intBancosID { get; set; }
        public string strCuentabancaria { get; set; }
        public string strc_Formapago { get; set; }
        public string strc_Metodopago { get; set; }
        public string strc_Usocfdi { get; set; }
        public int intVenceEnBaseA { get; set; }
        public DateTime Fechaingreso { get; set; }
        public int intTransportesID { get; set; }
        //public int intDiasentrega { get; set; }
        public int intFormaentrega { get; set; }
        public int intAuxCompras { get; set; }
        public string strCuentaMN { get; set; }
        public string strCuentaExt { get; set; }
        public string strCuentaGastos { get; set; }
        public string strCuentaComp { get; set; }
        public decimal dMesesdeconsumo { get; set; }
        public decimal dIncrementales { get; set; }
        public decimal dDiasdesurtido { get; set; }
        public decimal dDiasdetraslado { get; set; }
        //public decimal dDiasStock { get; set; }
        public DataTable dtd { get; set; }
        #endregion

        #region Constructor
        public ProveedoresCL()
        {
            intVenceEnBaseA = 0;
            intProveedoresID = 0;
            strNombre = string.Empty;
            strDireccion = string.Empty;
            intCiudadesID = 0;
            strRfc = string.Empty;
            strContacto = string.Empty;
            intTipo = 0;
            intPLazo = 0;
            intCreditoautorizado = 0;
            strTelefono = string.Empty;
            strEmail = string.Empty;
            strMonedasID = string.Empty;
            fFechaderegistro = DateTime.Now;
            //intTiempodeentrega = 0;
            intDiasTraslado = 0;
            intPiva = 0;
            strObservaciones = string.Empty;
            strClasificacion = string.Empty;
            decRetiva = 0;
            decRetisr = 0;
            intEsdeservicio = 0;
            intActivo = 0;
            strCorrespondenciaA = string.Empty;
            strLab = string.Empty;
            intVia = 0;
            intBancosID = 0;
            strCuentabancaria = string.Empty;
            strc_Formapago = string.Empty;
            strc_Metodopago = string.Empty;
            strc_Usocfdi = string.Empty;
            Fechaingreso = DateTime.Now;
            intTransportesID = 0;
            //intDiasentrega = 0;
            intFormaentrega = 0;
            intAuxCompras = 0;
            strCuentaMN = string.Empty;
            strCuentaExt = string.Empty;
            strCuentaGastos = string.Empty;
            strCuentaComp = string.Empty;
            dMesesdeconsumo = 0;
            dIncrementales = 0;
            dDiasdesurtido = 0;
            dDiasdetraslado = 0;
            //dDiasStock = 0;
            dtd = null;
            intIvaEstricto = 0;
            intRetIvaEstricto = 0;
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
        public DataTable ProveedoresGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "proveedoresGrid";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //proveedoresGrid

        public string ProveedoresCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ProveedoresCRUD";
                cmd.Parameters.AddWithValue("@prmProveedoresID", intProveedoresID);
                cmd.Parameters.AddWithValue("@prmNombre", strNombre);
                cmd.Parameters.AddWithValue("@prmDireccion", strDireccion);
                cmd.Parameters.AddWithValue("@prmCiudadesID", intCiudadesID);
                cmd.Parameters.AddWithValue("@prmRfc", strRfc);
                cmd.Parameters.AddWithValue("@prmContacto", strContacto);
                cmd.Parameters.AddWithValue("@prmTipo", intTipo);
                cmd.Parameters.AddWithValue("@prmPLazo", intPLazo);
                cmd.Parameters.AddWithValue("@prmCreditoautorizado", intCreditoautorizado);
                cmd.Parameters.AddWithValue("@prmTelefono", strTelefono);
                cmd.Parameters.AddWithValue("@prmEmail", strEmail);
                cmd.Parameters.AddWithValue("@prmMonedasID", strMonedasID);
                cmd.Parameters.AddWithValue("@prmFechaderegistro", fFechaderegistro);
                //cmd.Parameters.AddWithValue("@prmTiempodeentrega", intTiempodeentrega);
                cmd.Parameters.AddWithValue("@prmDiasTraslado", intDiasTraslado);
                cmd.Parameters.AddWithValue("@prmPiva", intPiva);
                cmd.Parameters.AddWithValue("@prmObservaciones", strObservaciones);
                cmd.Parameters.AddWithValue("@prmClasificacion", strClasificacion);
                cmd.Parameters.AddWithValue("@prmRetiva", decRetiva);
                cmd.Parameters.AddWithValue("@prmRetisr", decRetisr);
                cmd.Parameters.AddWithValue("@prmEsdeservicio", intEsdeservicio);
                cmd.Parameters.AddWithValue("@prmCorrespondenciaA", strCorrespondenciaA);
                cmd.Parameters.AddWithValue("@prmLab", strLab);
                cmd.Parameters.AddWithValue("@prmVia", intVia);
                cmd.Parameters.AddWithValue("@prmBancosID", intBancosID);
                cmd.Parameters.AddWithValue("@prmCuentabancaria", strCuentabancaria);
                cmd.Parameters.AddWithValue("@prmc_Formapago", strc_Formapago);
                cmd.Parameters.AddWithValue("@prmc_Metodopago", strc_Metodopago);
                cmd.Parameters.AddWithValue("@prmc_Usocfdi", strc_Usocfdi);
                cmd.Parameters.AddWithValue("@prmTransportesID", intTransportesID);
                //cmd.Parameters.AddWithValue("@prmDiasentrega", intDiasentrega);
                cmd.Parameters.AddWithValue("@prmFormaentrega", intFormaentrega);
                cmd.Parameters.AddWithValue("@prmAuxCompras", intAuxCompras);
                cmd.Parameters.AddWithValue("@prmCuentaMN", strCuentaMN);
                cmd.Parameters.AddWithValue("@prmCuentaExt", strCuentaExt);
                cmd.Parameters.AddWithValue("@prmCuentaGastos", strCuentaGastos);
                cmd.Parameters.AddWithValue("@prmCuentaComp", strCuentaComp);
                cmd.Parameters.AddWithValue("@prmMesesdeconsumo", dMesesdeconsumo);
                cmd.Parameters.AddWithValue("@prmincrementales", dIncrementales);
                cmd.Parameters.AddWithValue("@prmDiasdesurtido", dDiasdesurtido);
                cmd.Parameters.AddWithValue("@prmDiasdetraslado", dDiasdetraslado);
                //cmd.Parameters.AddWithValue("@prmDiasstock", dDiasStock);
                cmd.Parameters.AddWithValue("@prmActivo", intActivo);
                cmd.Parameters.AddWithValue("@prmIvaEstricto", intIvaEstricto);
                cmd.Parameters.AddWithValue("@prmRetIvaEstricto", intRetIvaEstricto);
                cmd.Parameters.AddWithValue("@prmPais", intPais);
                cmd.Parameters.AddWithValue("@prmVenceenBaseA", intVenceEnBaseA);
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
        } //ProveedoresCrud

        public string ProveedoresLlenaCajas()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ProveedoresllenaCajas";
                cmd.Parameters.AddWithValue("@prmProveedoresID", intProveedoresID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    intProveedoresID = Convert.ToInt32(dr["ProveedoresID"]);
                    strNombre = dr["Nombre"].ToString();
                    strDireccion = dr["Direccion"].ToString();
                    intCiudadesID = Convert.ToInt32(dr["CiudadesID"]);
                    strRfc = dr["Rfc"].ToString();
                    strContacto = dr["Contacto"].ToString();
                    intTipo = Convert.ToInt32(dr["Tipo"]);
                    intPLazo = Convert.ToInt32(dr["PLazo"]);
                    intCreditoautorizado = Convert.ToDecimal(dr["CreditoAutorizado"]);
                    strTelefono = dr["Telefono"].ToString();
                    strEmail = dr["Email"].ToString();
                    strMonedasID = dr["MonedasID"].ToString();
                    fFechaderegistro = Convert.ToDateTime(dr["Fechaderegistro"]);
                    //intTiempodeentrega = Convert.ToInt32(dr["Tiempodeentrega"]);
                    intDiasTraslado = Convert.ToInt32(dr["DiasTraslado"]);
                    intPiva = Convert.ToInt32(dr["Piva"]);
                    strObservaciones = dr["Observaciones"].ToString();
                    strClasificacion = dr["Clasificacion"].ToString();
                    decRetiva = Convert.ToDecimal(dr["Retiva"]);
                    decRetisr = Convert.ToDecimal(dr["Retisr"]);
                    intEsdeservicio = Convert.ToInt32(dr["Esdeservicio"]);
                    strCorrespondenciaA = dr["CorrespondenciaA"].ToString();
                    strLab = dr["Lab"].ToString();
                    intVia = Convert.ToInt32(dr["Via"]);
                    intBancosID = Convert.ToInt32(dr["BancosID"]);
                    strCuentabancaria = dr["Cuentabancaria"].ToString();
                    strc_Formapago = dr["c_Formapago"].ToString();
                    strc_Metodopago = dr["c_Metodopago"].ToString();
                    strc_Usocfdi = dr["c_Usocfdi"].ToString();
                    intActivo = Convert.ToInt32(dr["Activo"]);

                    intTransportesID = Convert.ToInt32(dr["TransportesID"]);
                    //intDiasentrega = Convert.ToInt32(dr["Diasentrega"]);
                    intFormaentrega = Convert.ToInt32(dr["Formaentrega"]);
                    intAuxCompras = Convert.ToInt32(dr["AuxCompras"]);
                    strCuentaMN = dr["CuentaMN"].ToString();
                    strCuentaExt = dr["CuentaExt"].ToString();
                    strCuentaGastos = dr["CuentaGastos"].ToString();
                    strCuentaComp = dr["CuentaComp"].ToString();
                    dMesesdeconsumo = Convert.ToDecimal(dr["Mesesdeconsumo"]);
                    dIncrementales = Convert.ToDecimal(dr["Incrementales"]);
                    dDiasdesurtido = Convert.ToDecimal(dr["Diasdesurtido"]);
                    dDiasdetraslado = Convert.ToDecimal(dr["Diasdetraslado"]);
                    intIvaEstricto= Convert.ToInt32(dr["IvaEstricto"]);
                    intRetIvaEstricto= Convert.ToInt32(dr["RetIvaEstricto"]);
                    intPais = Convert.ToInt32(dr["PaisesID"]);
                    intVenceEnBaseA = Convert.ToInt32(dr["VenceenBaseA"]);
                    //dDiasStock = Convert.ToDecimal(dr["Diasstock"]);
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

        public string ProveedoresEliminar()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ProveedoresEliminar";
                cmd.Parameters.AddWithValue("@prmProveedoresID", intProveedoresID);
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

        public string ProveedoresActualizaTesk()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ProveedoresActualizaTesk";
                cmd.Parameters.AddWithValue("@prmProv", intProveedoresID);
                cmd.Parameters.AddWithValue("@prmCta", strCuentaMN); //Cuenta tesk cuando se da de alta
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

        public DataTable ProveedoresBuscarArticulos()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ProveedoresArticulosGRID";
                cmd.Parameters.AddWithValue("@prmProvee", intProveedoresID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                dtd = dt;
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //proveedoresGrid

        public string ProveedoresArticulosCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ProveedoresArticulosCRUD";
                cmd.Parameters.AddWithValue("@prmProveedoresArticulos", dtd);
                cmd.Parameters.AddWithValue("@prmProveedoresID", intProveedoresID);
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

        //InfoProc -----------------------------------------------
        public DataTable infoProveedores()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "infoProv";
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

        public DataTable infoProveedoresCompras()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "infoProveedoresCompras";
                cmd.Parameters.AddWithValue("@prmProveedoresID", intProveedoresID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //clientesGrid

        public string ProveedoresInfoCxP()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ProveedoresInfoCxP";
                cmd.Parameters.AddWithValue("@prmProveedoresID", intProveedoresID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    strNombre = dr["Nombre"].ToString();
                    intPLazo = Convert.ToInt32(dr["PLazo"]);
                    intCreditoautorizado = Convert.ToInt32(dr["Creditoautorizado"]);
                    strTelefono = dr["Telefono"].ToString();
                    strEmail = dr["Email"].ToString();

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

        public DataTable infoProveedoresCxpAntiguedaddeSaldosRep()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CxpAntiguedaddeSaldosRep";
                cmd.Parameters.AddWithValue("@prmEmp", 1);
                cmd.Parameters.AddWithValue("@prmSuc", 0);
                cmd.Parameters.AddWithValue("@prmFechacorte", fFechaInfo);
                cmd.Parameters.AddWithValue("@prmProvIn", intProveedoresID);
                cmd.Parameters.AddWithValue("@prmProvFin", intProveedoresID);
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

        public DataTable infoProveedoresMovimientosCxP()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CxPEstadodecuentaRep";
                cmd.Parameters.AddWithValue("@prmEmp", 1);
                cmd.Parameters.AddWithValue("@prmSuc", 0);
                cmd.Parameters.AddWithValue("@prmFI", fFechaInfoI);
                cmd.Parameters.AddWithValue("@prmFF", fFechaInfo);
                cmd.Parameters.AddWithValue("@prmProv", intProveedoresID);
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

        #endregion
    }
}