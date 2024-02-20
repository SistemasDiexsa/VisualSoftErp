using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VisualSoftErp.Interface.Response
{
    public class remitentesydestinatariosResponse
    {
    }

    
    public class remitentesyDestinatariosLlenaCajasResponse
    {
         public int RemitentesydestinatariosID { get; set; }
         public string Rfc { get; set; }
         public string Nombre { get; set; }
         public string NumRegIdTrib { get; set; }
         public string ResidenciaFiscal { get; set; }
         public string Recoger { get; set; }
         public int Activo { get; set; }

    }

    
    public class remitentesyDestinatariosGRIDResponse
    {
         public string RemitentesydestinatariosID { get; set; }
         public string Rfc { get; set; }
         public string Nombre { get; set; }
         public string NumRegIdTrib { get; set; }
         public string ResidenciaFiscal { get; set; }
         public string Recoger { get; set; }
         public string Activo { get; set; }
    }

}