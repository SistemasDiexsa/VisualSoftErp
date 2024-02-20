using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSoftErp.Interface.Request
{
    public class cfdiDatosFiscalesVariosRequest
    {
        public int Empresa { get; set; }
        public int Carro { get; set; }
        public int Remolque { get; set; }
        public int Remolque2 { get; set; }
        public int Operador { get; set; }
        public int Cliente { get; set; }
        public int Remitente { get; set; }
        public int Destinatario { get; set; }
        public int DomOri { get; set; }
        public int DomDes { get; set; }
        public string Serie { get; set; }
    }
}
