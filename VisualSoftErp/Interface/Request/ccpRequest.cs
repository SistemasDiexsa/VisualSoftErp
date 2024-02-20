using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSoftErp.Interface.Request
{
    public class ccpRequest
    {
        public int ArrendatariosID { get; set; }
        public decimal Autopistas { get; set; }
        public int CarrosID { get; set; }
        public string CveTransporte { get; set; }
        public decimal Demoras { get; set; }
        public int DestinatariosID { get; set; }
        public decimal DistanciaRecorrida { get; set; }
        public string Documento { get; set; }
        public string EntradaSalidaMerc { get; set; }
        public decimal Estadias { get; set; }
        public string FechaHoraProgLlegada { get; set; }
        public string FechaHoraSalida { get; set; }
        public decimal Flete { get; set; }
        public int Folio { get; set; }
        public int IDDestinoID { get; set; }
        public int IDOrigenID { get; set; }
        public decimal Maniobras { get; set; }
        public string NombreAseg { get; set; }
        public string NumPermisoSCT { get; set; }
        public string NumPolizaSeguro { get; set; }
        public int NumTotalMercancias { get; set; }
        public int OperadoresID { get; set; }
        public string PermSCT { get; set; }
        public decimal PesoNetoTotal { get; set; }
        public int PropietariosID { get; set; }
        public string Referencia { get; set; }
        public int RemitentesID { get; set; }
        public int Remolques2ID { get; set; }
        public int RemolquesID { get; set; }
        public decimal Reparto { get; set; }
        public string Satelite { get; set; }
        public decimal Seguro { get; set; }
        public string Sellos { get; set; }
        public string Serie { get; set; }
        public string TipoEstacion { get; set; }
        public decimal TotalDistRec { get; set; }
        public string TranspInternac { get; set; }
        public string ViaEntradaSalida { get; set; }
    }
}
