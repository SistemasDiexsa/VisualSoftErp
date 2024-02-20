using DevExpress.DocumentServices.ServiceModel.DataContracts;
using DevExpress.XtraSpreadsheet.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace VisualSoftErp.Clases.VentasCLs
{
    internal class VentaEstadoClienteCanalCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
        public DateTime FechaIni { get; set; }
        public DateTime FechaFin { get; set; }
        public int intCanalesdeventaID { get; set; }
        public int intSepomexEstadosID { get; set; }

        #endregion


        #region constructor

        public VentaEstadoClienteCanalCL()
        {
        

            DateTime strFechaIni = DateTime.Now;
            FechaIni = DateTime.Now;
            FechaFin = DateTime.Now;
            intCanalesdeventaID = 0;
            intSepomexEstadosID = 0;
            



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
  



       

        #endregion

















    }
}
