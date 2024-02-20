using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualSoftErp.Clases;

namespace VisualSoftErp.Operacion.CyB.Clases
{
    public class cybCL
    {
        #region Propiedades
        private OleDbConnection cnnCyB;
        string strCnn = globalCL.gv_strcnn;
        public int intEje { get; set; }
        public int intMes { get; set; }
        public string strRfc { get; set; }
        public string strCuenta { get; set; }
        public string strCuentaGastos { get; set; }
        public string strPrimerPoliza { get; set; }

        public string strCtaDiot=string.Empty;
        private string sCtaIvaAcreditable = string.Empty;
        private string sCuentaCompras0 = string.Empty;
        private int intPolCompras = 0;
        private string strSerieCompra = string.Empty;
        private int intFolioCompra = 0;
        private int intNivel1 = 0;



        #endregion
        #region Constructor
        public cybCL()
        {
            intEje = 0;
            intMes = 0;
            strRfc = string.Empty;
            strCuenta = string.Empty;
        }
        #endregion
        #region Métodos
        public DataTable Compras()
        {

            LeeGralEmp();

            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ComprasGRIDCyB";
                cmd.Parameters.AddWithValue("@prmEje", intEje);
                cmd.Parameters.AddWithValue("@prmMes", intMes);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //Compras
        public DataTable Servicios()
        {
            string result=LeeGrales();
            LeeGralEmp();

            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ContrarecibosGRIDCyB";
                cmd.Parameters.AddWithValue("@prmEje", intEje);
                cmd.Parameters.AddWithValue("@prmMes", intMes);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex)
            {
                return dt;
            }
        } //Servicios

        public DataTable ContrarecibosDetalle()
        {
            //string result = LeeGrales();
            //LeeGralEmp();

            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ContrarecibosDetalleCyB";
                cmd.Parameters.AddWithValue("@prmSerie", strSerieCompra);
                cmd.Parameters.AddWithValue("@prmFolio", intFolioCompra);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex)
            {
                return dt;
            }
        } //Servicios

        public string ComprasActualizaPoliza()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ComprasActualizaPoliza";
                cmd.Parameters.AddWithValue("@prmSerie", strSerieCompra);
                cmd.Parameters.AddWithValue("@prmFolio", intFolioCompra);
                cmd.Parameters.AddWithValue("@prmPol", intPolCompras);
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
        } // Comprasactualizapoliza
        public string ContrarecibosActualizaPoliza()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ContrarecibosActualizaPoliza";
                cmd.Parameters.AddWithValue("@prmSerie", strSerieCompra);
                cmd.Parameters.AddWithValue("@prmFolio", intFolioCompra);
                cmd.Parameters.AddWithValue("@prmPol", intPolCompras);
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
        } // ContrarecibosActualizaPoliza
        public DataTable PagosCxP()
        {
            string result = LeeGrales();    
            LeeGralEmp();

            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PagosCxPGridCyB";
                cmd.Parameters.AddWithValue("@prmEje", intEje);
                cmd.Parameters.AddWithValue("@prmMes", intMes);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //Compras

        public DataTable PagosCxPDetalle()
        {
            //string result = LeeGrales();
            //LeeGralEmp();

            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PagosCxPDetalleCyB";
                cmd.Parameters.AddWithValue("@prmSerie", strSerieCompra);
                cmd.Parameters.AddWithValue("@prmFolio", intFolioCompra);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
        } //Servicios
        public string PagosCxPActualizaPoliza()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PagosCxPActualizaPoliza";
                cmd.Parameters.AddWithValue("@prmSerie", strSerieCompra);
                cmd.Parameters.AddWithValue("@prmFolio", intFolioCompra);
                cmd.Parameters.AddWithValue("@prmPol", intPolCompras);
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
        } // ContrarecibosActualizaPoliza

        #endregion Métodos
        #region Métodos Access
        private string AbreDBCyB()
        {
            try
            {
                string sPathConta = System.Configuration.ConfigurationManager.AppSettings["pathConta"].ToString();
                //Se abre GralEmp14 para traer datos de control de CxP
                sPathConta = sPathConta + intEje.ToString() + "\\CybMdb2020.Mdb";
                string strcnn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sPathConta;

                cnnCyB = new OleDbConnection();
                cnnCyB.ConnectionString = strcnn;
                cnnCyB.Open();


                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        public string Leecuentacontable()
        {
            try
            {
                
                string result = string.Empty;                
                string Sql;
                if (AbreDBCyB() == "OK")
                {
                    Sql = "Select Cuenta,CuentaGastos from InfoCyP Where Rfc='" + strRfc + "' And Left(Cuenta," + intNivel1.ToString() + ") IN (" + strCtaDiot + ");";
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.Connection = cnnCyB;
                    cmd.CommandText = Sql;

                    OleDbDataReader reader = cmd.ExecuteReader();

                    strCuentaGastos = string.Empty;
                    strCuenta = string.Empty;
                    if (reader.Read())
                    {
                        if (reader["Cuenta"] != DBNull.Value)
                        {
                            strCuenta = reader["Cuenta"].ToString();
                            strCuentaGastos = reader["CuentaGastos"].ToString();
                            result = "OK";
                        }
                        else
                        {
                            strCuenta = "";
                            result = "Nulo";
                        }
                    }
                    else
                    {
                        strCuenta = "";
                        result = "No existe";
                    }

                    reader.Close();

                    cnnCyB.Close();

                    return result;
                }
                else
                {
                    return "No se pudo abrir la base de datos de contabilidad";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string Polizadecompras(GridView gv)
        {
            bool iniTran = false;
            OleDbTransaction transaction = null;

            try
            {
                string Sql = string.Empty;
                string strCpt1 = string.Empty;
                string strCpt2 = string.Empty;
                string strCpt3 = string.Empty;
                int intPoliza = 0;
                string sFactura = string.Empty;
                string Sfecha = string.Empty;
                decimal dSubTotal = 0;
                decimal dIva = 0;
                decimal dSubtotalCero = 0;
                decimal dRetIva = 0;
                decimal dRetIsr = 0;
                decimal dIeps = 0;
                decimal dNeto = 0;
                DateTime dFecha;
                string strMoneda = string.Empty;
                decimal decParidad = 0;
                string strCtaGasto = string.Empty;
                int intSeqPol = 0;
                string pRfc = string.Empty;
                string strCuentaProv = string.Empty;
                string strUUID = string.Empty;
                int intSeqXmlAnexados = 0;
                string strSerie = string.Empty;                
                int intPol = 0;
                int intFolio = 0;

                string result = LeeGrales();
                if (result != "OK")
                {
                    return "No se pudo leer grales2020";
                }

                result=LeeGralEmp();
                if (result != "OK")
                {
                    return "No se pudo leer gralEmp2020";
                }

                if (AbreDBCyB() != "OK")
                {
                    return "No se pudo abrir la BD de contabilidad";
                }

                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = cnnCyB;
                
                //------------------------- CONSECUTIVO DE LA POLIZA ---------------------
                Sql = "Select Max(Poliza) as MP From PolGen Where Ejercicio=" + intEje.ToString() + " And Mes=" + intMes.ToString() + " And Tipo='D';";
                cmd.CommandText = Sql;
                OleDbDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    if (reader["MP"] != DBNull.Value)
                    {
                        intPoliza = Convert.ToInt32(reader["MP"]);
                    }
                    else
                    {
                        reader.Close();
                        return "No se pudo obtener el siguiente número de póliza (null)";
                    }
                }
                else
                {
                    reader.Close();
                    return "No se pudo obtener el siguiente número de póliza (noread)";
                }

                reader.Close();

                intPoliza = intPoliza + 1;

                strPrimerPoliza = intPoliza.ToString();
                
                transaction = cnnCyB.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd.Transaction = transaction;
                iniTran = true;

                for (int i = 0; i <= gv.RowCount - 1; i++)
                {


                    //'------------------------ POLGEN --------------------------

                    strSerie = gv.GetRowCellValue(i, "Serie").ToString();
                    strCpt1 = "COMPRA: " + gv.GetRowCellValue(i, "Serie").ToString() + gv.GetRowCellValue(i, "Folio").ToString();
                    sFactura = gv.GetRowCellValue(i, "Factura").ToString();
                    dFecha = Convert.ToDateTime(gv.GetRowCellValue(i, "Fechafactura"));
                    dSubTotal = Convert.ToDecimal(gv.GetRowCellValue(i, "Subtotal"));
                    dIva = Convert.ToDecimal(gv.GetRowCellValue(i, "Iva"));
                    dNeto = Convert.ToDecimal(gv.GetRowCellValue(i, "Neto"));
                    strMoneda = gv.GetRowCellValue(i, "MonedasID").ToString();
                    decParidad = Convert.ToDecimal(gv.GetRowCellValue(i, "Tipodecambio"));
                    strCtaGasto = gv.GetRowCellValue(i, "CuentaGastoCompra").ToString();
                    strCuentaProv = gv.GetRowCellValue(i, "Cuenta").ToString();
                    pRfc = gv.GetRowCellValue(i, "Rfc").ToString();
                    intPol = Convert.ToInt32(gv.GetRowCellValue(i, "Poliza"));
                    intFolio = Convert.ToInt32(gv.GetRowCellValue(i, "Folio"));

                    Sfecha = dFecha.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);

                    if (intPol==0 && strCtaGasto.Length>0 && strCuentaProv.Length>0)
                    {

                    


                        if (strCpt1.Length > 100)
                            {
                                strCpt1 = strCpt1.Substring(0, 100);
                                strCpt2 = strCpt1.Substring(101);
                            }
                       
                            Sql = "INSERT INTO POLGEN(Ejercicio,Mes,Tipo,Poliza,Cpt1,Cpt2,Cpt3,Capturista,Origen,editando,Conciliado,Fecha,FechaConciliacion,Fiscal,Interno) Values(";
                            Sql = Sql + intEje + ",";
                            Sql = Sql + intMes + ",";
                            Sql = Sql + "'D',";
                            Sql = Sql + intPoliza.ToString() + ",'";
                            Sql = Sql + strCpt1 + "',";
                            Sql = Sql + "'" + strCpt2 + "',";
                            Sql = Sql + "'" + strCpt3 + "',";
                            Sql = Sql + "'AUT',";
                            Sql = Sql + 35 + ",";
                            Sql = Sql + 0 + ",";
                            Sql = Sql + 1 + ",#";
                            Sql = Sql + Sfecha + "#,#";
                            Sql = Sql + Sfecha + "#,";
                            Sql = Sql + 1 + ",";
                            Sql = Sql + 1 + ");";

                            cmd.CommandText = Sql;
                            cmd.ExecuteNonQuery();
                    

                        //'------------------------ POLDET --------------------------
                        //'-------- CARGO A GASTOS / COMPRAS ------------------------

                        if (strMoneda != "USD")
                        {
                            dSubTotal = Math.Round(dSubTotal * decParidad, 2);
                            dIva = Math.Round(dIva * decParidad, 2);
                            dSubtotalCero = Math.Round(dSubtotalCero * decParidad, 2);
                            dRetIva = Math.Round(dRetIva * decParidad, 2);
                            dRetIsr = Math.Round(dRetIsr * decParidad, 2);
                            dIeps = Math.Round(dIeps * decParidad, 2);
                            dNeto = Math.Round(dNeto * decParidad, 2);
                        }

                        if (dSubTotal > 0) // Compras o gastos al 16%
                        {
                            if (Convert.ToDouble(strCtaGasto) == 0)
                            {
                                transaction.Rollback();
                                cnnCyB.Close();                                
                                return "La cuenta de compras / gastos al 16% no puede ser cero o vacía";
                            }

                            intSeqPol = intSeqPol + 1;

                            Sql = "INSERT INTO POLDET(Ejercicio,Mes,Tipo,Poliza,Seq,Cuenta,Fecha,Concepto,Importe,Saldo,Status,CCosto,Rfc,";
                            Sql = Sql + "TipoYPol,Debe,Haber,Ref,Chequera,Cheque,TipoToc,DebeAnt,HaberAnt,ImporteAnt,movBan,Diot,Carro,Otros,OrigenAdm) Values(";
                            Sql = Sql + intEje + ",";
                            Sql = Sql + intMes + ",";
                            Sql = Sql + "'D',";
                            Sql = Sql + intPoliza + ",";
                            Sql = Sql + intSeqPol + ",'";
                            Sql = Sql + strCtaGasto + "',#";
                            Sql = Sql + Sfecha + "#,'";
                            Sql = Sql + strCpt1 + "',";
                            Sql = Sql + dSubTotal + ",'";
                            Sql = Sql + "D" + "','";
                            Sql = Sql + "C" + "',";
                            Sql = Sql + 0 + ",'";
                            Sql = Sql + pRfc + "',";
                            Sql = Sql + "'D" + intPoliza.ToString() + "',";
                            Sql = Sql + dSubTotal + ",";
                            Sql = Sql + 0 + ",'";
                            Sql = Sql + sFactura + "',";
                            Sql = Sql + 0 + ",";
                            Sql = Sql + 0 + ",'";
                            Sql = Sql + "T" + "',";
                            Sql = Sql + 0 + ",";
                            Sql = Sql + 0 + ",";
                            Sql = Sql + 0 + ",";
                            Sql = Sql + 0 + ",";
                            Sql = Sql + 0 + ",";
                            Sql = Sql + 0 + ",";
                            Sql = Sql + 0 + ",";
                            Sql = Sql + 0 + ");";

                            cmd.CommandText = Sql;
                            cmd.ExecuteNonQuery();

                        } // eof:if (dSubTotal>0)

                        if (dSubtotalCero > 0) // Compras o gastos al 0%
                        {
                            if (Convert.ToDouble(sCuentaCompras0) == 0)
                            {
                                transaction.Rollback();
                                cnnCyB.Close();                               
                                return "La cuenta de compras / gastos al 0% no puede ser cero o vacía";
                            }

                            intSeqPol = intSeqPol + 1;

                            Sql = "INSERT INTO POLDET(Ejercicio,Mes,Tipo,Poliza,Seq,Cuenta,Fecha,Concepto,Importe,Saldo,Status,CCosto,Rfc,";
                            Sql = Sql + "TipoYPol,Debe,Haber,Ref,Chequera,Cheque,TipoToc,DebeAnt,HaberAnt,ImporteAnt,movBan,Diot,Carro,Otros,OrigenAdm) Values(";
                            Sql = Sql + intEje + ",";
                            Sql = Sql + intMes + ",";
                            Sql = Sql + "'D',";
                            Sql = Sql + intPoliza + ",";
                            Sql = Sql + intSeqPol + ",'";
                            Sql = Sql + sCuentaCompras0 + "',#";
                            Sql = Sql + Sfecha + "#,'";
                            Sql = Sql + strCpt1 + "',";
                            Sql = Sql + dSubtotalCero + ",'";
                            Sql = Sql + "D" + "','";
                            Sql = Sql + "C" + "',";
                            Sql = Sql + 0 + ",'";
                            Sql = Sql + pRfc + "',";
                            Sql = Sql + "'D" + intPoliza.ToString() + "',";
                            Sql = Sql + dSubtotalCero + ",";
                            Sql = Sql + 0 + ",'";
                            Sql = Sql + sFactura + "',";
                            Sql = Sql + 0 + ",";
                            Sql = Sql + 0 + ",'";
                            Sql = Sql + "T" + "',";
                            Sql = Sql + 0 + ",";
                            Sql = Sql + 0 + ",";
                            Sql = Sql + 0 + ",";
                            Sql = Sql + 0 + ",";
                            Sql = Sql + 0 + ",";
                            Sql = Sql + 0 + ",";
                            Sql = Sql + 0 + ",";
                            Sql = Sql + 0 + ");";

                            cmd.CommandText = Sql;
                            cmd.ExecuteNonQuery();

                        } // eof:if (dSubTotal>0)

                        //'-------- CARGO A IVA ACREDITABLE ------------------------
                        if (dIva > 0)
                        {
                            if (Convert.ToDouble(sCtaIvaAcreditable) == 0)
                            {
                                transaction.Rollback();
                                cnnCyB.Close();                               
                                return "La cuenta de iva acreditable no puede ser cero o vacía";
                            }

                            intSeqPol = intSeqPol + 1;

                            Sql = "INSERT INTO POLDET(Ejercicio,Mes,Tipo,Poliza,Seq,Cuenta,Fecha,Concepto,Importe,Saldo,Status,CCosto,Rfc,";
                            Sql = Sql + "TipoYPol,Debe,Haber,Ref,Chequera,Cheque,TipoToc,DebeAnt,HaberAnt,ImporteAnt,movBan,Diot,Carro,Otros,OrigenAdm) Values(";
                            Sql = Sql + intEje + ",";
                            Sql = Sql + intMes + ",";
                            Sql = Sql + "'D',";
                            Sql = Sql + intPoliza + ",";
                            Sql = Sql + intSeqPol + ",'";
                            Sql = Sql + sCtaIvaAcreditable + "',#";
                            Sql = Sql + Sfecha + "#,'";
                            Sql = Sql + strCpt1 + "',";
                            Sql = Sql + dIva + ",'";
                            Sql = Sql + "D" + "','";
                            Sql = Sql + "C" + "',";
                            Sql = Sql + 0 + ",'";
                            Sql = Sql + pRfc + "',";
                            Sql = Sql + "'D" + intPoliza.ToString() + "',";
                            Sql = Sql + dIva + ",";
                            Sql = Sql + 0 + ",'";
                            Sql = Sql + sFactura + "',";
                            Sql = Sql + 0 + ",";
                            Sql = Sql + 0 + ",'";
                            Sql = Sql + "T" + "',";
                            Sql = Sql + 0 + ",";
                            Sql = Sql + 0 + ",";
                            Sql = Sql + 0 + ",";
                            Sql = Sql + 0 + ",";
                            Sql = Sql + 0 + ",";
                            Sql = Sql + 0 + ",";
                            Sql = Sql + 0 + ",";
                            Sql = Sql + 0 + ");";

                            cmd.CommandText = Sql;
                            cmd.ExecuteNonQuery();
                        }


                        if (Convert.ToDouble(strCuentaProv) == 0)
                        {
                            transaction.Rollback();
                            cnnCyB.Close();                          
                            return "La cuenta del proveedor no puede ser cero o vacía";
                        }

                        intSeqPol = intSeqPol + 1;
                        //'-------- ABONO A PROVEEDORES ------------------------
                        Sql = "INSERT INTO POLDET(Ejercicio,Mes,Tipo,Poliza,Seq,Cuenta,Fecha,Concepto,Importe,Saldo,Status,CCosto,Rfc,";
                        Sql = Sql + "TipoYPol,Debe,Haber,Ref,Chequera,Cheque,TipoToc,DebeAnt,HaberAnt,ImporteAnt,movBan,Diot,Carro,Otros,OrigenAdm) Values(";
                        Sql = Sql + intEje + ",";
                        Sql = Sql + intMes + ",";
                        Sql = Sql + "'D',";
                        Sql = Sql + intPoliza + ",";
                        Sql = Sql + intSeqPol + ",'";
                        Sql = Sql + strCuentaProv + "',#";
                        Sql = Sql + Sfecha + "#,'";
                        Sql = Sql + strCpt1 + "',";
                        Sql = Sql + dNeto + ",'";
                        Sql = Sql + "H" + "','";
                        Sql = Sql + "C" + "',";
                        Sql = Sql + 0 + ",'";
                        Sql = Sql + pRfc + "',";
                        Sql = Sql + "'D" + intPoliza.ToString() + "',";
                        Sql = Sql + 0 + ",";
                        Sql = Sql + dNeto + ",'";
                        Sql = Sql + sFactura + "',";
                        Sql = Sql + 0 + ",";
                        Sql = Sql + 0 + ",'";
                        Sql = Sql + "T" + "',";
                        Sql = Sql + 0 + ",";
                        Sql = Sql + 0 + ",";
                        Sql = Sql + 0 + ",";
                        Sql = Sql + 0 + ",";
                        Sql = Sql + 0 + ",";
                        Sql = Sql + 0 + ",";
                        Sql = Sql + 0 + ",";
                        Sql = Sql + 0 + ");";

                        cmd.CommandText = Sql;
                        cmd.ExecuteNonQuery();


                        //'------------------- XML AnEXADOS ------------------------------

                        if (strUUID.Length>0)
                        {
                            OleDbDataReader dr;

                            //''1.- Se verifica que no exista
                            Sql = "Select UUID from XmlAnexados Where Ejercicio=" + intEje + " And Mes=" + intMes + " And ";
                            Sql = Sql + "Tipo='D' And Poliza=" + intPoliza + " And UUID='" + strUUID + "';";

                            cmd.CommandText = Sql;
                            dr = cmd.ExecuteReader();

                            if (!dr.HasRows)
                            {
                                dr.Close();
                                //'    '2- Se busca el siguiente consecutivo para el año, mes, tipo de poliza y poliza
                                Sql = "Select Max(Seq) as MS From XmlAnexados Where Ejercicio=" + intEje + " And Mes=" + intMes + " And ";
                                Sql = Sql + "Tipo='D' And Poliza=" + intPoliza + ";";
                                cmd.CommandText = Sql;
                                dr = cmd.ExecuteReader();

                                if (dr.HasRows)
                                {
                                    dr.Read();
                                    if (dr["MS"] == DBNull.Value)
                                    {
                                        intSeqXmlAnexados = 1;
                                    }
                                    else
                                    {
                                        intSeqXmlAnexados = Convert.ToInt32(dr["MS"]) + 1;
                                    }
                                }
                                else
                                {
                                    intSeqXmlAnexados = 1;
                                }

                                dr.Close();

                                //'    '3- Se inerta en XMLANEXADOS
                                Sql = "Insert into XMLAnexados(Ejercicio,Mes,Tipo,Poliza,Seq,Cuenta,UUID,Monto,Rfc,Origen,ArchivoXML,ClteoProv,Serie,Folio,CuentaGastos16,CuentaGastos0) Values(";
                                Sql = Sql + intEje + ",";
                                Sql = Sql + intMes + ",";
                                Sql = Sql + "'D',";
                                Sql = Sql + intPoliza + ",";
                                Sql = Sql + intSeqXmlAnexados + ",'";
                                Sql = Sql + strCuentaProv + "','";
                                Sql = Sql + strUUID + "','";
                                Sql = Sql + dNeto.ToString() + "','";
                                Sql = Sql + strRfc + "','";
                                Sql = Sql + "2" + "','";
                                Sql = Sql + "" + "','";
                                Sql = Sql + "P" + "','";
                                Sql = Sql + strSerie + "','";
                                Sql = Sql + sFactura + "','";
                                Sql = Sql + strCtaGasto + "','";
                                Sql = Sql + sCuentaCompras0 + "');";

                                cmd.CommandText = Sql;
                                cmd.ExecuteNonQuery();

                            } // dr.HasRows
                            else
                            {
                                dr.Close();
                            }
                        }


                        strSerieCompra = strSerie;
                        intFolioCompra = intFolio;
                        intPolCompras = intPoliza;

                        result = ComprasActualizaPoliza();
                        if (result != "OK")
                        {
                            transaction.Rollback();
                            cnnCyB.Close();
                            return "No se pudo actualizar póliza en compras SQL: " + result;
                        }


                        intPoliza = intPoliza + 1;

                    } //Pasa filtro para insertar
                } // Eof:ForEach Grid


                if (iniTran)
                {
                    transaction.Commit();
                }
                cnnCyB.Close();
                return "OK";
            }
            catch (Exception ex)
            {
                if (iniTran)
                {
                    transaction.Rollback();
                }
                return ex.Message;
            }
        } // Póliza de compras
        public string Polizadeservicio(GridView gv)
        {
            bool iniTran = false;
            OleDbTransaction transaction = null;

            try
            {
                string Sql = string.Empty;
                string strCpt1 = string.Empty;
                string strCpt2 = string.Empty;
                string strCpt3 = string.Empty;
                int intPoliza = 0;
                string sFactura = string.Empty;
                string Sfecha = string.Empty;
                decimal dSubTotal = 0;
                decimal dIva = 0;
                decimal dSubtotalCero = 0;
                decimal dRetIva = 0;
                decimal dRetIsr = 0;
                decimal dIeps = 0;
                decimal dNeto = 0;
                DateTime dFecha;
                string strMoneda = string.Empty;
                decimal decParidad = 0;
                string strCtaGasto = string.Empty;
                int intSeqPol = 0;
                string pRfc = string.Empty;
                string strCuentaProv = string.Empty;
                string strUUID = string.Empty;
                int intSeqXmlAnexados = 0;
                string strSerie = string.Empty;
                int intPol = 0;
                int intFolio = 0;

                DataTable dtd = new DataTable();

                string result = LeeGrales();
                if (result != "OK")
                {
                    return "No se pudo leer grales2020";
                }

                result = LeeGralEmp();
                if (result != "OK")
                {
                    return "No se pudo leer gralEmp2020";
                }

                if (AbreDBCyB() != "OK")
                {
                    return "No se pudo abrir la BD de contabilidad";
                }

                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = cnnCyB;

                //------------------------- CONSECUTIVO DE LA POLIZA ---------------------
                Sql = "Select Max(Poliza) as MP From PolGen Where Ejercicio=" + intEje.ToString() + " And Mes=" + intMes.ToString() + " And Tipo='D';";
                cmd.CommandText = Sql;
                OleDbDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    if (reader["MP"] != DBNull.Value)
                    {
                        intPoliza = Convert.ToInt32(reader["MP"]);
                    }
                    else
                    {
                        reader.Close();
                        return "No se pudo obtener el siguiente número de póliza (null)";
                    }
                }
                else
                {
                    reader.Close();
                    return "No se pudo obtener el siguiente número de póliza (noread)";
                }

                reader.Close();

                intPoliza = intPoliza + 1;

                strPrimerPoliza = intPoliza.ToString();

                transaction = cnnCyB.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd.Transaction = transaction;
                iniTran = true;

                for (int i = 0; i <= gv.RowCount - 1; i++)
                {


                    //'------------------------ POLGEN --------------------------

                    strSerie = gv.GetRowCellValue(i, "Serie").ToString();
                    strCpt1 = "CONTRARECIBO: " + gv.GetRowCellValue(i, "Serie").ToString() + gv.GetRowCellValue(i, "Folio").ToString();
                    
                    strMoneda = gv.GetRowCellValue(i, "MonedasID").ToString();
                    decParidad = Convert.ToDecimal(gv.GetRowCellValue(i, "Tipodecambio"));
                    strCtaGasto = gv.GetRowCellValue(i, "CuentaGastoCompra").ToString();
                    strCuentaProv = gv.GetRowCellValue(i, "Cuenta").ToString();
                    pRfc = gv.GetRowCellValue(i, "Rfc").ToString();
                    intPol = Convert.ToInt32(gv.GetRowCellValue(i, "Poliza"));
                    intFolio = Convert.ToInt32(gv.GetRowCellValue(i, "Folio"));

                    

                    if (intPol == 0 && strCtaGasto.Length > 0 && strCuentaProv.Length > 0)
                    {








                        //'------------------------ POLDET -------------------------- por factura ContrarecibosDetalle
                        //'-------- CARGO A GASTOS / COMPRAS ------------------------

                        sFactura = string.Empty;
                        dSubTotal = 0;
                        dIva = 0;
                        dNeto = 0;

                        dtd = new DataTable();

                        strSerieCompra = strSerie;
                        intFolioCompra = intFolio;
                        dtd = ContrarecibosDetalle();

                        intSeqPol = 0;

                        if (strCpt1.Length > 100)
                        {
                            strCpt1 = strCpt1.Substring(0, 100);
                            strCpt2 = strCpt1.Substring(101);
                        }

                        foreach (DataRow row in dtd.Rows)
                        {
                            
                            sFactura = row["Factura"].ToString();
                            dFecha = Convert.ToDateTime(row["Fecha"]);
                            dSubTotal = Convert.ToDecimal(row["Subtotal"]);
                            dIva = Convert.ToDecimal(row["Iva"]);
                            dNeto = Convert.ToDecimal(row["Neto"]);

                            Sfecha = dFecha.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);

                            if (intSeqPol==0)
                            {
                               

                                Sql = "INSERT INTO POLGEN(Ejercicio,Mes,Tipo,Poliza,Cpt1,Cpt2,Cpt3,Capturista,Origen,editando,Conciliado,Fecha,FechaConciliacion,Fiscal,Interno) Values(";
                                Sql = Sql + intEje + ",";
                                Sql = Sql + intMes + ",";
                                Sql = Sql + "'D',";
                                Sql = Sql + intPoliza.ToString() + ",'";
                                Sql = Sql + strCpt1 + "',";
                                Sql = Sql + "'" + strCpt2 + "',";
                                Sql = Sql + "'" + strCpt3 + "',";
                                Sql = Sql + "'AUT',";
                                Sql = Sql + 36 + ",";
                                Sql = Sql + 0 + ",";
                                Sql = Sql + 1 + ",#";
                                Sql = Sql + Sfecha + "#,#";
                                Sql = Sql + Sfecha + "#,";
                                Sql = Sql + 1 + ",";
                                Sql = Sql + 1 + ");";

                                cmd.CommandText = Sql;
                                cmd.ExecuteNonQuery();
                            }

                        


                            if (strMoneda != "MXN")
                            {
                                dSubTotal = Math.Round(dSubTotal * decParidad, 2);
                                dIva = Math.Round(dIva * decParidad, 2);
                                dSubtotalCero = Math.Round(dSubtotalCero * decParidad, 2);
                                dRetIva = Math.Round(dRetIva * decParidad, 2);
                                dRetIsr = Math.Round(dRetIsr * decParidad, 2);
                                dIeps = Math.Round(dIeps * decParidad, 2);
                                dNeto = Math.Round(dNeto * decParidad, 2);
                            }

                            if (dSubTotal > 0) // Compras o gastos al 16%
                            {
                                if (Convert.ToDouble(strCtaGasto) == 0)
                                {
                                    transaction.Rollback();
                                    cnnCyB.Close();
                                    return "La cuenta de compras / gastos al 16% no puede ser cero o vacía";
                                }

                                intSeqPol = intSeqPol + 1;

                                Sql = "INSERT INTO POLDET(Ejercicio,Mes,Tipo,Poliza,Seq,Cuenta,Fecha,Concepto,Importe,Saldo,Status,CCosto,Rfc,";
                                Sql = Sql + "TipoYPol,Debe,Haber,Ref,Chequera,Cheque,TipoToc,DebeAnt,HaberAnt,ImporteAnt,movBan,Diot,Carro,Otros,OrigenAdm) Values(";
                                Sql = Sql + intEje + ",";
                                Sql = Sql + intMes + ",";
                                Sql = Sql + "'D',";
                                Sql = Sql + intPoliza + ",";
                                Sql = Sql + intSeqPol + ",'";
                                Sql = Sql + strCtaGasto + "',#";
                                Sql = Sql + Sfecha + "#,'";
                                Sql = Sql + strCpt1 + "',";
                                Sql = Sql + dSubTotal + ",'";
                                Sql = Sql + "D" + "','";
                                Sql = Sql + "C" + "',";
                                Sql = Sql + 0 + ",'";
                                Sql = Sql + pRfc + "',";
                                Sql = Sql + "'D" + intPoliza.ToString() + "',";
                                Sql = Sql + dSubTotal + ",";
                                Sql = Sql + 0 + ",'";
                                Sql = Sql + sFactura + "',";
                                Sql = Sql + 0 + ",";
                                Sql = Sql + 0 + ",'";
                                Sql = Sql + "T" + "',";
                                Sql = Sql + 0 + ",";
                                Sql = Sql + 0 + ",";
                                Sql = Sql + 0 + ",";
                                Sql = Sql + 0 + ",";
                                Sql = Sql + 0 + ",";
                                Sql = Sql + 0 + ",";
                                Sql = Sql + 0 + ",";
                                Sql = Sql + 0 + ");";

                                cmd.CommandText = Sql;
                                cmd.ExecuteNonQuery();

                            } // eof:if (dSubTotal>0)

                            if (dSubtotalCero > 0) // Compras o gastos al 0%
                            {
                                if (Convert.ToDouble(sCuentaCompras0) == 0)
                                {
                                    transaction.Rollback();
                                    cnnCyB.Close();
                                    return "La cuenta de compras / gastos al 0% no puede ser cero o vacía";
                                }

                                intSeqPol = intSeqPol + 1;

                                Sql = "INSERT INTO POLDET(Ejercicio,Mes,Tipo,Poliza,Seq,Cuenta,Fecha,Concepto,Importe,Saldo,Status,CCosto,Rfc,";
                                Sql = Sql + "TipoYPol,Debe,Haber,Ref,Chequera,Cheque,TipoToc,DebeAnt,HaberAnt,ImporteAnt,movBan,Diot,Carro,Otros,OrigenAdm) Values(";
                                Sql = Sql + intEje + ",";
                                Sql = Sql + intMes + ",";
                                Sql = Sql + "'D',";
                                Sql = Sql + intPoliza + ",";
                                Sql = Sql + intSeqPol + ",'";
                                Sql = Sql + sCuentaCompras0 + "',#";
                                Sql = Sql + Sfecha + "#,'";
                                Sql = Sql + strCpt1 + "',";
                                Sql = Sql + dSubtotalCero + ",'";
                                Sql = Sql + "D" + "','";
                                Sql = Sql + "C" + "',";
                                Sql = Sql + 0 + ",'";
                                Sql = Sql + pRfc + "',";
                                Sql = Sql + "'D" + intPoliza.ToString() + "',";
                                Sql = Sql + dSubtotalCero + ",";
                                Sql = Sql + 0 + ",'";
                                Sql = Sql + sFactura + "',";
                                Sql = Sql + 0 + ",";
                                Sql = Sql + 0 + ",'";
                                Sql = Sql + "T" + "',";
                                Sql = Sql + 0 + ",";
                                Sql = Sql + 0 + ",";
                                Sql = Sql + 0 + ",";
                                Sql = Sql + 0 + ",";
                                Sql = Sql + 0 + ",";
                                Sql = Sql + 0 + ",";
                                Sql = Sql + 0 + ",";
                                Sql = Sql + 0 + ");";

                                cmd.CommandText = Sql;
                                cmd.ExecuteNonQuery();

                            } // eof:if (dSubTotal>0)

                            //'-------- CARGO A IVA ACREDITABLE ------------------------
                            if (dIva > 0)
                            {
                                    if (Convert.ToDouble(sCtaIvaAcreditable) == 0)
                                    {
                                        transaction.Rollback();
                                        cnnCyB.Close();
                                        return "La cuenta de iva acreditable no puede ser cero o vacía";
                                    }

                                    intSeqPol = intSeqPol + 1;

                                    Sql = "INSERT INTO POLDET(Ejercicio,Mes,Tipo,Poliza,Seq,Cuenta,Fecha,Concepto,Importe,Saldo,Status,CCosto,Rfc,";
                                    Sql = Sql + "TipoYPol,Debe,Haber,Ref,Chequera,Cheque,TipoToc,DebeAnt,HaberAnt,ImporteAnt,movBan,Diot,Carro,Otros,OrigenAdm) Values(";
                                    Sql = Sql + intEje + ",";
                                    Sql = Sql + intMes + ",";
                                    Sql = Sql + "'D',";
                                    Sql = Sql + intPoliza + ",";
                                    Sql = Sql + intSeqPol + ",'";
                                    Sql = Sql + sCtaIvaAcreditable + "',#";
                                    Sql = Sql + Sfecha + "#,'";
                                    Sql = Sql + strCpt1 + "',";
                                    Sql = Sql + dIva + ",'";
                                    Sql = Sql + "D" + "','";
                                    Sql = Sql + "C" + "',";
                                    Sql = Sql + 0 + ",'";
                                    Sql = Sql + pRfc + "',";
                                    Sql = Sql + "'D" + intPoliza.ToString() + "',";
                                    Sql = Sql + dIva + ",";
                                    Sql = Sql + 0 + ",'";
                                    Sql = Sql + sFactura + "',";
                                    Sql = Sql + 0 + ",";
                                    Sql = Sql + 0 + ",'";
                                    Sql = Sql + "T" + "',";
                                    Sql = Sql + 0 + ",";
                                    Sql = Sql + 0 + ",";
                                    Sql = Sql + 0 + ",";
                                    Sql = Sql + 0 + ",";
                                    Sql = Sql + 0 + ",";
                                    Sql = Sql + 0 + ",";
                                    Sql = Sql + 0 + ",";
                                    Sql = Sql + 0 + ");";

                                    cmd.CommandText = Sql;
                                    cmd.ExecuteNonQuery();
                                }


                                if (Convert.ToDouble(strCuentaProv) == 0)
                                {
                                    transaction.Rollback();
                                    cnnCyB.Close();
                                    return "La cuenta del proveedor no puede ser cero o vacía";
                                }

                                intSeqPol = intSeqPol + 1;
                                //'-------- ABONO A PROVEEDORES ------------------------
                                Sql = "INSERT INTO POLDET(Ejercicio,Mes,Tipo,Poliza,Seq,Cuenta,Fecha,Concepto,Importe,Saldo,Status,CCosto,Rfc,";
                                Sql = Sql + "TipoYPol,Debe,Haber,Ref,Chequera,Cheque,TipoToc,DebeAnt,HaberAnt,ImporteAnt,movBan,Diot,Carro,Otros,OrigenAdm) Values(";
                                Sql = Sql + intEje + ",";
                                Sql = Sql + intMes + ",";
                                Sql = Sql + "'D',";
                                Sql = Sql + intPoliza + ",";
                                Sql = Sql + intSeqPol + ",'";
                                Sql = Sql + strCuentaProv + "',#";
                                Sql = Sql + Sfecha + "#,'";
                                Sql = Sql + strCpt1 + "',";
                                Sql = Sql + dNeto + ",'";
                                Sql = Sql + "H" + "','";
                                Sql = Sql + "C" + "',";
                                Sql = Sql + 0 + ",'";
                                Sql = Sql + pRfc + "',";
                                Sql = Sql + "'D" + intPoliza.ToString() + "',";
                                Sql = Sql + 0 + ",";
                                Sql = Sql + dNeto + ",'";
                                Sql = Sql + sFactura + "',";
                                Sql = Sql + 0 + ",";
                                Sql = Sql + 0 + ",'";
                                Sql = Sql + "T" + "',";
                                Sql = Sql + 0 + ",";
                                Sql = Sql + 0 + ",";
                                Sql = Sql + 0 + ",";
                                Sql = Sql + 0 + ",";
                                Sql = Sql + 0 + ",";
                                Sql = Sql + 0 + ",";
                                Sql = Sql + 0 + ",";
                                Sql = Sql + 0 + ");";

                                cmd.CommandText = Sql;
                                cmd.ExecuteNonQuery();


                                //'------------------- XML AnEXADOS ------------------------------

                                if (strUUID.Length > 0)
                                {
                                    OleDbDataReader dr;

                                    //''1.- Se verifica que no exista
                                    Sql = "Select UUID from XmlAnexados Where Ejercicio=" + intEje + " And Mes=" + intMes + " And ";
                                    Sql = Sql + "Tipo='D' And Poliza=" + intPoliza + " And UUID='" + strUUID + "';";

                                    cmd.CommandText = Sql;
                                    dr = cmd.ExecuteReader();

                                    if (!dr.HasRows)
                                    {
                                        dr.Close();
                                        //'    '2- Se busca el siguiente consecutivo para el año, mes, tipo de poliza y poliza
                                        Sql = "Select Max(Seq) as MS From XmlAnexados Where Ejercicio=" + intEje + " And Mes=" + intMes + " And ";
                                        Sql = Sql + "Tipo='D' And Poliza=" + intPoliza + ";";
                                        cmd.CommandText = Sql;
                                        dr = cmd.ExecuteReader();

                                        if (dr.HasRows)
                                        {
                                            dr.Read();
                                            if (dr["MS"] == DBNull.Value)
                                            {
                                                intSeqXmlAnexados = 1;
                                            }
                                            else
                                            {
                                                intSeqXmlAnexados = Convert.ToInt32(dr["MS"]) + 1;
                                            }
                                        }
                                        else
                                        {
                                            intSeqXmlAnexados = 1;
                                        }

                                        dr.Close();

                                        //'    '3- Se inerta en XMLANEXADOS
                                        Sql = "Insert into XMLAnexados(Ejercicio,Mes,Tipo,Poliza,Seq,Cuenta,UUID,Monto,Rfc,Origen,ArchivoXML,ClteoProv,Serie,Folio,CuentaGastos16,CuentaGastos0) Values(";
                                        Sql = Sql + intEje + ",";
                                        Sql = Sql + intMes + ",";
                                        Sql = Sql + "'D',";
                                        Sql = Sql + intPoliza + ",";
                                        Sql = Sql + intSeqXmlAnexados + ",'";
                                        Sql = Sql + strCuentaProv + "','";
                                        Sql = Sql + strUUID + "','";
                                        Sql = Sql + dNeto.ToString() + "','";
                                        Sql = Sql + strRfc + "','";
                                        Sql = Sql + "2" + "','";
                                        Sql = Sql + "" + "','";
                                        Sql = Sql + "P" + "','";
                                        Sql = Sql + strSerie + "','";
                                        Sql = Sql + sFactura + "','";
                                        Sql = Sql + strCtaGasto + "','";
                                        Sql = Sql + sCuentaCompras0 + "');";

                                        cmd.CommandText = Sql;
                                        cmd.ExecuteNonQuery();

                                    } // dr.HasRows
                                    else
                                    {
                                        dr.Close();
                                    }
                                }


                            strSerieCompra = strSerie;
                            intFolioCompra = intFolio;
                            intPolCompras = intPoliza;

                            result = ContrarecibosActualizaPoliza();
                            if (result != "OK")
                            {
                                transaction.Rollback();
                                cnnCyB.Close();
                                return "No se pudo actualizar póliza en contrarecibos SQL: " + result;
                            }


                        }

                        intPoliza = intPoliza + 1;
                    } //Pasa filtro para insertar
                } // Eof:ForEach Grid


                if (iniTran)
                {
                    transaction.Commit();
                }
                cnnCyB.Close();
                return "OK";
            }
            catch (Exception ex)
            {
                if (iniTran)
                {
                    transaction.Rollback();
                }
                return ex.Message;
            }
        } // Inserta vale

        public string LeeGralEmp()
        {
            try
            {
                string strResult;

                string sPathConta = string.Empty;
                sPathConta = System.Configuration.ConfigurationManager.AppSettings["Pathconta"].ToString();
                //Se abre GralEmp14 para traer datos de control de CxP
                string strcnn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sPathConta + "GralEmp2020.Mdb";

                string Sql = "Select CuentasDiot,CtaIvaAcreditable,CuentaCompras0 From Ejercicios Where Ejercicio=" + intEje.ToString() + ";";

                cnnCyB = new OleDbConnection();
                cnnCyB.ConnectionString = strcnn;
                cnnCyB.Open();

                //Comando
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = cnnCyB;
                cmd.CommandText = Sql;

                ////Lector
                OleDbDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();
                    strCtaDiot = dr["CuentasDiot"].ToString();
                    sCtaIvaAcreditable = dr["CtaIvaAcreditable"].ToString();
                    sCuentaCompras0 = dr["CuentaCompras0"].ToString();

                    strResult = "OK";
                }
                else
                {
                    strResult = "NOREAD";
                    dr.Close();
                }

                cnnCyB.Close();

                return strResult;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string LeeGrales()
        {
            try
            {
                string strResult;
                int EmpresaConta = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["EmpresaConta"]);

                string sPathConta = string.Empty;
                sPathConta = System.Configuration.ConfigurationManager.AppSettings["Pathconta"].ToString();
                sPathConta = sPathConta.Substring(0, 3) + "\\AVC\\CONTA\\GRALES2020.MDB";
                //Se abre GralEmp14 para traer datos de control de CxP
                string strcnn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sPathConta;

                string Sql = "Select N1 From Empresas Where Numero=" +  EmpresaConta.ToString() +";";

                cnnCyB = new OleDbConnection();
                cnnCyB.ConnectionString = strcnn;
                cnnCyB.Open();

                //Comando
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = cnnCyB;
                cmd.CommandText = Sql;

                ////Lector
                OleDbDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();
                    intNivel1 = Convert.ToInt32(dr["N1"]);

                    strResult = "OK";
                }
                else
                {
                    strResult = "NOREAD";
                    dr.Close();
                }

                cnnCyB.Close();

                return strResult;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        //------------------------------ POLIZA DE PAGOS A PROVEEDORES -------------------
        public string Polizadepagoscxp(GridView gv)
        {
            bool iniTran = false;
            OleDbTransaction transaction = null;
            string strCpt1 = string.Empty;
            try
            {
                string Sql = string.Empty;
                
                string strCpt2 = string.Empty;
                string strCpt3 = string.Empty;
                int intPoliza = 0;
                string sFactura = string.Empty;
                string Sfecha = string.Empty;
                decimal dSubTotal = 0;
                decimal dIva = 0;
                decimal dSubtotalCero = 0;
                decimal dRetIva = 0;
                decimal dRetIsr = 0;
                decimal dIeps = 0;
                decimal dNeto = 0;
                DateTime dFecha;
                string strMoneda = string.Empty;
                decimal decParidad = 0;
                string strCtaGasto = string.Empty;
                int intSeqPol = 0;
                string pRfc = string.Empty;
                string strCuentaProv = string.Empty;
                string strUUID = string.Empty;
                int intSeqXmlAnexados = 0;
                string strSerie = string.Empty;
                int intPol = 0;
                int intFolio = 0;
                string strCtaBanco = string.Empty;
                string strFP = string.Empty;
                int intChequera = 0;
                int intFolioBan = 0;
                string strNomProv = string.Empty;
                int intMoneda = 0;
                int intTipoMovBan = 0;
                int intOrigenBan = 0;
                int intCheque = 0;

                DataTable dtd = new DataTable();

                string result = LeeGrales();
                if (result != "OK")
                {
                    return "No se pudo leer grales2020";
                }

                result = LeeGralEmp();
                if (result != "OK")
                {
                    return "No se pudo leer gralEmp2020";
                }

                if (AbreDBCyB() != "OK")
                {
                    return "No se pudo abrir la BD de contabilidad";
                }

                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = cnnCyB;

                //------------------------- CONSECUTIVO DE LA POLIZA ---------------------
                Sql = "Select Max(Poliza) as MP From PolGen Where Ejercicio=" + intEje.ToString() + " And Mes=" + intMes.ToString() + " And Tipo='E';";
                cmd.CommandText = Sql;
                OleDbDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    if (reader["MP"] != DBNull.Value)
                    {
                        intPoliza = Convert.ToInt32(reader["MP"]);
                    }
                    else
                    {
                        intPoliza = 0;
                    }
                }
                else
                {
                    reader.Close();
                    return "No se pudo obtener el siguiente número de póliza (noread)";
                }

                reader.Close();

                intPoliza = intPoliza + 1;

                strPrimerPoliza = intPoliza.ToString();

                transaction = cnnCyB.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd.Transaction = transaction;
                iniTran = true;

                for (int i = 0; i <= gv.RowCount - 1; i++)
                {


                    //'------------------------ POLGEN --------------------------

                    strSerie = gv.GetRowCellValue(i, "Serie").ToString();
                    strCpt1 = "FOLIO: " + gv.GetRowCellValue(i, "Serie").ToString() + gv.GetRowCellValue(i, "Folio").ToString();

                    strMoneda = gv.GetRowCellValue(i, "MonedasID").ToString();
                    decParidad = Convert.ToDecimal(gv.GetRowCellValue(i, "Tipodecambio"));
                    strCtaGasto = gv.GetRowCellValue(i, "CuentaGastos").ToString();
                    strCuentaProv = gv.GetRowCellValue(i, "Cuenta").ToString();
                    pRfc = gv.GetRowCellValue(i, "Rfc").ToString();
                    intPol = Convert.ToInt32(gv.GetRowCellValue(i, "Poliza"));
                    intFolio = Convert.ToInt32(gv.GetRowCellValue(i, "Folio"));
                    dFecha = Convert.ToDateTime(gv.GetRowCellValue(i, "Fecha"));
                    strCtaBanco = gv.GetRowCellValue(i, "Cuentabancaria").ToString();
                    strFP = gv.GetRowCellValue(i, "Fp").ToString();
                    intChequera = Convert.ToInt32(gv.GetRowCellValue(i, "CuentasbancariaID"));
                    strNomProv= gv.GetRowCellValue(i, "Proveedor").ToString();

                    if (strMoneda == "MXN")
                        intMoneda = 1;            
                    else
                        intMoneda = 2;
                    

                    if (intPol == 0 && strCtaGasto.Length > 0 && strCuentaProv!="No existe")
                    {

                        //'------------------------ POLDET -------------------------- por factura ContrarecibosDetalle
                        //'-------- CARGO A GASTOS / COMPRAS ------------------------

                        sFactura = string.Empty;
                        dSubTotal = 0;
                        dIva = 0;
                        dNeto = 0;

                        dtd = new DataTable();

                        strSerieCompra = strSerie;
                        intFolioCompra = intFolio;
                        dtd = PagosCxPDetalle();

                        intSeqPol = 0;

                        if (strCpt1.Length > 100)
                        {
                            strCpt1 = strCpt1.Substring(0, 100);
                            strCpt2 = strCpt1.Substring(101);
                        }

                        foreach (DataRow row in dtd.Rows)
                        {

                            sFactura = row["Referencia"].ToString();                           
                            dSubTotal = Convert.ToDecimal(row["Subtotal"]);
                            dIva = Convert.ToDecimal(row["Iva"]);
                            dNeto = Convert.ToDecimal(row["Neto"]);

                            Sfecha = dFecha.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);

                            if (intSeqPol == 0)
                            {

                                //Siguiente Id en Cheques / Transferencias / Cargos segun forma de pago
                                switch (strFP)
                                {
                                    case "02":
                                        intTipoMovBan = 2;
                                        intOrigenBan = 2;
                                        Sql = "Select Max(Folio) as MF From Cheques Where Chequera=" + intChequera.ToString() + ";";
                                        break;
                                    case "03":
                                        intTipoMovBan = 30;
                                        intOrigenBan = 31;
                                        Sql = "Select Max(Folio) as MF From Transferencias Where Chequera=" + intChequera.ToString() + ";";
                                        break;
                                    case "04": case "28":
                                        intTipoMovBan = 3;
                                        intOrigenBan = 3;
                                        Sql = "Select Max(Folio) as MF From Cargos Where Chequera=" + intChequera.ToString() + ";";
                                        break;                                  
                                }

                                if (strFP !="99")
                                {

                                
                                    cmd.CommandText = Sql;
                                    reader = cmd.ExecuteReader();

                                    if (reader.Read())
                                    {
                                        if (reader["MF"] != DBNull.Value)
                                        {
                                            intFolioBan = Convert.ToInt32(reader["MF"]) + 1;
                                        }
                                        else
                                        {
                                            intFolioBan = 1;
                                        }
                                    }
                                    else
                                    {
                                        reader.Close();
                                        transaction.Rollback();
                                        cnnCyB.Close();
                                        return "No se pudo obtener el siguiente número bancario";
                                    }

                                    reader.Close();

                                    switch (strFP)
                                    {
                                        case "02":  //Cheques
                                            intCheque = intFolioBan;

                                            Sql = "Insert into Cheques(Chequera,Folio,Fecha,FechaApl,Categoria,Cpt1,Cpt2,Cpt3,";
                                            Sql = Sql + "Fechacon,Poliza,Importe,Paridad,Capturista,Ben,NombreBen,Recordar,Proveedor,";
                                            Sql = Sql + "Conciliado,Leyenda,Impreso,Moneda,FolioDep,Status,Editando,Mesdeconc,";
                                            Sql = Sql + "Poliza2,FechaAplAnt,EjercicioPol,MesPol,EjercicioPol2,MesPol2) Values(";
                                            Sql = Sql + intChequera + ",";
                                            Sql = Sql + intFolioBan + ",#";
                                            Sql = Sql + Sfecha + "#,#";
                                            Sql = Sql + Sfecha + "#,";
                                            Sql = Sql + 0 + ",'";
                                            Sql = Sql + strCpt1 + "','";
                                            Sql = Sql + strCpt2 + "','";
                                            Sql = Sql + strCpt3 + "',#";
                                            Sql = Sql + Sfecha + "#,";
                                            Sql = Sql + intPoliza + ",";
                                            Sql = Sql + dNeto + ",";
                                            Sql = Sql + decParidad + ",";
                                            Sql = Sql + "'GEN'" + ",'";
                                            Sql = Sql + strCuentaProv + "','";
                                            Sql = Sql + strNomProv + "',";
                                            Sql = Sql + "'0',";
                                            Sql = Sql + "'0',";
                                            Sql = Sql + 1 + ",";
                                            Sql = Sql + 1 + ",";
                                            Sql = Sql + 1 + ",";
                                            Sql = Sql + intMoneda + ",";
                                            Sql = Sql + 0 + ",";
                                            Sql = Sql + "'R'" + ",";
                                            Sql = Sql + "0" + ",";
                                            Sql = Sql + intMes + ",";
                                            Sql = Sql + "0" + ",#";
                                            Sql = Sql + Sfecha + "#,";
                                            Sql = Sql + intEje + ",";
                                            Sql = Sql + intMes + ",";
                                            Sql = Sql + 0 + ",";
                                            Sql = Sql + 0 + ");";
                                            break;
                                        case "03":  // Transferencias
                                            intCheque = 0;

                                            Sql = "Insert into transferencias(Chequera,Folio,Fecha,Cpt1,Cpt2,Conciliado,FechaCon,";
                                            Sql = Sql + "Poliza,Importe,Paridad,Capturista,Editando,Moneda,Referenciabancaria,";
                                            Sql = Sql + "Chequeradestino,Operacion,Cuenta,Beneficiario,Proveedor,CtaDestino,BancoDestino) Values(";
                                            Sql = Sql + intChequera + ",";
                                            Sql = Sql + intFolioBan + ",#";
                                            Sql = Sql + Sfecha + "#,'";
                                            Sql = Sql + strCpt1 + "','";
                                            Sql = Sql + strCpt2 + "',";
                                            Sql = Sql + 1 + ",#";
                                            Sql = Sql + Sfecha + "#,";
                                            Sql = Sql + intPoliza + ",";
                                            Sql = Sql + dNeto + ",";
                                            Sql = Sql + decParidad + ",";
                                            Sql = Sql + "'GEN'" + ",";
                                            Sql = Sql + 0 + ",";
                                            Sql = Sql + intMoneda + ",'";
                                            Sql = Sql + strRfc + "',";
                                            Sql = Sql + intChequera + ",";
                                            Sql = Sql + 1 + ",'";
                                            Sql = Sql + strCuentaProv + "','";
                                            if (strNomProv.Length>50)
                                                Sql = Sql + strNomProv.Substring(0, 50) + "',";
                                            else
                                                Sql = Sql + strNomProv + "',";
                                            Sql = Sql + "0,";
                                            Sql = Sql + "''" + ",";
                                            Sql = Sql + 1 + ");";

                                            break;
                                        case "04": case "28":  //Cargos
                                            intCheque = 0;

                                            Sql = "Insert into Cargos(Chequera,Folio,Fecha,Categoria,Cpt1,Cpt2,Conciliado,FechaCon,";
                                            Sql = Sql + "Poliza,Importe,Paridad,Capturista,Editando) Values(";
                                            Sql = Sql + intChequera + ",";
                                            Sql = Sql + intFolioBan + ",#";
                                            Sql = Sql + Sfecha + "#,";
                                            Sql = Sql + 0 + ",'";
                                            Sql = Sql + strCpt1 + "','";
                                            Sql = Sql + strCpt2 + "',";
                                            Sql = Sql + 1 + ",#";
                                            Sql = Sql + Sfecha + "#,";
                                            Sql = Sql + intPoliza + ",";
                                            Sql = Sql + dNeto + ",";
                                            Sql = Sql + decParidad + ",";
                                            Sql = Sql + "'GEN',";
                                            Sql = Sql + 0 + ");";


                                            break;
                                    }

                                    cmd.CommandText = Sql;
                                    cmd.ExecuteNonQuery();

                                    //MovsBan
                                    Sql = "Insert into MovsBan(TipoMov,Chequera,Folio,Fecha,FechaApl,Importe) Values(";
                                    Sql = Sql + intTipoMovBan + ",";
                                    Sql = Sql + intChequera + ",";
                                    Sql = Sql + intFolioBan + ",#";
                                    Sql = Sql + Sfecha + "#,#";
                                    Sql = Sql + Sfecha + "#,";
                                    Sql = Sql + dNeto * -1 + ");";
                                    cmd.CommandText = Sql;
                                    cmd.ExecuteNonQuery();
                                }
                                else
                                {
                                    intOrigenBan = 0;
                                    intFolioBan = 0;
                                }

                                


                                Sql = "INSERT INTO POLGEN(Ejercicio,Mes,Tipo,Poliza,Cpt1,Cpt2,Cpt3,Capturista,Origen,editando,Conciliado,Fecha,FechaConciliacion,Fiscal,Interno) Values(";
                                Sql = Sql + intEje + ",";
                                Sql = Sql + intMes + ",";
                                Sql = Sql + "'E',";
                                Sql = Sql + intPoliza.ToString() + ",'";
                                Sql = Sql + strCpt1 + "',";
                                Sql = Sql + "'" + strCpt2 + "',";
                                Sql = Sql + "'" + strCpt3 + "',";
                                Sql = Sql + "'AUT',";
                                Sql = Sql + intOrigenBan + ",";
                                Sql = Sql + 0 + ",";
                                Sql = Sql + 1 + ",#";
                                Sql = Sql + Sfecha + "#,#";
                                Sql = Sql + Sfecha + "#,";
                                Sql = Sql + 1 + ",";
                                Sql = Sql + 1 + ");";

                                cmd.CommandText = Sql;
                                cmd.ExecuteNonQuery();
                            }

                            if (strMoneda != "MXN")
                            {
                                dSubTotal = Math.Round(dSubTotal * decParidad, 2);
                                dIva = Math.Round(dIva * decParidad, 2);
                                dSubtotalCero = Math.Round(dSubtotalCero * decParidad, 2);
                                dRetIva = Math.Round(dRetIva * decParidad, 2);
                                dRetIsr = Math.Round(dRetIsr * decParidad, 2);
                                dIeps = Math.Round(dIeps * decParidad, 2);
                                dNeto = Math.Round(dNeto * decParidad, 2);
                            }

                            //--------- Cargo a proveedores ------------
                            if (Convert.ToDouble(strCuentaProv) == 0)
                            {
                                transaction.Rollback();
                                cnnCyB.Close();
                                return "La cuenta del proveedor no puede ser cero o vacía";
                            }

                            intSeqPol = intSeqPol + 1;
                            Sql = "INSERT INTO POLDET(Ejercicio,Mes,Tipo,Poliza,Seq,Cuenta,Fecha,Concepto,Importe,Saldo,Status,CCosto,Rfc,";
                            Sql = Sql + "TipoYPol,Debe,Haber,Ref,Chequera,Cheque,TipoToc,DebeAnt,HaberAnt,ImporteAnt,movBan,Diot,Carro,Otros,OrigenAdm) Values(";
                            Sql = Sql + intEje + ",";
                            Sql = Sql + intMes + ",";
                            Sql = Sql + "'E',";
                            Sql = Sql + intPoliza + ",";
                            Sql = Sql + intSeqPol + ",'";
                            Sql = Sql + strCuentaProv + "',#";
                            Sql = Sql + Sfecha + "#,'";
                            Sql = Sql + strCpt1 + "',";
                            Sql = Sql + dNeto + ",'";
                            Sql = Sql + "D" + "','";
                            Sql = Sql + "C" + "',";
                            Sql = Sql + 0 + ",'";
                            Sql = Sql + pRfc + "',";
                            Sql = Sql + "'E" + intPoliza.ToString() + "',";
                            Sql = Sql + dNeto + ",";
                            Sql = Sql + 0 + ",'";
                            Sql = Sql + sFactura + "',";
                            Sql = Sql + intChequera + ",";
                            Sql = Sql + intCheque + ",'";
                            Sql = Sql + "T" + "',";
                            Sql = Sql + 0 + ",";
                            Sql = Sql + 0 + ",";
                            Sql = Sql + 0 + ",";
                            Sql = Sql + intFolioBan + ",";
                            Sql = Sql + 0 + ",";
                            Sql = Sql + 0 + ",";
                            Sql = Sql + 0 + ",";
                            Sql = Sql + 0 + ");";

                            cmd.CommandText = Sql;
                            cmd.ExecuteNonQuery();
                            //--------- Eof:Cargo a proveedores --------

                            //--------- Abono a bancos -----------------
                            if (Convert.ToDouble(strCtaBanco) == 0)
                            {
                                transaction.Rollback();
                                cnnCyB.Close();
                                return "La cuenta del banco no puede ser cero o vacía";
                            }

                            intSeqPol = intSeqPol + 1;
                            Sql = "INSERT INTO POLDET(Ejercicio,Mes,Tipo,Poliza,Seq,Cuenta,Fecha,Concepto,Importe,Saldo,Status,CCosto,Rfc,";
                            Sql = Sql + "TipoYPol,Debe,Haber,Ref,Chequera,Cheque,TipoToc,DebeAnt,HaberAnt,ImporteAnt,movBan,Diot,Carro,Otros,OrigenAdm) Values(";
                            Sql = Sql + intEje + ",";
                            Sql = Sql + intMes + ",";
                            Sql = Sql + "'E',";
                            Sql = Sql + intPoliza + ",";
                            Sql = Sql + intSeqPol + ",'";
                            Sql = Sql + strCtaBanco + "',#";
                            Sql = Sql + Sfecha + "#,'";
                            Sql = Sql + strCpt1 + "',";
                            Sql = Sql + dNeto + ",'";
                            Sql = Sql + "H" + "','";
                            Sql = Sql + "C" + "',";
                            Sql = Sql + 0 + ",'";
                            Sql = Sql + pRfc + "',";
                            Sql = Sql + "'E" + intPoliza.ToString() + "',";
                            Sql = Sql + 0 + ",";
                            Sql = Sql + dNeto + ",'";
                            Sql = Sql + sFactura + "',";
                            Sql = Sql + intChequera + ",";
                            Sql = Sql + intCheque + ",'";
                            Sql = Sql + "T" + "',";
                            Sql = Sql + 0 + ",";
                            Sql = Sql + 0 + ",";
                            Sql = Sql + 0 + ",";
                            Sql = Sql + intFolioBan + ",";
                            Sql = Sql + 0 + ",";
                            Sql = Sql + 0 + ",";
                            Sql = Sql + 0 + ",";
                            Sql = Sql + 0 + ");";

                            cmd.CommandText = Sql;
                            cmd.ExecuteNonQuery();
                            //--------- Eof: Abono a bancos ------------

                            if (dSubTotal > 0) // Compras o gastos al 16%
                            {
                                if (Convert.ToDouble(strCtaGasto) == 0)
                                {
                                    transaction.Rollback();
                                    cnnCyB.Close();
                                    return "La cuenta de compras / gastos al 16% no puede ser cero o vacía";
                                }

                                intSeqPol = intSeqPol + 1;

                                Sql = "INSERT INTO POLDET(Ejercicio,Mes,Tipo,Poliza,Seq,Cuenta,Fecha,Concepto,Importe,Saldo,Status,CCosto,Rfc,";
                                Sql = Sql + "TipoYPol,Debe,Haber,Ref,Chequera,Cheque,TipoToc,DebeAnt,HaberAnt,ImporteAnt,movBan,Diot,Carro,Otros,OrigenAdm) Values(";
                                Sql = Sql + intEje + ",";
                                Sql = Sql + intMes + ",";
                                Sql = Sql + "'E',";
                                Sql = Sql + intPoliza + ",";
                                Sql = Sql + intSeqPol + ",'";
                                Sql = Sql + strCtaGasto + "',#";
                                Sql = Sql + Sfecha + "#,'";
                                Sql = Sql + strCpt1 + "',";
                                Sql = Sql + dSubTotal + ",'";
                                Sql = Sql + "D" + "','";
                                Sql = Sql + "C" + "',";
                                Sql = Sql + 0 + ",'";
                                Sql = Sql + pRfc + "',";
                                Sql = Sql + "'E" + intPoliza.ToString() + "',";
                                Sql = Sql + dSubTotal + ",";
                                Sql = Sql + 0 + ",'";
                                Sql = Sql + sFactura + "',";
                                Sql = Sql + intChequera + ",";
                                Sql = Sql + intCheque + ",'";
                                Sql = Sql + "C" + "',";
                                Sql = Sql + 0 + ",";
                                Sql = Sql + 0 + ",";
                                Sql = Sql + 0 + ",";
                                Sql = Sql + intFolioBan + ",";
                                Sql = Sql + 0 + ",";
                                Sql = Sql + 0 + ",";
                                Sql = Sql + 0 + ",";
                                Sql = Sql + 0 + ");";

                                cmd.CommandText = Sql;
                                cmd.ExecuteNonQuery();

                            } // eof:if (dSubTotal>0)

                            if (dSubtotalCero > 0) // Compras o gastos al 0%
                            {
                                if (Convert.ToDouble(sCuentaCompras0) == 0)
                                {
                                    transaction.Rollback();
                                    cnnCyB.Close();
                                    return "La cuenta de compras / gastos al 0% no puede ser cero o vacía";
                                }

                                intSeqPol = intSeqPol + 1;

                                Sql = "INSERT INTO POLDET(Ejercicio,Mes,Tipo,Poliza,Seq,Cuenta,Fecha,Concepto,Importe,Saldo,Status,CCosto,Rfc,";
                                Sql = Sql + "TipoYPol,Debe,Haber,Ref,Chequera,Cheque,TipoToc,DebeAnt,HaberAnt,ImporteAnt,movBan,Diot,Carro,Otros,OrigenAdm) Values(";
                                Sql = Sql + intEje + ",";
                                Sql = Sql + intMes + ",";
                                Sql = Sql + "'E',";
                                Sql = Sql + intPoliza + ",";
                                Sql = Sql + intSeqPol + ",'";
                                Sql = Sql + sCuentaCompras0 + "',#";
                                Sql = Sql + Sfecha + "#,'";
                                Sql = Sql + strCpt1 + "',";
                                Sql = Sql + dSubtotalCero + ",'";
                                Sql = Sql + "D" + "','";
                                Sql = Sql + "C" + "',";
                                Sql = Sql + 0 + ",'";
                                Sql = Sql + pRfc + "',";
                                Sql = Sql + "'E" + intPoliza.ToString() + "',";
                                Sql = Sql + dSubtotalCero + ",";
                                Sql = Sql + 0 + ",'";
                                Sql = Sql + sFactura + "',";
                                Sql = Sql + intChequera + ",";
                                Sql = Sql + intCheque + ",'";
                                Sql = Sql + "C" + "',";
                                Sql = Sql + 0 + ",";
                                Sql = Sql + 0 + ",";
                                Sql = Sql + 0 + ",";
                                Sql = Sql + intFolioBan + ",";
                                Sql = Sql + 0 + ",";
                                Sql = Sql + 0 + ",";
                                Sql = Sql + 0 + ",";
                                Sql = Sql + 0 + ");";

                                cmd.CommandText = Sql;
                                cmd.ExecuteNonQuery();

                            } // eof:if (dSubTotal>0)

                            //'-------- CARGO A IVA ACREDITABLE ------------------------
                            if (dIva > 0)
                            {
                                if (Convert.ToDouble(sCtaIvaAcreditable) == 0)
                                {
                                    transaction.Rollback();
                                    cnnCyB.Close();
                                    return "La cuenta de iva acreditable no puede ser cero o vacía";
                                }

                                intSeqPol = intSeqPol + 1;

                                Sql = "INSERT INTO POLDET(Ejercicio,Mes,Tipo,Poliza,Seq,Cuenta,Fecha,Concepto,Importe,Saldo,Status,CCosto,Rfc,";
                                Sql = Sql + "TipoYPol,Debe,Haber,Ref,Chequera,Cheque,TipoToc,DebeAnt,HaberAnt,ImporteAnt,movBan,Diot,Carro,Otros,OrigenAdm) Values(";
                                Sql = Sql + intEje + ",";
                                Sql = Sql + intMes + ",";
                                Sql = Sql + "'E',";
                                Sql = Sql + intPoliza + ",";
                                Sql = Sql + intSeqPol + ",'";
                                Sql = Sql + sCtaIvaAcreditable + "',#";
                                Sql = Sql + Sfecha + "#,'";
                                Sql = Sql + strCpt1 + "',";
                                Sql = Sql + dIva + ",'";
                                Sql = Sql + "D" + "','";
                                Sql = Sql + "C" + "',";
                                Sql = Sql + 0 + ",'";
                                Sql = Sql + pRfc + "',";
                                Sql = Sql + "'E" + intPoliza.ToString() + "',";
                                Sql = Sql + dIva + ",";
                                Sql = Sql + 0 + ",'";
                                Sql = Sql + sFactura + "',";
                                Sql = Sql + intChequera + ",";
                                Sql = Sql + intCheque + ",'";
                                Sql = Sql + "C" + "',";
                                Sql = Sql + 0 + ",";
                                Sql = Sql + 0 + ",";
                                Sql = Sql + 0 + ",";
                                Sql = Sql + intFolioBan + ",";
                                Sql = Sql + 0 + ",";
                                Sql = Sql + 0 + ",";
                                Sql = Sql + 0 + ",";
                                Sql = Sql + 0 + ");";

                                cmd.CommandText = Sql;
                                cmd.ExecuteNonQuery();
                            }


                            //--------- Abono a bancos contado-----------------
                            if (Convert.ToDouble(strCtaBanco) == 0)
                            {
                                transaction.Rollback();
                                cnnCyB.Close();
                                return "La cuenta del banco no puede ser cero o vacía";
                            }

                            intSeqPol = intSeqPol + 1;
                            Sql = "INSERT INTO POLDET(Ejercicio,Mes,Tipo,Poliza,Seq,Cuenta,Fecha,Concepto,Importe,Saldo,Status,CCosto,Rfc,";
                            Sql = Sql + "TipoYPol,Debe,Haber,Ref,Chequera,Cheque,TipoToc,DebeAnt,HaberAnt,ImporteAnt,movBan,Diot,Carro,Otros,OrigenAdm) Values(";
                            Sql = Sql + intEje + ",";
                            Sql = Sql + intMes + ",";
                            Sql = Sql + "'E',";
                            Sql = Sql + intPoliza + ",";
                            Sql = Sql + intSeqPol + ",'";
                            Sql = Sql + strCtaBanco + "',#";
                            Sql = Sql + Sfecha + "#,'";
                            Sql = Sql + strCpt1 + "',";
                            Sql = Sql + dNeto + ",'";
                            Sql = Sql + "H" + "','";
                            Sql = Sql + "C" + "',";
                            Sql = Sql + 0 + ",'";
                            Sql = Sql + pRfc + "',";
                            Sql = Sql + "'E" + intPoliza.ToString() + "',";
                            Sql = Sql + 0 + ",";
                            Sql = Sql + dNeto + ",'";
                            Sql = Sql + sFactura + "',";
                            Sql = Sql + intChequera + ",";
                            Sql = Sql + intCheque + ",'";
                            Sql = Sql + "C" + "',";
                            Sql = Sql + 0 + ",";
                            Sql = Sql + 0 + ",";
                            Sql = Sql + 0 + ",";
                            Sql = Sql + intFolioBan + ",";
                            Sql = Sql + 0 + ",";
                            Sql = Sql + 0 + ",";
                            Sql = Sql + 0 + ",";
                            Sql = Sql + 0 + ");";

                            cmd.CommandText = Sql;
                            cmd.ExecuteNonQuery();
                            //--------- Eof: Abono a bancos ------------


                            //'------------------- XML AnEXADOS ------------------------------

                            if (strUUID.Length > 0)
                            {
                                OleDbDataReader dr;

                                //''1.- Se verifica que no exista
                                Sql = "Select UUID from XmlAnexados Where Ejercicio=" + intEje + " And Mes=" + intMes + " And ";
                                Sql = Sql + "Tipo='D' And Poliza=" + intPoliza + " And UUID='" + strUUID + "';";

                                cmd.CommandText = Sql;
                                dr = cmd.ExecuteReader();

                                if (!dr.HasRows)
                                {
                                    dr.Close();
                                    //'    '2- Se busca el siguiente consecutivo para el año, mes, tipo de poliza y poliza
                                    Sql = "Select Max(Seq) as MS From XmlAnexados Where Ejercicio=" + intEje + " And Mes=" + intMes + " And ";
                                    Sql = Sql + "Tipo='D' And Poliza=" + intPoliza + ";";
                                    cmd.CommandText = Sql;
                                    dr = cmd.ExecuteReader();

                                    if (dr.HasRows)
                                    {
                                        dr.Read();
                                        if (dr["MS"] == DBNull.Value)
                                        {
                                            intSeqXmlAnexados = 1;
                                        }
                                        else
                                        {
                                            intSeqXmlAnexados = Convert.ToInt32(dr["MS"]) + 1;
                                        }
                                    }
                                    else
                                    {
                                        intSeqXmlAnexados = 1;
                                    }

                                    dr.Close();

                                    //'    '3- Se inerta en XMLANEXADOS
                                    Sql = "Insert into XMLAnexados(Ejercicio,Mes,Tipo,Poliza,Seq,Cuenta,UUID,Monto,Rfc,Origen,ArchivoXML,ClteoProv,Serie,Folio,CuentaGastos16,CuentaGastos0) Values(";
                                    Sql = Sql + intEje + ",";
                                    Sql = Sql + intMes + ",";
                                    Sql = Sql + "'D',";
                                    Sql = Sql + intPoliza + ",";
                                    Sql = Sql + intSeqXmlAnexados + ",'";
                                    Sql = Sql + strCuentaProv + "','";
                                    Sql = Sql + strUUID + "','";
                                    Sql = Sql + dNeto.ToString() + "','";
                                    Sql = Sql + strRfc + "','";
                                    Sql = Sql + "2" + "','";
                                    Sql = Sql + "" + "','";
                                    Sql = Sql + "P" + "','";
                                    Sql = Sql + strSerie + "','";
                                    Sql = Sql + sFactura + "','";
                                    Sql = Sql + strCtaGasto + "','";
                                    Sql = Sql + sCuentaCompras0 + "');";

                                    cmd.CommandText = Sql;
                                    cmd.ExecuteNonQuery();

                                } // dr.HasRows
                                else
                                {
                                    dr.Close();
                                }
                            }


                            strSerieCompra = strSerie;
                            intFolioCompra = intFolio;
                            intPolCompras = intPoliza;

                            result = PagosCxPActualizaPoliza();
                            if (result != "OK")
                            {
                                transaction.Rollback();
                                cnnCyB.Close();
                                return "No se pudo actualizar póliza en PagosCxP SQL: " + result;
                            }


                        }

                        intPoliza = intPoliza + 1;
                    } //Pasa filtro para insertar
                } // Eof:ForEach Grid


                if (iniTran)
                {
                    transaction.Commit();
                }
                cnnCyB.Close();
                return "OK";
            }
            catch (Exception ex)
            {
                if (iniTran)
                {
                    transaction.Rollback();
                }
                return ex.Message + ' ' + strCpt1;
            }
        } // Poliza de pagos a proveedores

        #endregion Métodos Access
    }
}
