using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualSoftErp.Clases;

namespace VisualSoftErp.Herramientas.Clases
{
    public class addendasCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
        public string strProveedor { get; set; }
        public string strProvSap { get; set; }
        public string strCentro { get; set; }
        public string strNumEnt { get; set; }
        public DateTime dFechaEnt { get; set; }
        public string strSerie { get; set; }
        public int intFolioPedido { get; set; }
        public string strRem { get; set; }
        public int intFolioFac { get; set; }

        #endregion
        #region Constructor
        public addendasCL()
        {
            strProveedor = string.Empty;
            strProvSap = string.Empty;
            strCentro = string.Empty;
            strNumEnt = string.Empty;
            dFechaEnt = DateTime.Now;
            strSerie = string.Empty;
            intFolioPedido = 0;
            strRem = string.Empty;
            intFolioFac = 0;
        }


        #endregion
        #region Metodos
        public string CasaLeyGrales()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "AddendaCasaLeyGenerales";
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolioPedido);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    result = dr["result"].ToString();
                    if (result=="OK")
                    {
                        strProveedor = dr["Proveedor"].ToString();
                        strProvSap = dr["ProvSap"].ToString();
                        strCentro = dr["Centro"].ToString();



                        strNumEnt = dr["NumEnt"].ToString();
                        dFechaEnt = Convert.ToDateTime(dr["FechaEnt"]);
                        strRem = dr["Remision"].ToString();
                    }

                    dr.Close();
                    cnn.Close();

                    if (strCentro != "NOCAPTURA")
                        result = "OK";
                    else
                        result = "NO HAY DATOS CAPTURADOS PARA LA ADDENDA";
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
        } // CasaLeyGrales
        public DataTable CasaLeyFacturasDetalle()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CasaLey_DetalleFactura";
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolioFac);
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
        } //CasaLeyFacturaDetalle

        public string FacturaObtenPedido()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FacturasObtenPedido";
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolioFac", intFolioFac);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                                            
                    intFolioPedido = Convert.ToInt32(dr["Folio"]);
                    

                    dr.Close();
                    cnn.Close();
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
        } // CasaLeyGrales

        public string AddendaCasaLey(string serieFac,int FolioFac,DateTime FechaFac)
        {
            try
            {
                globalCL clg = new globalCL();
                StringBuilder sb = new StringBuilder();
                string xmlfile = System.Configuration.ConfigurationManager.AppSettings["pathxml"].ToString();
                xmlfile = xmlfile + FechaFac.Year + "\\";
                xmlfile = xmlfile + clg.NombreDeMes(FechaFac.Month,3) + "\\";

                string carpetaXML = xmlfile;

                xmlfile = xmlfile + serieFac + FolioFac.ToString() + "timbrado.xml";

                string Proveedor = string.Empty;
                string ProvSap = string.Empty;
                string Centro = string.Empty;
                string NumEnt = string.Empty;
                DateTime FechaEnt;
                string Remision = string.Empty;

                string serie = string.Empty;

                addendasCL cl = new addendasCL();
                strSerie = "";
                intFolioFac = FolioFac;

                FacturaObtenPedido();
                

                string result = CasaLeyGrales();
                if (result != "OK")
                {
                    return "Error al leer datos generales Casa Ley:" + result;                    
                }

                vsFK.vsFinkok vs = new vsFK.vsFinkok();

        

                //Se trae el detalle de la factura y se mete a un datatable para recorrerlo
                System.Data.DataTable dt = new System.Data.DataTable("Facturasdetalle");
                dt.Columns.Add("Cantidad", Type.GetType("System.Decimal"));
                dt.Columns.Add("UMC", Type.GetType("System.String"));
                dt.Columns.Add("Descuento", Type.GetType("System.Decimal"));
                dt.Columns.Add("ValorUnitario", Type.GetType("System.Decimal"));
                dt.Columns.Add("Iva", Type.GetType("System.Decimal"));
                dt.Columns.Add("PIva", Type.GetType("System.Decimal"));
                dt.Columns.Add("CodigoBarras", Type.GetType("System.String"));
                dt.Columns.Add("CodigoCasaLey", Type.GetType("System.String"));


                decimal Cantidad = 0;
                string UMC = string.Empty;
                decimal Descuento = 0;
                decimal Precio = 0;
                decimal Iva = 0;
                decimal TasaIva = 0;
                string CB = string.Empty;
                string CCL = string.Empty;
                int Ren = 0;

                strSerie = "";
                intFolioFac = FolioFac;
                dt = CasaLeyFacturasDetalle();

                if (dt.Rows.Count == 0)
                {
                    return "Capture artículos para la addenda";
                }

                Proveedor = strProveedor;
                ProvSap = strProvSap;
                Centro = strCentro;
                NumEnt = strNumEnt;
                FechaEnt = dFechaEnt;
                Remision = strRem;

                sb.AppendLine("<cfdi:Addenda>");
                sb.AppendLine("<cley:ADDENDA_CLEY xmlns:cley=\"http://servicios.casaley.com.mx/factura_electronica\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xsi:schemaLocation=\"http://servicios.casaley.com.mx/factura_electronica http://servicios.casaley.com.mx/factura_electronica/XSD_ADDENDA_CASALEY.xsd\" VERSION=\"1.0\" CORREO=\"antonieta.beltran@casaley.com.mx\">");
                sb.AppendLine("<cley:MERCADERIAS>");
                sb.AppendLine("<cley:CD PROVEEDOR=\"" + Proveedor + "\" CENTRO=\"" + Centro + "\" NUMERO_ENTRADA=\"" + NumEnt + "\" FECHA_DE_ENTRADA=\"" + clg.FechaSQL(FechaEnt) + "\" PROVEEDOR_SAP=\"" + ProvSap + "\" NO_REMISION=\"" + Remision + "\" DESCUENTO=\"0.00\"/>");


                //Detalle
                foreach (DataRow dr in dt.Rows)
                {
                    Cantidad = Convert.ToDecimal(dr["Cantidad"]);
                    UMC = dr["UMC"].ToString();

                    if (UMC.Length == 0)
                    {
                        return "Capture datos de casa ley para el artículo: " + dr["Nom"].ToString();
                    }

                    Descuento = Convert.ToDecimal(dr["Descuento"]);
                    Precio = Convert.ToDecimal(dr["ValorUnitario"]);
                    Iva = Convert.ToDecimal(dr["Iva"]);
                    TasaIva = Convert.ToDecimal(dr["PIva"]);
                    CB = dr["CodigoBarras"].ToString();
                    CCL = dr["CodigoCasaLey"].ToString();


                    Ren = Ren + 1;

                    sb.Append("<cley:DETALLE RENGLON=\"" + Ren.ToString() + "\" ");
                    sb.Append("CANTIDAD=\"" + Cantidad.ToString("####0") + "\" ");
                    sb.Append("UMC=\"" + UMC + "\" ");
                    sb.Append("DESCUENTO =\"" + Descuento.ToString("#####0.00") + "\" ");
                    sb.Append("PRECIO_LISTA =\"" + Precio.ToString("#####0.00") + "\" ");
                    sb.Append("IMPUESTO_IVA =\"" + Iva.ToString("#####0.00") + "\" ");
                    sb.AppendLine("TASA_IVA =\"" + TasaIva.ToString() + "\">");
                    sb.AppendLine("<cley:CODBAR_ARTICULO COD_BAR=\"" + CB + "\" />");
                    sb.AppendLine("<cley:CLEY_ARTICULO COD_ARTICULO=\"" + CCL + "\" />");
                    sb.AppendLine("</cley:DETALLE>");

                }



                //Eof:Detalle

                sb.AppendLine("</cley:MERCADERIAS>");
                sb.AppendLine("</cley:ADDENDA_CLEY>");


                vs.Rutacarpeta = carpetaXML;
                vs.NombreArchivo = FolioFac.ToString() + "timbradaAddenda";
                vs.RutaXmlTimbrado = xmlfile;
                vs.Addenda = sb.ToString();
                vs.PegaAddenda();


                return "OK";


            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        //-------------------------------------- Auto Zone Addenda ----------------------------------------
        //Addenda Autozone
        public string AddendaZone(string seriefac, int foliofac, DateTime fechafac)
        {
            try
            {
                globalCL clg = new globalCL();
                string result=string.Empty;
                int año = fechafac.Year;
                int mes = fechafac.Month;
                string rutaxml = System.Configuration.ConfigurationManager.AppSettings["pathxml"].ToString() + año.ToString() + "\\" + clg.NombreDeMes(mes, 3) + "\\";
                string rutaxmltimbrado = rutaxml + seriefac + foliofac.ToString() + "timbrado.xml";

                strSerie = "";
                intFolioFac = foliofac;
                FacturaObtenPedido();

                ZoneAddendaCL cla = new ZoneAddendaCL();
                cla.strSerie = seriefac;
                cla.intFolio = intFolioPedido;
                result = cla.ZoneAddendaLlenaCajas();

                if (result=="OK")
                {
                    string buyer = cla.strBuyer;
                    string deptid = cla.strDeptID;
                    string email = cla.strEmail;
                    string podate = cla.strPoDate;
                    string poid = cla.strPoID;
                    string vendor_id = cla.strVendorID;
                    string version = "1.0";
                

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("<cfdi:Addenda>");
                    sb.AppendLine("<ADDENDA10 xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"");
                    sb.AppendLine(" BUYER=\"" + buyer + "\"");
                    sb.AppendLine(" DEPTID=\"" + deptid + "\"");
                    sb.AppendLine(" EMAIL=\"" + email + "\"");
                    sb.AppendLine(" PODATE=\"" + podate + "\"");
                    sb.AppendLine(" POID=\"" + poid + "\"");
                    sb.AppendLine(" VENDOR_ID=\"" + vendor_id + "\"");
                    sb.AppendLine(" VERSION=\"" + version + "\"");
                    sb.AppendLine(" xsi:noNamespaceSchemaLocation=\"https://azfes.autozone.com/xsd/Addenda_Merch_32.xsd\"/>");            

                    vsFK.vsFinkok vs = new vsFK.vsFinkok();
                    vs.Rutacarpeta = rutaxml;
                    vs.NombreArchivo = foliofac.ToString() + "timbradaAddenda";
                    vs.RutaXmlTimbrado = rutaxmltimbrado;
                    vs.Addenda = sb.ToString();
                    vs.PegaAddenda();

                    return "OK";
                }
                else
                {
                    return "Al leer la addenda de casa ley llena cajas:" + result;
                }




                

            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        

        #endregion
    }
}
