using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeskApiTest.Request
{
    public class pagarFacturasRequest
    {
        public string codigoBancoSatDestino { get; set; }
        public string cuentaBancoCaja { get; set; }
        public string cuentaProveedor { get; set; }
        public List<pagarFacturasDetalle> facturasAPagar { get; set; }
        public string fechaPago { get; set; }
        public string formaPagoSat { get; set; }
        public string metodoPago { get; set; }
        public List<pagarFacturasAjustes> movimientosAjuste { get; set; }
        public decimal netoAPagarMe { get; set; }
        public decimal netoAPagarMn { get; set; }
        public string numeroCuentaDestino { get; set; }
        public string referencia { get; set; }
        public decimal tipoCambioPagoMe { get; set; }
    }

    public class pagarFacturasDetalle
    {
        public string folio { get; set; }
        public decimal importeAPagarMe { get; set; }
        public decimal importeAPagarMn { get; set; }
    }

    public class pagarFacturasAjustes
    {
        public decimal abono { get; set; }
        public decimal cargo { get; set; }
        public string concepto { get; set; }
        public string cuentaContable { get; set; }
        public string departamentoCodigo { get; set; }
        public string sucursalCodigo { get; set; }
    }
}
