using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VSCartasPorte.Interface.Request
{
    public class cfdiImpresionDatosAdicionalesRequest
    {
        public string Serie { get; set; }
        public int Folio { get; set; }
        public string FP { get; set; }
        public string MP { get; set; }
        public string USO { get; set; }
        public string ClienteRfc { get; set; }
    }
}