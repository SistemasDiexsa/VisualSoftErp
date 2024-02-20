using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualSoftErp.Response;

namespace VisualSoftErp.Interface.Response
{
    public class remolquesResponse
    {
        public class remolquesLlenaCajasResponse
        {
            public int RemolquesID { get; set; }
            public string Descripcion { get; set; }
            public int MarcasID { get; set; }
            public int Modelo { get; set; }
            public string NumEco { get; set; }
            public string SubTipoRem { get; set; }
            public string Placa { get; set; }

        }

        public class remolquesGRIDResponse
        {
            public string RemolquesID { get; set; }
            public string Descripcion { get; set; }
            public string Marca { get; set; }
            public string Modelo { get; set; }
            public string NumEco { get; set; }
            public string SubTipoRem { get; set; }
            public string Placa { get; set; }
        }

        public class remolquesCombosListResponse
        {
            public List<StrCargaCombosResponse> Marcas { get; set; }
            public List<StrCargaCombosResponse> SubtiposRemolques { get; set; }
        }
    }
}
