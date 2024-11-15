﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml;

namespace VisualSoftErp.Clases
{
    public class CortesCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
        public string strSerie { get; set; }
        public int intFolio { get; set; }
        public int intSeq { get; set; }
        public DateTime fFecha { get; set; }
        public string strTurno { get; set; }
        public int intTurno { get; set; }
        public string strElaboro { get; set; }
        public int intArticulocortadoID { get; set; }
        public string strArtCor { get; set; }
        public decimal dCantidadcortada { get; set; }
        public string strLetraCortada { get; set; }
        public int intArticuloproducidoID { get; set; }
        public string strArtPro { get; set; }
        public decimal dCantidadproducida { get; set; }
        public string strletraproducida { get; set; }
        public string strStatus { get; set; }
        public int intUsuariosID { get; set; }
        public int intMaquina { get; set; }
        public string strObservaciones { get; set; }
        public decimal dUltimoCostoCortado { get; set; }
        public decimal dCostoProducido { get; set; }
        public int intAlmacenCortado { get; set; }
        public int intAlmacenProducido { get; set; }
        public DateTime fFechaReal { get; set; }
        public DataTable dtm { get; set; }
        public DataTable dtd { get; set; }
        public int intUsuarioID { get; set; }
        public string strMaquina { get; set; }
        public int intAño { get; set; }
        public int intMes { get; set; }
        public DataTable detalleUtilizado { get; set; }
        public DataTable detalleProducido { get; set; }
        public int intProduccionID { get; set; }
        public int intDetalleUtilizadoID { get; set; }
        public int intDetalleProducidoID { get; set; }
        #endregion

        #region Constructor
        public CortesCL()
        {
            strSerie = string.Empty;
            intFolio = 0;
            intSeq = 0;
            fFecha = DateTime.Now;
            strTurno = string.Empty;
            strElaboro = string.Empty;
            intArticulocortadoID = 0;
            strArtCor = string.Empty;
            dCantidadcortada = 0;
            strLetraCortada = string.Empty;
            intArticuloproducidoID = 0;
            strArtPro = string.Empty;
            dCantidadproducida = 0;
            strletraproducida = string.Empty;
            strStatus = string.Empty;
            intUsuariosID = 0;
            intMaquina = 0;
            strObservaciones = string.Empty;
            dUltimoCostoCortado = 0;
            dCostoProducido = 0;
            intAlmacenCortado = 0;
            intAlmacenProducido = 0;
            fFechaReal = DateTime.Now;
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
        
        public DataTable CortesGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "cortesGrid";
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
        } //cortesGrid

        public DataTable CortesLlenaCajas()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CortesLlenaCajas";
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
            
                dtd = dt;
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //notasdecreditoGrid

        public string CortesCRUD()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CortesCRUD";
                cmd.Parameters.AddWithValue("@prmCortes", dtm);
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

        public string DepositosdetalleLlenaCajas()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "DepositosdetallellenaCajas";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    //strSERIE = dr["SERIE"].ToString();
                    //intFolio = Convert.ToInt32(dr["Folio"]);
                    //intSEQ = Convert.ToInt32(dr["SEQ"]);
                    //strSerieCFDI = dr["SerieCFDI"].ToString();
                    //intFolioCfdi = Convert.ToInt32(dr["FolioCfdi"]);
                    //intSUPAGO = Convert.ToInt32(dr["FolioCfdi"]);
                    //intSUPAGOCONVERTIDO = Convert.ToInt32(dr["FolioCfdi"]);
                    //intTipoMov = Convert.ToInt32(dr["TipoMov"]);
                    //intCxCID = Convert.ToInt32(dr["CxCID"]);
                    //result = "OK";
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

        public string DepositosdetalleEliminar()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "DepositosdetalleEliminar";
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
        
        public DataTable ProduccionGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cnn = new SqlConnection(globalCL.gv_strcnn))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("ProduccionGrid", cnn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@prmAño", intAño);
                        cmd.Parameters.AddWithValue("@prmMes", intMes);
                        using (SqlDataAdapter sqlAD = new SqlDataAdapter(cmd))
                        {
                            sqlAD.Fill(dt);
                        }
                    }
                }
                return dt;             
            }
            catch (Exception ex) 
            {
                return dt; 
            }
        }

        public string ProduccionCrud()
        {
            try
            {
                string result = string.Empty;
                using (SqlConnection cnn = new SqlConnection(globalCL.gv_strcnn))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("ProduccionCrud", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                        cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                        cmd.Parameters.AddWithValue("@prmElaboro", strElaboro);
                        cmd.Parameters.AddWithValue("@prmTurno", intTurno);
                        cmd.Parameters.AddWithValue("@prmFecha", fFecha);
                        cmd.Parameters.AddWithValue("@prmObservaciones", strObservaciones);
                        cmd.Parameters.AddWithValue("@prmUsuariosID", globalCL.gv_UsuarioID);
                        cmd.Parameters.AddWithValue("@prmMaquina", Environment.MachineName);
                        cmd.Parameters.AddWithValue("@prmDetalleUtilizado", detalleUtilizado);
                        cmd.Parameters.AddWithValue("@prmDetalleProducido", detalleProducido);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                result = dr["result"].ToString();
                            }
                            else
                                result = "no read";
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string ProduccionLlenaCajas()
        {
            string result = string.Empty;
            try
            {
                DataSet dataSet = new DataSet();
                using (SqlConnection cnn = new SqlConnection(globalCL.gv_strcnn))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("ProduccionLlenaCajas", cnn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                        cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dataSet);
                        }
                    }
                }

                foreach (DataRow dr in dataSet.Tables[0].Rows)
                {
                    result = dr["result"].ToString();
                    strSerie = dr["Serie"].ToString();
                    intFolio = Convert.ToInt32(dr["Folio"]);
                    strElaboro = dr["Elaboro"].ToString();
                    intTurno = Convert.ToInt32(dr["Turno"]);
                    fFecha = Convert.ToDateTime(dr["Fecha"]);
                    strObservaciones = dr["Observaciones"].ToString();
                }

                detalleUtilizado = new DataTable();
                detalleUtilizado.Columns.Add("intProduccionID", typeof(int));
                detalleUtilizado.Columns.Add("intDetalleID", typeof(int));
                detalleUtilizado.Columns.Add("strSerie", typeof(string));
                detalleUtilizado.Columns.Add("intFolio", typeof(int));
                detalleUtilizado.Columns.Add("intAlmacenesID", typeof(int));
                detalleUtilizado.Columns.Add("intArticulosID", typeof(int));
                detalleUtilizado.Columns.Add("decCantidad", typeof(decimal));
                detalleUtilizado.Columns.Add("strLetra", typeof(string));
                foreach (DataRow item in dataSet.Tables[1].Rows)
                {
                    DataRow row = detalleUtilizado.NewRow();
                    row["intProduccionID"] = item["ProduccionID"];
                    row["intDetalleID"] = item["DetalleUtilizadoID"];
                    row["strSerie"] = item["Serie"];
                    row["intFolio"] = item["Folio"];
                    row["intAlmacenesID"] = item["AlmacenesID"];
                    row["intArticulosID"] = item["ArticulosID"];
                    row["decCantidad"] = item["Cantidad"];
                    row["strLetra"] = item["Letra"];
                    detalleUtilizado.Rows.Add(row);
                }

                detalleProducido = new DataTable();
                detalleProducido.Columns.Add("intProduccionID", typeof(int));
                detalleProducido.Columns.Add("intDetalleID", typeof(int));
                detalleProducido.Columns.Add("strSerie", typeof(string));
                detalleProducido.Columns.Add("intFolio", typeof(int));
                detalleProducido.Columns.Add("intAlmacenesID", typeof(int));
                detalleProducido.Columns.Add("intArticulosID", typeof(int));
                detalleProducido.Columns.Add("decCantidad", typeof(decimal));
                detalleProducido.Columns.Add("strLetra", typeof(string));
                detalleProducido.Columns.Add("intMaquina", typeof(int));
                foreach (DataRow item in dataSet.Tables[2].Rows)
                {
                    DataRow row = detalleProducido.NewRow();
                    row["intProduccionID"] = item["ProduccionID"];
                    row["intDetalleID"] = item["DetalleProducidoID"];
                    row["strSerie"] = item["Serie"];
                    row["intFolio"] = item["Folio"];
                    row["intAlmacenesID"] = item["AlmacenesID"];
                    row["intArticulosID"] = item["ArticulosID"];
                    row["decCantidad"] = item["Cantidad"];
                    row["strLetra"] = item["Letra"];
                    row["intMaquina"] = item["Maquina"];
                    detalleProducido.Rows.Add(row);
                }

                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public string ProduccionCancelar()
        {
            string result = string.Empty;
            try
            {
                using (SqlConnection cnn = new SqlConnection(globalCL.gv_strcnn))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("ProduccionCancelar", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                        cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                result = dr["result"].ToString();
                            }
                            else
                                result = "no read";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
        #endregion
    }
}