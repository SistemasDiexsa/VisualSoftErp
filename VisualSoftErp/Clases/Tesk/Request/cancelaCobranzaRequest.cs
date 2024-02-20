using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeskApiTest.Request
{
    public class cancelaCobranzaRequest
    {
        public bool cancelar { get; set; }
        public string fechaPoliza { get; set; }
        public string fechaPolizaCancelacion { get; set; }
        public int numeroPoliza { get; set; }
        public string tipoPoliza { get; set; }
    }
}
