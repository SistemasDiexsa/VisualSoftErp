using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSoftErp.Interface.Request
{
    public class remolquesRequest
    {
        public class remolquesLlenaCajasRequest
        {
            public int RemolquesID { get; set; }
        }

        public class remolquesCRUDRequest
        {
            public int RemolquesID { get; set; }
            public string Descripcion { get; set; }
            public int MarcasID { get; set; }
            public int Modelo { get; set; }
            public string NumEco { get; set; }
            public string SubTipoRem { get; set; }
            public string Placa { get; set; }

        }
    }
}
