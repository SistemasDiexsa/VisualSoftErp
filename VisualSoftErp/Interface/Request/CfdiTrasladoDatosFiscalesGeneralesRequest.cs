using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSoftErp.Interface.Request
{
    public class CfdiTrasladoDatosFiscalesGeneralesRequest
    {
        public int Empresa { get; set; }
        public int Carro { get; set; }
        public int Remolque { get; set; }
        public int Remolque2 { get; set; }
        public int Operador { get; set; }
        public string Serie { get; set; }
    }
}
