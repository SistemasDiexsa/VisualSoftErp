using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSoftErp.Interface.Request
{
    public class cfdiTraslado
    {
        public string Serie { get; set; }
        public int Folio { get; set; }
        public string Fecha { get; set; }
        public string LugarExpedicionCP { get; set; }
        public string UUID { get; set; }
        public int Status { get; set; }
        public string FechaCancelacion { get; set; }
        public string RazonCancelacion { get; set; }
        public int UsuariosID { get; set; }
        public int AlmacenesID { get; set; }
        public string Observaciones { get; set; }
    }
}
