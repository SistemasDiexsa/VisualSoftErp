using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeskApiTest.Request
{
    public class cancelaCfdiRequest
    {
        public string fechaCancelacionSat { get; set; }
        public string fechaPolizaCancelacion { get; set; }
        public int folio { get; set; }
        public string selloCancelacionSat { get; set; }
        public string serie { get; set; }
    }
}
