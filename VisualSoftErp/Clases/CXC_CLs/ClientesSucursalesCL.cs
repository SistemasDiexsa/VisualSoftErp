using DevExpress.CodeParser;
using DevExpress.XtraSpreadsheet.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using VisualSoftErp.Catalogos;
using XmlElement = System.Xml.XmlElement;

namespace VisualSoftErp.Clases.CXC_CLs
{
    public class ClientesSucursalesCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
        public int IntClientesID { get; set; }
        public int IntSucursal { get; set; }
        public string strClienteNombre { get; set; }
        public string strNombre { get; set; }
        public string strDir { get; set; }
        public string strCd { get; set; }
        public string strTel { get; set; }
        public string strResponsable { get; set; }
        public string strCorreo { get; set; }
        public string strTransporte { get; set; }

        #endregion  //Se Definen las propiedades de la tabla

        #region Constructor
        public ClientesSucursalesCL()
        {
            IntClientesID = 0 ;
            IntSucursal = 0;
            strClienteNombre = string.Empty;
            strNombre = string.Empty;
            strDir = string.Empty;
            strCd = string.Empty;
            strTel = string.Empty;
            strResponsable = string.Empty;
            strCorreo = string.Empty;
            strTransporte = string.Empty;
            strResponsable = string.Empty;
            strCorreo = string.Empty;
            strTransporte = string.Empty;

        } //se les da el valor inicial a las variables declaradas en propiedades

        #endregion

        #region Metodos
        private string loadConnectionString() //Regresa el string de conexion  ( no se usa porque se utiliza el STRcnn
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
            catch (Exception)
            {
                return "";
            }
        }

        public DataTable ClientesSucursalesGrid() // este metodo regresa un DT para llenar el Grid
        {
            DataTable dt = new DataTable(); // instanciamos una variable para manejar el data table
            try
            {
                SqlConnection cnn = new SqlConnection(); // declaramos la variable cnn de tipo conexión SQL
                cnn.ConnectionString = strCnn; // se asigna Str de conexion
                cnn.Open(); // abrimos conexión

                SqlCommand cmd = new SqlCommand(); //declaramos la variable CMD como un comando de SQL
                cmd.CommandText = "ClientesSucursalesGrid";// Ponemos el nombre del Store prosedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;// se indica que el comando es un Store Prosedure
                cmd.Connection = cnn;// le asignamos al comando la conexión

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);//declaramos una variable para el adaptador de SQL y le mandamos el comando (cmd)
                sqlAD.Fill(dt);// se ejecuta el Store prosedure y el resultado lo deja en (dt)
                cnn.Close();// se cierra la conexión a SQL
                return dt; // regresa el resultado .

            }
            catch (Exception ex) { return dt; }
        } //Proceso para llenado de el data Grid

        public string ClientesSucursalesLlenaCajas()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ClientesSucursalesLlenaCajas";
                cmd.Parameters.AddWithValue("@prmCliente", IntClientesID);
                cmd.Parameters.AddWithValue("@prmSucursal", IntSucursal );
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

 

                SqlDataReader dr = cmd.ExecuteReader();//se ejecuta el store prosedure y guarda el resultado en la variable dr que es un data reader de SQL
                if (dr.HasRows) // se valida que que existan registros
                {
                    dr.Read();   // leemos el Registro                 
                    strCd = dr["Cd"].ToString(); // pasamos los campos que vienen del store prosedure a las propiedades correspondientes
                    IntClientesID = Convert.ToInt32(dr["ClientesID"]);
                    strCorreo = dr["Correo"].ToString();
                    strDir = dr["Dir"].ToString();
                    strNombre = dr["Nombre"].ToString();
                    strResponsable = dr["Responsable"].ToString();
                    IntSucursal = Convert.ToInt32 ( dr["Sucursal"]);
                    strTel = dr["Tel"].ToString();
                    strResponsable = dr["Responsable"].ToString();
                    strTransporte = dr["Transporte"].ToString();
                    result = "OK"; // regresamos OK en este punto al estar correctamente ejecutado
                }
                else
                {
                    result = "no read"; // se ejecuta cuando no regresa ningún registo 
                }

                dr.Close();// cerramos el data reader
                cnn.Close(); // cerramos la conexíon a SQL
                return result;// regresamos el resultado que puede ser OK o no read
            }
            catch (Exception ex)
            {
                return ex.Message; // regrresa el mensaje de error 
            }
        } // Proceso para llenado de TXT al editar un registro 

        public string ClientesSucursalesCRUD()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ClientesSucursalesCRUD";
                cmd.Parameters.AddWithValue("@prmClientesID", IntClientesID);
                cmd.Parameters.AddWithValue("@prmSucursal", IntSucursal);
                cmd.Parameters.AddWithValue("@prmNombre", strNombre);
                cmd.Parameters.AddWithValue("@prmDir", strDir );
                cmd.Parameters.AddWithValue("@prmCd", strCd );
                cmd.Parameters.AddWithValue("@prmTel", strTel );
                cmd.Parameters.AddWithValue("@prmResponsable", strResponsable);
                cmd.Parameters.AddWithValue("@prmCorreo", strCorreo );
                cmd.Parameters.AddWithValue("@prmTransporte", strTransporte);
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
        } // Crea o actualiza un Registro

        public string ClientesSucursalesEliminar()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ClientesSucursalesEliminar";
                cmd.Parameters.AddWithValue("@prmClientesID", IntClientesID);
                cmd.Parameters.AddWithValue("@prmSucursal", IntSucursal);               
                
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
        } // Elimina un Registro
        #endregion

    }
}
