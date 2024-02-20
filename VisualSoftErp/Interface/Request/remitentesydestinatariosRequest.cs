using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VisualSoftErp.Interface.Request
{

    public class remitentesyDestinatariosLlenaCajasRequest
    {
        public int RemitentesydestinatariosID { get; set; }
    }

    
    public class remitentesyDestinatariosCRUDRequest
    {
         public int RemitentesydestinatariosID { get; set; }
         public string Rfc { get; set; }
         public string Nombre { get; set; }
         public string NumRegIdTrib { get; set; }
         public string ResidenciaFiscal { get; set; }
         public string Recoger { get; set; }
         public int Activo { get; set; }
    }
}