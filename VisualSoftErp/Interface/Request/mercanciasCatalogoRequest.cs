﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSoftErp.Interface.Request
{
    public class mercanciasCatalogoRequest
    {
        public int MercanciaCatalogoID { get; set; }
    }

    public class mercanciasCatalogoCRUDRequest
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
}