using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using VisualSoftErp.Clases;


namespace ViajesNet.Clases
{
    public class complementodepagoCL
    {
        #region Propiedades
        public int pFolio { get; set; }
        public int intCliente { get; set;}
        public DateTime pFecha { get; set; }
        public string pHora { get; set; }
        public string pImporte { get; set; }
        public string pMoneda { get; set; }
        public string pFP { get; set; }
        public string pParidad { get; set; }
        public string pNumOpe { get; set; }
        public int pBancoidOrd { get; set; }
        public int pBancoidBen { get; set; }
        public string pRfcbancobeneficiario { get; set; }
        public string pCuentaOrdenante { get; set; }
        public string pCuentaBeneficiario { get; set; }
        public string pRfcbancoordentante { get; set; }
        public string pNombrebancoordenante { get; set; }
        #endregion
        //-------------------------- COMPLEMENTO DE PAGO -----------------------
        public string TimbraCfdi33Complemento(GridView grid) { 

        try {

              
                vsFK.vsFinkok vs = new vsFK.vsFinkok();

                string strResult=string.Empty;
                string RutaCertificado = string.Empty;
                string RutaKey = string.Empty;
                string PasswordKey = string.Empty;
                string strCondicionesdePago = string.Empty;
                decimal curDescuento = 0;
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
                string strDirDatos = string.Empty;
                string intClaveSAT = string.Empty;
                string strClaveSatMetPago = string.Empty;
                string strClaveSatUsoCfdi = string.Empty;
                int intMes = 0;
                string strMes = string.Empty;
                string strSerie = System.Configuration.ConfigurationManager.AppSettings["serieCP"].ToString();

                int pCxCId = 0;
                
                string strUUID = string.Empty;
                string strRutaXmlTimbradoCFDI = string.Empty;
                string strCFDISerie = string.Empty;
                string strCFDIFolio = string.Empty;
                decimal decImporte = 0;
                string strMonedaFac = string.Empty;
                string strTCFac = string.Empty;
                string strMPFac = string.Empty;
                string strFechaDoc = string.Empty;
                string strrutaXmlRel = string.Empty;
                string strrutaxml2 = System.Configuration.ConfigurationManager.AppSettings["pathxml"].ToString();
                decimal decSaldo = 0;
                DateTime dFechaDoc=DateTime.Now;

                cfdiCL cl1 = new cfdiCL();
                DepositosCL clb=new DepositosCL();

                cl1.pSerie = System.Configuration.ConfigurationManager.AppSettings["SerieCP"].ToString();
                strResult = cl1.DatosCfdiEmisor();
                if (strResult != "OK")
                {
                    return "DatosCfdiEmisor: " + strResult;
                }
                else
                {
                    vs.EmisorNombre33 = cl1.pEmisorNombre;
                    strEmisorRfc = cl1.pEmisorRegFed;
                    vs.EmisorRfc33 = strEmisorRfc;
                    vs.EmisorRegimenFiscal33 = cl1.pEmisorRegimen;
                    vs.LugarExpedicion33 = cl1.pEmisorCP;
                    PasswordKey = cl1.pLlaveCFD;
                }



                //--Se obtienes datos del receptor --
                cl1.pCliente = intCliente;
                strResult = cl1.DatosReceptor();
                if (strResult == "OK") {
                    vs.ReceptorNombre = cl1.pReceptorNombre;
                    vs.ReceptorRfc33 = cl1.pReceptorRegFed;
                    vs.ResidenciaFiscal33 = "";
                    vs.NumRegIdTrib33 = "";
                    vs.UsoCfdi33 = "P01";
                }
                else
                {
                    return "No se pudo leer los datos del cliente";
                }


                vs.NombreArchivo = strSerie + pFolio.ToString() + "sintimbrar";
                vs.Version33 = "3.3";
                vs.Serie33 = strSerie;
                vs.Folio33 = pFolio.ToString();
                vs.FormaPago33 = "";
                vs.CondicionesDePago33 = "";
                vs.SubTotal33 = "0";
                vs.Descuento33 = "";
                vs.TipoDeComprobante33 = "P";
                vs.MetodoPago33 = "";

               

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

                if (sMin.Length < 2)
                {
                    sMin = "0" + sMin;
                }
                if (sSeg.Length < 2)
                {
                    sSeg = "0" + sSeg;
                }

                if (sHora.Length<2)
                {
                    sHora = "0" + sHora;
                }

                //Nota: verificar que ponga en formato de 24 horas la hora
                vs.Fecha33 = sYear + "-" + sMes + "-" + sDia + "T" + sHora + ":" + sMin + ":" + sSeg;

                vs.RutaCer33 = System.Configuration.ConfigurationManager.AppSettings["pathcer"].ToString();
                vs.RutaKey33 = System.Configuration.ConfigurationManager.AppSettings["pathkey"].ToString();
                vs.LlaveCSD33 = PasswordKey;


                
                strRutaXml = System.Configuration.ConfigurationManager.AppSettings["pathxml"].ToString();
                strRutaXml = strRutaXml + pFecha.Year.ToString() + "\\" + NombreDeMes(pFecha.Month) + "\\";


                vs.IvaExcento = "N"; //Se tiene que revisar para que no sea fijo y lo llenemos de CfdiEmisor
                vs.RutaSSL33 = System.Configuration.ConfigurationManager.AppSettings["pathopenssl"].ToString();

                vs.RutaXML33 = strRutaXml;

               
                if (!System.IO.Directory.Exists(strRutaXml))
                {
                    System.IO.Directory.CreateDirectory(strRutaXml);
                }



                string strRutaGuardarPdf = string.Empty;
                string strRutaXmlTimbradoImp = string.Empty;


                vs.RutaXSLT33 = System.Configuration.ConfigurationManager.AppSettings["pathxslt"].ToString();

                strRutaXml = vs.RutaXML33;
                strRutaXmlTimbrado = strRutaXml + strSerie + pFolio.ToString() + ".xml";
                    //+ "timbrado.xml";
                strRutaXmlTimbradoImp = strRutaXmlTimbrado;

                if (System.IO.File.Exists(strRutaXmlTimbrado))
                {
                    return "Este folio ya se timbro previamente";
                }

              

                vs.Confirmacion33 = "";

                vs.TipoRelacion33 = "";
                vs.DocumentosRelacionados = 0;
                vs.set_UUID33(0, "");

                curDescuento = 0;


                decimal curST = 0;
                decimal curIva = 0;
                decimal decIva = 0;                

                //Concepto
                vs.set_ClaveProdServ_Conceptos33(0, "84111506");
                vs.set_NoIdentificacion_Conceptos33(0, "");
                vs.set_Cantidad_Conceptos33(0, "1");
                vs.set_ClaveUnidad_Conceptos33(0, "ACT");
                vs.set_Unidad_Conceptos33(0, "");
                vs.set_Descripcion_Conceptos33(0, "Pago");
                vs.set_ValorUnitario_Conceptos33(0, "0");
                vs.set_Importe_Conceptos33(0, "0");
                vs.set_Descuento_Conceptos33(0, "");

                //Complemento de pagos
                vs.VersionComplementoNom33 = "1.0";
                string sMonto = Convert.ToDecimal(pImporte).ToString("########0.00");
                if (!sMonto.Contains("."))
                {
                    sMonto = sMonto + ".00";
                }
                vs.set_Montodelpago(0,sMonto);
                vs.Moneda33 = pMoneda;



                strcFormaPago = pFP;

                vs.FormaPago33 = "";
                vs.TipoCambio33 = pParidad;

                //---------------------------------- Datos del deposito ----------------------------------
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

                if (sMin.Length < 2)
                {
                    sMin = "0" + sMin;
                }
                if (sSeg.Length < 2)
                {
                    sSeg = "0" + sSeg;
                }

                //Nota: verificar que ponga en formato de 24 horas la hora
                vs.FechaPago = sYear + "-" + sMes + "-" + sDia + "T" + sHora + ":" + sMin + ":" + sSeg;


                //Fecha del deposito
                sYear = pFecha.Year.ToString();
                sMes = pFecha.Month.ToString();
                sDia = pFecha.Day.ToString();
                sHora = pHora.Substring(0, 2);
                sMin = pHora.Substring(3, 2);
                sSeg = pHora.Substring(6, 2);

                if (sMes.Length < 2)
                {
                    sMes = "0" + sMes;
                }
                if (sDia.Length < 2)
                {
                    sDia = "0" + sDia;
                }

                if (sMin.Length < 2)
                {
                    sMin = "0" + sMin;
                }
                if (sSeg.Length < 2)
                {
                    sSeg = "0" + sSeg;
                }
                vs.set_FechaPagoCP(0,sYear + "-" + sMes + "-" + sDia + "T" + sHora + ":" + sMin + ":" + sSeg);


                vs.set_FormaPago33Cp(0, strcFormaPago);
                vs.set_MonedaPago(0, pMoneda);                
                vs.set_TipoCambio33CP(0, pParidad);
                vs.set_Montodelpago(0, sMonto);
                vs.set_NumOperacion(0, pNumOpe);

                //--- Rfc banco beneficiario 
                if (strcFormaPago == "01")
                {
                    pRfcbancobeneficiario = "";
                    pCuentaBeneficiario = "";
                }
                    vs.set_rfcEmisorCtaBen(0, pRfcbancobeneficiario);
                    vs.set_ctaBeneficiario(0, pCuentaBeneficiario);
                


                //--- Rfc banco ordenante
                


                if (pCuentaOrdenante.Length == 0 || strcFormaPago == "01" || strcFormaPago == "17") {
                   
                        vs.set_RfcEmisorCtaOrd(0, "");
                        vs.set_nomBancoOrdExt(0, "");
                        vs.set_ctaOrdenante(0, "");                  

                }
                else
                {
                    vs.set_RfcEmisorCtaOrd(0, pRfcbancoordentante);
                    vs.set_nomBancoOrdExt(0, pNombrebancoordenante);
                    vs.set_ctaOrdenante(0, pCuentaOrdenante);
                }



                vs.set_tipoCadPago(0, "");
                vs.set_certPago(0, "");
                vs.set_cadPago(0, "");
                vs.set_selloPago(0, "");
                vs.TipoRelacion33 = "";

                int intrenglons = 0;


                //'Documentos relacionados
                foreach (int handle in grid.GetSelectedRows())
                {                  
                    
                        decImporte = Convert.ToDecimal(grid.GetRowCellValue(handle, "APagar"));
                        if (decImporte > 0 )
                        {
                            strCFDISerie = grid.GetRowCellValue(handle, "SerieFactura").ToString();
                            strCFDIFolio = grid.GetRowCellValue(handle, "Factura").ToString();
                            dFechaDoc = Convert.ToDateTime(grid.GetRowCellValue(handle, "Fecha")); // Tomamos la fecha de la CXC por que es la misma que la de la factura
                            strMonedaFac = grid.GetRowCellValue(handle, "Moneda").ToString();
                            decSaldo = Convert.ToDecimal(grid.GetRowCellValue(handle, "APagar"));

                            strrutaXmlRel = strrutaxml2 + dFechaDoc.Year + "\\" + NombreDeMes(dFechaDoc.Month) + "\\";

                            strRutaXmlTimbradoCFDI = strrutaxml2 + dFechaDoc.Year + "\\" + NombreDeMes(dFechaDoc.Month) + "\\" + strCFDISerie + strCFDIFolio + "timbrado.xml";


                            strUUID = vs.ExtraeValor(strRutaXmlTimbradoCFDI, "tfd:TimbreFiscalDigital", "UUID");
                            strMonedaFac = vs.ExtraeValor(strRutaXmlTimbradoCFDI, "cfdi:Comprobante", "Moneda");
                            strMPFac = vs.ExtraeValor(strRutaXmlTimbradoCFDI, "cfdi:Comprobante", "MetodoPago");


                            if (strUUID.Length == 0)
                            {
                                
                                return "No se pudo leer el UUID de la factura:" + strCFDISerie + strCFDIFolio;
                            }

                            //Pendiente manejar pagos con distinta moneda
                            //strTCFac = "";
                            //if (strMonedaFac != pMoneda) {
                            //    strTCFac = Math.Round(1 / CDec(txtMonedaPago.Text), 5)
                            //        }

                            vs.set_idDocumento(0, intrenglons, strUUID);
                            vs.set_SeriePago(0, intrenglons, strCFDISerie);
                            vs.set_FolioPago(0, intrenglons, strCFDIFolio);
                            vs.set_monedaDR(0, intrenglons, strMonedaFac);
                            vs.set_tipoCambioDR(0, intrenglons, strTCFac);
                            vs.set_metodoPagoDR(0, intrenglons, strMPFac);

                            clb.strSerie = strCFDISerie;
                            clb.intFolio = Convert.ToInt32(strCFDIFolio);
                            strResult = clb.Depositosparcialidades();
                            if (strResult != "OK")
                            {
                                return "No se pudo leer las parcialidades de la factura: " + strCFDISerie + strCFDIFolio;
                            }

                            vs.set_numParcialidad(0, intrenglons, clb.iParcialidad.ToString());
                            vs.set_impSaldoAnt(0, intrenglons, decSaldo.ToString("########0.00"));
                            vs.set_impPagado(0, intrenglons, decImporte.ToString("########0.00"));
                            vs.set_impSaldoInsoluto(0, intrenglons, (decSaldo - decImporte).ToString("########0.00"));

                            intrenglons = intrenglons + 1;

                    }
                        
                    
                } // For



                vs.Renglones33 = intrenglons;
                vs.RenglonesPago = 1; //Mandar 2 si hubiera factoraje

                strResult = vs.ComplementoDePagos(1);
               
              
                if (strResult == "OK") {

                    strRutaXmlTimbrado = strRutaXml + strSerie + pFolio.ToString() + ".xml"; ;
                    strRutaXml = strRutaXml + strSerie + pFolio.ToString() + "sintimbrar.xml";
                    
                    if (strEmisorRfc == "LAN7008173R5") {
                        strResult = vs.DemoTimbrado("jzambrano@live.com.mx", "Jzg1411*", strRutaXml, strRutaXmlTimbrado);
                    }
                    else
                    {
                        strResult = vs.Timbra("jzambrano@live.com.mx", "Jzg1411*", strRutaXml, strRutaXmlTimbrado);
                    }


                    if (strResult != "Ok") {

                        strResult = "Al timbrar:" + strResult;
                        return  strResult;
                    }
                    else
                    {
                        File.Delete(strRutaXml);
                        return "OK";
                    }                                          
                }
                else {
                    strResult = "Al firmar:" + strResult;
                    return strResult;
                }
            }
            catch (Exception ex) {
                return ex.Message;
        }
        }

        private string NombreDeMes(int Mes)
        {
            switch (Mes)
            {
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
            }

            return ""; //Aqui nunca va llegar, solo se pone para que no marque error la funcion
        }


    } //class
}