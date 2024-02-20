using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeskApiTest.Request
{
    public class cobranzaDepositoRequest
    {
        //public string cfdiRepXml { get; set; }
        public string codigoFormaPagoSat { get; set; }
        public string cuentaDepositadoEn { get; set; }
        public List<facturasCobradasModel> facturasCobradas { get; set; }
        public string fechaDeposito { get; set; }
        public string fechaPoliza { get; set; }
        public decimal importeMe { get; set; }
        public decimal importeMn { get; set; }
        public List<movimientosAjusteModel> movimientosAjuste { get; set; }
        public string referencia { get; set; }
        public decimal tipoCambioPagoMe { get; set; }
    }

    public class facturasCobradasModel
    {
        public int folio { get; set; }
        public decimal importeCobroMe { get; set; }
        public decimal importeCobroMn { get; set; }
        public string observaciones { get; set; }
        public string serie { get; set; }
    }

    public class movimientosAjusteModel
    {
        public decimal abono { get; set; }
        public decimal cargo { get; set; }
        public string concepto { get; set; }
        public string cuentaContable { get; set; }
        public string departamentoCodigo { get; set; }
        public string sucursalCodigo { get; set; }


    }

}
