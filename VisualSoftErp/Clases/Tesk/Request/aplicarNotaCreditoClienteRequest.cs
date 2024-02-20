using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSoftErp.Clases.Tesk.Request
{
    public class aplicarNotaCreditoClienteRequest
    {
        public string cfdiXml { get; set; }
        public string cuentaCliente { get; set; }
        public List<facturasAplicadaselementos> facturasAplicadas { get; set; }
        public string fechaCfdi { get; set; }
        public string fechaPoliza { get; set; }
        public int folio { get; set; }
        public string folioFiscal { get; set; }
        public decimal importe { get; set; }
        public string lugarExpedicion { get; set; }
        public string serie { get; set; }
        public string usoDeCfdi { get; set; }

    }

    public class facturasAplicadaselementos
    {
        public string cuentaContableIngresoTasa16 { get; set; }
        public string serie { get; set; }
        public int folio { get; set; }
        public decimal iepsCobrado { get; set; }
        public decimal importeAplicado { get; set; }
        public decimal ingresoTasa0 { get; set; }
        public decimal ingresoTasa16 { get; set; }
        public decimal ingresoTasa8 { get; set; }
        public decimal ingresoTasaExcento { get; set; }
        public decimal ivaCobrado { get; set; }
    }
}
