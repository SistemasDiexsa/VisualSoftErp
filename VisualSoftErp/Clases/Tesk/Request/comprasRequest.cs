using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeskApiTest.Request
{
    public class comprasRequest
    {
        public string cfdiXml { get; set; }
        public List<conceptosListas> conceptos { get; set; }
        public bool costoIva { get; set; }
        public string cuentaTipoProveedor { get; set; }
        public string cuentaAcreedor { get; set; }
        public string fechaCfdi { get; set; }
        public string fechaPoliza { get; set; }
        public int folio { get; set; }
        public string formaPago { get; set; }
        public string metodoPago { get; set; }
        public string moneda { get; set; }
        public bool pagoTercero { get; set; }
        public decimal retencionIsr { get; set; }
        public decimal retencionIva { get; set; }
        public string serie { get; set; }
        public decimal subTotal { get; set; }
        public decimal tipoCambio { get; set; }
        public decimal total { get; set; }
        public decimal totalIva { get; set; }
        public string usoDeCfdi { get; set; }
        public decimal valorActosExento { get; set; }
        public decimal valorActosExentoImportacion { get; set; }
        public decimal valorActosNoAfecto { get; set; }
        public decimal valorActosTasa0 { get; set; }
        public decimal valorActosTasa0Importacion { get; set; }
        public decimal valorActosTasa16 { get; set; }
        public decimal valorActosTasa16Importacion { get; set; }
        public decimal valorActosTasa4 { get; set; }
        public decimal valorActosTasa8 { get; set; }





    }

    public class conceptosListas
    {
        public decimal cantidad { get; set; }
        public string cuentaEgreso { get; set; }
        public string descripcion { get; set; }
        public decimal descuento { get; set; }
        public bool ivaNoDeducible { get; set; }
        public decimal precioUnitario { get; set; }
        public string tipoIva { get; set; }
    }
}
