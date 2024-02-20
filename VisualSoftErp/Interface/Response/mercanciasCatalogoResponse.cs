using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSoftErp.Interface.Response
{
    public class mercanciasCatalogoResponse
    {
        public string BienesTransp { get; set; }
        public string Descripcion { get; set; }
        public string ClaveUnidad { get; set; }
        public string Unidad { get; set; }
        public string MaterialPeligroso { get; set; }
        public string CveMaterialPeligroso { get; set; }
        public string Embalaje { get; set; }
        public string DescripEmbalaje { get; set; }
        public string Moneda { get; set; }
        public string FraccionArancelaria { get; set; }
        public string result { get; set; }
    }

    public class mercanciasCatalogoLlenaCajasResponse
    {
        public int MercanciasCatalogoID { get; set; }
        public string BienesTransp { get; set; }
        public string Descripcion { get; set; }
        public string ClaveUnidad { get; set; }
        public string Unidad { get; set; }
        public string MaterialPeligroso { get; set; }
        public string CveMaterialPeligroso { get; set; }
        public string Embalaje { get; set; }
        public string DescripEmbalaje { get; set; }
        public string Moneda { get; set; }
        public string FraccionArancelaria { get; set; }
    }


    public class mercanciasCatalogoGRIDResponse
    {
        public string MercanciasCatalogoID { get; set; }
        public string BienesTransp { get; set; }
        public string Descripcion { get; set; }
        public string ClaveUnidad { get; set; }
        public string Unidad { get; set; }
        public string MaterialPeligroso { get; set; }
        public string CveMaterialPeligroso { get; set; }
        public string Embalaje { get; set; }
        public string DescripEmbalaje { get; set; }
        public string Moneda { get; set; }
        public string FraccionArancelaria { get; set; }
    }
}
