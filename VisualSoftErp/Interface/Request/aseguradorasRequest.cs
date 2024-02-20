using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSoftErp.Interface.Request
{
    public class aseguradorasRequest
    {
        public int AseguradorasID { get; set; }
    }
    public class aseguradorasCRUDRequest
    {
        public int AseguradorasID { get; set; }
        public string Nombre { get; set; }
    }
}
