using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeskApiTest.Request
{
    public class cancelaComprasRequest
    {
        public string cuentaTipoProveedor { get; set; }
        public string folioInterno { get; set; }
        public string polizaCancelacionFecha { get; set; }
    }
}
