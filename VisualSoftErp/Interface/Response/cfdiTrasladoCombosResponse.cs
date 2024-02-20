using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualSoftErp.Response;

namespace VisualSoftErp.Interface.Response
{
    public class cfdiTrasladoCombosResponse
    {
        public ListaCarrosTraslado ListadeCarros { get; set; }
        public ListaRemolquesTraslado ListadeRemolques { get; set; }
        public ListaOperadoresTraslado ListadeOperadores { get; set; }
        public ListaArticulosTraslado ListadeArticulos { get; set; }
        public ListaRemitentesYDestinatariosTraslado ListadeRyD { get; set; }


        public class ListaCarrosTraslado
        {
            public List<comboResponse> Carros { get; set; }
        }

        public class ListaRemolquesTraslado
        {
            public List<comboResponse> Remolques { get; set; }
        }

        public class ListaOperadoresTraslado
        {
            public List<comboResponse> Operadores { get; set; }
        }

        public class ListaArticulosTraslado
        {
            public List<comboResponse> Articulos { get; set; }
        }

        public class ListaRemitentesYDestinatariosTraslado
        {
            public List<comboResponse> RyD { get; set; }
        }
    }
}
