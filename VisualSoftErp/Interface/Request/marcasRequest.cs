using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSoftErp.Interface.Request
{
    public class marcasRequest
    {
         public int MarcasID { get; set; }
    }

    public class marcasCRUDRequest
    {
         public int MarcasID { get; set; }
         public string Des { get; set; }
    }
}
