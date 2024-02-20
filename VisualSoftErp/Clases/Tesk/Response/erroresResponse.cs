using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSoftErp.Clases.Tesk.Response
{
    public class erroresResponse
    {
        public string mensaje { get; set; }
        public string tipoError { get; set; }
        public ValidacionResponse validacionResponse { get; set; }
        public string polizaCancelacion { get; set; }
        public string poliza { get; set; }


    }
    public class ValidacionResponse
    {
        public List<Error> errores { get; set; }
    }
    public class Error
    {
        public string campo { get; set; }
        public List<string> error { get; set; }
    }

    public class respuestaCobranza 
    {
        public string polizaCobranza { get; set; }
        public string mensaje { get; set; }
        public string poliza { get; set; }
    }

    public class respuestaCompras
    {
        public string poliza { get; set; }
        public string mensaje { get; set; }
    }

    public class erroresResponseContrarecibos
    {
        public string poliza { get; set; }
        public string mensaje { get; set; }
        public string tipoError { get; set; }
        public ValidacionResponse validacionResponse { get; set; }


    }
}
