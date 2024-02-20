using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSoftErp.Interface.Request
{
    public class cfdiTrasladoCrudRequest
    {       
        public cfdiTraslado Traslado { get; set; }
        public List<cfdiTrasladoDetalle> TrasladoDetalle { get; set; }
        public ccpRequest ccp { get; set; }
        public List<mercanciasRequest> mercancias { get; set; }
        public int renglonesDetalle { get; set; }
        public int renglonesMercancias { get; set; }        
        public List<ccpUbicacionesRequest> ccpUbicaciones { get; set; }
    }

    
    public class cfdiTrasladoDetalle
    {
        public string Serie { get; set; }
        public int Folio { get; set; }
        public int Seq { get; set; }
        public int ArticulosID { get; set; }
        public decimal Cantidad { get; set; }
        public string ClaveProdServ { get; set; }
        public string BienesTransp { get; set; }
        public string Descripcion { get; set; }
        public string NumeroPedimento { get; set; }

    }
}
