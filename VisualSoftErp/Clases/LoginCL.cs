using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualSoftErp.Clases.Tesk.Interface;
using VisualSoftErp.Interface;
using VisualSoftErp.Interfaces;

namespace VisualSoftErp.Clases
{
    public class LoginCL
    {

        #region Propiedades

        string strCnn = globalCL.gv_strcnn;
            //ConfigurationManager.ConnectionStrings["VisualSoftErpConnectionString"].ConnectionString;
        public string sLogin { get; set; }
        public string sPassword { get; set; }
        public string sPasswordNuevo { get; set; }
        public int iUsuarioId { get; set; }
        public string sNombre { get; set; }
        public int iNumerodeEmpresas { get; set; }
        public string sRfc { get; set; }
        public string sDir { get; set; }
        public string sTel { get; set; }
        public string sCorreo { get; set; }
        public string sPagina { get; set; }
        public string sNomEmp { get; set; }
        #endregion
        #region constructor
        public LoginCL()
        {
            sLogin = string.Empty;
            sPassword = string.Empty;
        }
        #endregion
        #region Metodos
        public DataTable Accesos()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "UsuariosAccesos";
                cmd.Parameters.AddWithValue("@prmUsu", iUsuarioId);
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


        }//CargarCombos()
        public string Login()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = globalCL.gv_strcnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "UsuarioLogin";
                cmd.Parameters.AddWithValue("@prmLogin", sLogin);
                cmd.Parameters.AddWithValue("@prmPassword", sPassword);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    iUsuarioId = Convert.ToInt32(dr["UsuariosID"]);
                    sNombre = dr["Nombre"].ToString();
                    sNomEmp = dr["NomEmp"].ToString();
                    result = "OK";
                }
                else
                {
                    result = "Login o Password incorrecto";
                }

                dr.Close();
                cnn.Close();
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        } // Login
        public string Verificasihayempresas()
        {
            try
            {
                string result = string.Empty;
                //iNumerodeEmpresas = 1;
                //result = "OK";
                //return result;



                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = globalCL.gv_strcnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "EmpresasVerificasihayalmenosuna";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    iNumerodeEmpresas = Convert.ToInt32(dr["Empresas"]);                  
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
        } // Verificasihayempresas
        public string LeeDatosEmpresa()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "EmpresaWizzardLlenaCajas";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    //iEmpresaID = Convert.ToInt32(dr["EmpresasID"]);
                    sNombre = dr["Nombre"].ToString();
                    sRfc = dr["Rfc"].ToString();
                    sDir = dr["Direccion"].ToString();
                    sTel = dr["Telefono"].ToString();
                    sCorreo = dr["Correo"].ToString();
                    sPagina = dr["www"].ToString();

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
        } // Verificasihayempresas
        public string EmpresasCrud()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "EmpresaCRUDWizzard";
                cmd.Parameters.AddWithValue("@prmNombre",sNombre);
                cmd.Parameters.AddWithValue("@prmRfc",sRfc);
                cmd.Parameters.AddWithValue("@prmDir",sDir);
                cmd.Parameters.AddWithValue("@prmTel",sTel);
                cmd.Parameters.AddWithValue("@prmCorreo",sCorreo);
                cmd.Parameters.AddWithValue("@prmPagina",sPagina);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    result = dr["result"].ToString();
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
        } // EmpresasCrud
        public string InsertaUsuarioyAccesos()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "UsuariosWizardCRUD";               
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    result = dr["result"].ToString();
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
        } // InsertaUsuarioyAccesos
        public string CambiarPassword()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CambiarPassword";
                cmd.Parameters.AddWithValue("@prmLogin", sLogin);
                cmd.Parameters.AddWithValue("@prmClave", sPasswordNuevo);
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
        } // CambiarPassword
        
        public string LeeConexion()
        {
            try
            {
                //"Data Source=SQL5111.site4now.net;Initial Catalog=db_a6b458_diexsaerp;User Id=db_a6b458_diexsaerp_admin;Password=YOUR_DB_PASSWORD

                string result = string.Empty;

                MalocClient cliente = new MalocClient();
                conexionRequest request = new conexionRequest();
                request.rfc = sRfc;

                //string url = "http://visualsoft.com.mx:100/";
                string url = "http://ocupoalgo.com:73/";

                //Descomentar sig 5 líneas para hacerlo dinamico
                //conexionResponse datos = cliente.PeticionesWSPost<conexionResponse>(url, "/VisualsoftErpWS.svc/Reportedeventasaseguradas", request);

                //string server = DesEncriptar(datos.srv);
                //string catalogo = DesEncriptar(datos.cat);
                //string user = DesEncriptar(datos.uid);
                //string pass = DesEncriptar(datos.pwd);


                
                string strambiente = string.Empty;
                strambiente = ConfigurationManager.AppSettings["Ambiente"].ToString();

                string server = string.Empty;
                string catalogo = string.Empty;
                string user = string.Empty;
                string pass = string.Empty;

                if (strambiente == "Productivo")
                {
                    server = "99.79.176.209";
                    catalogo = "DiexsaErp";
                    user = "diexsaUserSuEmp";
                    pass = "dx2024*";
                }
                else if (strambiente == "Desarrollo")
                {
                    server = "cartasportemty.com";
                    catalogo = "_DiexsaErp";
                    user = "diexsaUser24";
                    pass = "diexsaUser24";
                }
                else if (strambiente == "Local")
                {
                    server = "DIEXSASIST\\SQLEXPRESS";
                    catalogo = "DiexsaErp";
                    user = "DiexsaUser";
                    pass = "dx2024*";
                }


                globalCL.gv_strcnn = "Data Source=" + server + "; Initial Catalog=" + catalogo + "; user id=" + user + "; Password=" + pass;
                if (server == "NoLogin")
                    return "Credenciales inválidas";
                else
                    return "OK";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        } //Leeconexion

        /// Esta función desencripta la cadena que le envíamos en el parámentro de entrada.
        public string DesEncriptar(string _cadenaAdesencriptar)
        {
            string result = string.Empty;
            byte[] decryted = Convert.FromBase64String(_cadenaAdesencriptar);
            //result = System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
            result = System.Text.Encoding.Unicode.GetString(decryted);
            return result;
        }
        #endregion

    }
}
