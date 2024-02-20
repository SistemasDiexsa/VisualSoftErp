using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSoftErp.Interface.Request
{
    public class mercanciasRequest
    {
        public string Serie { get; set; }

        public int Folio { get; set; }

        public int Seq { get; set; }

        public string BienesTransp { get; set; }

        public string Descripcion { get; set; }

        public decimal Cantidad { get; set; }

        public string ClaveUnidad { get; set; }

        public string Unidad { get; set; }

        public string MaterialPeligroso { get; set; }

        public string CveMaterialPeligroso { get; set; }

        public string Embalaje { get; set; }

        public string DescripEmbalaje { get; set; }

        public decimal PesoEnKg { get; set; }

        public decimal ValorMercancia { get; set; }

        public string Moneda { get; set; }

        public string FraccionArancelaria { get; set; }

        public string UUIDComercioExt { get; set; }
        public int MercanciasCatalogoID { get; set; }
    }
}
