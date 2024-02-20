using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSoftErp.Interface.Request
{
    public class ccpUbicacionesRequest
    {
        public string Serie { get; set; }
        public int Folio { get; set; }
        public int RemitentesID { get; set; }
        public string FechaSalida { get; set; }
        public string HoraSalida { get; set; }
        public int OrigenID { get; set; }
        public int DestinatariosID { get; set; }
        public string FechaLlegada { get; set; }
        public string HoraLlegada { get; set; }
        public int DestinoID { get; set; }
        public decimal Kms { get; set; }
        public int Seq { get; set; }
    }
}
