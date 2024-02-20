using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeskApiTest.Request
{
    public class postServiciosModelRequest
    {
        public string codigo { get; set; }
        public string codigoServSat { get; set; }
        public string codigoUnidadSat { get; set; }
        public string cuentaTipoIngreso { get; set; }
        public string descripcion { get; set; }
        public bool ivaIncluido { get; set; }
        public decimal precioUnitario { get; set; }
        public string tipoIva { get; set; }
        public string tipoRetencionIsr { get; set; }
        public string tipoRetencionIva { get; set; }

    }
}
