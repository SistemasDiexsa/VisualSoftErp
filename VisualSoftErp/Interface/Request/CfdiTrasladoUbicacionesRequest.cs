using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSoftErp.Interface.Request
{
    public class CfdiTrasladoUbicacionesRequest
    {
        public int Remitente { get; set; }
        public int Destinatario { get; set; }
        public int DomOri { get; set; }
        public int DomDes { get; set; }
    }
}
