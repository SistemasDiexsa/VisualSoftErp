using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualSoftErp.Clases;
using VisualSoftErp.Clases.HerrramientasCLs;

namespace VisualSoftErp.Operacion.Ventas.Clases
{
    public class PreciosCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
        public decimal dPrecio { get; set; }
            public int iCliente { get; set; }
            public int iLp { get; set; }
            public int iArtID { get; set; }
            public string strSerie { get; set; }
            public string strSeTomaPrecioPublico { get; set; }
        #endregion
        #region Constructor
        public PreciosCL()
        {
            dPrecio = 0;
            iCliente = 0;
            iLp = 0;
            iArtID = 0;
        }
        #endregion
        #region Metodos
        public string Precio()
        {
            try
            {
                DatosdecontrolCL cl = new DatosdecontrolCL();
                string result = cl.DatosdecontrolLeer();
                if (result!="OK" )
                {
                    return "Al leer datos de control: " + result;
                }

                if (cl.iManejarlistasdeprecios==1)
                {
                    ListasdeprecioCL clp = new ListasdeprecioCL();
                    clp.intArt = iArtID;
                    clp.intListasdeprecioID = iLp;
                    result = clp.ListasdeprecioLeeunPrecio();
                    if (result=="OK")
                    {
                        dPrecio = clp.dPrecio;
                        strSeTomaPrecioPublico = clp.strSeTomaPrecioPublico;
                    }
                    else
                    {
                        return "Al leer el precio: " + result;
                    }
                } else if (cl.iManejarultimosprecios==1)
                {
                    //Falta meter validación si el usuario puede o no traer ultimos precios
                    result = UltimoPrecio();
                    if (result != "OK")
                    {
                        return "Ultimoprecio: " + result;
                    }
                } else  
                {   //Es posible que no maneje LP ni quieran últimos precios
                    dPrecio = 0;
                    return "OK";
                }
                

                return "OK";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
        public string UltimoPrecio()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FacturasObtenUltimoPrecio";
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmCliente", iCliente);
                cmd.Parameters.AddWithValue("@prmArt", iArtID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    dPrecio = Convert.ToDecimal(dr["Precio"]);
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
        #endregion
    }
}
