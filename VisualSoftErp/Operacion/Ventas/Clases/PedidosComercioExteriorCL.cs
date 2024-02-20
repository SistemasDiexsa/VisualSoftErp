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
    public class PedidosComercioExteriorCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
        public string strSerie { get; set; }
        public int intFolio { get; set; }
        public string strTipooperacion { get; set; }
        public string strClavedepedimento { get; set; }
        public string strCertificadororigen { get; set; }
        public string strNumcertificadoorigen { get; set; }
        public string strNumexportadorconfiable { get; set; }
        public string strIncoterm { get; set; }
        public string strSubdivision { get; set; }
        public string strceObs { get; set; }
        public DataTable dtCE { get; set; }
        public DataTable dtCEDet { get; set; }
        public string strTipoExportacion { get; set; }
        #endregion

        #region Constructor
        public PedidosComercioExteriorCL()
        {
            strSerie = string.Empty;
            intFolio = 0;
            strTipooperacion = string.Empty;
            strClavedepedimento = string.Empty;
            strCertificadororigen = string.Empty;
            strNumcertificadoorigen = string.Empty;
            strNumexportadorconfiable = string.Empty;
            strIncoterm = string.Empty;
            strSubdivision = string.Empty;
            strceObs = string.Empty;
            dtCE = null;
            dtCEDet = null;
            strTipoExportacion = string.Empty;
        }
        #endregion

        #region Metodos
       
        public DataTable PedidosComercioExteriorGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PedidosdetalleComercioExteriorLLenar";
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
        } //pedidoscomercioexteriorGrid

        public DataTable PedidosComercioExteriorDetalleGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PedidosComercioExteriorDetalleGrid";
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
        } //pedidoscomercioexteriorGrid

        public string PedidosComercioExteriorCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PedidosComercioExteriorCRUD";
                cmd.Parameters.AddWithValue("@prmPedidos", dtCE);
                cmd.Parameters.AddWithValue("@prmPedidosDetalle", dtCEDet);
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.Parameters.AddWithValue("@prmUsuario", globalCL.gv_UsuarioID);
                cmd.Parameters.AddWithValue("@prmMaquina", Environment.MachineName);
               
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
        } //PedidosComercioExteriorCrud

        public string PedidosComercioExteriorLlenaCajas()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PedidosComercioExteriorLLenaCajas";
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();                  
                    strTipooperacion = dr["Tipooperacion"].ToString();
                    strClavedepedimento = dr["Clavedepedimento"].ToString();
                    strCertificadororigen = dr["Certificadororigen"].ToString();
                    strNumcertificadoorigen = dr["Numcertificadoorigen"].ToString();
                    strNumexportadorconfiable = dr["Numexportadorconfiable"].ToString();
                    strIncoterm = dr["Incoterm"].ToString();
                    strSubdivision = dr["Subdivision"].ToString();
                    strceObs = dr["ceObs"].ToString();
                    strTipoExportacion = dr["TipoExportacion"].ToString();
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

        public string PedidosComercioExteriorEliminar()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PedidosComercioExteriorEliminar";
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
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

        #endregion
    }
}